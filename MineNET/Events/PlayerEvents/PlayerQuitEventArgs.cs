using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerQuitEventArgs : PlayerEventArgs
    {
        public string QuitMessage { get; set; }
        public string Reason { get; set; }

        public PlayerQuitEventArgs(Player player, string quitMessage, string reason) : base(player)
        {
            this.QuitMessage = quitMessage;
            this.Reason = reason;
        }
    }
}
