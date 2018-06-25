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

        public void SendMessage(TranslationMessage message)
        {
            string msg = $"%{message.TranslationKey}";
            if (message.TranslationFills == null)
            {
                OutLog.Info(msg);
            }
            else
            {
                OutLog.Info(msg, message.TranslationFills);
            }
        }

        public void SendMessage(string message)
        {
            OutLog.Info(message);
        }

        public void SendMessage(string message, params object[] args)
        {
            OutLog.Info(message, args);
        }
    }
}
