using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerCreateDataEventArgs : PlayerEventArgs
    {
        public PlayerCreateDataEventArgs(Player player) : base(player)
        {

        }
    }
}
