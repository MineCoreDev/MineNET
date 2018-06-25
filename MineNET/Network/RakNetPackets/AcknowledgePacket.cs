using MineNET.Utils;
using System;
using System.Collections.Generic;

namespace MineNET.Network.RakNetPackets
{
    public abstract class AcknowledgePacket : RakNetPacket
    {
        public int[] Packets { get; set; }

        public override void Encode()
        {
            base.Encode();

            BinaryStream stream = new BinaryStream();

            int records = 0;
            int count = this.Packets.Length;
            if (count > 1)
            {
                Array.Reverse(this.Packets);
            }

            if (count > 0)
            {
                int pointer = 1;
                int start = this.Packets[0];
                int last = this.Packets[0];

                while (pointer < count)
                {
                    int current = this.Packets[pointer++];
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

            this.Packets = pks.ToArray();
        }
    }
}
