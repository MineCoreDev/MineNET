using System;
using System.Collections.Generic;
using System.IO;
using MineNET.Entities.Data;
using MineNET.Events.PlayerEvents;
using MineNET.Inventories;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.Packets;
using MineNET.Worlds;

namespace MineNET.Entities.Players
{
    public partial class Player
    {
        internal Dictionary<Tuple<int, int>, double> loadedChunk = new Dictionary<Tuple<int, int>, double>();

        private GameMode gameMode = GameMode.Survival;

        public Player() : base(null, null)
        {
            this.ShowNameTag = true;
            this.AlwaysShowNameTag = true;

            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_BREATHING);
            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_CAN_CLIMB);
        }

        private void LoadData()
        {
            string path = $"{Server.ExecutePath}\\players\\{this.Name}.dat";
            if (!File.Exists(path))
            {
                PlayerCreateDataEventArgs playerCreateDataEvent = new PlayerCreateDataEventArgs(this);
                PlayerEvents.OnPlayerCreateData(playerCreateDataEvent);

                this.RegisterData();
            }
            else
            {
                this.NamedTag = NBTIO.ReadGZIPFile(path, NBTEndian.BIG_ENDIAN);
            }

            this.Inventory = new PlayerInventory(this);

            this.gameMode = GameModeExtention.FromIndex(this.NamedTag.GetInt("PlayerGameMode"));
        }

        private void RegisterData()
        {
            this.NamedTag = new CompoundTag();
            this.NamedTag.PutList(new ListTag("Attributes", NBTTagType.COMPOUND));

            this.NamedTag.PutString("WorldName", "");
            this.NamedTag.PutList(new ListTag("Pos", NBTTagType.FLOAT));
            this.NamedTag.PutList(new ListTag("Rotation", NBTTagType.FLOAT));

            this.NamedTag.PutInt("PlayerGameMode", Server.ServerConfig.GameMode.GameModeToInt());
            this.NamedTag.PutInt("PlayerLevel", 0);
            this.NamedTag.PutFloat("PlayerLevelProgress", 0f);
        }

        private int FixRadius(int radius)
        {
            int maxRequest = Server.ServerConfig.MaxViewDistance;
            if (radius > maxRequest) radius = maxRequest;
            return radius;
        }

        internal override void OnUpdate(int tick)
        {
        }

        internal void SendChunk()
        {
            if (this.HasSpawned)
            {
                foreach (Chunk c in this.World.LoadChunks(this, this.RequestChunkRadius))
                {
                    c.SendChunk(this);
                }
            }
        }

        private void SendGameMode()
        {
            SetPlayerGameTypePacket pk = new SetPlayerGameTypePacket();
            pk.GameMode = this.GameMode;
            this.SendPacket(pk);

            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.BUILD_AND_MINE, !this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.WORLD_BUILDER, !this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.NO_CLIP, this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.WORLD_IMMUTABLE, this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.NO_PVP, this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.FLYING, this.IsCreative || this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.ALLOW_FLIGHT, this.IsCreative || this.IsSpectator);
            this.AdventureSettingsEntry.Update(this);
        }
    }
}
