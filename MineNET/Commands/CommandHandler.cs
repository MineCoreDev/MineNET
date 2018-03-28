using MineNET.Commands.Data;
using MineNET.Utils;

namespace MineNET.Commands
{
    public sealed class CommandHandler : ICommandHandler
    {
        private CommandManager manager;

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
                sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.generic.unknown", cmd));
            }
        }
    }
}
