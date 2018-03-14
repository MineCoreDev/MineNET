using System;
using System.Collections.Generic;
using MineNET.BlockEntities;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.NBT.Tags;
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

        public bool LightPopulated { get; set; }
        public bool TerrainPopulated { get; set; }

        public long LastUpdate { get; set; }
        public long InhabitedTime { get; set; }

        public byte[] Biomes { get; private set; } = ArrayUtils.CreateArray<byte>(256);
        public short[] HeightMap { get; private set; } = ArrayUtils.CreateArray<short>(256);


        SubChunk[] subChunks = ArrayUtils.CreateArray<SubChunk>(16);
        List<Entity> entities = new List<Entity>();
        List<BlockEntity> blockEntities = new List<BlockEntity>();


        public Chunk(int x, int z, SubChunk[] chunkDatas, byte[] biomes, short[] heightMap, ListTag<CompoundTag> entitiesTag, ListTag<CompoundTag> blockEntitiesTag)
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

        public void SendChunk(Player player)
        {
            FullChunkDataPacket pk = new FullChunkDataPacket();
            pk.ChunkX = this.x;
            pk.ChunkY = this.z;
            pk.Data = this.GetBytes();

            player.SendPacket(pk);
        }

        public SubChunk[] GetSubChunk()
        {
            return this.subChunks;
        }

        public Entity[] GetEntities()
        {
            return this.entities.ToArray();
        }

        public BlockEntity[] GetBlockEntities()
        {
            return this.blockEntities.ToArray();
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

                byte[] b1 = new byte[512];
                Buffer.BlockCopy(HeightMap, 0, b1, 0, 512);
                stream.WriteBytes(Biomes);
                stream.WriteByte(0);
                stream.WriteSVarInt(0);

                return stream.ToArray();
            }
        }
    }
}
