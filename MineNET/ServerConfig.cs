using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET.Utils.Config;

namespace MineNET
{
    public class ServerConfig : YamlStaticConfig
    {
        private string serverMotd = "MineNETServer";
        public string ServerMotd
        {
            get
            {
                return this.serverMotd;
            }

            set
            {
                this.serverMotd = value;
            }
        }

        private ushort serverPort = 19132;
        public ushort ServerPort
        {
            get
            {
                return this.serverPort;
            }

            set
            {
                this.serverPort = value;
            }
        }
    }
}
