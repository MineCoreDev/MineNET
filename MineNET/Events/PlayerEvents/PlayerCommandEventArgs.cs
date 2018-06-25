using MineNET.Commands;
using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerCommandEventArgs : PlayerEventArgs, ICancelable
    {
        public CommandData CommandData { get; set; }
        public bool IsCancel { get; set; }

        public PlayerCommandEventArgs(CommandData command)
        {
            this.Player = (Player) command.Sender;
            this.CommandData = command;
        }
    }
}
