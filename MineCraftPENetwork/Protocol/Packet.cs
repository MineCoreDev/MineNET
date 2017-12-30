using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace MineCraftPENetwork.Protocol
{
    public abstract class Packet
    {
        const int STREAM_TYPE_NONE = -1;
        const int STREAM_TYPE_READ = 0;
        const int STREAM_TYPE_WRITE = 1;

        protected BinaryReader reader;
        protected BinaryWriter writer;

        private MemoryStream stream;

        private int streamType = -1;

        protected static byte ID;

        private byte[] buffer;

        public long sendTime;

        public byte[] Buffer
        {
            get
            {
                return stream.ToArray();
            }

            set
            {
                buffer = value;
            }
        }

        public abstract byte PacketID
        {
            get;
            set;
        }

        public virtual void Encode()
        {
            if (streamType == STREAM_TYPE_NONE)
            {
                streamType = STREAM_TYPE_WRITE;

                stream = new MemoryStream();
                writer = new BinaryWriter(stream);

                writer.Write(PacketID);
            }
        }

        public virtual void Decode()
        {
            if (streamType == STREAM_TYPE_NONE)
            {
                streamType = STREAM_TYPE_READ;

                stream = new MemoryStream(buffer);
                reader = new BinaryReader(stream);

                ID = reader.ReadByte();
            }
        }

        public virtual void Clear()
        {
            if (streamType != STREAM_TYPE_NONE)
            {
                streamType = STREAM_TYPE_NONE;

                stream = null;
                reader = null;
                writer = null;
            }
        }

        public void WriteFixedString(BinaryWriter writer, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                writer.Write((short)0);
                return;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(value);

            writer.Write(SwapUInt16((ushort)bytes.Length));
            writer.Write(bytes);
        }

        public string ReadFixedString()
        {
            if (reader.BaseStream.Position == reader.BaseStream.Length) return string.Empty;
            ushort len = SwapUInt16(reader.ReadUInt16());
            if (len <= 0) return string.Empty;
            return Encoding.UTF8.GetString(reader.ReadBytes(len));
        }

        public static byte[] Int32ToLTriad(int value)
        {
            var buffer = BitConverter.GetBytes(value);
            var list = new List<byte>();
            list.AddRange(buffer);
            list.RemoveRange(3, 1);

            return list.ToArray();
        }

        public static int LTriadToInt32(byte[] value)
        {
            var l = value.ToList();
            l.Add(0);
            try
            {
                return BitConverter.ToInt32(l.ToArray(), 0);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static int BytesToInt(byte[] value)
        {
            if (value.Length == 4)
            {
                return BitConverter.ToInt32(value, 0);
            }
            else
            {
                var l = value.ToList();
                var diff = 4 - value.Length;
                for(int i = 0; i < diff; ++i)
                {
                    l.Add(0);
                }
                return BitConverter.ToInt32(l.ToArray(), 0);
            }
        }

        public static int BytesToShort(byte[] value)
        {
            if (value.Length == 2)
            {
                return BitConverter.ToInt16(value, 0);
            }
            else
            {
                var l = value.ToList();
                var diff = 2 - value.Length;
                for (int i = 0; i < diff; ++i)
                {
                    l.Add(0);
                }
                return BitConverter.ToInt16(l.ToArray(), 0);
            }
        }

        public void WriteLTriad(int value)
        {
            var buffer = Int32ToLTriad(value);
            writer.Write(buffer);
        }

        public int ReadLTriad()
        {
            var buffer = reader.ReadBytes(3);
            return LTriadToInt32(buffer);
        }

        public void WriteAddress(string ip, int port, byte version)
        {
            if (version == 4)
            {
                writer.Write(version);
                var sip = ip.Split('.');
                foreach(var i in sip)
                {
                    writer.Write((byte)(~byte.Parse(i) & 0xff));
                }

                writer.Write((ushort)port);
            }
            else if (version == 6)
            {

            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void ReadAddress(ref string ip, ref int port)
        {
            var version = reader.ReadByte();

            if (version == 4)
            {
                ip = $"{~reader.ReadByte() & 0xff}.{~reader.ReadByte() & 0xff}.{~reader.ReadByte() & 0xff}.{~reader.ReadByte() & 0xff}";
                port = SwapUInt16(reader.ReadUInt16());
            }
            else if (version == 6)
            {
                reader.ReadInt16();
                port = SwapUInt16(reader.ReadUInt16());
                reader.ReadInt64();
                ip = new IPAddress(reader.ReadBytes(16)).ToString();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public Packet Clone()
        {
            var clone = (Packet)Activator.CreateInstance(GetType());
            clone.Buffer = Buffer;
            return clone;
        }

        internal static ushort SwapUInt16(ushort v)
        {
            return (ushort)(((v & 0xff) << 8) | ((v >> 8) & 0xff));
        }
    }
}
