using MineNET.Commands.Data;
using MineNET.Commands.Parameters;

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
                return "接続禁止状態を解除します";
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
                sender.SendMessage($"{args[0]} は接続禁止状態ではありませんでした");
                return false;
            }
            Server.Instance.RemoveBan(args[0]);
            sender.SendMessage($"{args[0]} の接続禁止状態を解除しました");
            return true;
        }
    }
}
