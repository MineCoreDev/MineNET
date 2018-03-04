using System;
using MineNET.Entities;

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
