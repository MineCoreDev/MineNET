using System;

namespace MineNET.Console
{
    public class Program
    {
        private static void Main(string[] args)
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
