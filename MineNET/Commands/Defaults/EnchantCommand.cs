using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Entities;
using MineNET.Items;
using MineNET.Items.Enchantments;
using MineNET.Utils;

namespace MineNET.Commands.Defaults
{
    public class EnchantCommand : Command
    {
        public override string Name
        {
            get
            {
                return "enchant";
            }
        }

        public override string Description
        {
            get
            {
                return "commands.enchant.description";
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
                        new CommandParameterString("enchantmentName", false),
                        new CommandParameterInt("enchantmentId", true)
                    ),
                    new CommandOverload(
                        new CommandParameterTarget("player", false),
                        new CommandParameterInt("enchantmentId", false),
                        new CommandParameterInt("level", true)
                    )
                };
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            if (args.Length < 2)
            {
                this.SendSyntaxMessage(sender);
                return false;
            }

            Entity[] entities = this.GetEntityFromSelector(args[0], sender);
            if (entities == null)
            {
                this.SendNoTargetMessage(sender);
                return false;
            }

            int level = 1;
            if (args.Length > 2)
            {
                if (!int.TryParse(args[2], out level))
                {
                    level = 1;
                }
            }
            for (int i = 0; i < entities.Length; ++i)
            {
                if (entities[i] is EntityLiving)
                {
                    EntityLiving entity = (EntityLiving) entities[i];
                    Enchantment enchant = Enchantment.GetEnchantment(args[1], level);
                    if (enchant.MinLevel < level || level > enchant.MaxLevel)
                    {
                        sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.enchant.invalidLevel", enchant.Name, level));
                        continue;
                    }
                    Item item = entity.Inventory.MainHandItem;
                    if (item.IsTool)
                    {
                        item.AddEnchant(enchant);
                        entity.Inventory.MainHandItem = item;
                        sender.SendMessage(new TranslationMessage("commands.enchant.success", entity.Name));
                    }
                    else
                    {
                        sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.enchant.cantEnchant", entity.Name));
                    }
                }
            }
            return true;
        }
    }
}
