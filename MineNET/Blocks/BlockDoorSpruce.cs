namespace MineNET.Blocks
{
    public class BlockDoorSpruce : BlockDoorBase
    {
        public BlockDoorSpruce() : base(BlockFactory.SPRUCE_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "SpruceDoor";
            }
        }
    }
}
