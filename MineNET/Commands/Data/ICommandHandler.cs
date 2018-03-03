namespace MineNET.Commands.Data
{
    public interface ICommandHandler
    {
        void CommandHandle(CommandSender sender, string cmd, params string[] args);
    }
}
