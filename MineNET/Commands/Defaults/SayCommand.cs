using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Utils;

namespace MineNET.Commands.Defaults
{
    public class SayCommand : Command
    {
        public override string Name
        {
            get
            {
                return "say";
            }
        }

        public override string Description
        {
            get
            {
                return "チャットで他のプレイヤーにメッセージを送信する";
            }
        }

        public override PlayerPermissions CommandPermission
        {
            get
            {
                //return PlayerPermissions.OPERATOR;
                return PlayerPermissions.VISITOR;
            }
        }

        public override CommandOverload[] CommandOverloads
        {
            get
            {
                return new CommandOverload[]
                {
                    new CommandOverload(
                        new CommandParameterMessage("message")
                    )
                };
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            if (args.Length < 1)
            {
                sender.SendMessage("/say [message]");
                return false;
            }
            Server.Instance.BroadcastMessage($"[{sender.Name}] {args[0]}");
            Logger.Info($"[{sender.Name}] {args[0]}");
            return true;
        }
    }
}
