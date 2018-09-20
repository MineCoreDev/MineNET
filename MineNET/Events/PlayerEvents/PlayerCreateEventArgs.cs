using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerCreateEventArgs : PlayerEventArgs
    {
        public Player CustomPlayer { get; set; }

        public PlayerCreateEventArgs(Player player) : base(player)
        {
            this.CustomPlayer = player;
        }
    }
}
