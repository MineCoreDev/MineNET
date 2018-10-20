using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities.Players;

namespace MineNET.Commands.Defaults
{
    public class XpCommand : Command
    {
        public override string Name
        {
            get
            {
                return "xp";
            }
        }

        public override string Description
        {
            get
            {
                return "プレイヤーの経験値を増加/減少させます";
            }
        }

        public override PlayerPermissions PermissionLevel
        {
            get
            {
                return PlayerPermissions.OPERATOR;
            }
        }

        public override CommandOverload[] CommandOverloads
        {
            get
            {
                return new CommandOverload[]
                {
                    new CommandOverload(
                        new CommandParameterInt("amount", false),
                        new CommandParameterTarget("player", true)
                    ),
                    new CommandOverload(
                        new CommandParameterInt("amount", false, "l"),
                        new CommandParameterTarget("player", true)
                    )
                };
            }
        }

        public override bool OnExecute(CommandSender sender, params string[] args)
        {
            if (args.Length < 1)
            {
                sender.SendMessage("/xp [amount] [target]");
                return false;
            }

            bool isLevel = args[0].EndsWith("L");
            args[0] = args[0].Trim('L');
            int amount = 0;
            if (!int.TryParse(args[0], out amount))
            {
                sender.SendMessage("/xp [amount] [target]");
                return false;
            }

            if (!isLevel && amount < 0)
            {
                sender.SendMessage("プレイヤーに負の経験値を与えることはできません");
                return false;
            }

            Player[] targets;
            if (args.Length < 2)
            {
                if (sender.IsPlayer)
                {
                    targets = new Player[] { (Player) sender };
                }
                else
                {
                    sender.SendMessage("/xp [amount] [target]");
                    return false;
                }
            }
            else
            {
                targets = this.GetPlayerFromSelector(args[1], sender);
            }

            for (int i = 0; i < targets.Length; ++i)
            {
                if (isLevel)
                {
                    targets[i].SetXpLevel(targets[i].GetXpLevel() + amount);
                    if (amount > 0)
                    {
                        sender.SendMessage($"{targets[i].Name} に {amount} レベルを付与しました");
                    }
                    else
                    {
                        sender.SendMessage($"{targets[i].Name} から {amount} レベルを剥奪しました");
                    }
                }
                else
                {
                    targets[i].AddXp(amount);
                    sender.SendMessage($"{targets[i].Name} に経験値 {amount} を付与しました");
                }
            }
            return true;
        }
    }
}
