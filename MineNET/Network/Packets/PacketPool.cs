using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Network.Packets
{
    public static class PacketPool
    {
        private static Dictionary<byte, Packet> pool = new Dictionary<byte, Packet>();

        public static void Init()
        {
            RegisterPacket(LoginPacket.NETWORK_ID, new LoginPacket());

            RegisterPacket(BatchPacket.NETWORK_ID, new BatchPacket());
        }

        public static void RegisterPacket(byte id, Packet packet, bool overridePacket = false)
        {
            if (overridePacket)
            {
                pool[id] = packet;
            }
            else
            {
                pool.Add(id, packet);
            }
        }

        public static Packet GetPacketByID(byte id)
        {
            if (pool.ContainsKey(id))
            {
                return (Packet)pool[id].Clone();
            }
            else
            {
                return new UnknownPacket();
            }
        }
    }
}
