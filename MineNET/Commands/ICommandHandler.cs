namespace MineNET.Commands
{
    public interface ICommandHandler
    {
        void CommandHandle(CommandSender sender, string cmd, params string[] args);
    }
}
