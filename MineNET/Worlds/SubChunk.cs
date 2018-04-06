using System.Linq;
using MineNET.Blocks;
using MineNET.Utils;

namespace MineNET.Worlds
{
    public class SubChunk
    {
        public byte[] BlockDatas { get; set; } = ArrayUtils.CreateArray<byte>(4096, 0);
        public NibbleArray MetaDatas { get; set; } = ArrayUtils.CreateNibbleArray(4096, 0);

        public NibbleArray SkyLights { get; set; } = ArrayUtils.CreateNibbleArray(4096, 0xff);
        public NibbleArray BlockLigths { get; set; } = ArrayUtils.CreateNibbleArray(4096, 0);

        public bool IsEnpty
        {
            get
            {
                return this.BlockDatas.All(b =>
                {
                    return b == 0;
                });
            }
        }

        public int GetArrayIndex(int x, int y, int z)
        {
            return (x * 256) + (z * 16) + y;
        }

        public byte GetBlock(int x, int y, int z)
        {
            return this.BlockDatas[GetArrayIndex(x, y, z)];
        }

        public void SetBlock(int x, int y, int z, byte id)
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
                bs.WriteByte(0);//TODO New ChunkFormat...(8)
                //bs.WriteUVarInt(4096);//block length
                //for (int i = 0; i < 4096; ++i)
                //{
                //    bs.WriteSVarInt(Block.GetRuntimeId(this.BlockDatas[i], this.MetaDatas[i]));//RuntimeId?
                //}
                bs.WriteBytes(this.BlockDatas);
                bs.WriteBytes(this.MetaDatas.ArrayData);

                return bs.ToArray();
            }
        }
    }
}
