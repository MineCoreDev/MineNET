using MineNET.Commands.Data;
using MineNET.Commands.Enums;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.IO;
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

        public override PlayerPermissions CommandPermission
        {
            get
            {
                //return PlayerPermissions.OPERATOR
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

        public override bool Execute(CommandSender sender, params string[] args)
        {
            if (args.Length < 2)
            {
                sender.SendMessage("/give <target> <itemName/id> ...");
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

            Item item = Item.Get(args[1]);
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
                    sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.give.tagError", tag));
                    return false;
                }
            }

            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Inventory.AddItem(item.Clone());
                sender.SendMessage(new TranslationMessage("commands.give.successRecipient", item.Name, item.Count));
                Server.Instance.BroadcastMessageAndLoggerSend(new TranslationMessage("commands.give.success", item.Name, item.Count, players[i].Name));
            }

            return true;
        }
    }
}
