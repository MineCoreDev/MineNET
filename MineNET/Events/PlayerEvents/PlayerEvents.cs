namespace MineNET.Events.PlayerEvents
{
    public class PlayerEvents : MineNETEvents
    {
        public static event EventHandler<PlayerPreLoginEventArgs> PlayerPreLogin;
        public static void OnPlayerPreLogin(PlayerPreLoginEventArgs args)
        {
            PlayerPreLogin?.Invoke(args);
        }

        public static event EventHandler<PlayerLoginEventArgs> PlayerLogin;
        public static void OnPlayerLogin(PlayerLoginEventArgs args)
        {
            PlayerLogin?.Invoke(args);
        }
    }
}
