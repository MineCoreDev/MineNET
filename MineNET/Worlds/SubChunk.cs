using MineNET.Blocks;
using MineNET.Utils;
using System.Collections.Generic;
using System.Linq;

namespace MineNET.Worlds
{
    public class SubChunk
    {
        public int[] BlockDatas { get; set; } = ArrayUtils.CreateArray<int>(4096, 0);
        public NibbleArray MetaDatas { get; set; } = ArrayUtils.CreateNibbleArray(4096, 0);

        public NibbleArray SkyLights { get; set; } = ArrayUtils.CreateNibbleArray(4096, 0xff);
        public NibbleArray BlockLigths { get; set; } = ArrayUtils.CreateNibbleArray(4096, 0);

        public bool IsEnpty => this.BlockDatas.All(b => b == BlockIDs.AIR);

        public int GetArrayIndex(int x, int y, int z)
        {
            return (x * 256) + (z * 16) + y;
        }

        public int GetBlock(int x, int y, int z)
        {
            return this.BlockDatas[GetArrayIndex(x, y, z)];
        }

        public void SetBlock(int x, int y, int z, int id)
        {
            this.BlockDatas[GetArrayIndex(x, y, z)] = id;
        }

        public byte GetMetaData(int x, int y, int z)
        {
            return this.MetaDatas[GetArrayIndex(x, y, z)];
        }

        public void SetMetaData(int x, int y, int z, byte meta)
        {
            this.MetaDatas[GetArrayIndex(x, y, z)] = meta;
        }

        public byte[] GetBytes()
        {
            using (BinaryStream bs = new BinaryStream())
            {
                bs.WriteByte(8);

                int numberOfStores = 1;
                bs.WriteByte((byte) numberOfStores);

                List<uint> palettes = new List<uint>(10);
                byte[] indexes = new byte[this.BlockDatas.Length];
                for (int i = 0; i < numberOfStores; i++)
                {
                    palettes.Clear();

                    bs.WriteByte((8 << 1) | 1);

                    int index = 0;
                    uint pHash = uint.MaxValue;
                    for (int bl = 0; bl < this.BlockDatas.Length; bl++)
                    {
                        int bid = this.BlockDatas[bl];
                        byte data = this.MetaDatas[bl];
                        uint hash = (uint) GlobalBlockPalette.GetRuntimeID(bid, data);
                        if (hash != pHash)
                        {
                            index = palettes.IndexOf(hash);
                            if (index == -1)
                            {
                                palettes.Add(hash);
                            }
                            index = palettes.IndexOf(hash);
                        }

                        indexes[bl] = (byte) index;
                        pHash = hash;
                    }

                    bs.WriteBytes(indexes);

                    bs.WriteSVarInt(palettes.Count);
                    foreach (var val in palettes)
                    {
                        bs.WriteSVarInt((int) val);
                    }
                }

                return bs.ToArray();
            }
        }
    }
}