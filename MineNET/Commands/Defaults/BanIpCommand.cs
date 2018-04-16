using System.Net;
using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Entities.Players;

namespace MineNET.Commands.Defaults
{
    public class BanIpCommand : Command
    {
        public override string Name
        {
            get
            {
                return "ban-ip";
            }
        }

        public override string Description
        {
            get
            {
                return "対象IPのプレイヤーを接続禁止にするコマンド";
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
                    new CommandOverload(
                        new CommandParameterString("ip")
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
            Player[] players = this.GetPlayerFromSelector(args[0], sender);
            if (players == null)
            {
                IPAddress address = null;
                if (IPAddress.TryParse(args[0], out address))
                {
                    Server.Instance.AddBanIp(address);
                    sender.SendMessage($"{address.ToString()} を接続できなくしました");
                    return true;
                }
                else
                {
                    sender.SendMessage($"{args[0]} はIPアドレスではありません");
                    return false;
                }
            }
            else
            {
                for (int i = 0; i < players.Length; ++i)
                {
                    players[i].BanIp = true;
                    sender.SendMessage($"{players[i].Name} をIPアドレスから接続できなくしました");
                }
            }
            return true;
        }
    }
}
