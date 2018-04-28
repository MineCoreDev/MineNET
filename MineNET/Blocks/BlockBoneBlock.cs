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
    }
}
