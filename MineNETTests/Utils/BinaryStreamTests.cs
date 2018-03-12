using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineNET.Utils.Tests
{
    [TestClass()]
    public class BinaryStreamTests
    {
        [TestMethod()]
        public void WriteBytesTest()
        {
            /*BinaryStream bs = new BinaryStream();
            for (int i = 0; i < 200000; ++i)
            {
                bs.WriteByte(0xa3);
            }*/
        }

        [TestMethod()]
        public void MemorySpanTest()
        {
            MemorySpan span = new MemorySpan(new byte[0]);
            span.WriteBytes(ArrayUtils.CreateArray<byte>(1000, 0));
            span.Offset = 0;
            Console.WriteLine(Binary.SplitBytes(span, 90).Length);
        }

        [TestMethod()]
        public void WriteReadTest2()
        {
            BinaryStream bs = new BinaryStream();
            
        }

        [TestMethod()]
        public void WriteReadTest()
        {
            byte[] ipbyte = new byte[]
            {
                0xC0,
                0xA8,
                0x0B,
                0x08
            };

            BinaryStream bs = new BinaryStream();
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteBool(true);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteByte(10);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteSByte(-10);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteShort(-20);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteUShort(20);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteLShort(200);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteTriad(30);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteLTriad(300);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteInt(-40);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteUInt(40);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteLInt(400);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteLong(-50);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteULong(50);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteLLong(500);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteVarInt(-60);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteUVarInt(60);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteSVarInt(-600);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteVarLong(-70);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteUVarLong(70);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteSVarLong(-700);
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteFixedString("hallo");
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteString("long hallo");
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteBytes(new byte[]
            {
                0x00,
                0x00,
                0x23,
                0x45,
                0x67
            });
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteIPEndPoint(new IPEndPoint(IPAddress.Parse("192.168.11.1"), 80));
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteGuid(Guid.NewGuid());
            Console.WriteLine("Offset" + bs.Offset);
            bs.WriteBytes(new byte[]
           {
                0x00,
                0x00,
                0x23,
                0x45,
                0x67
           });
            Console.WriteLine("Offset" + bs.Offset);

            bs = new BinaryStream(bs.ToArray());
            //Move();

            Console.WriteLine(bs.ReadBool());
            Console.WriteLine(bs.ReadByte());
            Console.WriteLine(bs.ReadSByte());
            Console.WriteLine(bs.ReadShort());
            Console.WriteLine(bs.ReadUShort());
            Console.WriteLine(bs.ReadLShort());
            Console.WriteLine(bs.ReadTriad());
            Console.WriteLine(bs.ReadLTriad());
            Console.WriteLine(bs.ReadInt());
            Console.WriteLine(bs.ReadUInt());
            Console.WriteLine(bs.ReadLInt());
            Console.WriteLine(bs.ReadLong());
            Console.WriteLine(bs.ReadULong());
            Console.WriteLine(bs.ReadLLong());
            Console.WriteLine(bs.ReadVarInt());
            Console.WriteLine(bs.ReadUVarInt());
            Console.WriteLine(bs.ReadSVarInt());
            Console.WriteLine(bs.ReadVarLong());
            Console.WriteLine(bs.ReadUVarLong());
            Console.WriteLine(bs.ReadSVarLong());
            Console.WriteLine(bs.ReadFixedString());
            Console.WriteLine(bs.ReadString());
            Console.WriteLine(bs.ReadBytes(5).Length);
            Console.WriteLine(bs.ReadIPEndPoint());
            Console.WriteLine(bs.ReadGuid());
            Console.WriteLine(bs.ReadBytes().Length);
            Console.WriteLine("Offset" + bs.Offset);
        }
    }
}