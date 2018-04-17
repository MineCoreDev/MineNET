using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Utils;

namespace MineNET.Commands.Defaults
{
    public class PardonIpCommand : Command
    {
        public override string Name
        {
            get
            {
                return "pardon-ip";
            }
        }

        public override string Description
        {
            get
            {
                return "IPから接続禁止状態を解除します。";
            }
        }

        public override CommandOverload[] CommandOverloads
        {
            get
            {
                return new CommandOverload[]
                {
                    new CommandOverload(
                        new CommandParameterString("ip")
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
            if (!Server.Instance.IsBanIp(args[0]))
            {
                sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.unbanip.invalid"));
                return false;
            }
            Server.Instance.RemoveBanIp(args[0]);
            sender.SendMessage(new TranslationMessage("commands.unbanip.success", args[0]));
            return true;
        }
    }
}
