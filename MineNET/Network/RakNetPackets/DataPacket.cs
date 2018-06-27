using System.Collections.Generic;

namespace MineNET.Network.RakNetPackets
{
    public abstract class DataPacket : RakNetPacket
    {
        public object[] Packets { get; set; } = new object[0];
        public int SeqNumber { get; set; }

        public int SendTimedOut { get; set; } = NetworkSession.TimedOutTime;

        public override void Encode()
        {
            base.Encode();

            WriteLTriad(this.SeqNumber);
            for (int i = 0; i < this.Packets.Length; ++i)
            {
                object obj = this.Packets[i];
                if (obj is EncapsulatedPacket)
                {
                    WriteBytes(((EncapsulatedPacket) obj).ToResult());
                }
                else
                {
                    WriteBytes((byte[]) obj);
                }
            }
        }

        public override void Decode()
        {
            base.Decode();

            List<object> list = new List<object>();

            this.SeqNumber = ReadLTriad();
            byte[] bytes = ReadBytes();
            while (bytes.Length != 0)
            {
                list.Add(EncapsulatedPacket.ToEncapsulatedPacket(ref bytes));
            }

            this.Packets = list.ToArray();
        }

        public void Clear()
        {
            this.SeqNumber = 0;
            this.Packets = null;
        }
    }
}
