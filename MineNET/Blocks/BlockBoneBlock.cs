using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockBoneBlock : BlockSolid
    {
        public BlockBoneBlock() : base(BlockFactory.BONE_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "BoneBlock";
            }
        }

        public override Item Item
        {
            get
            {
                return Item.Get(BlockFactory.BONE_BLOCK, 0, 1);
            }
        }
    }
}
