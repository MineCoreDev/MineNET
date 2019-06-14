using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MineNET.Blocks;
using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Items.Enchantments;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Items
{
    public abstract class Item : ICloneable<Item>
    {
        #region Item Getter

        public static Item Get(int id)
        {
            return Item.Get(id, 0, 1);
        }

        public static Item Get(int id, int damage)
        {
            return Item.Get(id, damage, 1);
        }

        public static Item Get(int id, int damage, int count)
        {
            Item item;
            if (MineNET_Registries.Item.ContainsKey(id))
            {
                item = MineNET_Registries.Item[id].Clone();
            }
            else
            {
                if (id < 256)
                {
                    item = new ItemBlock(Block.Get(id));
                }
                else
                {
                    item = new ItemUnknown(id);
                }
            }

            item.Damage = damage;
            item.Count = count;
            return item;
        }

        public static Item Get(int id, int damage, int count, byte[] nbt)
        {
            Item item = Item.Get(id, damage, count);

            CompoundTag tag = new CompoundTag();
            if (nbt != null && nbt.Length > 0)
            {
                tag = NBTIO.ReadTag(nbt);
            }
            item.NamedTag = tag;

            return item;
        }

        public static Item Get(string name)
        {
            string[] data = name.Replace("minecraft:", "").Replace(" ", "_").ToUpper().Split(':');
            int id = 0;
            int damage = 0;

            if (data.Length > 0)
            {
                int.TryParse(data[0], out id);
            }
            if (data.Length > 1)
            {
                int.TryParse(data[1], out damage);
            }

            ItemIDs factory = new ItemIDs();
            FieldInfo info = factory.GetType().GetField(data[0]);
            if (info != null)
            {
                id = (int) info.GetValue(factory);
            }
            else
            {
                BlockIDs factory2 = new BlockIDs();
                FieldInfo info2 = factory2.GetType().GetField(data[0]);
                if (info2 != null)
                {
                    id = (int) info2.GetValue(factory2);
                    if (id > 255)
                    {
                        id = -id + 255;
                    }
                }
            }

            return Item.Get(id, damage);
        }

        #endregion

        public abstract int ID { get; }

        public abstract string Name { get; }

        public int Damage { get; set; }
        public int Count { get; set; }

        public string[] CanPlaceOn { get; protected set; } = new string[0];
        public string[] CanDestroy { get; protected set; } = new string[0];

        public CompoundTag NamedTag { get; set; } = new CompoundTag();

        public virtual Block Block => Block.Get(BlockIDs.AIR);

        public virtual byte MaxStackSize => 64;

        public virtual bool IsTool => false;

        public virtual bool IsArmor => false;

        public virtual int AttackDamage => 1;

        public virtual int FuelTime => 0;

        #region Action methods

        public virtual bool Activate(Player player, World world, Block clicked, BlockFace blockFace, Vector3 clickPos)
        {
            return false;
        }

        public virtual bool ClickAir(Player player)
        {
            return false;
        }

        public virtual bool ReleaseUsing(Player player)
        {
            return false;
        }

        public virtual bool DestroyBlock(Block block, EntityLiving entity)
        {
            return false;
        }

        public virtual bool AttackEntity(EntityLiving attacker, EntityLiving target)
        {
            return false;
        }

        #endregion

        #region Components

        public Item AddCanPlaceOn(params string[] blocks)
        {
            List<string> canPlaceOn = this.CanPlaceOn.ToList();
            for (int i = 0; i < blocks.Length; ++i)
            {
                canPlaceOn.Add(blocks[i]);
            }
            this.CanPlaceOn = canPlaceOn.ToArray();
            return this;
        }

        public Item RemoveCanPlaceOn(params string[] blocks)
        {
            List<string> canPlaceOn = this.CanPlaceOn.ToList();
            for (int i = 0; i < blocks.Length; ++i)
            {
                if (canPlaceOn.Contains(blocks[i]))
                {
                    canPlaceOn.Remove(blocks[i]);
                }
            }
            this.CanPlaceOn = canPlaceOn.ToArray();
            return this;
        }

        public Item AddCanDestroy(params string[] blocks)
        {
            List<string> canDestroy = this.CanDestroy.ToList();
            for (int i = 0; i < blocks.Length; ++i)
            {
                canDestroy.Add(blocks[i]);
            }
            this.CanDestroy = canDestroy.ToArray();
            return this;
        }

        public Item RemoveCanDestroy(params string[] blocks)
        {
            List<string> canDestroy = this.CanDestroy.ToList();
            for (int i = 0; i < blocks.Length; ++i)
            {
                if (canDestroy.Contains(blocks[i]))
                {
                    canDestroy.Remove(blocks[i]);
                }
            }
            this.CanDestroy = canDestroy.ToArray();
            return this;
        }

        #endregion

        #region NBT CustomName

        public string GetCustomName()
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("display"))
            {
                return "";
            }

            CompoundTag display = tag.GetCompound("display");
            if (!display.Exist("Name"))
            {
                return "";
            }
            return display.GetString("Name");
        }

        public Item SetCustomName(string name)
        {
            if (name == null || name == "")
            {
                this.ClearCustomName();
                return this;
            }

            CompoundTag tag = this.NamedTag;
            if (tag.Exist("display"))
            {
                tag.GetCompound("display").PutString("Name", name);
            }
            else
            {
                tag.PutCompound("display", new CompoundTag("display").PutString("Name", name));
            }
            this.NamedTag = tag;
            return this;
        }

        public Item ClearCustomName()
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("display"))
            {
                return this;
            }

            CompoundTag display = tag.GetCompound("display");
            if (display.Exist("Name"))
            {
                display.Remove("Name");
            }
            this.NamedTag = tag;
            return this;
        }

        #endregion

        #region NBT Lore

        public string[] GetLore()
        {
            CompoundTag tag = this.NamedTag;
            if (tag.Exist("display"))
            {
                return new string[0];
            }

            CompoundTag display = tag.GetCompound("display");
            if (!display.Exist("Lore"))
            {
                return new string[0];
            }

            ListTag lores = display.GetList("Lore");
            string[] data = new string[lores.Count];
            for (int i = 0; i < lores.Count; ++i)
            {
                data[i] = ((StringTag) lores[i]).Data;
            }
            return data;
        }

        public Item SetLore(params string[] lores)
        {
            if (lores == null || lores.Length < 1)
            {
                this.ClearLore();
                return this;
            }

            CompoundTag tag = this.NamedTag;
            ListTag list = new ListTag("Lore", NBTTagType.STRING);
            for (int i = 0; i < lores.Length; ++i)
            {
                list.Add(new StringTag(lores[i]));
            }

            if (tag.Exist("display"))
            {
                tag.GetCompound("display").PutList(list);
            }
            else
            {
                tag.PutCompound("display", new CompoundTag("display").PutList(list));
            }
            this.NamedTag = tag;
            return this;
        }

        public Item AddLore(params string[] lores)
        {
            if (lores == null || lores.Length < 1)
            {
                return this;
            }
            if (this.GetLore().Length < 1)
            {
                this.SetLore(lores);
                return this;
            }

            CompoundTag tag = this.NamedTag;
            ListTag list = tag.GetCompound("display").GetList("Lore");
            for (int i = 0; i < lores.Length; ++i)
            {
                list.Add(new StringTag(lores[i]));
            }
            this.NamedTag = tag;
            return this;
        }

        public Item ClearLore()
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("display"))
            {
                return this;
            }

            CompoundTag display = tag.GetCompound("display");
            if (display.Exist("Lore"))
            {
                display.Remove("Lore");
            }
            this.NamedTag = tag;
            return this;
        }

        #endregion

        #region NBT Unbreakable

        public bool GetUnbreakable()
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("Unbreakable"))
            {
                return false;
            }
            return tag.GetBool("Unbreakable");
        }

        public Item SetUnbreakable(bool value)
        {
            CompoundTag tag = this.NamedTag;
            tag.PutBool("Unbreakable", value);
            this.NamedTag = tag;
            return this;
        }

        #endregion

        #region NBT Enchantment

        public bool HasEnchantment(int id)
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("ench"))
            {
                return false;
            }

            ListTag list = tag.GetList("ench");
            for (int i = 0; i < list.Count; ++i)
            {
                CompoundTag ench = (CompoundTag) list[i];
                if (ench.GetShort("id") == id)
                {
                    return true;
                }
            }
            return false;
        }

        public Enchantment GetEnchantment(int id)
        {
            if (!this.HasEnchantment(id))
            {
                return null;
            }

            CompoundTag tag = this.NamedTag;
            ListTag list = tag.GetList("ench");
            for (int i = 0; i < list.Count; ++i)
            {
                CompoundTag ench = (CompoundTag) list[i];
                if (ench.GetShort("id") == id)
                {
                    return Enchantment.GetEnchantment(id, ench.GetShort("lvl"));
                }
            }
            return null;
        }

        public Enchantment[] GetEnchantments()
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("ench"))
            {
                return new Enchantment[0];
            }

            ListTag list = tag.GetList("ench");
            List<Enchantment> enches = new List<Enchantment>();
            for (int i = 0; i < list.Count; ++i)
            {
                CompoundTag ench = (CompoundTag) list[i];
                enches.Add(Enchantment.GetEnchantment(ench.GetShort("id"), ench.GetShort("lvl")));
            }
            return enches.ToArray();
        }

        public Item AddEnchantment(Enchantment enchantment)
        {
            CompoundTag tag = this.NamedTag;
            ListTag list;
            if (tag.Exist("ench"))
            {
                list = tag.GetList("ench");
            }
            else
            {
                list = new ListTag("ench", NBTTagType.COMPOUND);
                tag.PutList(list);
            }
            for (int i = 0; i < list.Count; ++i)
            {
                if (((CompoundTag) list[i]).GetShort("id") == enchantment.ID)
                {
                    list[i] = new CompoundTag()
                        .PutShort("id", (short) enchantment.ID)
                        .PutShort("lvl", (short) enchantment.Level);
                    this.NamedTag = tag;
                    return this;
                }
            }
            CompoundTag ench = new CompoundTag()
                        .PutShort("id", (short) enchantment.ID)
                        .PutShort("lvl", (short) enchantment.Level);
            list.Add(ench);
            this.NamedTag = tag;
            return this;
        }

        public Item RemoveEnchantment(Enchantment enchantment)
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("ench"))
            {
                return this;
            }
            ListTag list = tag.GetList("ench");
            for (int i = 0; i < list.Count; ++i)
            {
                if (((CompoundTag) list[i]).GetShort("id") == enchantment.ID)
                {
                    list.RemoveAt(i);
                }
            }
            this.NamedTag = tag;
            return this;
        }

        public Item RemoveEnchantments()
        {
            CompoundTag tag = this.NamedTag;
            if (tag.Exist("ench"))
            {
                tag.Remove("ench");
            }
            return this;
        }

        #endregion

        #region Clone method

        public Item Clone()
        {
            return (Item) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        public override bool Equals(object obj)
        {
            return this.Equals(obj);
        }

        public bool Equals(object obj, bool checkDamage = true, bool checkCount = true, bool checkNBT = true, bool checkComponents = true)
        {
            if (!(obj is Item))
            {
                return false;
            }

            Item item = (Item) obj;
            if (this.ID != item.ID)
            {
                return false;
            }

            if (checkDamage && this.Damage != item.Damage)
            {
                return false;
            }

            if (checkCount && this.Count != item.Count)
            {
                return false;
            }

            if (checkNBT && this.NamedTag != item.NamedTag)
            {
                return false;
            }

            if (checkComponents)
            {
                if (!((IStructuralEquatable) this.CanPlaceOn).Equals(item.CanPlaceOn, StructuralComparisons.StructuralEqualityComparer) ||
                    !((IStructuralEquatable) this.CanDestroy).Equals(item.CanDestroy, StructuralComparisons.StructuralEqualityComparer))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator ==(Item A, Item B)
        {
            if (object.ReferenceEquals(A, B))
            {
                return true;
            }
            if ((object) A == null || (object) B == null)
            {
                return false;
            }
            return A.Equals(B);
        }

        public static bool operator !=(Item A, Item B)
        {
            if (object.ReferenceEquals(A, B))
            {
                return false;
            }
            if ((object) A == null || (object) B == null)
            {
                return true;
            }
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
