using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineCraftPENetwork.Server;
using MineCraftPENetwork.Protocol;

using MineNET.Entities;
using MineNET.Network.Packets;

namespace MineNET.Network
{
    public class MineNETServerHandler : ServerInstance
    {
        private Dictionary<string, Player> players = new Dictionary<string, Player>();

        private Dictionary<int, string> identifiers = new Dictionary<int, string>();

        private Dictionary<string, int> identifiersACK = new Dictionary<string, int>();

        public void CloseSession(string identifier, string reason)
        {
            //throw new NotImplementedException();
        }

        public void HandleEncapsulated(string identifier, EncapsulatedPacket packet, int flags)
        {
            try
            {
                this.GetPacket(packet.buffer);
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

        public async void GetPacket(byte[] buffer)
        {
            var pid = buffer[0];
            if (pid != 0xfe)
            {
                return;
            }

            Console.WriteLine("[Debug]HandleBatchPacket");

            var pk = new BatchPacket();
            pk.Write(buffer, 0, buffer.Length);

            pk.Decode();

            var packets = await pk.GetPackets();

            foreach(var p in packets)
            {
                if (p is LoginPacket)
                {
                    var s = (LoginPacket)p;
                    s.Decode();
                }
            }
        }
    }
}
