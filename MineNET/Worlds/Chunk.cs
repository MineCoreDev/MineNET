using System;
using System.Collections.Generic;
using System.Linq;
using MineNET.BlockEntities;
using MineNET.Blocks;
using MineNET.Entities;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;
using MineNET.Utils;
using MineNET.Values;

namespace MineNET.Worlds
{
    public class Chunk
    {
        public int X { get; }
        public int Z { get; }

        public Vector2 Vector2
        {
            get { return new Vector2(this.X, this.Z); }
        }

        public World World { get; private set; }

        public bool LightPopulated { get; set; }
        public bool TerrainPopulated { get; set; }

        public long LastUpdate { get; set; }
        public long InhabitedTime { get; set; }

        public byte[] Biomes { get; private set; } = ArrayUtils.CreateArray<byte>(256);
        public short[] HeightMap { get; private set; } = ArrayUtils.CreateArray<short>(256);

        public SubChunk[] SubChunks { get; set; } = ArrayUtils.CreateArray<SubChunk>(16);
        private List<Entity> Entities { get; } = new List<Entity>();

        private Dictionary<BlockCoordinate3D, BlockEntity> BlockEntities { get; } =
            new Dictionary<BlockCoordinate3D, BlockEntity>();

        public Chunk(World world, int x, int z, SubChunk[] chunkDatas = null, byte[] biomes = null,
            short[] heightMap = null,
            ListTag entitiesTag = null, ListTag blockEntitiesTag = null)
        {
            this.World = world;
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

            for (int i = 0; i < entitiesTag?.Count; ++i)
            {
                CompoundTag entity = (CompoundTag) entitiesTag[i];
                this.AddEntity(Entity.CreateEntity(entity.GetString("id"), this, entity));
            }

            for (int i = 0; i < blockEntitiesTag?.Count; ++i)
            {
                CompoundTag blockEntity = (CompoundTag) blockEntitiesTag[i];
                this.AddBlockEntity(BlockEntity.CreateBlockEntity(blockEntity.GetString("id").ToLower(), this,
                    blockEntity));
            }
        }

        public FullChunkDataPacket ChunkData()
        {
            FullChunkDataPacket pk = new FullChunkDataPacket();
            pk.ChunkX = this.X;
            pk.ChunkY = this.Z;
            pk.Data = this.GetBytes();

            return pk;
        }

        public int GetBlock(int bx, int by, int bz)
        {
            SubChunk chunk = this.SubChunks[by >> 4];
            return chunk.GetBlock(bx, by - 16 * (by >> 4), bz);
        }

        public void SetBlock(int bx, int by, int bz, int bid)
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

        /*public byte GetBlocklight(int bx, int by, int bz)
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

        public void AddEntity(Entity entity)
        {
            this.Entities.Add(entity);
        }

        public Entity[] GetEntities()
        {
            return this.Entities.ToArray();
        }

        public void AddBlockEntity(BlockEntity blockEntity)
        {
            if (this.BlockEntities.ContainsKey(blockEntity.ToBlockCoordinate3D()))
            {
                return;
            }

            this.BlockEntities.Add(blockEntity.ToBlockCoordinate3D(), blockEntity);
        }

        public void RemoveBlockEntity(BlockEntity blockEntity)
        {
            if (this.BlockEntities.ContainsKey(blockEntity.ToBlockCoordinate3D()))
            {
                this.BlockEntities.Remove(blockEntity.ToBlockCoordinate3D());
            }
        }

        public BlockEntity[] GetBlockEntities()
        {
            return this.BlockEntities.Values.ToArray();
        }

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
                stream.WriteSVarInt(0); //TODO Extra Data

                foreach (KeyValuePair<BlockCoordinate3D, BlockEntity> blockEntity in this.BlockEntities)
                {
                    BlockEntitySpawnable s = blockEntity.Value as BlockEntitySpawnable;
                    stream.WriteBytes(NBTIO.WriteTag(s?.SpawnCompound(), NBTEndian.LITTLE_ENDIAN, true));
                }

                return stream.ToArray();
            }
        }
    }
}