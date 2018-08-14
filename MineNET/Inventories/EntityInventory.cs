using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Network.MinecraftPackets;

namespace MineNET.Inventories
{
    public class EntityInventory : BaseInventory
    {
        private int slot = 1;

        private int mainHand = 0;

        private EntityArmorInventory armor;
        private EntityOffhandInventory offhand;

        public EntityInventory(EntityLiving holder, int slot) : base(holder)
        {
            if (!holder.NamedTag.Exist("Mainhand"))
            {
                holder.NamedTag.PutInt("Mainhand", 0);
            }
            this.mainHand = holder.NamedTag.GetInt("Mainhand");

            this.armor = new EntityArmorInventory(holder);
            this.offhand = new EntityOffhandInventory(holder);

            this.slot = slot;
        }

        public override int Size
        {
            get
            {
                return this.slot;
            }
        }

        public override byte Type
        {
            get
            {
                return ContainerIds.INVENTORY.GetIndex();
            }
        }

        public new EntityLiving Holder
        {
            get
            {
                return (EntityLiving) base.Holder;
            }

            protected set
            {
                base.Holder = value;
            }
        }

        public override void OnSlotChange(int index, ItemStack item, bool send)
        {
            base.OnSlotChange(index, item, send);

            if (send && index == this.MainHandSlot)
            {
                //this.SendMainHand(this.Holder.Viewers);
            }
        }

        public int MainHandSlot
        {
            get
            {
                return this.mainHand;
            }

            set
            {
                this.mainHand = value;
                //this.SendMainHand(this.Holder.Viewers);
            }
        }

        public ItemStack MainHandItem
        {
            get
            {
                return this.GetItem(this.MainHandSlot);
            }

            set
            {
                this.SetItem(this.mainHand, value.Clone());
            }
        }

        public ItemStack OffHandItem
        {
            get
            {
                return this.PlayerOffhandInventory.OffHandItem;
            }

            set
            {
                this.PlayerOffhandInventory.OffHandItem = value;
            }
        }

        public ItemStack Helmet
        {
            get
            {
                return this.ArmorInventory.Helmet;
            }

            set
            {
                this.ArmorInventory.Helmet = value;
            }
        }

        public ItemStack ChestPlate
        {
            get
            {
                return this.ArmorInventory.ChestPlate;
            }

            set
            {
                this.ArmorInventory.ChestPlate = value;
            }
        }

        public ItemStack Leggings
        {
            get
            {
                return this.ArmorInventory.Leggings;
            }

            set
            {
                this.ArmorInventory.Leggings = value;
            }
        }

        public ItemStack Boots
        {
            get
            {
                return this.ArmorInventory.Boots;
            }

            set
            {
                this.ArmorInventory.Boots = value;
            }
        }

        public EntityArmorInventory ArmorInventory
        {
            get
            {
                return this.armor;
            }
        }

        public EntityOffhandInventory PlayerOffhandInventory
        {
            get
            {
                return this.offhand;
            }
        }

        public void SendMainHand(params Player[] players)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                MobEquipmentPacket pk = new MobEquipmentPacket();
                pk.EntityRuntimeId = this.Holder.EntityID;
                pk.Item = this.MainHandItem;
                pk.InventorySlot = (byte) this.MainHandSlot;
                pk.HotbarSlot = (byte) this.MainHandSlot;
                pk.WindowId = this.Type;
                players[i].SendPacket(pk);
            }
        }

        public override void SaveNBT()
        {
            this.Holder.NamedTag.PutInt("MainHand", this.MainHandSlot);

            this.ArmorInventory.SaveNBT();
            this.PlayerOffhandInventory.SaveNBT();
        }
    }
}
