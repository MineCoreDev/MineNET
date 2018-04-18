namespace MineNET.Blocks
{
    public class BlockDoorBirch : BlockDoorBase
    {
        public BlockDoorBirch() : base(BlockFactory.BIRCH_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "BirchDoor";
            }
        }
    }
}
