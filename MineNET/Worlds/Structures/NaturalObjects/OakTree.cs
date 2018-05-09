using System;
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

            return new Vector3();
        }
    }
}
