using System;
using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public abstract class PlayerEventArgs : EventArgs
    {
        public Player Player { get; protected set; }

        public PlayerEventArgs(Player player)
        {
            this.Player = player;
        }
    }
}
