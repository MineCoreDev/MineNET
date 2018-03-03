using System;

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
                server.ErrorStop(e);
                server.Stop();
            }
        }
    }
}
