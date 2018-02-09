using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET;

namespace MineNET.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MineNETServer server = new MineNETServer();
            server.Start();
            while (!server.IsShutdown())
            {
            }
        }
    }
}
