using MineNET.Commands.Data;
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
                return "%commands.list.description";
            }
        }

        public override CommandOverload[] CommandOverloads
        {
            get
            {
                return new CommandOverload[0];
            }
        }

        public override bool OnExecute(CommandSender sender, string command, params string[] args)
        {
            Server server = Server.Instance;
            sender.SendMessage($"{server.GetPlayers().Length} / {server.ServerProperty.MaxPlayers} のプレイヤーがオンラインです");
            Player[] players = server.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                sender.SendMessage(players[i].Name);
            }
            return true;
        }
    }
}
