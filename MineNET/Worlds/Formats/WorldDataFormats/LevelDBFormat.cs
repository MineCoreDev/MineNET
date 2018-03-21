using MineNET.NBT.IO;
using MineNET.NBT.Tags;

namespace MineNET.Worlds.Formats.WorldDataFormats
{
    public class LevelDBFormat : IWorldDataFormat
    {
        public void Create(World world)
        {
            CompoundTag tag = new CompoundTag();

            NBTIO.WriteGZIPFile($"{Server.ExecutePath}\\worlds\\{world.Name}\\level.dat", tag);
        }

        public void Load(World world)
        {
            CompoundTag tag = NBTIO.ReadGZIPFile($"{Server.ExecutePath}\\worlds\\{world.Name}\\level.dat");
        }

        public void Save(World world)
        {
            CompoundTag tag = new CompoundTag();

            NBTIO.WriteGZIPFile($"{Server.ExecutePath}\\worlds\\{world.Name}\\level.dat", tag);
        }
    }
}
