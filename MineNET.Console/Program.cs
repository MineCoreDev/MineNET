using System;
using MineNET.Utils;

namespace MineNET.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = null;
            try
            {
                server = new Server();
                server.Start();
                while (!server.IsShutdown())
                {
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                server.Stop();
            }
        }
    }
}
