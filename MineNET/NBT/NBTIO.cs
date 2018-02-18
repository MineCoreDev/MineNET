using System.IO;
using MineNET.NBT.Tags;

namespace MineNET.NBT
{
    public static class NBTIO
    {
        public static void WriteFile(string fileName, CompoundTag tag)
        {
            using (NBTStream stream = new NBTStream())
            {
                tag.Write(stream);
                File.WriteAllBytes(fileName, stream.ToArray());
            }
        }

        public static CompoundTag ReadFile(string fileName)
        {
            CompoundTag tag = new CompoundTag();
            byte[] bytes = File.ReadAllBytes(fileName);
            using (NBTStream stream = new NBTStream(bytes))
            {
                tag.Read(stream);
            }

            return tag;
        }
    }
}
