using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Events.EventArgs
{
    public abstract class ServerEventArgs : System.EventArgs
    {
        private Server server;
        public Server Server
        {
            get
            {
                return this.server;
            }
        }
    }
}
