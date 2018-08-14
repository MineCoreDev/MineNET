using MineNET.NBT.IO;
using MineNET.Worlds.Formats.ChunkFormats;
using MineNET.Worlds.Formats.WorldDataFormats;
using System;
using System.Collections.Generic;
using System.IO;

namespace MineNET.Worlds.Formats.WorldSaveFormats
{
    public class RegionWorldSaveFormat : IWorldSaveFormat
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
        public Dictionary<string, RegionFile> Files = new Dictionary<string, RegionFile>();

        public RegionWorldSaveFormat(string worldName)
        {
            this.WorldName = worldName;

            string worldFolder = $"{Server.ExecutePath}\\worlds\\{worldName}";
            if (!Directory.Exists(worldFolder))
            {
                Directory.CreateDirectory(worldFolder);
            }

            string regionFolder = $"{worldFolder}\\region";
            if (!Directory.Exists(regionFolder))
            {
                Directory.CreateDirectory(regionFolder);
            }

            this.LevelDataFilePath = $"{worldFolder}\\level.dat";
        }

        public Chunk GetChunk(int chunkX, int chunkZ)
        {
            int regionX = chunkX >> 5;
            int regionZ = chunkZ >> 5;
            string key = $"{regionX}:{regionZ}";

            lock (this.Files)
            {
                RegionFile file = null;
                if (!this.Files.ContainsKey(key))
                {
                    file = new RegionFile(this.WorldName, regionX, regionZ);
                    this.Files.Add(key, file);

                    Chunk chunk = new Chunk(chunkX, chunkZ);
                    return chunk;
                }
                else
                {
                    file = this.Files[key];
                    if (file.IsFileCreated)
                    {
                        byte[] data = file.GetChunkBytes(chunkX, chunkZ);
                        if (data == null)
                        {
                            Chunk chunk = new Chunk(chunkX, chunkZ);
                            return chunk;
                        }

                        return this.ChunkFormat.NBTDeserialize(NBTIO.ReadZLIBFile(data, NBT.Data.NBTEndian.BIG_ENDIAN));
                    }
                    else
                    {
                        Chunk chunk = new Chunk(chunkX, chunkZ);
                        return chunk;
                    }
                }
            }
        }

        public void SetChunk(Chunk chunk)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {

        }
    }
}