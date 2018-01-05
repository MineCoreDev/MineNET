using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineCraftPENetwork.Server;
using MineCraftPENetwork.Protocol;

using MineNET.Network.Packets;

namespace MineNET.Network
{
    public class MineNETServerHandler : ServerInstance
    {
        public void CloseSession(string identifier, string reason)
        {
            //throw new NotImplementedException();
        }

        public void HandleEncapsulated(string identifier, EncapsulatedPacket packet, int flags)
        {
            try
            {
                var pk = this.GetPacket(packet.buffer);
            }
            catch (Exception e)
            {
                Server.GetLogger().Error(e);
            }
        }

        public void HandleOption(string option, byte[] value)
        {
            //throw new NotImplementedException();
        }

        public void HandleRaw(string address, int port, byte[] payload)
        {
            //throw new NotImplementedException();
        }

        public void NotifyACK(string identifier, int identifierACK)
        {
            //throw new NotImplementedException();
        }

        public void OpenSession(string identifier, string address, int port, long clientID)
        {
            //throw new NotImplementedException();
        }

        public Packets.Packet GetPacket(byte[] buffer)
        {
            var pid = buffer[0];
            if (pid != 0xfe)
            {
                return null;
            }

            Console.WriteLine("[Debug]HandleBatchPacket");

            var pk = new BatchPacket();
            pk.Write(buffer, 0, buffer.Length);

            pk.Decode();

            pk.GetPackets();

            return pk;
        }
    }
}
