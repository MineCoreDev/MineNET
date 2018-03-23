using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerChatEventArgs : PlayerEventArgs, ICancellable
    {
        public string Message { get; set; }

        public bool IsCancel { get; set; }

        public PlayerChatEventArgs(Player player, string message) : base(player)
        {
            this.Message = message;
        }
    }
}
