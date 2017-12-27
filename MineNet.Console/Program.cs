using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MineNET;

using MineCraftPENetwork.Server;
using MineCraftPENetwork.Protocol;

namespace MineNET.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var main = new MineNETMain();

            var tmr = new System.Threading.Timer((obj) =>
            {
                var h = new ServerHandler(main.server, new Test());
                h.SendOption("name", Encoding.UTF8.GetBytes("MCPE;MineNETServer;160;1.2.8;0;20;MineNET;Survival"));
            }, null, 100, 1000);

            for (;;) ;
        }
    }

    class Test : ServerInstance
    {
        public void CloseSession(string identifier, string reason)
        {
            throw new NotImplementedException();
        }

        public void HandleEncapsulated(string identifier, EncapsulatedPacket packet, int flags)
        {
            throw new NotImplementedException();
        }

        public void HandleOption(string option, string value)
        {
            throw new NotImplementedException();
        }

        public void HandleRaw(string address, int port, byte[] payload)
        {
            throw new NotImplementedException();
        }

        public void NotifyACK(string identifier, int identifierACK)
        {
            throw new NotImplementedException();
        }

        public void OpenSession(string identifier, string address, int port, long clientID)
        {
            throw new NotImplementedException();
        }
    }
}
