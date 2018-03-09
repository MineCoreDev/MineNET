using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Network.Packets;
using MineNET.Network.Packets.Data;

namespace MineNET.Inventories
{
    public class ArmorInventory : BaseInventory
    {
        public const int SLOT_ARMOR_HEAD = 0;
        public const int SLOT_ARMOR_CHEST = 1;
        public const int SLOT_ARMOR_LEGS = 2;
        public const int SLOT_ARMOR_FEET = 3;

        public ArmorInventory(EntityLiving entity) : base(entity)
        {

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

        public override void OnSlotChange(int index, Item item, bool send)
        {
            if (send)
            {
                this.SendSlot(index, this.Viewers.ToArray());
                this.SendArmorContents(((EntityLiving) this.Holder).GetViewers());
            }
        }

        public override void SendSlot(int index, params Player[] players)
        {
            base.SendSlot(index, players);
        }

        public override void SendContents(params Player[] players)
        {
            base.SendContents(players);
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

        public Item[] GetArmorContents()
        {
            return new Item[] {
                this.GetHelmet(),
                this.GetChestPlate(),
                this.GetLeggings(),
                this.GetBoots(),
            };
        }

        public void SetArmorContents(Item[] items)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                this.SetItem(i, items[i]);
            }
        }

        public Item GetHelmet()
        {
            return this.GetItem(ArmorInventory.SLOT_ARMOR_HEAD);
        }

        public bool SetHelmet(Item item)
        {
            return this.SetItem(ArmorInventory.SLOT_ARMOR_HEAD, item.Clone());
        }

        public Item GetChestPlate()
        {
            return this.GetItem(ArmorInventory.SLOT_ARMOR_CHEST);
        }

        public bool SetChestPlate(Item item)
        {
            return this.SetItem(ArmorInventory.SLOT_ARMOR_CHEST, item.Clone());
        }

        public Item GetLeggings()
        {
            return this.GetItem(ArmorInventory.SLOT_ARMOR_LEGS);
        }

        public bool SetLeggings(Item item)
        {
            return this.SetItem(ArmorInventory.SLOT_ARMOR_LEGS, item.Clone());
        }

        public Item GetBoots()
        {
            return this.GetItem(ArmorInventory.SLOT_ARMOR_FEET);
        }

        public bool SetBoots(Item item)
        {
            return this.SetItem(ArmorInventory.SLOT_ARMOR_FEET, item.Clone());
        }
    }
}
