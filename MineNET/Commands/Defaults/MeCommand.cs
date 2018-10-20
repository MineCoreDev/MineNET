using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.IO;

namespace MineNET.Commands.Defaults
{
    public class MeCommand : Command
    {
        public override string Name
        {
            get
            {
                return "me";
            }
        }

        public override string Description
        {
            get
            {
                return "%commands.me.description";
            }
        }

        public override CommandOverload[] CommandOverloads
        {
            get
            {
                return new CommandOverload[]
                {
                    new CommandOverload(
                        new CommandParameterMessage("message", false)
                    )
                };
            }
        }

        public override bool OnExecute(CommandSender sender, params string[] args)
        {
            if (args.Length < 1)
            {
                sender.SendMessage("/me <message>");
                return false;
            }

            string msg = $"* {sender.Name} {args[0]}";
            Server.Instance.BroadcastMessage(msg);
            Logger.Info(msg);
            return true;
        }
    }
}
