using MineNET.Events.PlayerEvents;
using MineNET.Events.ServerEvents;

namespace MineNET.Commands
{
    public class CommandHandler : ICommandHandler
    {
        private CommandManager Manager { get; }

        public CommandHandler(CommandManager manager)
        {
            this.Manager = manager;
        }

        public void OnCommandExecute(CommandData data)
        {
            data.SplitText();

            if (data.Sender.IsPlayer)
            {
                PlayerCommandEventArgs ev = new PlayerCommandEventArgs(data);
                Server.Instance.Event.Player.OnPlayerCommand(this, ev);
                if (ev.IsCancel)
                {
                    return;
                }
            }
            else
            {
                ServerCommandEventArgs ev = new ServerCommandEventArgs(data);
                Server.Instance.Event.Server.OnServerCommand(this, ev);
                if (ev.IsCancel)
                {
                    return;
                }
            }

            Command command = this.Manager.GetCommand(data.Command);
            if (command != null)
            {
                command.OnExecute(data.Sender, data.Args);
            }
            else
            {
                //data.Sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.generic.unknown", data.Command));
            }
        }
    }
}
