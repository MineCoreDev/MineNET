using MineNET.BlockEntities;
using MineNET.Entities;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;
using MineNET.Utils;

namespace MineNET.Worlds.Formats.ChunkFormats
{
    public class McaChunkFormat : IChunkFormat
    {
        public Chunk NBTDeserialize(CompoundTag tag)
        {
            CompoundTag level = (CompoundTag) tag["Level"];
            int x = level.GetInt("xPos");
            int z = level.GetInt("zPos");

            SubChunk[] subChunks = ArrayUtils.CreateArray<SubChunk>(16);
            ListTag sections = level.GetList("Sections");
            for (int i = 0; i < sections.Count; ++i)
            {
                CompoundTag section = ((CompoundTag) sections[i]);
                SubChunk subChunk = new SubChunk();
                byte y = section.GetByte("Y");
                subChunk.BlockDatas = section.GetIntArray("Blocks");
                subChunk.MetaDatas = new NibbleArray(section.GetByteArray("Data"));
                subChunk.SkyLights = new NibbleArray(section.GetByteArray("SkyLight"));
                subChunk.BlockLigths = new NibbleArray(section.GetByteArray("BlockLight"));
                subChunks[y] = subChunk;
            }

            byte[] biomes = level.GetByteArray("Biomes");
            short[] cast = new short[256];
            int[] heightMap = level.GetIntArray("HeightMap");
            for (int i = 0; i < 256; ++i)
            {
                cast[i] = (short) heightMap[i];
            }

            Chunk chunk = new Chunk(x, z, subChunks, biomes, cast, level.GetList("Entities"), level.GetList("TileEntities"));
            chunk.LastUpdate = level.GetLong("LastUpdate");
            chunk.InhabitedTime = level.GetLong("InhabitedTime");
            chunk.LightPopulated = level.GetByte("LightPopulated") == 1;
            chunk.TerrainPopulated = level.GetByte("TerrainPopulated") == 1;

            return chunk;
        }

        public CompoundTag NBTSerialize(Chunk chunk)
        {
            CompoundTag tag = new CompoundTag("Level");
            tag.PutInt("xPos", chunk.X);//Chunk X
            tag.PutInt("zPos", chunk.Z);//Chunk Z

            tag.PutLong("LastUpdate", chunk.LastUpdate);//Last Save Tick

            tag.PutByte("LightPopulated", chunk.LightPopulated ? (byte) 1 : (byte) 0);//
            tag.PutByte("TerrainPopulated", chunk.TerrainPopulated ? (byte) 1 : (byte) 0);//

            tag.PutByte("V", 1);//Version

            tag.PutLong("InhabitedTime", chunk.InhabitedTime);

            tag.PutByteArray("Biomes", chunk.Biomes);

            int[] cast = new int[256];
            chunk.HeightMap.CopyTo(cast, 0);
            tag.PutIntArray("HeightMap", cast);

            ListTag sections = new ListTag("Sections", NBTTagType.COMPOUND);
            SubChunk[] subChunks = chunk.SubChunks;
            for (int i = 0; i < subChunks.Length; ++i)
            {
                if (subChunks[i].IsEnpty)
                {
                    continue;
                }
                CompoundTag data = new CompoundTag();
                data.PutByte("Y", (byte) i);
                data.PutIntArray("Blocks", subChunks[i].BlockDatas);
                data.PutByteArray("Data", subChunks[i].MetaDatas.ArrayData);
                data.PutByteArray("SkyLight", subChunks[i].SkyLights.ArrayData);
                data.PutByteArray("BlockLight", subChunks[i].BlockLigths.ArrayData);
                sections.Add(data);
            }
            tag.PutList(sections);

            ListTag entitiesTag = new ListTag("Entities", NBTTagType.COMPOUND);
            Entity[] entities = chunk.GetEntities();
            for (int i = 0; i < entities.Length; ++i)
            {
                entities[i].SaveNBT();
                entitiesTag.Add(entities[i].NamedTag);
            }
            tag.PutList(entitiesTag);

            ListTag blockEntitiesTag = new ListTag("TileEntities", NBTTagType.COMPOUND);
            BlockEntity[] blockEntities = chunk.GetBlockEntities();
            for (int i = 0; i < blockEntities.Length; ++i)
            {
                blockEntities[i].SaveNBT();
                blockEntitiesTag.Add(blockEntities[i].NamedTag);
            }
            tag.PutList(blockEntitiesTag);

            CompoundTag outTag = new CompoundTag("");
            outTag.PutCompound(tag.Name, tag);

            return outTag;
        }
    }
}
