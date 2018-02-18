using MineNET.NBT.Tags;

namespace MineNET.NBT
{
    public static class NBTIO
    {
        public static void CreateFile(string fileName, CompoundTag tag)
        {
            using (NBTStream stream = new NBTStream())
            {
                tag.WriteTag(stream);

            }
        }
    }
}
