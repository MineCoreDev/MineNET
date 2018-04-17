using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Entities.Players;

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
                return "プレイヤーにオペレーターのステータスを与える。";
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
                sender.SendMessage($"{args[0]} にオペレーターの権限を与えました");
                return true;
            }
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Op = true;
                sender.SendMessage($"{players[i].Name} にオペレーターの権限を与えました");
            }
            return true;
        }
    }
}
