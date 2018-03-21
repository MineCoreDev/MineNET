using System.Collections.Generic;
using MineNET.Data;
using MineNET.Utils;

namespace MineNET.Commands.Defaults
{
    public class HelpCommand : Command
    {
        public override string Name
        {
            get
            {
                return "help";
            }
        }

        public override string Description
        {
            get
            {
                return "コマンド一覧を表示するコマンド";
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new string[] { "?" };
            }
        }

        public override PlayerPermissions Permission
        {
            get
            {
                return PlayerPermissions.VISITOR;
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            Logger.Info("使えるコマンド一覧");
            Dictionary<string, Command> commandList = Server.Instance.CommandManager.CommandList;

            List<KeyValuePair<string, Command>> list = new List<KeyValuePair<string, Command>>(commandList);
            list.Sort((a, b) => a.Key.CompareTo(b.Key));
            for (int i = 0; i < list.Count; ++i)
            {
                KeyValuePair<string, Command> Data = list[i];
                Command command = Data.Value;
                Logger.Info($" §2/{command.Name}§f : {command.Description}");

            }
            return true;
        }
    }
}
