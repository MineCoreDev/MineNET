using System;
using MineNET.Values;

namespace MineNET.Blocks
{
    public class Block : ICloneable<Block>
    {
        public static Block Get(int id, int meta = 0)
        {
            if (MineNET_Registries.Block.ContainsKey(id))
            {
                Block b = MineNET_Registries.Block[id];
                b.Damage = meta;
                return b;
            }

            return new BlockAir();
        }

        public static Block Get(string name)
        {
            string[] data = name.Replace("minecraft:", "").Replace(" ", "_").ToUpper().Split(':');
            int id = 0;
            int meta = 0;

            if (data.Length == 1)
            {
                int.TryParse(data[0], out id);
            }

            if (data.Length == 2)
            {
                int.TryParse(data[0], out id);
                int.TryParse(data[1], out meta);
            }

            try
            {
                BlockIDs factory = new BlockIDs();
                id = (int) factory.GetType().GetField(data[0]).GetValue(factory);
            }
            catch
            {

            }

            Block block = Block.Get(id);

            return block;
        }

        public int ID { get; }
        public int Damage { get; set; }

        public Position Position { get; internal set; }

        public string Name { get; }

        public float Hardness { get; set; } = 0;
        public float Resistance { get; set; } = 0;

        public float LightLevel { get; set; } = 0;
        public float LightOpacity { get; set; } = 0;

        public Color MapColor { get; set; } = BlockColor.Air;

        public Block(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

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

        public void UpdateTick(int type)
        {
        }
    }
}
