using MineNET.Utils;

namespace MineNET.Commands
{
    public sealed class ConsoleSender : CommandSender
    {
        public bool IsPlayer { get { return false; } }

        public string Name
        {
            get
            {
                return "Server";
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

        public void SendMessage(TranslationMessage message)
        {
            string msg = $"%{message.TranslationKey}";
            if (message.TranslationFills == null)
            {
                Logger.Info(msg);
            }
            else
            {
                Logger.Info(msg, message.TranslationFills);
            }
        }
    }
}
