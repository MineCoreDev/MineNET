using System;

namespace MineNET.Events.PlayerEvents
{
    public sealed class PlayerEvent
    {
        public event EventHandler<PlayerBlockPickRequestEventArgs> PlayerBlockPickRequest;
        public void OnPlayerBlockPickRequest(object sender, PlayerBlockPickRequestEventArgs e)
        {
            this.PlayerBlockPickRequest?.Invoke(sender, e);
        }

        public event EventHandler<PlayerChatEventArgs> PlayerChat;
        public void OnPlayerChat(object sender, PlayerChatEventArgs e)
        {
            this.PlayerChat?.Invoke(sender, e);
        }

        public event EventHandler<PlayerCommandEventArgs> PlayerCommand;
        public void OnPlayerCommand(object sender, PlayerCommandEventArgs e)
        {
            this.PlayerCommand?.Invoke(sender, e);
        }

        public event EventHandler<PlayerCreateEventArgs> PlayerCreate;
        public void OnPlayerCreate(object sender, PlayerCreateEventArgs e)
        {
            this.PlayerCreate?.Invoke(sender, e);
        }

        public event EventHandler<PlayerEatFoodEventArgs> PlayerEatFood;
        public void OnPlayerEatFood(object sender, PlayerEatFoodEventArgs e)
        {
            this.PlayerEatFood?.Invoke(sender, e);
        }

        public event EventHandler<PlayerExhaustEventArgs> PlayerExhaust;
        public void OnPlayerExhaust(object sender, PlayerExhaustEventArgs e)
        {
            this.PlayerExhaust?.Invoke(sender, e);
        }

        public event EventHandler<PlayerInteractEventArgs> PlayerInteract;
        public void OnPlayerInteract(object sender, PlayerInteractEventArgs e)
        {
            this.PlayerInteract?.Invoke(sender, e);
        }

        public event EventHandler<PlayerItemConsumeEventArgs> PlayerItemConsume;
        public void OnPlayerItemConsume(object sender, PlayerItemConsumeEventArgs e)
        {
            this.PlayerItemConsume?.Invoke(sender, e);
        }

        public event EventHandler<PlayerItemHeldEventArgs> PlayerItemHeld;
        public void OnPlayerItemHeld(object sender, PlayerItemHeldEventArgs e)
        {
            this.PlayerItemHeld?.Invoke(sender, e);
        }

        public event EventHandler<PlayerJoinEventArgs> PlayerJoin;
        public void OnPlayerJoin(object sender, PlayerJoinEventArgs e)
        {
            this.PlayerJoin?.Invoke(sender, e);
        }

        public event EventHandler<PlayerJumpEventArgs> PlayerJump;
        public void OnPlayerJump(object sender, PlayerJumpEventArgs e)
        {
            this.PlayerJump?.Invoke(sender, e);
        }

        public event EventHandler<PlayerLoginEventArgs> PlayerLogin;
        public void OnPlayerLogin(object sender, PlayerLoginEventArgs e)
        {
            this.PlayerLogin?.Invoke(sender, e);
        }

        public event EventHandler<PlayerPreLoginEventArgs> PlayerPreLogin;
        public void OnPlayerPreLogin(object sender, PlayerPreLoginEventArgs e)
        {
            this.PlayerPreLogin?.Invoke(sender, e);
        }

        public event EventHandler<PlayerQuitEventArgs> PlayerQuit;
        public void OnPlayerQuit(object sender, PlayerQuitEventArgs e)
        {
            this.PlayerQuit?.Invoke(sender, e);
        }

        public event EventHandler<PlayerSkinChangeEventArgs> PlayerSkinChange;
        public void OnPlayerSkinChange(object sender, PlayerSkinChangeEventArgs e)
        {
            this.PlayerSkinChange?.Invoke(sender, e);
        }

        public event EventHandler<PlayerToggleGlideEventArgs> PlayerToggleGlide;
        public void OnPlayerToggleGlide(object sender, PlayerToggleGlideEventArgs e)
        {
            this.PlayerToggleGlide?.Invoke(sender, e);
        }

        public event EventHandler<PlayerToggleSneakEventArgs> PlayerToggleSneak;
        public void OnPlayerToggleSneak(object sender, PlayerToggleSneakEventArgs e)
        {
            this.PlayerToggleSneak?.Invoke(sender, e);
        }

        public event EventHandler<PlayerToggleSprintEventArgs> PlayerToggleSprint;
        public void OnPlayerToggleSprint(object sender, PlayerToggleSprintEventArgs e)
        {
            this.PlayerToggleSprint?.Invoke(sender, e);
        }
    }
}
