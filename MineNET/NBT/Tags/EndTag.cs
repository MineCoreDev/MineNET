using System;

namespace MineNET.NBT.Tags
{
    public class EndTag : Tag
    {
        public new const byte ID = TAG_END;

        public EndTag() : base(null)
        {

        }

        public override void Read(NBTStream stream)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "EndTag";
        }

        public override void Write(NBTStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
