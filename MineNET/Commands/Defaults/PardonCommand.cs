using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Utils;

namespace MineNET.Commands.Defaults
{
    public class PardonCommand : Command
    {
        public override string Name
        {
            get
            {
                return "pardon";
            }
        }

        public override string Description
        {
            get
            {
                return "接続禁止状態を解除します。";
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
            if (!Server.Instance.IsBan(args[0]))
            {
                sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.unban.failed", args[0]));
                return false;
            }
            Server.Instance.RemoveBan(args[0]);
            sender.SendMessage(new TranslationMessage("commands.unban.success", args[0]));
            return true;
        }
    }
}
