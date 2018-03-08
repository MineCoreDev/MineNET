using System;
using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerEventArgs : EventArgs
    {
        public Player Player { get; }

        public PlayerEventArgs(Player player)
        {
            this.Player = player;
        }
    }
}
