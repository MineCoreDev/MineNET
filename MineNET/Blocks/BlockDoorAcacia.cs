namespace MineNET.Blocks
{
    public class BlockDoorAcacia : BlockDoorBase
    {
        public BlockDoorAcacia() : base(BlockFactory.ACACIA_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "AcaciaDoor";
            }
        }
    }
}
