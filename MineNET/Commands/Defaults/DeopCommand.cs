using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Utils;

namespace MineNET.Commands.Defaults
{
    public class DeopCommand : Command
    {
        public override string Name
        {
            get
            {
                return "deop";
            }
        }

        public override string Description
        {
            get
            {
                return "%commands.deop.description";
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
                    new CommandOverload(
                        new CommandParameterTarget("player")
                    )
                };
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            if (args.Length < 1)
            {
                this.SendSyntaxMessage(sender);
                return false;
            }
            Player[] players = this.GetPlayerFromSelector(args[0], sender);
            if (players == null)
            {
                Server.Instance.RemoveOp(args[0]);
                sender.SendMessage(new TranslationMessage("commands.deop.success", args[0]));
                return true;
            }
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Op = false;
                sender.SendMessage(new TranslationMessage("commands.deop.success", players[i].Name));
            }
            return true;
        }
    }
}
