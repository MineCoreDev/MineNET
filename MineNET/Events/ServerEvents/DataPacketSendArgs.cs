using MineNET.Entities;
using MineNET.Network.Packets;

namespace MineNET.Events.ServerEvents
{
    public class DataPacketSendArgs : ServerEventArgs, ICancellable
    {
        public Player Player { get; }
        public DataPacket Packet { get; }

        public bool IsCancel { get; set; }

        public DataPacketSendArgs(Player player, DataPacket packet)
        {
            this.Player = player;
            this.Packet = packet;
        }
    }
}
