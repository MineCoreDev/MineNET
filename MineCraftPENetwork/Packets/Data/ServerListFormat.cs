using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineCraftPENetwork.Packets.Data
{
    public class ServerListFormat
    {
        public string serverName;

        public short protocol;
        public string versionString;

        public short playerCount;
        public short maxPlayer;

        public long serverID;

        public string gameMode;

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("MCPE;");
            str.Append(serverName);
            str.Append(";");
            str.Append(protocol);
            str.Append(";");
            str.Append(versionString);
            str.Append(";");
            str.Append(playerCount);
            str.Append(";");
            str.Append(maxPlayer);
            str.Append(";");
            str.Append(serverID);
            str.Append(";");
            str.Append("Test;");
            str.Append(gameMode);
            str.Append(";");

            return str.ToString();
        }
    }
}
