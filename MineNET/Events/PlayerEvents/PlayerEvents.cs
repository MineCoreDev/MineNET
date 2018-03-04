namespace MineNET.Events.PlayerEvents
{
    public class PlayerEvents : MineNETEvents
    {
        public static event EventHandler<PlayerPreLoginEventArgs> PlayerPreLogin;
        public static void OnPlayerPreLogin(PlayerPreLoginEventArgs args)
        {
            PlayerPreLogin?.Invoke(args);
        }
    }
}
