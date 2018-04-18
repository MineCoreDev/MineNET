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
                return "%commands.list.description";
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
                return this.EnptyCommandOverloads;
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            Server server = Server.Instance;
            sender.SendMessage($"{server.GetPlayers().Length} / {Server.ServerConfig.MaxPlayers} のプレイヤーがオンラインです");
            Player[] players = server.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                sender.SendMessage(players[i].Name);
            }
            return true;
        }
    }
}
