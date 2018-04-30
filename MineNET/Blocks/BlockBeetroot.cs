using System;
using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockBeetroot : BlockTransparent
    {
        public BlockBeetroot() : base(BlockFactory.BEETROOT)
        {

        }

        public override string Name
        {
            get
            {
                return "Beetroot";
            }
        }

        public override Item Item
        {
            get
            {
                return Item.Get(ItemFactory.BEETROOT_SEEDS);
            }
        }

        public override Item[] GetDrops(Item item)
        {
            if (this.Damage >= 0x07)
            {
                return new Item[]
                {
                    Item.Get(ItemFactory.BEETROOT, 0, 1),
                    Item.Get(ItemFactory.BEETROOT_SEEDS, 0, new Random().Next(3))
                };
            }
            return new Item[] { Item.Get(ItemFactory.BEETROOT_SEEDS, 0, 1) };
        }
    }
}
