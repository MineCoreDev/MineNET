using System.Collections.Generic;
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

        public override bool Execute(CommandSender sender, params string[] args)
        {
            Logger.Info("使えるコマンド一覧");
            Dictionary<string, Command> commandList = Server.Instance.CommandManager.CommandList;
            foreach (string name in commandList.Keys)
            {
                Command command = commandList[name];
                Logger.Info($" §2/{command.Name}§f : {command.Description}");
            }
            return true;
        }
    }
}
