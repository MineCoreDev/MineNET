namespace MineNET.Blocks
{
    public class BlockUnknown : Block
    {
        public BlockUnknown(int id) : base(id)
        {

        }

        public override string Name
        {
            get
            {
                return "Unknown";
            }
        }
    }
}
