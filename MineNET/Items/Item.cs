using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MineNET.Blocks;
using MineNET.Blocks.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Items.Enchantments;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Resources.Data;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds;
using Newtonsoft.Json.Linq;

namespace MineNET.Items
{
    public class Item : ICloneable<Item>
    {
        private static List<Item> creativeItems = new List<Item>();

        public static Item Get(int id, int meta = 0, int count = 1, byte[] tags = null)
        {
            Item item = ItemFactory.GetItem(id);
            item.Damage = meta;
            item.Count = count;
            if (item.ID == 0)
            {
                item.Damage = 0;
                item.Count = 0;
            }
            if (tags == null)
            {
                tags = new byte[0];
            }
            item.tags = tags;
            return item;
        }

        public static void AddCreativeItem(Item item)
        {
            Item.creativeItems.Add(item);
        }

        public static void RemoveCreativeItem(Item item)
        {
            Item.creativeItems.Remove(item);
        }

        public static void RemoveCreativeItem(int index)
        {
            Item.creativeItems.RemoveAt(index);
        }

        public static void AddCreativeItems(params Item[] items)
        {
            Item.creativeItems.AddRange(items);
        }

        public static void RemoveAllCreativeItems()
        {
            Item.creativeItems = new List<Item>();
        }

        public static Item[] GetCreativeItems()
        {
            return Item.creativeItems.ToArray();
        }

        public static void LoadCreativeItems()
        {
            string data = Encoding.UTF8.GetString(ResourceData.CreativeItems);
            JObject json = JObject.Parse(data);
            JToken items = json.GetValue("items");
            foreach (JObject item in items)
            {
                int id = item.Value<int>("id");
                int damage = item.Value<int>("damage");
                string tags = item.Value<string>("nbt_hex");
                byte[] nbt = null;
                if (!string.IsNullOrEmpty(tags))
                {
                    nbt = tags.Chunks(2).Select(x => Convert.ToByte(new string(x.ToArray()), 16)).ToArray();
                }

                Item.AddCreativeItem(Item.Get(id, damage, 1, nbt));
            }
        }

        public static Item Get(string name)
        {
            return ItemFactory.GetItem(name);
        }

        public Item Clone()
        {
            return (Item) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        private byte[] tags = new byte[0];
        private CompoundTag cachedNBT = null;

        private List<string> canPlaceOn = new List<string>();

        private List<string> canDestroy = new List<string>();

        private Block block = null;

        public Item(int id, int meta = 0, int count = 1)
        {
            this.ID = id;
            this.Damage = meta;
            this.Count = count;
        }

        public virtual string Name
        {
            get
            {
                return "Unknown";
            }
        }

        public int ID { get; }

        public int Damage { get; set; }

        public int Count { get; set; }

        public bool HasTags
        {
            get
            {
                if (this.tags != null)
                {
                    return this.tags.Length > 0;
                }
                else
                {
                    return false;
                }
            }
        }

        public byte[] Tags
        {
            get
            {
                return this.tags;
            }

            set
            {
                this.tags = value;
                this.cachedNBT = null;
            }
        }

        public CompoundTag GetNamedTag()
        {
            if (!this.HasTags)
            {
                return new CompoundTag();
            }

            if (this.cachedNBT == null)
            {
                this.cachedNBT = NBTIO.ReadTag(this.tags);
            }

            if (this.cachedNBT != null)
            {
                this.cachedNBT.Name = "";
            }
            return this.cachedNBT;
        }

        public Item SetNamedTag(CompoundTag nbt)
        {
            nbt.Name = "";
            this.cachedNBT = nbt;
            this.tags = NBTIO.WriteTag(nbt);
            return this;
        }

        public virtual bool Activate(Player player, World world, Block clicked, BlockFace blockFace, Vector3 clickPos)
        {
            return false;
        }

        public virtual bool Use(Block block)
        {
            return false;
        }

        public virtual bool Use(Entity entity)
        {
            return false;
        }

        public Block Block
        {
            get
            {
                if (this.block == null)
                {
                    return Block.Get(BlockFactory.AIR);
                }
                else
                {
                    return this.block.Clone();
                }
            }

            set
            {
                this.block = value;
            }
        }

        public virtual byte MaxStackSize
        {
            get
            {
                return 64;
            }
        }

        public virtual bool IsTool
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsPickaxe
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsAxe
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsSword
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsShovel
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsHoe
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsShears
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsFlintAndSteel
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsArmor
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsHemlet
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsChestplate
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsLeggings
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsBoots
        {
            get
            {
                return false;
            }
        }

        public virtual bool CanBeConsumed
        {
            get
            {
                return false;
            }
        }

        public virtual bool CanBePlace
        {
            get
            {
                return this.Block != null && this.Block.CanBePlaced;
            }
        }

        public virtual bool CanBeActivate
        {
            get
            {
                return false;
            }
        }

        public string GetCustomName()
        {
            if (!this.HasTags)
            {
                return "";
            }

            CompoundTag tag = this.GetNamedTag();
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
            }
            else
            {
                CompoundTag tag;
                if (this.HasTags)
                {
                    tag = this.GetNamedTag();
                }
                else
                {
                    tag = new CompoundTag();
                }
                if (tag.Exist("display"))
                {
                    tag.GetCompound("display").PutString("Name", name);
                }
                else
                {
                    tag.PutCompound("display", new CompoundTag("display").PutString("Name", name));
                }
                this.SetNamedTag(tag);
            }
            return this;
        }

        public Item ClearCustomName()
        {
            if (!this.HasTags)
            {
                return this;
            }

            CompoundTag tag = this.GetNamedTag();
            if (!tag.Exist("display"))
            {
                return this;
            }
            CompoundTag display = tag.GetCompound("display");
            if (display.Exist("Name"))
            {
                display.Remove("Name");
            }
            this.SetNamedTag(tag);
            return this;
        }

        public string[] GetLore()
        {
            if (!this.HasTags)
            {
                return new string[0];
            }

            CompoundTag tag = this.GetNamedTag();
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
            }
            else
            {
                CompoundTag tag;
                if (this.HasTags)
                {
                    tag = this.GetNamedTag();
                }
                else
                {
                    tag = new CompoundTag();
                }
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
                this.SetNamedTag(tag);
            }
            return this;
        }

        public Item AddLore(params string[] lores)
        {
            if (lores == null || lores.Length < 1)
            {
                return this;
            }
            if (!this.HasTags || this.GetLore().Length < 1)
            {
                this.SetLore(lores);
                return this;
            }
            CompoundTag tag = this.GetNamedTag();
            ListTag list = tag.GetCompound("display").GetList("Lore");
            for (int i = 0; i < lores.Length; ++i)
            {
                list.Add(new StringTag(lores[i]));
            }
            this.SetNamedTag(tag);
            return this;
        }

        public Item ClearLore()
        {
            if (!this.HasTags)
            {
                return this;
            }

            CompoundTag tag = this.GetNamedTag();
            if (!tag.Exist("display"))
            {
                return this;
            }
            CompoundTag display = tag.GetCompound("display");
            if (display.Exist("Lore"))
            {
                display.Remove("Lore");
            }
            this.SetNamedTag(tag);
            return this;
        }

        public bool HasEnchantment(int id)
        {
            if (!this.HasTags)
            {
                return false;
            }
            CompoundTag tag = this.GetNamedTag();
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
            CompoundTag tag = this.GetNamedTag();
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
            if (!this.HasTags)
            {
                return new Enchantment[0];
            }
            CompoundTag tag = this.GetNamedTag();
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

        public Item AddEnchant(Enchantment enchant)
        {
            CompoundTag tag;
            if (this.HasTags)
            {
                tag = this.GetNamedTag();
            }
            else
            {
                tag = new CompoundTag();
            }
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
                if (((CompoundTag) list[i]).GetShort("id") == enchant.ID)
                {
                    list[i] = new CompoundTag()
                        .PutShort("id", (short) enchant.ID)
                        .PutShort("lvl", (short) enchant.Level);
                    this.SetNamedTag(tag);
                    return this;
                }
            }
            CompoundTag ench = new CompoundTag()
                        .PutShort("id", (short) enchant.ID)
                        .PutShort("lvl", (short) enchant.Level);
            list.Add(ench);
            this.SetNamedTag(tag);
            return this;
        }

        public string[] CanPlaceOn
        {
            get
            {
                return this.canPlaceOn.ToArray();
            }

            set
            {
                this.canPlaceOn = value.ToList();
            }
        }

        public Item AddCanPlaceOn(params string[] blocks)
        {
            for (int i = 0; i < blocks.Length; ++i)
            {
                this.canPlaceOn.Add(blocks[i]);
            }
            return this;
        }

        public Item RemoveCanPlaceOn(params string[] blocks)
        {
            for (int i = 0; i < blocks.Length; ++i)
            {
                if (this.canPlaceOn.Contains(blocks[i]))
                {
                    this.canPlaceOn.Remove(blocks[i]);
                }
            }
            return this;
        }

        public string[] CanDestroy
        {
            get
            {
                return this.canDestroy.ToArray();
            }

            set
            {
                this.canDestroy = value.ToList();
            }
        }

        public Item AddCanDestroy(params string[] blocks)
        {
            for (int i = 0; i < blocks.Length; ++i)
            {
                this.canDestroy.Add(blocks[i]);
            }
            return this;
        }

        public Item RemoveCanDestroy(params string[] blocks)
        {
            for (int i = 0; i < blocks.Length; ++i)
            {
                if (this.canDestroy.Contains(blocks[i]))
                {
                    this.canDestroy.Remove(blocks[i]);
                }
            }
            return this;
        }

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
            if (checkNBT)
            {
                if (this.HasTags != item.HasTags)
                {
                    return false;
                }
                else
                {
                    if (this.HasTags && this.GetNamedTag() != item.GetNamedTag())
                    {
                        return false;
                    }
                }
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
