using System.Collections.Generic;
using System.Net;

namespace MineNET.RakNet.Packets
{
    public class CLIENT_HANDSHAKE_DataPacket : Packet
    {
        public const int ID = 0x13;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        IPEndPoint endPoint;
        public IPEndPoint EndPoint
        {
            get
            {
                return this.endPoint;
            }
        }

        IPEndPoint[] systemEndPoints;
        public IPEndPoint[] SystemEndPoint
        {
            get
            {
                return this.systemEndPoints;
            }
        }

        long sendPing;
        public long SendPing
        {
            get
            {
                return this.sendPing;
            }
        }

        long sendPong;
        public long SendPong
        {
            get
            {
                return this.sendPong;
            }
        }

        public override void Decode()
        {
            base.Decode();

            List<IPEndPoint> list = new List<IPEndPoint>();

            this.endPoint = ReadIPEndPoint();
            for (int i = 0; i < 20; ++i)
            {
                list.Add(ReadIPEndPoint());
            }

            this.systemEndPoints = list.ToArray();

            this.sendPing = ReadLong();
            this.sendPong = ReadLong();
        }
    }
}
