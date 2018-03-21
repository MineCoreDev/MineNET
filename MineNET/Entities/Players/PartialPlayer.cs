using System.IO;
using System.Threading.Tasks;
using MineNET.Entities.Data;
using MineNET.Inventories;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.Packets;
using MineNET.Network.Packets.Data;
using MineNET.Utils;
using MineNET.Worlds;

namespace MineNET.Entities.Players
{
    public partial class Player
    {
        public Player()
        {
            this.ShowNameTag = true;
            this.AlwaysShowNameTag = true;

            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_CAN_CLIMB);
        }

        private void LoadData()
        {
            string path = $"{Server.ExecutePath}\\players\\{this.Name}.dat";
            if (!File.Exists(path))
            {
                NBTIO.WriteGZIPFile(path, new CompoundTag(), NBTEndian.BIG_ENDIAN);
                this.RegisterData();
            }
            else
            {
                this.namedTag = NBTIO.ReadGZIPFile(path, NBTEndian.BIG_ENDIAN);
            }

            this.Inventory = new PlayerInventory(this);

            this.gameMode = GameModeExtention.FromIndex(this.namedTag.GetInt("PlayerGameMode"));
            Logger.Info($"{this.GameMode}");
        }

        private void RegisterData()
        {
            CompoundTag item = NBTIO.WriteItem(Item.Get(0));
            this.namedTag = new CompoundTag();
            this.namedTag.PutList(new ListTag<CompoundTag>("Attributes"));

            this.namedTag.PutList(new ListTag<FloatTag>("Pos"));
            this.namedTag.PutList(new ListTag<FloatTag>("Rotation"));

            this.namedTag.PutInt("PlayerGameMode", Server.ServerConfig.GameMode.GameModeToInt());
            this.namedTag.PutInt("PlayerLevel", 0);
            this.namedTag.PutFloat("PlayerLevelProgress", 0f);
        }

        private int FixRadius(int radius)
        {
            int maxRequest = Server.ServerConfig.MaxViewDistance;
            if (radius > maxRequest) radius = maxRequest;
            return radius;
        }

        internal override void OnUpdate()
        {
            if (this.HasSpawned)
            {
                Task.Run(() =>
                {
                    foreach (Chunk chunk in this.World.LoadChunks(this.GetChunkVector(), this.RequestChunkRadius))
                    {
                        chunk.SendChunk(this);
                    }
                });
            }
        }

        private void SendGameMode()
        {
            SetPlayerGameTypePacket pk = new SetPlayerGameTypePacket();
            pk.GameMode = this.GameMode;
            this.SendPacket(pk);

            AdventureSettingsEntry entry = Server.Instance.GetAdventureSettingsEntry(this);
            entry.SetFlag(AdventureSettingsPacket.BUILD_AND_MINE, !this.IsSpectator());
            entry.SetFlag(AdventureSettingsPacket.WORLD_BUILDER, !this.IsSpectator());
            entry.SetFlag(AdventureSettingsPacket.NO_CLIP, this.IsSpectator());
            entry.SetFlag(AdventureSettingsPacket.WORLD_IMMUTABLE, this.IsSpectator());
            entry.SetFlag(AdventureSettingsPacket.NO_PVP, this.IsSpectator());
            entry.SetFlag(AdventureSettingsPacket.FLYING, this.IsCreative() || this.IsSpectator());
            entry.SetFlag(AdventureSettingsPacket.ALLOW_FLIGHT, this.IsCreative() || this.IsSpectator());
            Server.Instance.UpdateAdventureSettings(entry);
        }
    }
}
