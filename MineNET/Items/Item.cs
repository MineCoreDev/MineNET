using System.Reflection;
using MineNET.Blocks;
using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Items
{
    public class Item
    {
        #region Item Getter

        public static Item Get(int id)
        {
            if (MineNET_Registries.Item.ContainsKey(id))
            {
                return MineNET_Registries.Item[id];
            }
            else
            {
                return new ItemUnknown(id);
                //return new ItemBlock(Block.Get(BlockIDs.AIR));
            }
        }

        public static Item Get(string name)
        {
            string[] data = name.Replace("minecraft:", "").Replace(" ", "_").ToUpper().Split(':');
            int id = 0;

            if (data.Length == 1)
            {
                int.TryParse(data[0], out id);
            }

            ItemIDs factory = new ItemIDs();
            FieldInfo info = factory.GetType().GetField(data[0]);
            if (info != null)
            {
                id = (int)info.GetValue(factory);
            }
            else
            {
                BlockIDs factory2 = new BlockIDs();
                FieldInfo info2 = factory2.GetType().GetField(data[0]);
                if (info2 != null)
                {
                    id = (int)info2.GetValue(factory2);
                    if (id > 255)
                    {
                        id = -id + 255;
                    }
                }
            }

            Item item = Item.Get(id);
            return item;
        }

        #endregion

        private Block block = null;

        public int ID { get; }

        public Item(string name, int id)
        {
            this.Name = name;
            this.ID = id;
        }

        public virtual string Name { get; } = "Unknown";

        public virtual byte MaxStackSize { get; } = 64;

        public virtual bool IsTool { get; } = false;

        public virtual bool IsArmor { get; } = false;

        public virtual bool Activate(Player player, World world, Block clicked, BlockFace blockFace, Vector3 clickPos)
        {
            return false;
        }

        public virtual bool BlockDestroyed(Block block, EntityLiving entity)
        {
            return false;
        }

        public virtual bool HitEntity(EntityLiving attacker, EntityLiving target)
        {
            return false;
        }

        public Block Block
        {
            get
            {
                if (this.block == null)
                {
                    return Block.Get(BlockIDs.AIR);
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

        public virtual bool CanBeConsumed { get; } = false;

        public virtual bool CanBePlace
        {
            get
            {
                return this.Block != null && this.Block.CanBePlaced;
            }
        }

        public virtual bool CanBeActivate { get; } = false;

        public virtual int AttackDamage { get; } = 1;

        public override bool Equals(object obj)
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
