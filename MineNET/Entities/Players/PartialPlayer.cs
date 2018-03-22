using System;
using System.Collections.Generic;
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
using MineNET.Worlds;

namespace MineNET.Entities.Players
{
    public partial class Player
    {
        internal Dictionary<Tuple<int, int>, double> loadedChunk = new Dictionary<Tuple<int, int>, double>();

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
                this.RegisterData();
                NBTIO.WriteGZIPFile(path, this.namedTag, NBTEndian.BIG_ENDIAN);
            }
            else
            {
                this.namedTag = NBTIO.ReadGZIPFile(path, NBTEndian.BIG_ENDIAN);
            }

            this.Inventory = new PlayerInventory(this);

            this.gameMode = GameModeExtention.FromIndex(this.namedTag.GetInt("PlayerGameMode"));
        }

        private void RegisterData()
        {
            CompoundTag item = NBTIO.WriteItem(Item.Get(0));
            this.namedTag = new CompoundTag();
            this.namedTag.PutList(new ListTag("Attributes", NBTTagType.COMPOUND));

            this.namedTag.PutList(new ListTag("Pos", NBTTagType.FLOAT));
            this.namedTag.PutList(new ListTag("Rotation", NBTTagType.FLOAT));

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

        internal override void OnUpdate(int tick)
        {
            if (this.HasSpawned)
            {
                if (tick % 10 == 0)
                {
                    Task.Run(() =>
                    {
                        foreach (Chunk chunk in this.World.LoadChunks(this, this.RequestChunkRadius))
                        {
                            chunk.SendChunk(this);
                        }
                    });
                }
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
