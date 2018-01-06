using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET.Events.EventArgs;

namespace MineNET.Events
{
    public static class ServerEvents
    {
        public delegate void PacketReceiveEventHandler(PacketReceiveEventArgs args);
        public delegate void PacketSendEventHandler(PacketSendEventArgs args);

        public static event PacketReceiveEventHandler PacketReceive;
        public static event PacketSendEventHandler PacketSend;

        public static void OnPacketReceive(PacketReceiveEventArgs args)
        {
            PacketReceive?.Invoke(args);
        }

        public static void OnPacketSend(PacketSendEventArgs args)
        {
            PacketSend?.Invoke(args);
        }
    }
}
