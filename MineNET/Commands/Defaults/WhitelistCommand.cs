using System.Collections.Generic;
using System.Linq;
using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities.Players;

namespace MineNET.Commands.Defaults
{
    public class WhitelistCommand : Command
    {
        public override string Name
        {
            get
            {
                return "whitelist";
            }
        }

        public override string Description
        {
            get
            {
                return "ホワイトリスト";
            }
        }

        public override PlayerPermissions CommandPermission
        {
            get
            {
                return PlayerPermissions.VISITOR; //TODO
            }
        }

        public override CommandOverload[] CommandOverloads
        {
            get
            {
                return new CommandOverload[]
                {
                    new CommandOverload(
                        new CommandParameterValueEnum("on")
                    ),
                    new CommandOverload(
                        new CommandParameterValueEnum("off")
                    ),
                    new CommandOverload(
                        new CommandParameterValueEnum("list")
                    ),
                    new CommandOverload(
                        new CommandParameterValueEnum("add"),
                        new CommandParameterTarget("player")
                    ),
                    new CommandOverload(
                        new CommandParameterValueEnum("remove"),
                        new CommandParameterTarget("player")
                    ),
                    new CommandOverload(
                        new CommandParameterValueEnum("reload")
                    ),
                };
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            if (args.Length < 1)
            {
                sender.SendMessage("/whitelist ...");
                return false;
            }
            if (args[0] == "on")
            {
                Server.Instance.SetWhitelist(true);
                sender.SendMessage("ホワリスをオンにした");
                return true;
            }
            else if (args[0] == "off")
            {
                Server.Instance.SetWhitelist(false);
                sender.SendMessage("ホワリスをオフにした");
                return true;
            }
            else if (args[0] == "list")
            {
                Dictionary<string, object> whitelists = Server.Instance.WhitelistConfig.GetAll();
                string[] keys = whitelists.Keys.ToArray();
                for (int i = 0; i < keys.Length; ++i)
                {
                    sender.SendMessage(keys[i]);
                }
                return true;
            }
            else if (args[0] == "add")
            {
                if (args.Length > 1)
                {
                    Player[] players = this.GetPlayerFromSelector(args[1], sender);
                    if (players == null)
                    {
                        return false;
                    }
                    for (int i = 0; i < players.Length; ++i)
                    {
                        Server.Instance.AddWhitelist(players[i]);
                    }
                    return true;
                }
                sender.SendMessage("/whitelist add [Name]");
                return false;
            }
            else if (args[0] == "remove")
            {
                if (args.Length > 1)
                {
                    Player[] players = this.GetPlayerFromSelector(args[1], sender);
                    if (players == null)
                    {
                        return false;
                    }
                    for (int i = 0; i < players.Length; ++i)
                    {
                        Server.Instance.RemoveWhitelist(players[i]);
                    }
                    return true;
                }
                sender.SendMessage("/whitelist remove [Name]");
                return false;
            }
            else if (args[0] == "reload")
            {
                //TODO
                return true;
            }
            sender.SendMessage("/whitelist ...");
            return false;
        }
    }
}
