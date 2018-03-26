using MineNET.Commands.Data;
using MineNET.Commands.Enums;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;

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
                return "プレイヤーにアイテムを与える";
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
                        new CommandParameterTarget("player", true),
                        new CommandParameterString("itemName", true, new CommandEnumItems()),
                        new CommandParameterInt("amount", false),
                        new CommandParameterInt("data", false)
                        //new CommandParameterJson("components")
                    ),
                    new CommandOverload(
                        new CommandParameterTarget("player", true),
                        new CommandParameterInt("id", true),
                        new CommandParameterInt("amount", false),
                        new CommandParameterInt("data", false)
                        //new CommandParameterJson("components")
                    )
                };
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            if (args.Length < 2)
            {
                sender.SendMessage("/give [target] [item] ....");
                return false;
            }
            if (args[0] == "@e")
            {
                sender.SendMessage("セレクターはプレイヤー型にする必要があります");
                return false;
            }
            Player[] players = this.GetPlayerFromSelector(args[0], sender);
            if (players.Length < 1)
            {
                sender.SendMessage("セレクターに合う対象がいません");
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
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Inventory.AddItem(item.Clone());
                players[i].SendMessage($"{item.Name} を {item.Count} 個受け取りました");
                sender.SendMessage($"{players[i].Name} に {item.Name} を {item.Count} 個与えました");
            }
            return true;
        }
    }
}
