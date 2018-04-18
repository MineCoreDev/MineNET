namespace MineNET.Blocks
{
    public class BlockDoorIron : BlockDoorBase
    {
        public BlockDoorIron() : base(BlockFactory.IRON_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "IronDoor";
            }
        }
    }
}
