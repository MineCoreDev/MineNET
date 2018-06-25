using System;

namespace MineNET.Events.PlayerEvents
{
    public sealed class PlayerEvent
    {
        public event EventHandler<PlayerCommandEventArgs> PlayerCommand;
        internal void OnPlayerCommand(object sender, PlayerCommandEventArgs e)
        {
            this.PlayerCommand?.Invoke(sender, e);
        }

        public event EventHandler<PlayerCreateEventArgs> PlayerCreate;
        internal void OnPlayerCreate(object sender, PlayerCreateEventArgs e)
        {
            this.PlayerCreate?.Invoke(sender, e);
        }
    }
}
