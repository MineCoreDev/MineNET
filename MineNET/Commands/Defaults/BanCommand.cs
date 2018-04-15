using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Entities.Players;

namespace MineNET.Commands.Defaults
{
    public class BanCommand : Command
    {
        public override string Name
        {
            get
            {
                return "ban";
            }
        }

        public override string Description
        {
            get
            {
                return "対象プレイヤーを接続禁止にするコマンド";
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
                    ),
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
            if (args[0] == "@e")
            {
                this.SendTargetNotPlayerMessage(sender);
                return false;
            }
            Player[] players = this.GetPlayerFromSelector(args[0], sender);
            if (players == null)
            {
                Server.Instance.BanConfig.Set(args[0], true);
                Server.Instance.BanConfig.Save();
                sender.SendMessage($"{args[0]} を接続できなくしました");
                return true;
            }
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Close("banされました"); //TODO : kick
                Server.Instance.BanConfig.Set(players[i].Name, true);
                Server.Instance.BanConfig.Save();
                sender.SendMessage($"{players[i].Name} を接続できなくしました");
            }
            return true;
        }
    }
}
