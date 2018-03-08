using MineNET.Commands.Data;
using MineNET.Entities.Players;
using MineNET.Utils;

namespace MineNET.Commands
{
    public sealed class CommandHandler : ICommandHandler
    {
        CommandManager manager;

        public CommandHandler(CommandManager mgr)
        {
            this.manager = mgr;
        }

        public void CommandHandle(CommandSender sender, string cmd, params string[] args)
        {
            Command command = this.manager.GetCommand(cmd);
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
