using System.Linq;
using MineNET.Utils;
using MineNET.Worlds.Formats;

namespace MineNET.Worlds
{
    public class SubChunk
    {
        byte[] blockData = ArrayUtils.CreateArray<byte>(4096, 0);
        public byte[] BlockData
        {
            get
            {
                return blockData;
            }

            set
            {
                blockData = value;
            }
        }

        NibbleArray metaData = ArrayUtils.CreateNibbleArray(4096, 0);
        public NibbleArray MetaDatas
        {
            get
            {
                return metaData;
            }

            set
            {
                metaData = value;
            }
        }

        public IChunkFormat format;
        IChunkFormat ChunkFormat
        {
            get
            {
                return format;
            }

            set
            {
                format = value;
            }
        }

        public bool IsEnpty()
        {
            return blockData.All(b =>
            {
                return b == 0;
            });
        }

        public int GetArrayIndex(int x, int y, int z)
        {
            return (x * 256) + (z * 16) + y;
        }

        public byte GetBlock(int x, int y, int z)
        {
            return blockData[GetArrayIndex(x, y, z)];
        }

        public void SetBlock(int x, int y, int z, byte id)
        {
            blockData[GetArrayIndex(x, y, z)] = id;
        }

        public byte GetMetaData(int x, int y, int z)
        {
            return metaData[GetArrayIndex(x, y, z)];
        }

        public void SetMetaData(int x, int y, int z, byte meta)
        {
            metaData[GetArrayIndex(x, y, z)] = meta;
        }

        public byte[] GetBytes()
        {
            using (BinaryStream bs = new BinaryStream())
            {
                bs.WriteByte(0);
                bs.WriteBytes(BlockData);
                bs.WriteBytes(MetaDatas.ArrayData);

                return bs.ToArray();
            }
        }
    }
}
