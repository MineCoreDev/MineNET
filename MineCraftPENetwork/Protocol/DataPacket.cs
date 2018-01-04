using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public abstract class DataPacket : Packet
    {
        public List<object> packets = new List<object>();

	    public int seqNumber;

        public override void Encode()
        {
            base.Encode();

            WriteLTriad(seqNumber);

            for (int i = 0; i < packets.Count; ++i)
            {
                if (packets[i] is EncapsulatedPacket)
                {
                    writer.Write(((EncapsulatedPacket)packets[i]).ToBinary());
                }
                else
                {
                    writer.Write((byte[])packets[i]);
                }
            }
        }

        public int Length()
        {
            var length = 4;
            for (int i = 0; i < packets.Count; ++i)
            {
                if (packets[i] is EncapsulatedPacket)
                {
                    length += ((EncapsulatedPacket)packets[i]).GetTotalLength();
                }
                else
                {
                    length += ((byte[])packets[i]).Length;
                }
            }

            return length;
        }

        public override void Decode()
        {
            base.Decode();

            seqNumber = ReadLTriad();

            while(!RakNet.EndOfBuffer(Buffer, (int)reader.BaseStream.Position))
            {
                var offset = 0;
                var data = RakNet.GetBuffer(Buffer, (int)reader.BaseStream.Position);
                var packet = EncapsulatedPacket.FromBinary(data, ref offset, false);
                reader.BaseStream.Position += offset;
                if (Buffer.Length == 0)
                {
                    break;
                }
                packets.Add(packet);
            }
        }

        public override void Clear()
        {
            packets.Clear();
            seqNumber = 0;

            base.Clear();
        }
    }
}
