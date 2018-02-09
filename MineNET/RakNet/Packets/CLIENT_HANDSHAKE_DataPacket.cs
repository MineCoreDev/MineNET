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
                return endPoint;
            }
        }

        IPEndPoint[] systemEndPoints;
        public IPEndPoint[] SystemEndPoint
        {
            get
            {
                return systemEndPoints;
            }
        }

        long sendPing;
        public long SendPing
        {
            get
            {
                return sendPing;
            }
        }

        long sendPong;
        public long SendPong
        {
            get
            {
                return sendPong;
            }
        }

        public override void Decode()
        {
            base.Decode();

            List<IPEndPoint> list = new List<IPEndPoint>();

            endPoint = ReadIPEndPoint();
            for (int i = 0; i < 10; ++i)
            {
                list.Add(ReadIPEndPoint());
            }

            systemEndPoints = list.ToArray();

            sendPing = ReadLong();
            sendPong = ReadLong();
        }
    }
}
