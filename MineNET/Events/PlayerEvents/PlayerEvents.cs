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

        public static event EventHandler<PlayerJoinEventArgs> PlayerJoin;
        public static void OnPlayerJoin(PlayerJoinEventArgs args)
        {
            PlayerJoin?.Invoke(args);
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
    }
}
