using System;
using MineNET.Utils;

namespace MineNET
{
    public class ServerException : Exception
    {
        public ServerException() : base(LangManager.GetString("sever_exception"))
        {
        }

        public ServerException(string message) : base(message)
        {
        }

        public ServerException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
