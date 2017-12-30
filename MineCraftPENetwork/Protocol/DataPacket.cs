using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public abstract class DataPacket : Packet
    {
        public List<EncapsulatedPacket> packets = new List<EncapsulatedPacket>();

	    public int seqNumber;

        public override void Encode()
        {
            base.Encode();

            WriteLTriad(seqNumber);

            foreach (var packet in packets)
            {
                writer.Write(packet.ToBinary());
            }
        }

        public int Length()
        {
            var length = 4;
            foreach (var packet in packets)
            {
			    length += packet.GetTotalLength();
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
                if (Buffer.Length < 0)
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
