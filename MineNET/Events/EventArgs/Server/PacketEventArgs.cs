using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET.Entities;
using MineNET.Network.Packets;

namespace MineNET.Events.EventArgs
{
    public class PacketEventArgs : ServerEventArgs, ICancellable
    {
        private Player player;
        public Player Player
        {
            get
            {
                return this.player;
            }
        }

        private Packet packet;
        public Packet Packet
        {
            get
            {
                return this.packet;
            }
        }

        private bool isCancel;

        public PacketEventArgs(Server server, Player player, Packet packet)
        {
            this.player = player;
            this.packet = packet;
        }

        public bool IsCancel
        {
            get
            {
                return this.isCancel;
            }

            set
            {
                this.isCancel = value;
            }
        }
    }
}
