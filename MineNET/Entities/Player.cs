using System.Net;
using MineNET.Commands;

namespace MineNET.Entities
{
    public class Player : Human, CommandSender
    {
        IPEndPoint endPoint;
        public IPEndPoint EndPoint
        {
            get
            {
                return endPoint;
            }

            set
            {
                endPoint = value;
            }
        }

        public void Close()
        {
            //Server.GetInstance().networkManager.mineNetServerHandler.CloseSession();
        }
    }
}
