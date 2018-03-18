using System;
using System.Collections.Generic;
using MineNET.Utils;

namespace MineNET.RakNet.Packets
{
    public abstract class AcknowledgePacket : Packet
    {
        public int[] packets;

        public override void Encode()
        {
            base.Encode();

            BinaryStream stream = new BinaryStream();

            int records = 0;
            int count = this.packets.Length;
            if (count > 1)
            {
                Array.Reverse(this.packets);
            }

            if (count > 0)
            {
                int pointer = 1;
                int start = this.packets[0];
                int last = this.packets[0];

                while (pointer < count)
                {
                    int current = this.packets[pointer++];
                    int diff = current - last;
                    if (diff == 1)
                    {
                        last = current;
                    }
                    else if (diff > 1)
                    {
                        if (start == last)
                        {
                            stream.WriteByte(1);
                            stream.WriteLTriad(start);
                            start = last = current;
                        }
                        else
                        {
                            stream.WriteByte(0);
                            stream.WriteLTriad(start);
                            stream.WriteLTriad(last);
                            start = last = current;
                        }
                        ++records;
                    }
                }

                if (start == last)
                {
                    stream.WriteByte(1);
                    stream.WriteLTriad(start);
                }
                else
                {
                    stream.WriteByte(0);
                    stream.WriteLTriad(start);
                    stream.WriteLTriad(last);
                }
                ++records;
            }

            this.WriteShort((short) records);
            this.WriteBytes(stream.ToArray());

            stream.Clone();
        }

        public override void Decode()
        {
            base.Decode();

            int count = this.ReadShort();
            List<int> pks = new List<int>();
            int cnt = 0;
            for (int i = 0; i < count && !this.EndOfStream && cnt < 4096; ++i)
            {
                if (this.ReadByte() == 0)
                {
                    int start = this.ReadLTriad();
                    int end = this.ReadLTriad();
                    if ((end - start) > 512)
                    {
                        end = start + 512;
                    }
                    for (int j = start; j <= end; ++j)
                    {
                        pks.Add(j);
                    }
                }
                else
                {
                    pks.Add(this.ReadLTriad());
                }
            }

            this.packets = pks.ToArray();
        }
    }
}
