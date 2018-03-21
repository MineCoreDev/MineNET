namespace MineNET.Commands
{
    public interface CommandSender
    {
        bool IsPlayer { get; }

        void SendMessage(string message);
    }
}
