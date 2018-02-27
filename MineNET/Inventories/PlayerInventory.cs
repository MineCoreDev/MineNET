using MineNET.Entities;
using MineNET.Items;

namespace MineNET.Inventories
{
    public class PlayerInventory : BaseInventory
    {
        public const int SLOT_ARMOR_HEAD = 36;
        public const int SLOT_ARMOR_CHEST = 37;
        public const int SLOT_ARMOR_LEGS = 38;
        public const int SLOT_ARMOR_FEET = 39;

        public const int SLOT_WEAPON_OFFHAND = 40;

        private int mainHand = 0;

        public PlayerInventory(Player player) : base(player)
        {

        }

        public override int Size
        {
            get
            {
                return 36 + 4 + 1;
            }
        }

        public override string Name
        {
            get
            {
                return "Player";
            }
        }

        public override int Type
        {
            get
            {
                return -1;
            }
        }

        public Item GetItemMainHand()
        {
            return this.GetItem(this.mainHand);
        }

        public bool SetItemMainHand(Item item)
        {
            return this.SetItem(this.mainHand, item.Clone());
        }

        public Item GetItemOffHand()
        {
            return this.GetItem(PlayerInventory.SLOT_WEAPON_OFFHAND);
        }

        public bool SetItemOffHand(Item item)
        {
            return this.SetItem(PlayerInventory.SLOT_WEAPON_OFFHAND, item.Clone());
        }

        public Item GetHelmet()
        {
            return this.GetItem(PlayerInventory.SLOT_ARMOR_HEAD);
        }

        public bool SetHelmet(Item item)
        {
            return this.SetItem(PlayerInventory.SLOT_ARMOR_HEAD, item.Clone());
        }

        public Item GetChestPlate()
        {
            return this.GetItem(PlayerInventory.SLOT_ARMOR_CHEST);
        }

        public bool SetChestPlate(Item item)
        {
            return this.SetItem(PlayerInventory.SLOT_ARMOR_CHEST, item.Clone());
        }

        public Item GetLeggings()
        {
            return this.GetItem(PlayerInventory.SLOT_ARMOR_LEGS);
        }

        public bool SetLeggings(Item item)
        {
            return this.SetItem(PlayerInventory.SLOT_ARMOR_LEGS, item.Clone());
        }

        public Item GetBoots()
        {
            return this.GetItem(PlayerInventory.SLOT_ARMOR_FEET);
        }

        public bool SetBoots(Item item)
        {
            return this.SetItem(PlayerInventory.SLOT_ARMOR_FEET, item.Clone());
        }
    }
}
