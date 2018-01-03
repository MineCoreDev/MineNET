using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineCraftPENetwork.Server;
using MineCraftPENetwork.Protocol;

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

        public byte[] GetPacket(byte[] buffer)
        {
            var pid = buffer[0];
            var ms = new System.IO.MemoryStream(MineCraftPENetwork.RakNet.GetBuffer(buffer, 1));

            return null;
        }
    }
}
