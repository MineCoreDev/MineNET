using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class StartGamePacket : DataPacket
    {
        public const int ID = ProtocolInfo.START_GAME_PACKET;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        long entityUniqueId;
        public long EntityUniqueId
        {
            get
            {
                return this.entityUniqueId;
            }

            set
            {
                this.entityUniqueId = value;
            }
        }

        long entityRuntimeId;
        public long EntityRuntimeId
        {
            get
            {
                return this.entityRuntimeId;
            }

            set
            {
                this.entityRuntimeId = value;
            }
        }

        int playerGamemode;
        public int PlayerGamemode
        {
            get
            {
                return this.playerGamemode;
            }

            set
            {
                this.playerGamemode = value;
            }
        }

        Vector3 playerPosition;
        public Vector3 PlayerPosition
        {
            get
            {
                return this.playerPosition;
            }

            set
            {
                this.playerPosition = value;
            }
        }

        Vector2 direction;
        public Vector2 Direction
        {
            get
            {
                return this.direction;
            }

            set
            {
                this.direction = value;
            }
        }

        int seed = 0;
        public int Seed
        {
            get
            {
                return this.seed;
            }

            set
            {
                this.seed = value;
            }
        }

        byte dimension = 0;
        public byte Dimension
        {
            get
            {
                return this.dimension;
            }

            set
            {
                this.dimension = value;
            }
        }

        int generator = 1;
        public int Generator
        {
            get
            {
                return this.generator;
            }

            set
            {
                this.generator = value;
            }
        }

        int worldGamemode;
        public int WorldGamemode
        {
            get
            {
                return this.worldGamemode;
            }

            set
            {
                this.worldGamemode = value;
            }
        }

        int difficulty;
        public int Difficulty
        {
            get
            {
                return this.difficulty;
            }

            set
            {
                this.difficulty = value;
            }
        }

        int spawnX;
        public int SpawnX
        {
            get
            {
                return this.spawnX;
            }

            set
            {
                this.spawnX = value;
            }
        }

        int spawnY;
        public int SpawnY
        {
            get
            {
                return this.spawnY;
            }

            set
            {
                this.spawnY = value;
            }
        }

        int spawnZ;
        public int SpawnZ
        {
            get
            {
                return this.spawnZ;
            }

            set
            {
                this.spawnZ = value;
            }
        }

        bool hasAchievementsDisabled = true;
        public bool HasAchievementsDisabled
        {
            get
            {
                return this.hasAchievementsDisabled;
            }

            set
            {
                this.hasAchievementsDisabled = value;
            }
        }

        int dayCycleStopTime = -1;
        public int DayCycleStopTime
        {
            get
            {
                return this.dayCycleStopTime;
            }

            set
            {
                this.dayCycleStopTime = value;
            }
        }

        bool eduMode = false;
        public bool EduMode
        {
            get
            {
                return this.eduMode;
            }

            set
            {
                this.eduMode = value;
            }
        }

        float rainLevel = -1;
        public float RainLevel
        {
            get
            {
                return this.rainLevel;
            }

            set
            {
                this.rainLevel = value;
            }
        }

        float lightningLevel = -1;
        public float LightningLevel
        {
            get
            {
                return this.lightningLevel;
            }

            set
            {
                this.lightningLevel = value;
            }
        }

        bool multiplayerGame = true;
        public bool MultiplayerGame
        {
            get
            {
                return this.multiplayerGame;
            }

            set
            {
                this.multiplayerGame = value;
            }
        }

        bool broadcastToLAN = true;
        public bool BroadcastToLAN
        {
            get
            {
                return this.broadcastToLAN;
            }

            set
            {
                this.broadcastToLAN = value;
            }
        }

        bool broadcastToXboxLive = false;
        public bool BroadcastToXboxLive
        {
            get
            {
                return this.broadcastToXboxLive;
            }

            set
            {
                this.broadcastToXboxLive = value;
            }
        }

        bool commandsEnabled = true;
        public bool CommandsEnabled
        {
            get
            {
                return this.commandsEnabled;
            }

            set
            {
                this.commandsEnabled = value;
            }
        }

        bool isTexturePacksRequired = false;
        public bool IsTexturePacksRequired
        {
            get
            {
                return this.isTexturePacksRequired;
            }

            set
            {
                this.isTexturePacksRequired = value;
            }
        }

        //ruleDatas

        bool bonusChest = false;
        public bool BonusChest
        {
            get
            {
                return this.bonusChest;
            }

            set
            {
                this.bonusChest = value;
            }
        }

        bool startWithMap = false;
        public bool StartWithMap
        {
            get
            {
                return this.startWithMap;
            }

            set
            {
                this.startWithMap = value;
            }
        }

        bool trustPlayers = false;
        public bool TrustPlayers
        {
            get
            {
                return this.trustPlayers;
            }

            set
            {
                this.trustPlayers = value;
            }
        }

        int permissionLevel = 1;
        public int PermissionLevel
        {
            get
            {
                return this.permissionLevel;
            }

            set
            {
                this.permissionLevel = value;
            }
        }

        int gamePublish = 0;
        public int GamePublish
        {
            get
            {
                return this.gamePublish;
            }

            set
            {
                this.gamePublish = value;
            }
        }

        int serverChunkTickRadius = 4;
        public int ServerChunkTickRadius
        {
            get
            {
                return this.serverChunkTickRadius;
            }

            set
            {
                this.serverChunkTickRadius = value;
            }
        }

        string levelId = "";
        public string LevelId
        {
            get
            {
                return this.levelId;
            }

            set
            {
                this.levelId = value;
            }
        }

        string worldName;
        public string WorldName
        {
            get
            {
                return this.worldName;
            }

            set
            {
                this.worldName = value;
            }
        }

        string premiumWorldTemplateId = "";
        public string PremiumWorldTemplateId
        {
            get
            {
                return this.premiumWorldTemplateId;
            }

            set
            {
                this.premiumWorldTemplateId = value;
            }
        }

        bool unknown = false;
        public bool Unknown
        {
            get
            {
                return this.unknown;
            }

            set
            {
                this.unknown = value;
            }
        }

        long currentTick = 0;
        public long CurrentTick
        {
            get
            {
                return this.currentTick;
            }

            set
            {
                this.currentTick = value;
            }
        }

        int enchantmentSeed = 0;
        public int EnchantmentSeed
        {
            get
            {
                return this.enchantmentSeed;
            }

            set
            {
                this.enchantmentSeed = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityUniqueId(this.entityUniqueId);
            this.WriteEntityRuntimeId(this.entityRuntimeId);
            this.WriteVarInt(this.playerGamemode);
            this.WriteVector3(this.playerPosition);
            this.WriteVector2(this.direction);
            this.WriteVarInt(this.seed);
            this.WriteVarInt(this.dimension);
            this.WriteVarInt(this.generator);
            this.WriteVarInt(this.worldGamemode);
            this.WriteVarInt(this.difficulty);
            this.WriteBlockPosition(this.spawnX, this.spawnY, this.spawnZ);
            this.WriteBool(this.hasAchievementsDisabled);
            this.WriteVarInt(this.dayCycleStopTime);
            this.WriteBool(this.eduMode);
            this.WriteFloat(this.rainLevel);
            this.WriteFloat(this.lightningLevel);
            this.WriteBool(this.multiplayerGame);
            this.WriteBool(this.broadcastToLAN);
            this.WriteBool(this.broadcastToXboxLive);
            this.WriteBool(this.commandsEnabled);
            this.WriteBool(this.isTexturePacksRequired);
            this.WriteUVarInt(0);//GameruleCount
            //GameRule
            this.WriteBool(this.bonusChest);
            this.WriteBool(this.startWithMap);
            this.WriteBool(this.trustPlayers);
            this.WriteVarInt(this.permissionLevel);
            this.WriteVarInt(this.gamePublish);
            this.WriteLInt((uint)this.serverChunkTickRadius);
            this.WriteString(this.levelId);
            this.WriteString(this.worldName);
            this.WriteString(this.premiumWorldTemplateId);
            this.WriteBool(this.unknown);
            this.WriteLLong((ulong)this.currentTick);
            this.WriteVarInt(this.enchantmentSeed);
        }
    }
}