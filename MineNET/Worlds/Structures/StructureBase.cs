using MineNET.Values;

namespace MineNET.Worlds.Structures
{
    public abstract class StructureBase
    {
        public abstract string Name { get; }
        public abstract Vector3 GenerationStruct(World world, Vector3 center);
    }
}
