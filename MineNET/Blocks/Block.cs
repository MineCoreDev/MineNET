using System;
using System.Collections.Generic;
using System.Text;
using MineNET.Blocks.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Resources.Data;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds;
using Newtonsoft.Json.Linq;

namespace MineNET.Blocks
{
    public abstract class Block : ICloneable<Block>, IPosition
    {
        private static Dictionary<int, int> RuntimeIds = new Dictionary<int, int>();
        private static Dictionary<int, int> FromRuntimeId = new Dictionary<int, int>();

        public static void LoadRuntimeIds()
        {
            string data = Encoding.UTF8.GetString(ResourceData.Runtimeid_table);
            JArray json = JArray.Parse(data);
            foreach (JObject block in json.Values<JObject>())
            {
                int runtimeId = block.Value<int>("runtimeID");
                int id = block.Value<int>("id");
                int damage = block.Value<int>("data");
                Block.RuntimeIds[(id << 4) | damage] = runtimeId;
                Block.FromRuntimeId[runtimeId] = (id << 4) | damage;
            }
            Logger.Info(Block.FromRuntimeId.Count);
        }

        public static int GetRuntimeId(int id, int damage)
        {
            return Block.RuntimeIds[(id << 4) | damage];
        }

        public static Block GetBlockFromRuntimeId(int runtimeId)
        {
            int v = Block.FromRuntimeId[runtimeId];
            return Block.Get(v >> 4, v & 0xf);
        }

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

        public int RuntimeId
        {
            get
            {
                return Block.GetRuntimeId(this.ID, this.Damage);
            }
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

        public virtual bool CanBePlaced
        {
            get
            {
                return false;
            }
        }

        public virtual bool CanBreak
        {
            get
            {
                return true;
            }
        }

        public virtual bool CanBeActivated
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

        public static bool operator !=(Block A, Block B)
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
