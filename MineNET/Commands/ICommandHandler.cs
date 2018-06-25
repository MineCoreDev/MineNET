namespace MineNET.Commands
{
    public interface ICommandHandler
    {
        void OnCommandExecute(CommandData data);
    }
}
