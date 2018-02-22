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

        int z;
        public int Z
        {
            get
            {
                return this.z;
            }
        }

        SubChunk[] subChunks = ArrayUtil.CreateArray<SubChunk>(16);

        public Chunk(int x, int z)
        {
            this.x = x;
            this.z = z;

            subChunks = ArrayUtil.CreateArray(16, new SubChunk());
        }

        public void TestChunkSend(Player player)
        {
            FullChunkDataPacket pk = new FullChunkDataPacket();
            pk.ChunkX = this.x;
            pk.ChunkY = this.z;
            pk.Data = this.GetBytes();

            player.SendPacket(pk);
        }

        public byte[] GetBytes()
        {
            using (NBTStream nbt = new NBTStream())
            {
                byte dataChunk = 16;

                for (byte i = 15; i >= 0; --i)
                {
                    if (subChunks[i].IsEnpty())
                        dataChunk = i;
                    else break;
                }

                nbt.WriteByte(dataChunk);
                for (int i = 0; i < dataChunk; ++i)
                {
                    Binary.WriteBytes(nbt, subChunks[i].GetBytes());
                }

                short[] b2 = new short[256];
                byte[] b1 = new byte[512];
                Buffer.BlockCopy(b2, 0, b1, 0, 512);
                Binary.WriteBytes(nbt, b1);
                nbt.WriteByte(0);
                VarInt.WriteSInt32(nbt, 0);

                return nbt.ToArray();
            }
        }
    }
}
