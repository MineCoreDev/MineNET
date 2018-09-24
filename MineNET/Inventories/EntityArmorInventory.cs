﻿using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;

namespace MineNET.Inventories
{
    public class EntityArmorInventory : BaseInventory
    {
        public const int SLOT_ARMOR_HEAD = 0;
        public const int SLOT_ARMOR_CHEST = 1;
        public const int SLOT_ARMOR_LEGS = 2;
        public const int SLOT_ARMOR_FEET = 3;

        public EntityArmorInventory(EntityLiving entity) : base(entity)
        {
            if (!entity.NamedTag.Exist("Armor"))
            {
                ListTag newTag = new ListTag("Armor", NBTTagType.COMPOUND);
                for (int i = 0; i < this.Size; ++i)
                {
                    newTag.Add(NBTIO.WriteItem(new ItemStack(Item.Get(0), 0, 0), i));
                }
                entity.NamedTag.PutList(newTag);
            }

            ListTag items = entity.NamedTag.GetList("Armor");
            for (int i = 0; i < this.Size; ++i)
            {
                ItemStack item = NBTIO.ReadItem((CompoundTag) items[i]);
                this.SetItem(i, item, false);
            }
        }

        public override int Size
        {
            get
            {
                return 4;
            }
        }

        public override byte Type
        {
            get
            {
                return ContainerIds.ARMOR.GetIndex();
            }
        }

        public override void OnSlotChange(int index, ItemStack item, bool send)
        {
            this.SendSlot(index, this.Viewers.ToArray());
            //this.SendArmorContents(this.Holder.Viewers);
        }

        public override void SendSlot(int index, params Player[] players)
        {
            base.SendSlot(index, players);
        }

        public override void SendContents(params Player[] players)
        {
            base.SendContents(players);
            this.SendArmorContents(players);
        }

        public void SendArmorContents(params Player[] players)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                MobArmorEquipmentPacket pk = new MobArmorEquipmentPacket();
                pk.EntityRuntimeId = players[i].EntityID;
                pk.Items = this.GetArmorContents();
                players[i].SendPacket(pk);
            }
        }

        public ItemStack[] GetArmorContents()
        {
            return new ItemStack[] {
                this.Helmet,
                this.ChestPlate,
                this.Leggings,
                this.Boots,
            };
        }

        public void SetArmorContents(ItemStack[] items)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                this.SetItem(i, items[i]);
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

        public ItemStack Helmet
        {
            get
            {
                return this.GetItem(EntityArmorInventory.SLOT_ARMOR_HEAD);
            }

            set
            {
                this.SetItem(EntityArmorInventory.SLOT_ARMOR_HEAD, value.Clone());
            }
        }

        public ItemStack ChestPlate
        {
            get
            {
                return this.GetItem(EntityArmorInventory.SLOT_ARMOR_CHEST);
            }

            set
            {
                this.SetItem(EntityArmorInventory.SLOT_ARMOR_CHEST, value.Clone());
            }
        }

        public ItemStack Leggings
        {
            get
            {
                return this.GetItem(EntityArmorInventory.SLOT_ARMOR_LEGS);
            }

            set
            {
                this.SetItem(EntityArmorInventory.SLOT_ARMOR_LEGS, value.Clone());
            }
        }

        public ItemStack Boots
        {
            get
            {
                return this.GetItem(EntityArmorInventory.SLOT_ARMOR_FEET);
            }

            set
            {
                this.SetItem(EntityArmorInventory.SLOT_ARMOR_FEET, value.Clone());
            }
        }

        public override void SaveNBT()
        {
            ListTag inventory = new ListTag("Armor", NBTTagType.COMPOUND);
            for (int i = 0; i < this.Size; ++i)
            {
                inventory.Add(NBTIO.WriteItem(this.GetItem(i), i));
            }
            this.Holder.NamedTag.PutList(inventory);
        }
    }
}