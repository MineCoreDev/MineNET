using System.Collections.Generic;

namespace MineNET.RakNet.Packets
{
    public abstract class DataPacket : Packet
    {
        object[] packets;
        public object[] Packets
        {
            get
            {
                return this.packets;
            }

            set
            {
                this.packets = value;
            }
        }

        int seqNumber;
        public int SeqNumber
        {
            get
            {
                return this.seqNumber;
            }

            set
            {
                this.seqNumber = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            WriteLTriad(seqNumber);
            for (int i = 0; i < packets.Length; ++i)
            {
                object obj = packets[i];
                if (obj is EncapsulatedPacket)
                {
                    WriteBytes(((EncapsulatedPacket)obj).ToResult());
                }
                else
                {
                    WriteBytes((byte[])obj);
                }
            }
        }

        public override void Decode()
        {
            base.Decode();

            List<object> list = new List<object>();

            seqNumber = ReadLTriad();
            byte[] bytes = ReadBytes();
            while (bytes.Length != 0)
            {
                list.Add(EncapsulatedPacket.ToEncapsulatedPacket(ref bytes));
            }

            packets = list.ToArray();
        }

        public void Clear()
        {
            seqNumber = 0;
            packets = null;
        }
    }
}
