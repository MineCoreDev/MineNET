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
                return "%commands.help.description";
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new string[] { "?" };
            }
        }

        public override PlayerPermissions CommandPermission
        {
            get
            {
                return PlayerPermissions.VISITOR;
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            if (!sender.IsPlayer)
            {
                Logger.Info("%commands.help.header", 1, 1);
                Dictionary<string, Command> commandList = Server.Instance.CommandManager.CommandList;

                List<KeyValuePair<string, Command>> list = new List<KeyValuePair<string, Command>>(commandList);
                list.Sort((a, b) => a.Key.CompareTo(b.Key));
                for (int i = 0; i < list.Count; ++i)
                {
                    KeyValuePair<string, Command> Data = list[i];
                    Command command = Data.Value;
                    if (!string.IsNullOrEmpty(command.Description))
                    {
                        if (command.Description[0] == '%')
                        {
                            Logger.Info($" §2/{command.Name}§f : {this.GetTranslationDescription(command.Description.Remove(0, 1))}");
                        }
                        else
                        {
                            Logger.Info($" §2/{command.Name}§f : {command.Description}");
                        }
                    }
                    else
                    {
                        Logger.Info($" §2/{command.Name}§f : ");
                    }
                }
            }

            return true;
        }
    }
}
