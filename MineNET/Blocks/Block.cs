using System;
using MineNET.Blocks.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Blocks
{
    public abstract class Block : ICloneable<Block>, IPosition
    {

        public static Block Get(int id, int meta = 0)
        {
            Block block = BlockFactory.GetBlock(id);
            block.Damage = meta;

            return block;
        }

        public static Block Get(string name)
        {
            return BlockFactory.GetBlock(name);
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public World World { get; set; } = null;

        public int ID { get; }

        public int Damage { get; set; }

        public Block(int id, int meta = 0)
        {
            this.ID = id;
            this.Damage = meta;
        }

        public abstract string Name
        {
            get;
        }

        public virtual bool Place(Block clicked, Block replace, BlockFace face, Vector3 clickPos, Player player, Item item)
        {
            this.World.SetBlock(this.Vector3, this);
            return true;
        }

        public virtual bool Break(Player player, Item item)
        {
            this.World.SetBlock(this.Vector3, new BlockAir());
            return true;
        }

        public virtual bool Activate(Player player, Item item)
        {
            return false;
        }

        public virtual void Update(int type)
        {

        }

        public virtual Item[] GetDrops(Item item)
        {
            if (this.ID < 1)
            {
                return new Item[] { Item.Get(BlockFactory.AIR, 0, 0) };
            }
            return new Item[] { this.Item };
        }

        public virtual Item Item
        {
            get
            {
                return Item.Get(this.ID, this.Damage);
            }
        }

        public virtual int DropExp
        {
            get
            {
                return 0;
            }
        }

        public virtual byte MaxStackSize
        {
            get
            {
                return 64;
            }
        }

        public virtual bool IsTransparent
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsSolid
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
                return false;
            }
        }

        public virtual bool CanBeActivate
        {
            get
            {
                return false;
            }
        }

        public Block GetSideBlock(BlockFace face)
        {
            if (this.HasPosition())
            {
                return this.World.GetBlock(this.Vector3 + face.GetPosition());
            }
            return null;
        }

        public Vector3 Vector3
        {
            get
            {
                return new Vector3(this.X, this.Y, this.Z);
            }
        }

        public Position Position
        {
            get
            {
                return new Position(this.X, this.Y, this.Z, this.World);
            }

            set
            {
                this.X = value.X;
                this.Y = value.Y;
                this.Z = value.Z;
                this.World = value.World;
            }
        }

        public bool HasPosition()
        {
            return this.World != null;
        }

        public virtual Block Clone()
        {
            return (Block) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"Name : {this.Name} | ID : {this.ID} | Damage : {this.Damage}";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Block))
            {
                return false;
            }
            Block block = (Block) obj;
            if (this.ID != block.ID || this.Damage != block.Damage)
            {
                return false;
            }
            return true;
        }

        public static bool operator ==(Block A, Block B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(Block A, Block B)
        {
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
