using System;
using System.Collections.Generic;
using MineNET.NBT.IO;
using MineNET.Worlds.Data;
using MineNET.Worlds.Formats.ChunkFormats;
using MineNET.Worlds.Formats.WorldDataFormats;

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

        public string LevelDataFilePath { get; }
        public Dictionary<string, RegionFile> Files = new Dictionary<string, RegionFile>();

        public RegionWorldSaveFormat(string worldName)
        {
            this.LevelDataFilePath = $"{Server.ExecutePath}\\level.dat";
        }

        public Chunk GetChunk(int chunkX, int chunkZ)
        {
            int regionX = chunkX >> 5;
            int regionZ = chunkZ >> 5;
            string key = $"{regionX}:{regionZ}";

            RegionFile file = null;
            if (!this.Files.ContainsKey(key))
            {
                file = new RegionFile(regionX, regionZ);
                this.Files.Add(key, file);

                Chunk chunk = new Chunk(chunkX, chunkZ);
                chunk.GenerationFlat();//TODO
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
                        chunk.GenerationFlat();//TODO
                        return chunk;
                    }
                    
                    return this.ChunkFormat.NBTDeserialize(NBTIO.ReadZLIBFile(data, NBT.Data.NBTEndian.BIG_ENDIAN));
                }
                else
                {
                    Chunk chunk = new Chunk(chunkX, chunkZ);
                    chunk.GenerationFlat();//TODO
                    return chunk;
                }
            }
        }

        public void SetChunk(Chunk chunk)
        {
            throw new NotImplementedException();
        }
    }
}