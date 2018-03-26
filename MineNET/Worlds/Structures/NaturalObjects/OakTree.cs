using System;
using MineNET.Blocks;
using MineNET.Values;

namespace MineNET.Worlds.Structures.NaturalObjects
{
    public class OakTree : StructureBase
    {
        public override string Name
        {
            get
            {
                return "OakTree";
            }
        }

        public override Vector3 GenerationStruct(World world, Vector3 center)
        {
            Random r = new Random();
            int h = r.Next(4, 6);

            Block block1 = Block.Get(17, 0);

            for (int i = 0; i < h; ++i)
            {
                Vector3 pos = new Vector3(center.X, center.Y + i, center.Z);
                world.SetBlock(pos, block1);
            }

            return new Vector3(1f, h, 1f);
        }
    }
}
