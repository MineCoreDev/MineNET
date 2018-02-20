using MineNET.Entities;
using MineNET.NBT;
using MineNET.Network.Packets;
using MineNET.Utils;

namespace MineNET.Worlds
{
    public class Chunk
    {
        int x;
        public int X
        {
            get
            {
                return this.x;
            }
        }

        int y;
        public int Y
        {
            get
            {
                return this.y;
            }
        }

        public Chunk(int x, int z)
        {
            this.x = x;
            this.y = z;
        }

        public void TestChunkSend(Player player)
        {
            FullChunkDataPacket pk = new FullChunkDataPacket();
            pk.ChunkX = this.x;
            pk.ChunkY = this.y;
            pk.Data = this.GetBytes();

            player.SendPacket(pk);
        }

        public byte[] GetBytes()
        {
            using (NBTStream nbt = new NBTStream())
            {
                nbt.WriteByte(16);
                for (int i = 0; i < 16; ++i)
                {
                    nbt.WriteByte(0);
                    Binary.WriteBytes(nbt, CreateArray<byte>(0, 16 * 16 * 16));
                    Binary.WriteBytes(nbt, CreateArray<byte>(0, 16 * 16 * 16));
                }

                Binary.WriteBytes(nbt, CreateArray<byte>(0, 512));
                Binary.WriteBytes(nbt, CreateArray<byte>(1, 256));
                nbt.WriteByte(0);
                VarInt.WriteSInt32(nbt, 0);

                return nbt.ToArray();
            }
        }

        public T[] CreateArray<T>(T defaultValue, int length)
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = defaultValue;
            }

            return array;
        }
    }
}
