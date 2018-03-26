using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Entities.Players;

namespace MineNET.Commands.Defaults
{
    public class TellCommand : Command
    {
        public override string Name
        {
            get
            {
                return "tell";
            }
        }

        public override string Description
        {
            get
            {
                return "1人または複数のプレイヤーにプライベート メッセージを送信する";
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new string[] { "w", "msg" };
            }
        }

        public override CommandOverload[] CommandOverloads
        {
            get
            {
                return new CommandOverload[]
                {
                    new CommandOverload(
                        new CommandParameterTarget("target", true),
                        new CommandParameterMessage("message", true)
                    )
                };
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            if (args.Length < 2)
            {
                sender.SendMessage("/tell [target] [message]");
                return false;
            }
            if (args[0] == "@e")
            {
                sender.SendMessage("セレクターはプレイヤー型にする必要があります");
                return false;
            }
            Player[] players = this.GetPlayerFromSelector(args[0], sender);
            if (players.Length < 1)
            {
                sender.SendMessage("セレクターに合う対象がいません");
                return false;
            }
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].SendMessage($"[{sender.Name}: {players[i].Name} にささやかれました: {args[1]}]");
                sender.SendMessage($"{players[i].Name} にささやきました: {args[1]}");
            }
            return true;
        }
    }
}
