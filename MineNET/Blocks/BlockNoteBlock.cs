using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockNoteBlock : BlockSolid
    {
        public BlockNoteBlock() : base(BlockFactory.NOTEBLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "NoteBlock";
            }
        }
    }
}
