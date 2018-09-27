using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Network.MinecraftPackets;

namespace MineNET.Inventories
{
    public class EntityOffhandInventory : BaseInventory
    {
        public EntityOffhandInventory(EntityLiving holder) : base(holder)
        {
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

        public override string Name
        {
            get
            {
                return "Inventory";
            }
        }

        public override void OnSlotChange(int index, ItemStack item, bool send)
        {
            base.OnSlotChange(index, item, send);

            if (index == 0)
            {
                if (this.Holder is Player)
                {
                    this.SendOffHand((Player) this.Holder);
                }
                //this.SendOffHand(this.Holder.Viewers);
            }
        }

        public ItemStack OffHandItem
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
    }
}
