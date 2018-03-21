using MineNET.Utils;

namespace MineNET.Commands
{
    public sealed class ConsoleSender : CommandSender
    {
        public bool IsPlayer { get { return false; } }

        public void SendMessage(string message)
        {
            Logger.Info(message);
        }
    }
}
