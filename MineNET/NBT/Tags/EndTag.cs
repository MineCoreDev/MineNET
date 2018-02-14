namespace MineNET.NBT.Tags
{
    public class EndTag : Tag
    {
        public new const byte ID = TAG_END;

        public EndTag() : base(null)
        {

        }

        public override string ToString()
        {
            return "EndTag";
        }
    }
}
