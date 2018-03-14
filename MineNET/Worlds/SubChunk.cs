using System.Linq;
using MineNET.Utils;

namespace MineNET.Worlds
{
    public class SubChunk
    {
        byte[] blockData = ArrayUtils.CreateArray<byte>(4096, 0);
        public byte[] BlockData
        {
            get
            {
                return this.blockData;
            }

            set
            {
                this.blockData = value;
            }
        }

        NibbleArray metaData = ArrayUtils.CreateNibbleArray(4096, 0);
        public NibbleArray MetaDatas
        {
            get
            {
                return this.metaData;
            }

            set
            {
                this.metaData = value;
            }
        }

        NibbleArray skyLigth = ArrayUtils.CreateNibbleArray(4096, 0xff);
        public NibbleArray SkyLights
        {
            get
            {
                return this.skyLigth;
            }

            set
            {
                this.skyLigth = value;
            }
        }

        NibbleArray blockLigth = ArrayUtils.CreateNibbleArray(4096, 0);
        public NibbleArray BlockLigths
        {
            get
            {
                return this.blockLigth;
            }

            set
            {
                this.blockLigth = value;
            }
        }

        public bool IsEnpty()
        {
            return this.blockData.All(b =>
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
            return this.blockData[GetArrayIndex(x, y, z)];
        }

        public void SetBlock(int x, int y, int z, byte id)
        {
            this.blockData[GetArrayIndex(x, y, z)] = id;
        }

        public byte GetMetaData(int x, int y, int z)
        {
            return this.metaData[GetArrayIndex(x, y, z)];
        }

        public void SetMetaData(int x, int y, int z, byte meta)
        {
            this.metaData[GetArrayIndex(x, y, z)] = meta;
        }

        public byte[] GetBytes()
        {
            using (BinaryStream bs = new BinaryStream())
            {
                bs.WriteByte(0);
                bs.WriteBytes(this.BlockData);
                bs.WriteBytes(this.MetaDatas.ArrayData);

                return bs.ToArray();
            }
        }
    }
}
