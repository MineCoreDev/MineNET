using MineNET.BlockEntities;
using MineNET.Entities;
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
            ListTag<CompoundTag> section = level.GetList<CompoundTag>("Sections");
            for (int i = 0; i < section.Count; ++i)
            {
                SubChunk subChunk = new SubChunk();
                byte y = section[i].GetByte("Y");
                subChunk.BlockData = section[i].GetByteArray("Blocks");
                subChunk.MetaDatas = new NibbleArray(section[i].GetByteArray("Data"));
                subChunk.SkyLights = new NibbleArray(section[i].GetByteArray("SkyLight"));
                subChunk.BlockLigths = new NibbleArray(section[i].GetByteArray("BlockLight"));
                subChunks[y] = subChunk;
            }

            byte[] biomes = level.GetByteArray("Biomes");
            short[] cast = new short[256];
            int[] heightMap = level.GetIntArray("HeightMap");
            heightMap.CopyTo(cast, 0);

            Chunk chunk = new Chunk(x, z, subChunks, biomes, cast, level.GetList<CompoundTag>("Entities"), level.GetList<CompoundTag>("TileEntities"));
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

            ListTag<CompoundTag> sections = new ListTag<CompoundTag>("Sections");
            SubChunk[] subChunks = chunk.GetSubChunk();
            for (int i = 0; i < subChunks.Length; ++i)
            {
                if (subChunks[i].IsEnpty())
                {
                    continue;
                }
                CompoundTag data = new CompoundTag();
                data.PutByte("Y", (byte) i);
                data.PutByteArray("Blocks", subChunks[i].BlockData);
                data.PutByteArray("Data", subChunks[i].MetaDatas.ArrayData);
                data.PutByteArray("SkyLight", subChunks[i].SkyLights.ArrayData);
                data.PutByteArray("BlockLight", subChunks[i].BlockLigths.ArrayData);
                sections.Add(data);
            }
            tag.PutList(sections);

            ListTag<CompoundTag> entitiesTag = new ListTag<CompoundTag>("Entities");
            Entity[] entities = chunk.GetEntities();
            for (int i = 0; i < entities.Length; ++i)
            {
                entities[i].SaveNBT();
                entitiesTag.Add(entities[i].namedTag);
            }

            ListTag<CompoundTag> blockEntitiesTag = new ListTag<CompoundTag>("TileEntities");
            BlockEntity[] blockEntities = chunk.GetBlockEntities();
            for (int i = 0; i < blockEntities.Length; ++i)
            {
                blockEntities[i].SaveNBT();
                blockEntitiesTag.Add(blockEntities[i].namedTag);
            }

            CompoundTag outTag = new CompoundTag("");
            outTag.PutCompound(tag.Name, tag);

            return outTag;
        }
    }
}
