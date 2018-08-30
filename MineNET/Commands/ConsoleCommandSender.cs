using MineNET.IO;
using MineNET.Text;

namespace MineNET.Commands
{
    public class ConsoleCommandSender : CommandSender
    {
        public bool IsPlayer
        {
            get
            {
                return false;
            }
        }

        public string Name
        {
            get
            {
                return "Server";
            }
        }

        public void SendMessage(TranslationContainer message)
        {
            string msg = $"%{message.Key}";
            if (message.Args == null)
            {
                Logger.Info(msg);
            }
            else
            {
                Logger.Info(msg, message.Args);
            }
        }

        public void SendMessage(string message)
        {
            Logger.Info(message);
        }

        public void SendMessage(string message, params object[] args)
        {
            Logger.Info(message, args);
        }
    }
}
