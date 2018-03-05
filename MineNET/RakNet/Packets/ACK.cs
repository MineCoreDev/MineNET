using System;
using System.Collections.Generic;

namespace MineNET.RakNet.Packets
{
    public class ACK : Packet
    {
        public const int ID = 0xC0;

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

            List<Tuple<int, int>> ranges = Slize(new List<int>(this.packets));

            WriteLShort((ushort) ranges.Count);

            foreach (var range in ranges)
            {
                byte singleEntry = (byte) (range.Item1 == range.Item2 ? 0x01 : 0);

                WriteByte(singleEntry);
                WriteLTriad(range.Item1);
                if (singleEntry == 0)
                    WriteLTriad(range.Item2);
            }
        }

        public static List<Tuple<int, int>> Slize(List<int> acks)
        {
            List<Tuple<int, int>> ranges = new List<Tuple<int, int>>();

            if (acks.Count == 0) return ranges;

            int start = acks[0];
            int prev = start;

            if (acks.Count == 1)
            {
                ranges.Add(new Tuple<int, int>(start, start));
                return ranges;
            }

            acks.Sort();

            for (int i = 1; i < acks.Count; i++)
            {
                bool last = i + 1 == acks.Count;
                int current = acks[i];

                if (current - prev == 1 && !last)
                {
                    prev = current;
                    continue;
                }

                if (current - prev > 1 && !last)
                {
                    ranges.Add(new Tuple<int, int>(start, prev));

                    start = current;
                    prev = current;
                    continue;
                }

                if (current - prev == 1 && last)
                {
                    ranges.Add(new Tuple<int, int>(start, current));
                }

                if (current - prev > 1 && last)
                {
                    if (prev == start)
                    {
                        ranges.Add(new Tuple<int, int>(start, current));
                    }

                    if (prev != start)
                    {
                        ranges.Add(new Tuple<int, int>(start, prev));
                        ranges.Add(new Tuple<int, int>(current, current));
                    }
                }
            }

            return ranges;
        }
    }
}
