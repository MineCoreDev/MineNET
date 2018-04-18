namespace MineNET.Blocks
{
    public class BlockDoorJungle : BlockDoorBase
    {
        public BlockDoorJungle() : base(BlockFactory.JUNGLE_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "JungleDoor";
            }
        }
    }
}
