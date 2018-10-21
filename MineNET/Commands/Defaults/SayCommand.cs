using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.IO;

namespace MineNET.Commands.Defaults
{
    public class SayCommand : Command
    {
        public override string Name
        {
            get
            {
                return "say";
            }
        }

        public override string Description
        {
            get
            {
                return "%commands.say.description";
            }
        }

        public override PlayerPermissions PermissionLevel
        {
            get
            {
                return PlayerPermissions.OPERATOR;
            }
        }

        public override CommandOverload[] CommandOverloads
        {
            get
            {
                return new CommandOverload[]
                {
                    new CommandOverload(
                        new CommandParameterMessage("message")
                    )
                };
            }
        }

        public override bool OnExecute(CommandSender sender, string command, params string[] args)
        {
            if (args.Length < 1)
            {
                this.SendLengthErrorMessage(sender, command, args, args.Length);
                return false;
            }

            string msg = $"[{sender.Name}] {args[0]}";
            Server.Instance.BroadcastMessage(msg);
            Logger.Info(msg);
            return true;
        }
    }
}
