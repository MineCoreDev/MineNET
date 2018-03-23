using MineNET.Commands;

namespace MineNET.Events.ServerEvents
{
    public class ServerCommandEventArgs : ServerEventArgs, ICancellable
    {
        public CommandSender Sender { get; set; }

        public string Message { get; set; }

        public bool IsCancel { get; set; }

        public ServerCommandEventArgs(CommandSender sender, string message)
        {
            this.Sender = sender;
            this.Message = message;
        }
    }
}
