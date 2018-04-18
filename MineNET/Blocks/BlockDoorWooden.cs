namespace MineNET.Blocks
{
    public class BlockDoorWooden : BlockDoorBase
    {
        public BlockDoorWooden() : base(BlockFactory.WOODEN_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenDoor";
            }
        }
    }
}
