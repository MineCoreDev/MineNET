using MineNET.Commands.Data;
using MineNET.Entities;
using MineNET.Utils;

namespace MineNET.Commands
{
    public sealed class CommandHandler : ICommandHandler
    {
        CommandManager manager;

        public CommandHandler(CommandManager mgr)
        {
            manager = mgr;
        }

        public void CommandHandle(CommandSender sender, string cmd, params string[] args)
        {
            Command command = manager.GetCommand(cmd);
            if (command != null)
            {
                command.Execute(sender, args);
            }
            else
            {
                if (sender is Player)
                {
                    //TODO: Send Player Message
                }
                else
                {
                    Logger.Info("%command_notFound", cmd);
                }
            }
        }
    }
}
