using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineCraftPENetwork.Protocol;

namespace MineCraftPENetwork.Server
{
    public interface ServerInstance
    {
        void OpenSession(string identifier, string address, int port, long clientID);
        
        void CloseSession(string identifier, string reason);
        
        void HandleEncapsulated(string identifier, EncapsulatedPacket packet, int flags);
        
        void HandleRaw(string address, int port, byte[] payload);

        void NotifyACK(string identifier, int identifierACK);
        
        void HandleOption(string option, string value);
    }
}
