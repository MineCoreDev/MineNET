using System.IO;
using MineNET.Utils;

namespace MineNET.NBT
{
    public class NBTStream : MemoryStream
    {
        NBTEndian endian;
        public NBTEndian Endian
        {
            get
            {
                return endian;
            }
        }

        public NBTStream()
        {

        }

        public NBTStream(byte[] buffer) : this(buffer, NBTEndian.LITTLE_ENDIAN)
        {

        }

        public NBTStream(byte[] buffer, NBTEndian endian)
        {
            this.endian = endian;
        }

        public new byte ReadByte()
        {
            return Binary.ReadByte(this);
        }

        public new void WriteByte(byte value)
        {
            Binary.WriteByte(this, value);
        }

        public short ReadShort()
        {
            return Binary.ReadShort(this);
        }
    }
}
