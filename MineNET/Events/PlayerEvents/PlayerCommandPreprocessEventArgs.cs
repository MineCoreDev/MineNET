using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerCommandPreprocessEventArgs : PlayerEventArgs, ICancellable
    {
        public string Message { get; set; }

        public bool IsCancel { get; set; }

        public PlayerCommandPreprocessEventArgs(Player player, string message) : base(player)
        {
            this.Message = message;
        }
    }
}
