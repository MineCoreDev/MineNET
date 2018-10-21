using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Text;

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
                return "%commands.xp.description";
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
                    )/*,
                    new CommandOverload(
                        new CommandParameterInt("amount", false, "l"),
                        new CommandParameterTarget("player", true)
                    )*/
                };
            }
        }

        public override bool OnExecute(CommandSender sender, string command, params string[] args)
        {
            if (args.Length < 1)
            {
                this.SendLengthErrorMessage(sender, command, args, args.Length);
                return false;
            }

            bool isLevel = args[0].EndsWith("L");
            args[0] = args[0].Trim('L');
            int amount = 0;
            if (!int.TryParse(args[0], out amount))
            {
                this.SendLengthErrorMessage(sender, command, args, 0);
                return false;
            }

            if (!isLevel && amount < 0)
            {
                sender.SendMessage(new TranslationContainer("commands.xp.failure.widthdrawXp"));
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
                    this.SendLengthErrorMessage(sender, command, args, 1);
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
                        sender.SendMessage(new TranslationContainer("commands.xp.success.levels", amount, targets[i].Name));
                    }
                    else
                    {
                        sender.SendMessage(new TranslationContainer("commands.xp.success.negative.levels", amount, targets[i].Name));
                    }
                }
                else
                {
                    targets[i].AddXp(amount);
                    sender.SendMessage(new TranslationContainer("commands.xp.success", amount, targets[i].Name));
                }
            }
            return true;
        }
    }
}
