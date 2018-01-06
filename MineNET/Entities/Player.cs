using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET.Network.Packets;

namespace MineNET.Entities
{
    public class Player : Human
    {
        public Player()
        {

        }

        public void HandlePacket(Packet packet)
        {
            if (packet is LoginPacket)
            {
                this.HandleLoginPacket((LoginPacket)packet);
            }
        }

        public void HandleLoginPacket(LoginPacket packet)
        {

        }
    }
}
