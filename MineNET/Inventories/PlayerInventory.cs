using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Network.Packets.Data;

namespace MineNET.Inventories
{
    public class PlayerInventory : BaseInventory
    {
        private int mainHand = 0;

        private PlayerCursorInventory cursor;
        private PlayerOffhandInventory offhand;
        private ArmorInventory armor;

        private Inventory other;

        public PlayerInventory(Player player) : base(player)
        {
            this.cursor = new PlayerCursorInventory(player);
            this.offhand = new PlayerOffhandInventory(player);
            this.armor = new ArmorInventory(player);
        }

        public override int Size
        {
            get
            {
                return 36;
            }
        }

        public override byte Type
        {
            get
            {
                return ContainerIds.INVENTORY.GetIndex();
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
            return this.offhand.GetItem();
        }

        public bool SetItemOffHand(Item item)
        {
            return this.offhand.SetItem(item);
        }

        public Item GetHelmet()
        {
            return this.armor.GetHelmet();
        }

        public bool SetHelmet(Item item)
        {
            return this.armor.SetHelmet(item);
        }

        public Item GetChestPlate()
        {
            return this.armor.GetChestPlate();
        }

        public bool SetChestPlate(Item item)
        {
            return this.armor.SetChestPlate(item);
        }

        public Item GetLeggings()
        {
            return this.armor.GetLeggings();
        }

        public bool SetLeggings(Item item)
        {
            return this.armor.SetLeggings(item);
        }

        public Item GetBoots()
        {
            return this.armor.GetBoots();
        }

        public bool SetBoots(Item item)
        {
            return this.armor.SetBoots(item);
        }

        public void OpenInventory(Inventory inventory)
        {
            this.other = inventory;
        }

        public void CloseInventory()
        {
            this.other = null;
        }

        public Inventory GetInventory(byte id)
        {
            if (id == this.Type)
            {
                return this;
            }
            else if (id == this.cursor.Type)
            {
                return this.cursor;
            }
            else if (id == this.offhand.Type)
            {
                return this.offhand;
            }
            else if (id == this.armor.Type)
            {
                return this.armor;
            }
            else if (id == this.other.Type)
            {
                return this.other;
            }
            else
            {
                return null;
            }
        }

        public PlayerCursorInventory GetPlayerCursorInventory()
        {
            return this.cursor;
        }

        public PlayerOffhandInventory GetPlayerOffhandInventory()
        {
            return this.offhand;
        }

        public ArmorInventory GetArmorInventory()
        {
            return this.armor;
        }
    }
}
