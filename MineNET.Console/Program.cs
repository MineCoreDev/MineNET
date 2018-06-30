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
                server.StartUpdate();
            }
            catch (Exception e)
            {
                server.ErrorStop(e);
            }
        }
    }
}
