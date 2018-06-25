using MineNET.Commands;

namespace MineNET.Events.ServerEvents
{
    public class ServerCommandEventArgs : ServerEventArgs, ICancelable
    {
        public CommandData CommandData { get; }
        public bool IsCancel { get; set; }

        public ServerCommandEventArgs(CommandData command)
        {
            this.Server = Server.Instance;
            this.CommandData = command;
        }
    }
}
