using MineNET.Commands.Data;
using MineNET.Commands.Enums;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.IO;
using MineNET.Text;
using MineNET.Utils;
using Newtonsoft.Json.Linq;

namespace MineNET.Commands.Defaults
{
    public class GiveCommand : Command
    {
        public override string Name
        {
            get
            {
                return "give";
            }
        }

        public override string Description
        {
            get
            {
                return "%commands.give.description";
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
                        new CommandParameterTarget("player", false),
                        new CommandParameterString("itemName", false, new CommandEnumItems()),
                        new CommandParameterInt("amount", true),
                        new CommandParameterInt("data", true),
                        new CommandParameterJson("dataTag", true)
                    ),
                    new CommandOverload(
                        new CommandParameterTarget("player", false),
                        new CommandParameterInt("id", false),
                        new CommandParameterInt("amount", true),
                        new CommandParameterInt("data", true),
                        new CommandParameterJson("dataTag", true)
                    )
                };
            }
        }

        public override bool OnExecute(CommandSender sender, string command, params string[] args)
        {
            if (args.Length < 2)
            {
                this.SendLengthErrorMessage(sender, command, args, args.Length);
                return false;
            }

            if (args[0] == "@e")
            {
                this.SendTargetNotPlayerMessage(sender);
                return false;
            }

            Player[] players = this.GetPlayerFromSelector(args[0], sender);
            if (players == null)
            {
                this.SendNoTargetMessage(sender);
                return false;
            }

            ItemStack item = new ItemStack(Item.Get(args[1]));
            if (args.Length > 2)
            {
                int count;
                int.TryParse(args[2], out count);
                if (count == 0)
                {
                    count = 1;
                }
                item.Count = count;
            }

            if (args.Length > 3)
            {
                int damage;
                int.TryParse(args[3], out damage);
                item.Damage = damage;
            }

            if (args.Length > 4)
            {
                string tag = args[4];
                try
                {
                    item.SetNamedTag(NBTJsonSerializer.Deserialize(JObject.Parse(tag)));
                }
                catch
                {
                    sender.SendMessage(new TranslationContainer(TextFormat.RED, "commands.give.tagError", tag));
                    return false;
                }
            }

            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Inventory.AddItem(item.Clone());
                players[i].SendMessage(new TranslationContainer("commands.give.successRecipient", item.Item.Name, item.Count));
                Server.Instance.BroadcastMessageAndLoggerSend(new TranslationContainer("commands.give.success", item.Item.Name, item.Count, players[i].Name));
            }

            return true;
        }
    }
}
