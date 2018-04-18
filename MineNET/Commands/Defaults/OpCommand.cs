using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Utils;

namespace MineNET.Commands.Defaults
{
    public class OpCommand : Command
    {
        public override string Name
        {
            get
            {
                return "op";
            }
        }

        public override string Description
        {
            get
            {
                return "%commands.op.description";
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
                Server.Instance.AddOp(args[0]);
                sender.SendMessage(new TranslationMessage("commands.op.success", args[0]));
                return true;
            }
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Op = true;
                sender.SendMessage(new TranslationMessage("commands.op.success", players[i].Name));
            }
            return true;
        }
    }
}
