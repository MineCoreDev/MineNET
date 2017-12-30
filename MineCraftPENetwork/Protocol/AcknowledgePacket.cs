using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public abstract class AcknowledgePacket : Packet
    {
        public int[] packets = new int[0];

        public override void Encode()
        {
            base.Encode();
            var payload = new List<byte>();

            var s = packets.ToList();
            s.Sort();
            packets = s.ToArray();

            var count = packets.Length;
            var records = 0;

            if (count > 0) {
                var pointer = 1;
                var start = packets[0];
                var last = packets[0];

                while (pointer < count) {
                    var current = packets[pointer++];
                    var diff = current - last;
                    if (diff == 1) {
                        last = current;
                    }
                    else if(diff > 1)
                    { //Forget about duplicated packets (bad queues?)
                        if (start == last)
                        {
                            payload.Add(1);
                            payload.AddRange(Int32ToLTriad(start));
                            start = last = current;
                        }
                        else
                        {
                            payload.Add(0);
                            payload.AddRange(Int32ToLTriad(start));
                            payload.AddRange(Int32ToLTriad(last));
                            start = last = current;
                        }
                        ++records;
                    }
                }

                if (start == last) {
                    payload.Add(1);
                    payload.AddRange(Int32ToLTriad(start));
                } else {
                    payload.Add(0);
                    payload.AddRange(Int32ToLTriad(start));
                    payload.AddRange(Int32ToLTriad(last));
                }
                ++records;
            }

            writer.Write((short)records);
            writer.Write(payload.ToArray());
        }

        public override void Decode()
        {
            base.Decode();
            var count = reader.ReadInt16();
            var packets = new Dictionary<int, int>();
            var cnt = 0;
            for (int i = 0; i < count && !RakNet.EndOfBuffer(Buffer, (int)reader.BaseStream.Position) && cnt < 4096; ++i){
                if (reader.ReadByte() == 0) {
                    var start = ReadLTriad();
                    var end = ReadLTriad();
                    if ((end - start) > 512) {
                        end = start + 512;
                    }
                    for (int c = start; c <= end; ++c) {
                        packets[cnt++] = c;
                    }
                } else {
                    packets.Add(cnt++, ReadLTriad());
                }
            }
        }

        public override void Clear()
        {
            packets = new int[0];

            base.Clear();
        }
    }
}
