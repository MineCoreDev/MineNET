using MineNET.Text;

namespace MineNET.Commands
{
    public interface CommandSender
    {
        bool IsPlayer { get; }

        string Name { get; }

        void SendMessage(string message);
        void SendMessage(TranslationContainer message);
        void SendMessage(string message, params object[] args);
    }
}