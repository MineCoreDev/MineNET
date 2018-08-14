using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Worlds.Formats.ChunkFormats;
using MineNET.Worlds.Formats.WorldDataFormats;
using System.IO;

namespace MineNET.Worlds.Formats.WorldSaveFormats
{
    public class MineNETWorldSaveFormat : IWorldSaveFormat
    {
        public IChunkFormat ChunkFormat
        {
            get
            {
                return new McaChunkFormat();
            }
        }

        public IWorldDataFormat WorldData
        {
            get
            {
                return new LevelDBFormat();
            }
        }

        public string WorldName { get; }
        public string LevelDataFilePath { get; }
        public CompoundTag Data { get; }

        public MineNETWorldSaveFormat(string worldName)
        {
            string worldFolder = $"{Server.ExecutePath}\\worlds\\{worldName}";
            string dataPath = $"{worldFolder}\\{worldName}.world";
            if (!Directory.Exists(worldFolder))
            {
                Directory.CreateDirectory(worldFolder);
                NBTIO.WriteRawFile(dataPath, new CompoundTag(), NBTEndian.BIG_ENDIAN);
            }
            this.Data = NBTIO.ReadRawFile(dataPath, NBTEndian.BIG_ENDIAN);
            this.WorldName = worldName;
            this.LevelDataFilePath = $"{worldFolder}\\level.dat";
        }

        public Chunk GetChunk(int chunkX, int chunkZ)
        {
            string key = chunkX + ":" + chunkZ;
            if (this.Data.Exist(key))
            {
                return this.ChunkFormat.NBTDeserialize((NBTIO.ReadTag(this.Data.GetByteArray(key))));
            }
            else
            {
                return new Chunk(chunkX, chunkZ);
            }
        }

        public void SetChunk(Chunk chunk)
        {
            string key = chunk.X + ":" + chunk.Z;
            this.Data.PutByteArray(key, NBTIO.WriteTag(this.ChunkFormat.NBTSerialize(chunk)));
        }

        public void Save()
        {
            string worldFolder = $"{Server.ExecutePath}\\worlds\\{this.WorldName}";
            string dataPath = $"{worldFolder}\\{this.WorldName}.world";
            NBTIO.WriteRawFile(dataPath, this.Data, NBTEndian.BIG_ENDIAN);
        }
    }
}
