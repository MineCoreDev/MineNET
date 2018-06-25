﻿using MineNET.Blocks;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;
using MineNET.Utils;
using MineNET.Values;
using System;
using System.Collections.Generic;

namespace MineNET.Worlds
{
    public class Chunk
    {
        public int X { get; }
        public int Z { get; }

        public Vector2 Vector2
        {
            get
            {
                return new Vector2(this.X, this.Z);
            }
        }

        //public World World { get; private set; }

        public bool LightPopulated { get; set; }
        public bool TerrainPopulated { get; set; }

        public long LastUpdate { get; set; }
        public long InhabitedTime { get; set; }

        public byte[] Biomes { get; private set; } = ArrayUtils.CreateArray<byte>(256);
        public short[] HeightMap { get; private set; } = ArrayUtils.CreateArray<short>(256);

        public SubChunk[] SubChunks { get; set; } = ArrayUtils.CreateArray<SubChunk>(16);
        List<Entity> entities = new List<Entity>();
        ListTag entitiesTag = new ListTag(NBTTagType.COMPOUND);
        //List<BlockEntity> blockEntities = new List<BlockEntity>();
        //ListTag blockEntitiesTag = new ListTag(NBTTagType.COMPOUND);

        public Chunk(int x, int z, SubChunk[] chunkDatas = null, byte[] biomes = null, short[] heightMap = null, ListTag entitiesTag = null, ListTag blockEntitiesTag = null)
        {
            this.X = x;
            this.Z = z;

            if (biomes != null)
            {
                this.Biomes = biomes;
            }

            if (heightMap != null)
            {
                this.HeightMap = heightMap;
            }

            if (chunkDatas != null)
            {
                this.SubChunks = chunkDatas;
            }

            this.entitiesTag = entitiesTag;
            //this.blockEntitiesTag = blockEntitiesTag;
        }

        public void SendChunk(Player player)
        {
            FullChunkDataPacket pk = new FullChunkDataPacket();
            pk.ChunkX = this.X;
            pk.ChunkY = this.Z;
            pk.Data = this.GetBytes();

            player.SendPacket(pk);
        }

        public byte GetBlock(int bx, int by, int bz)
        {
            SubChunk chunk = this.SubChunks[by >> 4];
            return chunk.GetBlock(bx, by - 16 * (by >> 4), bz);
        }

        public void SetBlock(int bx, int by, int bz, byte bid)
        {
            SubChunk chunk = this.SubChunks[by >> 4];
            chunk.SetBlock(bx, by - 16 * (by >> 4), bz, bid);
        }

        public void SetHeight(int bx, int bz, short h)
        {
            this.HeightMap[((bz << 4) + (bx))] = h;
        }

        public byte GetHeight(int bx, int bz)
        {
            return (byte) this.HeightMap[((bz << 4) + (bx))];
        }

        public void SetBiome(int bx, int bz, byte biome)
        {
            this.Biomes[(bz << 4) + (bx)] = biome;
        }

        public byte GetBiome(int bx, int bz)
        {
            return this.Biomes[(bz << 4) + (bx)];
        }

        /* public byte GetBlocklight(int bx, int by, int bz)
         {
             SubChunk chunk = this.subChunks[by >> 4];
             return chunk.GetBlocklight(bx, by - 16 * (by >> 4), bz);
         }

         public void SetBlocklight(int bx, int by, int bz, byte data)
         {
             SubChunk chunk = subChunks[by >> 4];
             chunk.SetBlocklight(bx, by - 16 * (by >> 4), bz, data);
         }*/

        public byte GetMetadata(int bx, int by, int bz)
        {
            SubChunk chunk = this.SubChunks[by >> 4];
            return chunk.GetMetaData(bx, by - 16 * (by >> 4), bz);
        }

        public void SetMetadata(int bx, int by, int bz, byte data)
        {
            SubChunk chunk = this.SubChunks[by >> 4];
            chunk.SetMetaData(bx, by - 16 * (by >> 4), bz, data);
        }

        public Entity[] Entities
        {
            get
            {
                return this.entities.ToArray();
            }
        }

        /*public BlockEntity[] BlockEntities
        {
            get
            {
                return this.blockEntities.ToArray();
            }
        }*/

        public int GetBlockHighest(Vector2 pos)
        {
            for (int i = 255; i > 0; --i)
            {
                int id = this.GetBlock((int) pos.X, i, (int) pos.Y);
                int meta = this.GetMetadata((int) pos.X, i, (int) pos.Y);
                if (Block.Get(id).IsSolid)
                {
                    return i;
                }
            }

            return 0;
        }

        public byte[] GetBytes()
        {
            using (BinaryStream stream = new BinaryStream())
            {
                int sendChunk = 16;

                for (int i = 15; i >= 0; i--)
                {
                    if (this.SubChunks[i].IsEnpty)
                        sendChunk = i;
                    else break;
                }

                stream.WriteByte((byte) sendChunk);
                for (int i = 0; i < sendChunk; ++i)
                {
                    stream.WriteBytes(this.SubChunks[i].GetBytes());
                }

                byte[] b1 = new byte[512];
                Buffer.BlockCopy(this.HeightMap, 0, b1, 0, 512);
                stream.WriteBytes(b1);
                stream.WriteBytes(this.Biomes);
                stream.WriteByte(0);
                stream.WriteSVarInt(0);

                return stream.ToArray();
            }
        }
    }
}
