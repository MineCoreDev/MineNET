using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerJumpEventArgs : PlayerEventArgs
    {
        public PlayerJumpEventArgs(Player player) : base(player)
        {

        }
    }
}
