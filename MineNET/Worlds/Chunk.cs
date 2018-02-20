using System;
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
                    Binary.WriteBytes(nbt, ArrayUtil.CreateArray<byte>(4096, 1));
                    Binary.WriteBytes(nbt, ArrayUtil.CreateNibbleArray(4096).ArrayData);
                }

                short[] b2 = new short[256];
                byte[] b1 = new byte[512];
                Buffer.BlockCopy(b2, 0, b1, 0, 512);
                Binary.WriteBytes(nbt, b1);
                Binary.WriteBytes(nbt, ArrayUtil.CreateArray<byte>(256, 1));
                nbt.WriteByte(0);
                VarInt.WriteSInt32(nbt, 0);

                return nbt.ToArray();
            }
        }
    }
}
