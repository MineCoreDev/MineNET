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
        public GameModeCommand()
        {
            this.RemoveAllOverloads();
            this.AddOverloads(new CommandOverload(
                new CommandParameterString("gamemode", false, new CommandEnumGameMode()),
                new CommandParameterTarget("player", true)
                ));
            this.AddOverloads(new CommandOverload(
                new CommandParameterInt("gamemode", false),
                new CommandParameterTarget("player", true)
                ));
        }

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
                return "ゲームモードを変更するコマンド";
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new string[] { "gm" };
            }
        }

        public override PlayerPermissions Permission
        {
            get
            {
                //return PlayerPermissions.OPERATOR;
                return PlayerPermissions.VISITOR;
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            if (args.Length < 1)
            {
                sender.SendMessage("/gamemode [int] [target]");
                return false;
            }
            else if (args.Length < 2)
            {
                if (!sender.IsPlayer)
                {
                    sender.SendMessage("/gamemode [int] [target]");
                    return false;
                }
                int gamemode = 0;
                int.TryParse(args[0], out gamemode);
                if (gamemode < 0 || 3 < gamemode)
                {
                    sender.SendMessage("ゲームモードは0～3です");
                    return false;
                }
                Player player = (Player) sender;
                player.GameMode = GameModeExtention.FromIndex(gamemode);
                player.SendMessage("ゲームモードが変更されました");
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
                }
                if (gamemode < 0 || 3 < gamemode)
                {
                    sender.SendMessage("ゲームモードは0～3です");
                    return false;
                }
                if (args[1] == "@e")
                {
                    sender.SendMessage("セレクターはプレイヤーの型にする必要があります");
                    return false;
                }
                Player[] players = this.GetPlayerFromSelector(args[1], sender);
                if (players.Length < 1)
                {
                    sender.SendMessage("セレクターに合う対象がいません");
                    return false;
                }
                for (int i = 0; i < players.Length; ++i)
                {
                    players[i].GameMode = GameModeExtention.FromIndex(gamemode);
                    players[i].SendMessage("ゲームモードが変更されました");
                    Logger.Info($"{players[i].Name} のゲームモードが変更されました");
                    //TODO: need send message at op
                }
            }
            return true;
        }
    }
}
