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
            int val = r.Next(2);

            string[,,] structureData = null;
            if (val <= 0 && val >= 1)
            {
                structureData = new string[,,]
                {
                    {
                        {
                            "1", "1", "1", "1", "1",
                        },
                    },
                };
            }
            else
            {
                structureData = new string[,,]
                {
                    {
                        {
                            "1", "1", "1", "1", "1",
                        },
                    },
                };
            }

            Vector3 pos = new Vector3();
            int xl = structureData.GetLength(0);
            int yl = structureData.GetLength(1);
            int zl = structureData.GetLength(2);
            float cx = center.X - (xl / 2);
            float cz = center.Z - (zl / 2);
            for (float x = cx; x < cx + xl; ++x)
            {
                for (float z = cz; z < cz + zl; ++z)
                {
                    for (float y = center.Y; y < yl; ++y)
                    {
                        pos.X = x;
                        pos.Y = y;
                        pos.Z = z;
                        world.SetBlock(pos, Block.Get(structureData[(int) x, (int) y, (int) z]));
                    }
                }
            }

            return new Vector3();
        }
    }
}
