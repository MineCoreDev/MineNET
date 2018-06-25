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

                long tick = 0;
                while (server.Status == ServerStatus.Running)
                {
                    server.OnUpdate(tick);
                    tick++;
                }
            }
            catch (Exception e)
            {
                server.ErrorStop(e);
            }
        }
    }
}
