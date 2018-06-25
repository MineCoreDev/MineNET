using MineNET.Values;
using System;

namespace MineNET.Blocks
{
    public class Block : ICloneable<Block>
    {
        public int ID { get; }
        public int Damage { get; }

        public static Block Get(int id)
        {
            if (MineNET_Registries.Block.ContainsKey(id))
            {
                return MineNET_Registries.Block[id];
            }

            return Init.BlockInit.In.Air;
        }

        public Block(string name, int id)
        {
            this.Name = name;
            this.ID = id;
        }

        public string Name { get; } = "Unknown";

        public Block Clone()
        {
            return (Block) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual AxisAlignedBB BoundingBox { get; } = AxisAlignedBB.None;


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
    }
}
