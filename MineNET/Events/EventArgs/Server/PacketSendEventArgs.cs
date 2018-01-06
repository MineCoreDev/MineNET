using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineNET.Entities;
using MineNET.Network.Packets;

namespace MineNET.Events.EventArgs
{
    public class PacketSendEventArgs : PacketEventArgs
    {
        public PacketSendEventArgs(Server server, Player player, Packet packet) : base(server, player, packet)
        {
        }
    }
}
