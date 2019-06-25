using System.Net;
using System.Threading;
using MineNET.Network.RakNet;
using NUnit.Framework;

namespace MineNET.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            RakNetServer server = new RakNetServer();
            server.Start(new IPEndPoint(IPAddress.Any, 19132));

            Thread.Sleep(1000);

            server.Stop();
        }
    }
}