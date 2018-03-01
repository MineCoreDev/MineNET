using System;
using MineNET.Utils;

namespace MineNET.RakNet.Packets
{
    public class NACK : Packet
    {
        public const int ID = 0xA0;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        public int[] packets;

        public override void Encode()
        {
            base.Encode();

            BinaryStream payload = new BinaryStream();

            Array.Sort(packets);

            var count = packets.Length;
            var records = 0;

            if (count > 0)
            {
                int pointer = 1;
                int start = packets[0];
                int last = packets[0];

                while (pointer < count)
                {
                    int current = packets[pointer++];
                    int diff = current - last;
                    if (diff == 1)
                    {
                        last = current;
                    }
                    else if (diff > 1)
                    { //Forget about duplicated packets (bad queues?)
                        if (start == last)
                        {
                            payload.WriteByte(1);
                            payload.WriteLTriad(start);
                            start = last = current;
                        }
                        else
                        {
                            payload.WriteByte(0);
                            payload.WriteLTriad(start);
                            payload.WriteLTriad(last);
                            start = last = current;
                        }
                        ++records;
                    }
                }

                if (start == last)
                {
                    payload.WriteByte(1);
                    payload.WriteLTriad(start);
                }
                else
                {
                    payload.WriteByte(0);
                    payload.WriteLTriad(start);
                    payload.WriteLTriad(last);
                }
                ++records;
            }

            WriteLShort((ushort) records);
            WriteBytes(payload.ToArray());
        }
    }
}
