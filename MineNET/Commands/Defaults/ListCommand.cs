using MineNET.Commands.Data;
using MineNET.Data;
using MineNET.Entities.Players;

namespace MineNET.Commands.Defaults
{
    public class ListCommand : Command
    {
        public override string Name
        {
            get
            {
                return "list";
            }
        }

        public override string Description
        {
            get
            {
                return "サーバー上のプレイヤーの一覧を表示する";
            }
        }

        public override PlayerPermissions CommandPermission
        {
            get
            {
                return PlayerPermissions.VISITOR;
            }
        }

        public override CommandOverload[] CommandOverloads
        {
            get
            {
                return new CommandOverload[]
                {
                    new CommandOverload(),
                };
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            Server server = Server.Instance;
            sender.SendMessage($"{server.GetPlayers().Length} / {20} のプレイヤーがオンラインです");
            Player[] players = server.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                sender.SendMessage(players[i].Name);
            }
            return true;
        }
    }
}
