using System;
using System.Collections.Generic;
using System.IO;
using MineNET.Utils;

namespace MineNET.Worlds.Data
{
    public class RegionFile : IDisposable
    {
        public const int VERSION = 1;
        public const byte COMPRESSION_GZIP = 1;
        public const byte COMPRESSION_ZLIB = 2;
        public const int MAX_SECTOR_LENGTH = 256 << 12;
        public const int COMPRESSION_LEVEL = 7;

        public string ChunkDataFilePath { get; }

        public int X { get; }
        public int Z { get; }

        public Dictionary<int, int[]> LocationTable { get; } = new Dictionary<int, int[]>();

        public int LastSector { get; private set; }

        public long LastUsed { get; private set; }

        public BinaryStream stream;

        public bool IsFileCreated { get; private set; }

        public RegionFile(string worldName, int rX, int rZ, string ext = "mca")
        {
            this.ChunkDataFilePath = $"{Server.ExecutePath}\\worlds\\{worldName}\\region\\r.{rX}.{rZ}.{ext}";

            this.X = rX;
            this.Z = rZ;

            this.IsFileCreated = this.Load();
        }

        private bool Load()
        {
            if (!File.Exists(this.ChunkDataFilePath))
            {
                //File.WriteAllBytes(this.ChunkDataFilePath, new byte[0]);
                return false;
            }
            else
            {
                BinaryStream stream = new BinaryStream(File.ReadAllBytes(this.ChunkDataFilePath));
                this.LastSector = 1;

                int[] data = new int[1024 * 2];
                for (int i = 0; i < 1024 * 2; ++i)
                {
                    data[i] = stream.ReadInt();
                }

                for (int i = 0; i < 1024; ++i)
                {
                    int index = data[i];
                    this.LocationTable.Add(i, new int[] { index >> 8, index & 0xff, data[1024 + i] });
                    int value = this.LocationTable[i][0] + this.LocationTable[i][1] - 1;
                    if (value > this.LastSector)
                    {
                        this.LastSector = value;
                    }
                }

                return true;
            }
        }

        public byte[] GetChunkBytes(int chunkX, int chunkZ)
        {
            int index = GetChunkOffset(chunkX, chunkZ);
            if (index < 0 || index >= 4096)
            {
                return null;
            }

            this.LastUsed = DateTime.Now.Ticks;

            if (!this.IsChunkGenerated(index))
            {
                return null;
            }

            int[] table = this.LocationTable[index];
            this.stream.Offset = table[0] << 12;
            int length = this.stream.ReadInt();
            byte compression = this.stream.ReadByte();
            if (length <= 0 || length >= MAX_SECTOR_LENGTH)
            {
                if (length >= MAX_SECTOR_LENGTH)
                {
                    table[0] = ++this.LastSector;
                    table[1] = 1;
                    this.LocationTable.Add(index, table);
                }
                return null;
            }

            if (length > (table[1] << 12))
            {
                table[1] = length >> 12;
                this.LocationTable.Add(index, table);
                //this.WriteLocationIndex(index);
            }
            else if (compression != COMPRESSION_ZLIB && compression != COMPRESSION_GZIP)
            {
                return null;
            }

            byte[] data = this.stream.ReadBytes(length - 1);
            return data;
        }

        public int GetChunkOffset(int x, int z)
        {
            return x | (z << 5);
        }

        public bool IsChunkGenerated(int index)
        {
            int[] array = this.LocationTable[index];
            return !(array[0] == 0 || array[1] == 0);
        }

        public void Save()
        {
            if (!File.Exists(this.ChunkDataFilePath))
            {

            }
            else
            {

            }
        }

        public void Dispose()
        {
            ((IDisposable) this.stream).Dispose();
        }
    }
}
