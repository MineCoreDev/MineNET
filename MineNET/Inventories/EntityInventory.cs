using System.Collections.Generic;
using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.Tags;
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

        public override string Name
        {
            get
            {
                return "Inventory";
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

        public override void SendSlot(int index, params Player[] players)
        {
            base.SendSlot(index, players);
            if (index == this.MainHandSlot)
            {
                this.SendMainHand(this.Holder.Viewers);
            }
        }

        public override void SendContents(params Player[] players)
        {
            base.SendContents(players);
            this.SendMainHand(this.Holder.Viewers);
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
                this.SendMainHand(this.Holder.Viewers);
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
                this.SetItem(this.mainHand, value);
            }
        }

        public ItemStack OffHandItem
        {
            get
            {
                return this.EntityOffhandInventory.OffHandItem;
            }

            set
            {
                this.EntityOffhandInventory.OffHandItem = value;
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

        public EntityOffhandInventory EntityOffhandInventory
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
                MobEquipmentPacket pk = new MobEquipmentPacket
                {
                    EntityRuntimeId = this.Holder.EntityID,
                    Item = this.MainHandItem,
                    InventorySlot = (byte)this.MainHandSlot,
                    HotbarSlot = (byte)this.MainHandSlot,
                    WindowId = this.Type
                };
                players[i].SendPacket(pk);
            }
        }

        public override void LoadNBT(CompoundTag nbt)
        {
            base.LoadNBT(nbt);

            this.MainHandSlot = nbt.GetInt("MainHand");

            this.ArmorInventory.LoadNBT(nbt);
            this.EntityOffhandInventory.LoadNBT(nbt);
        }

        public override CompoundTag SaveNBT()
        {
            CompoundTag nbt = base.SaveNBT();
            nbt.PutInt("MainHand", this.MainHandSlot);

            Dictionary<string, Tag> tags = this.ArmorInventory.SaveNBT().Tags;
            foreach (string key in tags.Keys)
            {
                nbt.PutTag(key, tags[key]);
            }
            tags = this.EntityOffhandInventory.SaveNBT().Tags;
            foreach (string key in tags.Keys)
            {
                nbt.PutTag(key, tags[key]);
            }
            return nbt;
        }
    }
}
