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

        float x;
        public float X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = value;
            }
        }

        float y;
        public float Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = value;
            }
        }

        float z;
        public float Z
        {
            get
            {
                return this.z;
            }

            set
            {
                this.z = value;
            }
        }

        float yaw;
        public float Yaw
        {
            get
            {
                return this.yaw;
            }

            set
            {
                this.yaw = value;
            }
        }

        float pitch;
        public float Pitch
        {
            get
            {
                return this.pitch;
            }

            set
            {
                this.pitch = value;
            }
        }

        int seed;
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

        byte dimension;
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

        int generator;
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

        bool hasAchievementsDisabled;
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

        int dayCycleStopTime;
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

        bool eduMode;
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

        float rainLevel;
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

        float lightningLevel;
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

        bool multiplayerGame;
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

        bool broadcastToLAN;
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

        bool broadcastToXboxLive;
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

        bool commandsEnabled;
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

        bool isTexturePacksRequired;
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

        bool bonusChest;
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

        bool trustPlayers;
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

        int permissionLevel;
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

        int gamePublish;
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

        int serverChunkTickRadius;
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

        string levelId;
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

        string premiumWorldTemplateId;
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

        bool unknown;
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

        long currentTick;
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

        int enchantmentSeed;
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


        }
    }
}