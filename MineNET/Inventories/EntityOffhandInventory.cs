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
                return "EntityOffhandInventory";
            }
        }

        public override void SendSlot(int index, params Player[] players)
        {
            base.SendSlot(index, players);
            if (index == 0)
            {
                this.SendOffHand(this.Holder.Viewers);
            }
        }

        public override void SendContents(params Player[] players)
        {
            base.SendContents(players);
            this.SendOffHand(this.Holder.Viewers);
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
                MobEquipmentPacket pk = new MobEquipmentPacket
                {
                    EntityRuntimeId = this.Holder.EntityID,
                    Item = this.OffHandItem,
                    InventorySlot = 0,
                    WindowId = this.Type
                };
                players[i].SendPacket(pk);
            }
        }
    }
}
