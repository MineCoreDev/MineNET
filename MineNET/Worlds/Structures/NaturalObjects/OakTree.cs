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
            Random r = new Random((int) world.Seed + center.GetHashCode());
            int h = r.Next(4, 6);

            Block block1 = Block.Get(17, 0);
            Block block2 = Block.Get(18);

            for (int i = 0; i < h; ++i)
            {
                Vector3 pos = new Vector3(center.X, center.Y + i, center.Z);
                world.SetBlock(pos, block1);
            }

            int lsh = h - r.Next(3, 4);
            if (lsh == 0)
            {
                lsh = 1;
            }

            int maxW = r.Next(2, 3);

            int type = lsh >= 3 ? r.Next(0, 1) : 0;
            if (type == 0)//Down FlatType
            {
                for (int y = 0; y < h - lsh + 1; ++y)
                {
                    for (int i = -maxW; i < maxW + 1; ++i)
                    {
                        for (int j = -maxW; j < maxW + 1; ++j)
                        {
                            Vector3 pos = new Vector3(center.X + i, center.Y + lsh + y, center.Z + j);
                            if (world.GetBlock(pos).ID == BlockFactory.AIR)
                            {
                                world.SetBlock(pos, block2);
                            }
                        }
                    }
                }
            }

            return new Vector3(1f, h, 1f);
        }
    }
}
