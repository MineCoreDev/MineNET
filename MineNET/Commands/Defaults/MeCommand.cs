using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Utils;

namespace MineNET.Commands.Defaults
{
    public class MeCommand : Command
    {
        public MeCommand()
        {
            this.RemoveAllOverloads();
            this.AddOverloads(new CommandOverload(new CommandParameterMessage("message", false)));
        }

        public override string Name
        {
            get
            {
                return "me";
            }
        }

        public override string Description
        {
            get
            {
                return "あなたに関するメッセージを表示します";
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            if (args.Length < 1)
            {
                sender.SendMessage("/me [message]");
                return false;
            }
            Server.Instance.BroadcastMessage($"* {sender.Name} {args[0]}");
            Logger.Info($"* {sender.Name} {args[0]}");
            return true;
        }
    }
}
