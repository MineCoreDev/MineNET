using MineNET.Commands.Data;
using MineNET.Commands.Enums;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities.Data;
using MineNET.Entities.Players;
using MineNET.Utils;

namespace MineNET.Commands.Defaults
{
    public class GameModeCommand : Command
    {
        public override string Name
        {
            get
            {
                return "gamemode";
            }
        }

        public override string Description
        {
            get
            {
                return "commands.gamemode.description";
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new string[] { "gm" };
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
                        new CommandParameterString("gamemode", false, new CommandEnumGameMode()),
                        new CommandParameterTarget("player", true)
                    ),
                    new CommandOverload(
                        new CommandParameterInt("gamemode", false),
                        new CommandParameterTarget("player", true)
                    )
                };
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            if (args.Length < 1)
            {
                if (!sender.IsPlayer)
                {
                    sender.SendMessage("/gamemode <int> <target>");
                }
                else
                {
                    sender.SendMessage("/gamemode <int/string> [target]");
                }
                return false;
            }
            else if (args.Length < 2)
            {
                if (!sender.IsPlayer)
                {
                    sender.SendMessage("/gamemode <int> <target>");
                    return false;
                }

                int gamemode = 0;
                if (!int.TryParse(args[0], out gamemode))
                {
                    if (args[0] == "s" || args[0] == "survival")
                    {
                        gamemode = 0;
                    }
                    else if (args[0] == "c" || args[0] == "creative")
                    {
                        gamemode = 1;
                    }
                    else if (args[0] == "a" || args[0] == "adventure")
                    {
                        gamemode = 2;
                    }
                    else
                    {
                        sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.gamemode.fail.invalid", args[0]));
                        return false;
                    }
                }

                if (gamemode < 0 || 3 < gamemode)
                {
                    sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.gamemode.fail.invalid", gamemode));
                    return false;
                }

                Player player = (Player) sender;
                player.GameMode = GameModeExtention.FromIndex(gamemode);
                player.SendMessage(new TranslationMessage("commands.gamemode.success.self", player.GameMode.GameModeToString()));
                Server.Instance.BroadcastMessageAndLoggerSend(new TranslationMessage("commands.gamemode.success.other", player.GameMode.GameModeToString(), player.Name));
            }
            else
            {
                int gamemode = 0;
                if (!int.TryParse(args[0], out gamemode))
                {
                    if (args[0] == "s" || args[0] == "survival")
                    {
                        gamemode = 0;
                    }
                    else if (args[0] == "c" || args[0] == "creative")
                    {
                        gamemode = 1;
                    }
                    else if (args[0] == "a" || args[0] == "adventure")
                    {
                        gamemode = 2;
                    }
                    else
                    {
                        sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.gamemode.fail.invalid", args[0]));
                        return false;
                    }
                }

                if (gamemode < 0 || 3 < gamemode)
                {
                    sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.gamemode.fail.invalid", gamemode));
                    return false;
                }

                if (args[1] == "@e")
                {
                    this.SendTargetNotPlayerMessage(sender);
                    return false;
                }

                Player[] players = this.GetPlayerFromSelector(args[1], sender);
                if (players == null)
                {
                    return false;
                }

                for (int i = 0; i < players.Length; ++i)
                {
                    players[i].GameMode = GameModeExtention.FromIndex(gamemode);
                    players[i].SendMessage(new TranslationMessage("commands.gamemode.success.self", players[i].GameMode.GameModeToString()));
                    Server.Instance.BroadcastMessageAndLoggerSend(new TranslationMessage("commands.gamemode.success.other", players[i].GameMode.GameModeToString(), players[i].Name));
                }

                //TODO: need send message at op
            }
            return true;
        }
    }
}