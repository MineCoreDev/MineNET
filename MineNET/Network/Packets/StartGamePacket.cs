using MineNET.Data;
using MineNET.Entities.Data;
using MineNET.Values;
using MineNET.Worlds.Data;

namespace MineNET.Network.Packets
{
    public class StartGamePacket : DataPacket
    {
        public const int ID = ProtocolInfo.START_GAME_PACKET;

        public override byte PacketID
        {
            get
            {
                return StartGamePacket.ID;
            }
        }

        public long EntityUniqueId { get; set; }

        public long EntityRuntimeId { get; set; }

        public GameMode PlayerGamemode { get; set; }

        public Vector3 PlayerPosition { get; set; }

        public Vector2 Direction { get; set; }

        public int Seed { get; set; } = 0;

        public byte Dimension { get; set; } = 0;

        public int Generator { get; set; } = 1;

        public int WorldGamemode { get; set; }

        public int Difficulty { get; set; }

        public int SpawnX { get; set; }

        public int SpawnY { get; set; }

        public int SpawnZ { get; set; }

        public bool HasAchievementsDisabled { get; set; } = true;

        public int DayCycleStopTime { get; set; } = 0;

        public bool EduMode { get; set; } = false;

        public float RainLevel { get; set; } = 0;

        public float LightningLevel { get; set; } = 0;

        public bool MultiplayerGame { get; set; } = true;

        public bool BroadcastToLAN { get; set; } = true;

        public bool BroadcastToXboxLive { get; set; } = true;

        public bool CommandsEnabled { get; set; } = true;

        public bool IsTexturePacksRequired { get; set; } = false;

        public GameRules GameRules { get; set; }

        public bool BonusChest { get; set; } = false;

        public bool StartWithMap { get; set; } = false;

        public bool TrustPlayers { get; set; } = false;

        public PlayerPermissions PermissionLevel { get; set; } = PlayerPermissions.MEMBER;

        public int GamePublish { get; set; } = 3;

        public int ServerChunkTickRadius { get; set; } = 4;

        public string LevelId { get; set; } = "";

        public string WorldName { get; set; } = "";

        public string PremiumWorldTemplateId { get; set; } = "";

        public bool Unknown { get; set; } = false;

        public long CurrentTick { get; set; } = 0;

        public int EnchantmentSeed { get; set; } = 0;

        public override void Encode()
        {
            base.Encode();

            //TODO: Packet
            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteSVarInt(this.PlayerGamemode.GameModeToInt());
            this.WriteVector3(this.PlayerPosition);
            this.WriteVector2(this.Direction);
            this.WriteSVarInt(this.Seed);
            this.WriteSVarInt(this.Dimension);
            this.WriteSVarInt(this.Generator);
            this.WriteSVarInt(this.WorldGamemode);
            this.WriteSVarInt(this.Difficulty);
            this.WriteBlockVector3(this.SpawnX, this.SpawnY, this.SpawnZ);
            this.WriteBool(this.HasAchievementsDisabled);
            this.WriteSVarInt(this.DayCycleStopTime);
            this.WriteBool(this.EduMode);
            this.WriteFloat(this.RainLevel);
            this.WriteFloat(this.LightningLevel);
            this.WriteBool(this.MultiplayerGame);
            this.WriteBool(this.BroadcastToLAN);
            this.WriteBool(this.BroadcastToXboxLive);
            this.WriteBool(this.CommandsEnabled);
            this.WriteBool(this.IsTexturePacksRequired);
            this.WriteGameRules(this.GameRules);
            this.WriteBool(this.BonusChest);
            this.WriteBool(this.StartWithMap);
            this.WriteBool(this.TrustPlayers);
            this.WriteSVarInt((int) this.PermissionLevel);
            this.WriteSVarInt(this.GamePublish);
            this.WriteInt(this.ServerChunkTickRadius);
            this.WriteString(this.LevelId);
            this.WriteString(this.WorldName);
            this.WriteString(this.PremiumWorldTemplateId);
            this.WriteBool(this.Unknown);
            this.WriteLong(this.CurrentTick);
            this.WriteSVarInt(this.EnchantmentSeed);
        }
    }
}