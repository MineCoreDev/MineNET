namespace MineNET.Events.PlayerEvents
{
    public class PlayerEvents : MineNETEvents
    {
        public static event EventHandler<PlayerChatEventArgs> PlayerChat;
        public static void OnPlayerChat(PlayerChatEventArgs args)
        {
            PlayerChat?.Invoke(args);
        }

        public static event EventHandler<PlayerCommandPreprocessEventArgs> PlayerCommandPreprocess;
        public static void OnPlayerCommandPreprocess(PlayerCommandPreprocessEventArgs args)
        {
            PlayerCommandPreprocess?.Invoke(args);
        }

        public static event EventHandler<PlayerCreateDataEventArgs> PlayerCreateData;
        public static void OnPlayerCreateData(PlayerCreateDataEventArgs args)
        {
            PlayerCreateData?.Invoke(args);
        }

        public static event EventHandler<PlayerEatFoodEvent> PlayerEatFood;
        public static void OnPlayerEatFood(PlayerEatFoodEvent args)
        {
            PlayerEatFood?.Invoke(args);
        }

        public static event EventHandler<PlayerExhaustEventArgs> PlayerExhaust;
        public static void OnPlayerExhaust(PlayerExhaustEventArgs args)
        {
            PlayerExhaust?.Invoke(args);
        }

        public static event EventHandler<PlayerInteractEventArgs> PlayerInteract;
        public static void OnPlayerInteract(PlayerInteractEventArgs args)
        {
            PlayerInteract?.Invoke(args);
        }

        public static event EventHandler<PlayerItemConsumeableEventArgs> PlayerItemConsumeable;
        public static void OnPlayerItemConsumeable(PlayerItemConsumeableEventArgs args)
        {
            PlayerItemConsumeable?.Invoke(args);
        }

        public static event EventHandler<PlayerJoinEventArgs> PlayerJoin;
        public static void OnPlayerJoin(PlayerJoinEventArgs args)
        {
            PlayerJoin?.Invoke(args);
        }

        public static event EventHandler<PlayerJumpEventArgs> PlayerJump;
        public static void OnPlayerJump(PlayerJumpEventArgs args)
        {
            PlayerJump?.Invoke(args);
        }

        public static event EventHandler<PlayerLoginEventArgs> PlayerLogin;
        public static void OnPlayerLogin(PlayerLoginEventArgs args)
        {
            PlayerLogin?.Invoke(args);
        }

        public static event EventHandler<PlayerPreLoginEventArgs> PlayerPreLogin;
        public static void OnPlayerPreLogin(PlayerPreLoginEventArgs args)
        {
            PlayerPreLogin?.Invoke(args);
        }

        public static event EventHandler<PlayerQuitEventArgs> PlayerQuit;
        public static void OnPlayerQuit(PlayerQuitEventArgs args)
        {
            PlayerQuit?.Invoke(args);
        }

        public static event EventHandler<PlayerToggleGlideEventArgs> PlayerToggleGlide;
        public static void OnPlayerToggleGlide(PlayerToggleGlideEventArgs args)
        {
            PlayerToggleGlide?.Invoke(args);
        }

        public static event EventHandler<PlayerToggleSneakEventArgs> PlayerToggleSneak;
        public static void OnPlayerToggleSneak(PlayerToggleSneakEventArgs args)
        {
            PlayerToggleSneak?.Invoke(args);
        }

        public static event EventHandler<PlayerToggleSprintEventArgs> PlayerToggleSprint;
        public static void OnPlayerToggleSprint(PlayerToggleSprintEventArgs args)
        {
            PlayerToggleSprint?.Invoke(args);
        }
    }
}
