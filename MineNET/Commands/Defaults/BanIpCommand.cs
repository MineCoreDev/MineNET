using System.Net;
using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Entities.Players;
using MineNET.Utils;

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
                return "commands.banip.description";
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
                    sender.SendMessage(new TranslationMessage("commands.banip.success", address.ToString()));
                    return true;
                }
                else
                {
                    sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.banip.invalid"));
                    return false;
                }
            }
            else
            {
                for (int i = 0; i < players.Length; ++i)
                {
                    players[i].BanIp = true;
                    sender.SendMessage(new TranslationMessage("commands.banip.success.players", players[i].EndPoint.Address, players[i].Name));
                }
            }
            return true;
        }
    }
}
