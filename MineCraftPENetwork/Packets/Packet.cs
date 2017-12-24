using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets
{
    public abstract class Packet
    {
        public virtual byte[] Encode()
        {
            throw new NotImplementedException();
        }

        public virtual void Decode(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public void ReadIPPort(BinaryReader reader, ref string ip, ref short port)
        {
            var version = reader.ReadByte();
            var ip_c1 = reader.ReadByte();
            var ip_c2 = reader.ReadByte();
            var ip_c3 = reader.ReadByte();
            var ip_c4 = reader.ReadByte();
            var str = new StringBuilder();
            str.Append(ip_c1);
            str.Append(".");
            str.Append(ip_c2);
            str.Append(".");
            str.Append(ip_c3);
            str.Append(".");
            str.Append(ip_c4);

            ip = str.ToString();
            port = reader.ReadInt16();
        }

        public string ReadIPPort(BinaryReader reader)
        {
            var version = reader.ReadByte();
            var ip_c1 = reader.ReadByte();
            var ip_c2 = reader.ReadByte();
            var ip_c3 = reader.ReadByte();
            var ip_c4 = reader.ReadByte();
            var str = new StringBuilder();
            str.Append(ip_c1);
            str.Append(".");
            str.Append(ip_c2);
            str.Append(".");
            str.Append(ip_c3);
            str.Append(".");
            str.Append(ip_c4);
            str.Append(":");
            str.Append(reader.ReadInt16());

            return str.ToString();
        }

        public void WriteIPPort(BinaryWriter writer, string ip, short port)
        {
            writer.Write((byte)4);
            foreach(var str in ip.Split('.'))
            {
                writer.Write(byte.Parse(str));
            }
            writer.Write(port);
        }
    }
}
