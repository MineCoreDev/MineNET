using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Utils;

namespace MineNET.Commands.Defaults
{
    public class StopCommand : Command
    {
        public override string Name
        {
            get
            {
                return "stop";
            }
        }

        public override string Description
        {
            get
            {
                return "%commands.stop.description";
            }
        }

        public override PlayerPermissions CommandPermission
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
                    new CommandOverload(),
                    new CommandOverload(
                        new CommandParameterString("stopReason", true)
                    )
                };
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            string msg = "";
            if (args.Length == 1)
            {
                msg = args[0];
            }

            Server.Instance.BroadcastMessage(new TranslationMessage("commands.stop.start"));

            Server.Instance.Stop(msg);
            return true;
        }
    }
}
