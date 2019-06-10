using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MineNET.IO;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Worlds.Formats.ChunkFormats;
using MineNET.Worlds.Formats.WorldDataFormats;

namespace MineNET.Worlds.Formats.WorldSaveFormats
{
    public class AnvilWorldSaveFormat : IWorldSaveFormat
    {
        public IChunkFormat ChunkFormat { get; private set; }
        public IWorldDataFormat WorldData => new LevelDBFormat();
        public World World { get; private set; }

        public string WorldName { get; }
        public string WorldPath { get; }
        public string RegionPath { get; }
        public string LevelDataFilePath { get; }

        private Dictionary<Tuple<int, int>, FileStream> _files = new Dictionary<Tuple<int, int>, FileStream>();
        private Dictionary<Tuple<int, int>, Chunk> _chunks = new Dictionary<Tuple<int, int>, Chunk>();

        public AnvilWorldSaveFormat(string worldName)
        {
            this.WorldPath = $"{Server.ExecutePath}/worlds/{worldName}";
            this.RegionPath = $"{this.WorldPath}/" + "region";
            if (!Directory.Exists(this.WorldPath))
            {
                Directory.CreateDirectory(this.WorldPath);
            }

            if (!Directory.Exists(this.RegionPath))
            {
                Directory.CreateDirectory(this.RegionPath);
            }

            this.WorldName = WorldName;
            this.LevelDataFilePath = $"{this.WorldPath}/level.dat";
        }

        public void SetWorld(World world)
        {
            this.ChunkFormat = new McaChunkFormat(world);
            this.World = world;
        }

        public Chunk GetChunk(int chunkX, int chunkZ)
        {
            int width = 32;
            int depth = 32;

            int rx = chunkX >> 5;
            int rz = chunkZ >> 5;
            Tuple<int, int> regionPos = new Tuple<int, int>(rx, rz);
            Tuple<int, int> chunkPos = new Tuple<int, int>(chunkX, chunkZ);

            if (this._chunks.ContainsKey(chunkPos))
            {
                return this._chunks[chunkPos];
            }

            string filePath = Path.Combine(this.WorldPath, $@"region/r.{rx}.{rz}.mca");
            if (!File.Exists(filePath))
            {
                return new Chunk(this.World, chunkX, chunkZ);
            }

            if (!_files.ContainsKey(regionPos))
            {
                this._files[regionPos] = File.OpenRead(filePath);
            }

            FileStream regionFile = this._files[regionPos];
            byte[] buffer = new byte[8192];

            regionFile.Read(buffer, 0, 8192);

            int xi = (chunkX % width);
            if (xi < 0) xi += 32;
            int zi = (chunkZ % depth);
            if (zi < 0) zi += 32;
            int tableOffset = (xi + zi * width) * 4;

            regionFile.Seek(tableOffset, SeekOrigin.Begin);

            byte[] offsetBuffer = new byte[4];
            regionFile.Read(offsetBuffer, 0, 3);
            Array.Reverse(offsetBuffer);
            int offset = BitConverter.ToInt32(offsetBuffer, 0) << 4;

            byte[] bytes = BitConverter.GetBytes(offset >> 4);
            Array.Reverse(bytes);
            if (offset != 0 && offsetBuffer[0] != bytes[0] && offsetBuffer[1] != bytes[1] &&
                offsetBuffer[2] != bytes[2])
            {
                throw new FormatException();
            }

            int length = regionFile.ReadByte();

            if (offset == 0 || length == 0)
            {
                return new Chunk(this.World, chunkX, chunkZ);
            }

            regionFile.Seek(offset, SeekOrigin.Begin);
            byte[] waste = new byte[4];
            regionFile.Read(waste, 0, 4);
            int compressionMode = regionFile.ReadByte();

            if (compressionMode != 0x02)
                throw new FormatException();

            CompoundTag tag = NBTIO.ReadZLIBFile(new BinaryReader(regionFile).ReadBytes((int) (regionFile.Length - regionFile.Position)));
            return this.ChunkFormat.NBTDeserialize(tag);
        }

        public void SetChunk(Chunk chunk)
        {
            Tuple<int, int> pos = new Tuple<int, int>(chunk.X, chunk.Z);
            this._chunks[pos] = chunk;
        }

        public void SaveChunks(Chunk[] chunks)
        {
            int width = 32;
            int depth = 32;

            Chunk chunk = chunks[0];
            int rx = chunk.X >> 5;
            int rz = chunk.Z >> 5;

            string filePath = Path.Combine(this.WorldPath, $@"region/r.{rx}.{rz}.mca");
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(this.RegionPath);

                using (var regionFile = File.Open(filePath, FileMode.CreateNew))
                {
                    byte[] buffer = new byte[8192];
                    regionFile.Write(buffer, 0, buffer.Length);
                }
            }

            using (var regionFile = File.Open(filePath, FileMode.Open))
            {
                byte[] buffer = new byte[8192];
                regionFile.Read(buffer, 0, buffer.Length);

                for (int i = 0; i < chunks.Length; i++)
                {
                    chunk = chunks[i];
                    int xi = (chunk.X % width);
                    if (xi < 0) xi += 32;
                    int zi = (chunk.Z % depth);
                    if (zi < 0) zi += 32;
                    int tableOffset = (xi + zi * width) * 4;

                    regionFile.Seek(tableOffset, SeekOrigin.Begin);

                    byte[] offsetBuffer = new byte[4];
                    regionFile.Read(offsetBuffer, 0, 3);
                    Array.Reverse(offsetBuffer);
                    int offset = BitConverter.ToInt32(offsetBuffer, 0) << 4;
                    byte sectorCount = (byte) regionFile.ReadByte();

                    byte[] nbtBuf = NBTIO.WriteZLIBFile(this.ChunkFormat.NBTSerialize(chunk));
                    int nbtLength = nbtBuf.Length;
                    byte nbtSectorCount = (byte) Math.Ceiling(nbtLength / 4096d);

                    if (offset == 0 || sectorCount == 0 || nbtSectorCount > sectorCount)
                    {
                        regionFile.Seek(0, SeekOrigin.End);
                        offset = (int) ((int) regionFile.Position & 0xfffffff0);

                        regionFile.Seek(tableOffset, SeekOrigin.Begin);

                        byte[] bytes = BitConverter.GetBytes(offset >> 4);
                        Array.Reverse(bytes);
                        regionFile.Write(bytes, 0, 3);
                        regionFile.WriteByte(nbtSectorCount);
                    }

                    byte[] lenghtBytes = BitConverter.GetBytes(nbtLength + 1);
                    Array.Reverse(lenghtBytes);

                    regionFile.Seek(offset, SeekOrigin.Begin);
                    regionFile.Write(lenghtBytes, 0, 4);
                    regionFile.WriteByte(0x02);

                    regionFile.Write(nbtBuf, 0, nbtBuf.Length);

                    int reminder;
                    Math.DivRem(nbtLength + 4, 4096, out reminder);

                    byte[] padding = new byte[4096 - reminder];
                    if (padding.Length > 0) regionFile.Write(padding, 0, padding.Length);
                }
            }
        }

        public void Save(Dictionary<Tuple<int, int>, Chunk> chunks)
        {
            try
            {
                foreach (KeyValuePair<Tuple<int, int>, FileStream> file in _files)
                {
                    file.Value.Close();
                }

                foreach (KeyValuePair<Tuple<int, int>, Chunk> chunk in chunks)
                {
                    this._chunks[chunk.Key] = chunk.Value;
                }

                Dictionary<Tuple<int, int>, List<Chunk>> regions = new Dictionary<Tuple<int, int>, List<Chunk>>();
                foreach (KeyValuePair<Tuple<int, int>, Chunk> chunk in this._chunks.OrderBy(pair => pair.Key.Item1 >> 5)
                    .ThenBy(pair => pair.Key.Item2 >> 5))
                {
                    var regionKey = new Tuple<int, int>(chunk.Key.Item1 >> 5, chunk.Key.Item2 >> 5);
                    if (!regions.ContainsKey(regionKey))
                    {
                        regions.Add(regionKey, new List<Chunk>());
                    }

                    regions[regionKey].Add(chunk.Value);
                }

                List<Task> tasks = new List<Task>();
                foreach (var region in regions.OrderBy(pair => pair.Key.Item1).ThenBy(pair => pair.Key.Item2))
                {
                    Task task = new Task(() =>
                    {
                        List<Chunk> cks = region.Value;
                        this.SaveChunks(cks.ToArray());
                    });
                    task.Start();
                    tasks.Add(task);
                }

                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                this._chunks.Clear();
                this._files.Clear();
            }
        }
    }
}