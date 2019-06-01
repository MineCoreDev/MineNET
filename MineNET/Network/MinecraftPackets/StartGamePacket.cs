using MineNET.Data;
using MineNET.Values;
using MineNET.Worlds;
using MineNET.Worlds.Dimensions;
using MineNET.Worlds.Generators;
using MineNET.Worlds.Rule;

namespace MineNET.Network.MinecraftPackets
{
    public class StartGamePacket : MinecraftPacket
    {
        public const int GAME_PUBLISH_SETTING_NO_MULTI_PLAY = 0;
        public const int GAME_PUBLISH_SETTING_INVITE_ONLY = 1;
        public const int GAME_PUBLISH_SETTING_FRIENDS_ONLY = 2;
        public const int GAME_PUBLISH_SETTING_FRIENDS_OF_FRIENDS = 3;
        public const int GAME_PUBLISH_SETTING_PUBLIC = 4;

        public override byte PacketID { get; } = MinecraftProtocol.START_GAME_PACKET;

        public long EntityUniqueId { get; set; }
        public long EntityRuntimeId { get; set; }

        public GameMode PlayerGamemode { get; set; }
        public Vector3 PlayerPosition { get; set; }
        public Vector2 Direction { get; set; }

        public int Seed { get; set; } = -1;
        public byte Dimension { get; set; } = DimensionIDs.OverWorld;
        public int Generator { get; set; } = GeneratorIDs.Infinite;
        public GameMode WorldGamemode { get; set; } = GameMode.Survival;
        public Difficulty Difficulty { get; set; } = Difficulty.Normal;

        public int SpawnX { get; set; }
        public int SpawnY { get; set; }
        public int SpawnZ { get; set; }

        public bool HasAchievementsDisabled { get; set; } = true;
        public int DayCycleStopTime { get; set; } = 0;

        public bool EduMode { get; set; } = false;
        public bool HasEduFeaturesEnabled { get; set; } = false;

        public float RainLevel { get; set; } = 0;
        public float LightningLevel { get; set; } = 0;

        public bool HasConfirmedPlatformLockedContent { get; set; } = false;
        public bool MultiplayerGame { get; set; } = true;
        public bool BroadcastToLAN { get; set; } = true;
        public int XblBroadcastIntent { get; set; } = StartGamePacket.GAME_PUBLISH_SETTING_PUBLIC;
        public int PlatformBroadcastIntent { get; set; }  = StartGamePacket.GAME_PUBLISH_SETTING_PUBLIC;
        public bool CommandsEnabled { get; set; } = true;
        public bool IsTexturePacksRequired { get; set; } = false;

        public GameRules GameRules { get; set; }

        public bool BonusChest { get; set; } = false;
        public bool StartWithMap { get; set; } = false;

        public PlayerPermissions PermissionLevel { get; set; } = PlayerPermissions.MEMBER;
        public int ServerChunkTickRadius { get; set; } = 4;
        
        public bool HasLockedBehaviour { get; set; } = false;
        public bool HasLockedResourcePack { get; set; } = false;
        public bool IsFromLockedWorldTemplate { get; set; } = false;
        public bool IsUsingMsaGamertagsOnly { get; set; } = false;
        public bool IsFormWorldTemplate { get; set; } = false;
        public bool IsWorldTemplateOptionLocked { get; set; } = false;

        public string LevelId { get; set; } = "";
        public string WorldName { get; set; } = "";
        public string PremiumWorldTemplateId { get; set; } = "";

        public bool IsTrial { get; set; } = false;

        public long CurrentTick { get; set; } = 0;
        public int EnchantmentSeed { get; set; } = 0;

        public string MultiplayerCorrelationId { get; set; } = "";

        protected override void EncodePayload()
        {
            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteSVarInt(this.PlayerGamemode.GetIndex());
            this.WriteVector3(this.PlayerPosition);
            this.WriteVector2(this.Direction);
            this.WriteSVarInt(this.Seed);
            this.WriteSVarInt(this.Dimension);
            this.WriteSVarInt(this.Generator);
            this.WriteSVarInt(this.WorldGamemode.GetIndex());
            this.WriteSVarInt(this.Difficulty.GetIndex());
            this.WriteBlockVector3(this.SpawnX, this.SpawnY, this.SpawnZ);
            this.WriteBool(this.HasAchievementsDisabled);
            this.WriteSVarInt(this.DayCycleStopTime);
            this.WriteBool(this.EduMode);
            this.WriteBool(this.HasEduFeaturesEnabled);
            this.WriteLFloat(this.RainLevel);
            this.WriteLFloat(this.LightningLevel);
            this.WriteBool(this.HasConfirmedPlatformLockedContent);
            this.WriteBool(this.MultiplayerGame);
            this.WriteBool(this.BroadcastToLAN);
            this.WriteVarInt(this.XblBroadcastIntent);
            this.WriteVarInt(this.PlatformBroadcastIntent);
            this.WriteBool(this.CommandsEnabled);
            this.WriteBool(this.IsTexturePacksRequired);
            this.WriteGameRules(this.GameRules);
            this.WriteBool(this.BonusChest);
            this.WriteBool(this.StartWithMap);
            this.WriteSVarInt((int) this.PermissionLevel);
            this.WriteLInt((uint) this.ServerChunkTickRadius);
            this.WriteBool(this.HasLockedBehaviour);
            this.WriteBool(this.HasLockedResourcePack);
            this.WriteBool(this.IsFromLockedWorldTemplate);
            this.WriteBool(this.IsUsingMsaGamertagsOnly);
            this.WriteBool(this.IsFormWorldTemplate);
            this.WriteBool(this.IsWorldTemplateOptionLocked);
            this.WriteString(this.LevelId);
            this.WriteString(this.WorldName);
            this.WriteString(this.PremiumWorldTemplateId);
            this.WriteBool(this.IsTrial);
            this.WriteLLong((ulong) this.CurrentTick);
            this.WriteSVarInt(this.EnchantmentSeed);
            this.WriteBytes(GlobalBlockPalette.PaletteBytes);
            this.WriteString(this.MultiplayerCorrelationId);
        }

        protected override void DecodePayload()
        {

        }
    }
}
