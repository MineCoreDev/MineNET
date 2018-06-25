using MineNET.Entities.Players;
using System;

namespace MineNET.Events.PlayerEvents
{
    public abstract class PlayerEventArgs : EventArgs
    {
        public Player Player { get; protected set; }
    }
}
