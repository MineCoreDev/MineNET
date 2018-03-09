using System;
using MineNET.Entities;
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

        SubChunk[] subChunks = ArrayUtils.CreateArray<SubChunk>(16);

        public Chunk(int x, int z)
        {
            this.x = x;
            this.z = z;

            //TODO: remove...
            SubChunk flat = new SubChunk();
            for (int i = 0; i < 16; ++i)//X
            {
                for (int j = 0; j < 16; ++j)//Z
                {
                    for (int k = 0; k < 16; ++k)//Y
                    {
                        if (k == 0)
                        {
                            flat.SetBlock(i, k, j, 7);
                        }
                        else if (k == 1 || k == 2)
                        {
                            flat.SetBlock(i, k, j, 3);
                        }
                        else if (k == 3)
                        {
                            flat.SetBlock(i, k, j, 2);
                        }
                    }
                }
            }

            this.subChunks[0] = flat;
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
            using (BinaryStream stream = new BinaryStream())
            {
                int sendChunk = 16;

                for (int i = 15; i >= 0; i--)
                {
                    if (this.subChunks[i].IsEnpty())
                        sendChunk = i;
                    else break;
                }

                stream.WriteByte((byte) sendChunk);
                for (int i = 0; i < sendChunk; ++i)
                {
                    stream.WriteBytes(this.subChunks[i].GetBytes());
                }

                short[] b2 = new short[256];
                byte[] b1 = new byte[512];
                Buffer.BlockCopy(b2, 0, b1, 0, 512);
                stream.WriteBytes(b1);
                stream.WriteByte(0);
                stream.WriteSVarInt(0);

                return stream.ToArray();
            }
        }
    }
}
