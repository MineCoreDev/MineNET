using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.Packets;
using MineNET.Network.Packets.Data;

namespace MineNET.Inventories
{
    public class EntityOffhandInventory : BaseInventory
    {
        public EntityOffhandInventory(EntityLiving holder) : base(holder)
        {
            if (!holder.NamedTag.Exist("Offhand"))
            {
                ListTag newTag = new ListTag("Offhand", NBTTagType.COMPOUND);
                for (int i = 0; i < this.Size; ++i)
                {
                    newTag.Add(NBTIO.WriteItem(Item.Get(0, 0, 0)));
                }
                holder.NamedTag.PutList(newTag);
            }

            ListTag items = holder.NamedTag.GetList("Offhand");
            for (int i = 0; i < this.Size; ++i)
            {
                Item item = NBTIO.ReadItem((CompoundTag) items[i]);
                this.SetItem(i, item, false);
            }
        }

        public override int Size
        {
            get
            {
                return 1;
            }
        }

        public override byte Type
        {
            get
            {
                return ContainerIds.OFFHAND.GetIndex();
            }
        }

        public override void OnSlotChange(int index, Item item, bool send)
        {
            base.OnSlotChange(index, item, send);

            if (index == 0)
            {
                if (this.Holder is Player)
                {
                    this.SendOffHand((Player) this.Holder);
                }
                this.SendOffHand(this.Holder.Viewers);
            }
        }

        public Item OffHandItem
        {
            get
            {
                return this.GetItem(0);
            }

            set
            {
                this.SetItem(0, value);
            }
        }

        public new EntityLiving Holder
        {
            get
            {
                return (EntityLiving) base.Holder;
            }

            set
            {
                base.Holder = value;
            }
        }

        public void SendOffHand(params Player[] players)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                MobEquipmentPacket pk = new MobEquipmentPacket();
                pk.EntityRuntimeId = this.Holder.EntityID;
                pk.Item = this.OffHandItem;
                pk.InventorySlot = 0;
                pk.WindowId = this.Type;
                players[i].SendPacket(pk);
            }
        }

        public override void SaveNBT()
        {
            ListTag inventory = new ListTag("Offhand", NBTTagType.COMPOUND);
            for (int i = 0; i < this.Size; ++i)
            {
                inventory.Add(NBTIO.WriteItem(this.GetItem(i), i));
            }
            this.Holder.NamedTag.PutList(inventory);
        }
    }
}
