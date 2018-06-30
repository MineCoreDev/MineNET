using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;
using MineNET.Worlds.Generators;

namespace MineNET.Worlds.Formats.WorldDataFormats
{
    public class LevelDBFormat : IWorldDataFormat
    {
        public void Create(World world)
        {
            CompoundTag tag = new CompoundTag("");
            tag.PutCompound("Data", this.CreateData(world));

            NBTIO.WriteGZIPFile($"{Server.ExecutePath}\\worlds\\{world.Name}\\level.dat", tag);
        }

        public void Load(World world)
        {
            CompoundTag tag = NBTIO.ReadGZIPFile($"{Server.ExecutePath}\\worlds\\{world.Name}\\level.dat");

            CompoundTag data = tag.GetCompound("Data");
        }

        public void Save(World world)
        {
            /*CompoundTag tag = new CompoundTag();

            NBTIO.WriteGZIPFile($"{Server.ExecutePath}\\worlds\\{world.Name}\\level.dat", tag);*/
        }

        public CompoundTag CreateData(World world)
        {
            CompoundTag customBossEvents = new CompoundTag("CustomBossEvents");
            CompoundTag dimensionData = new CompoundTag("DimensionData");
            CompoundTag gameRules = new CompoundTag("GameRules");
            CompoundTag version = new CompoundTag("Version");
            version.PutInt("Id", -1);
            version.PutString("Name", MinecraftProtocol.ClientVersion);
            version.PutByte("Snapshot", 0);

            CompoundTag data = new CompoundTag("Data");
            data.PutCompound("CustomBossEvents", customBossEvents);
            data.PutCompound("DimensionData", dimensionData);

            data.PutInt("version", 19133);

            data.PutByte("initialized", 0);

            data.PutString("LevelName", world.Name);
            data.PutString("generatorName", GeneratorIDs.GetString(world.GeneratorType));
            data.PutInt("generatorVersion", 0);
            data.PutString("generatorOptions", "{}");

            data.PutLong("RandomSeed", world.Seed);

            data.PutByte("MapFeatures", 0);

            data.PutLong("LastPlayed", world.LastPlayed);
            data.PutLong("SizeOnDisk", 0);

            data.PutByte("allowCommands", 1);

            data.PutByte("hardcore", 0);

            data.PutInt("GameType", world.Gamemode.GetIndex());

            data.PutByte("Difficulty", (byte) world.Difficulty);
            data.PutByte("DifficultyLocked", 0);

            data.PutLong("Time", 0);
            data.PutLong("DayTime", 0);

            data.PutInt("SpawnX", (int) world.SpawnX);
            data.PutInt("SpawnY", (int) world.SpawnY);
            data.PutInt("SpawnZ", (int) world.SpawnZ);

            data.PutDouble("BorderCenterX", 0d);
            data.PutDouble("BorderCenterZ", 0d);

            data.PutDouble("BorderSize", 60000000);

            data.PutDouble("BorderSafeZone", 5);
            data.PutDouble("BorderWarningBlocks", 5);
            data.PutDouble("BorderWarningTime", 15);
            data.PutDouble("BorderSizeLerpTarget", 60000000);
            data.PutDouble("BorderSizeLerpTime", 0);
            data.PutDouble("BorderDamagePerBlock", 0.2d);

            data.PutByte("raining", 0);
            data.PutInt("rainTime", 0);
            data.PutByte("thundering", 0);
            data.PutInt("thunderTime", 0);
            data.PutInt("clearWeatherTime", 0);

            data.PutCompound("GameRules", gameRules);

            data.PutCompound("Version", version);

            return data;
        }
    }
}
