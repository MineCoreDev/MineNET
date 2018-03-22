namespace MineNET.Commands
{
    public interface CommandSender
    {
        bool IsPlayer { get; }

        string Name { get; }

        void SendMessage(string message);
    }
}
