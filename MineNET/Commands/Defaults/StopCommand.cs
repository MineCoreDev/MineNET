using MineNET.Data;

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

        public override PlayerPermissions PermissionLevel
        {
            get
            {
                return PlayerPermissions.OPERATOR;
            }
        }

        public override bool OnExecute(CommandSender sender, params string[] args)
        {
            string msg = "";
            if (args.Length == 1)
            {
                msg = args[0];
            }

            //Server.Instance.BroadcastMessage(new TranslationMessage("commands.stop.start"));

            Server.Instance.Stop();//TODO: msg
            return true;
        }
    }
}
