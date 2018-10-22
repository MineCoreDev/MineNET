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
                if (server != null)
                {
                    try
                    {
                        server.ErrorStop(e);
                    }
                    catch (Exception e2)
                    {
                        System.Console.WriteLine(e);
                        System.Console.Read();
                    }
                }
                else
                {
                    System.Console.WriteLine(e);
                    System.Console.Read();
                }
            }
        }
    }
}
