using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Data
{
    public class LoginExtraData
    {
        private string displayName;
        private string clientUUID;

        public string DisplayName
        {
            get
            {
                return displayName;
            }

            internal set
            {
                displayName = value;
            }
        }

        public string ClientUUID
        {
            get
            {
                return clientUUID;
            }

            internal set
            {
                clientUUID = value;
            }
        }
    }
}
