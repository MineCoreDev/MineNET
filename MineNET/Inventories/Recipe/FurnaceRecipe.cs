using MineNET.Network.Packets;
using MineNET.Utils;

namespace MineNET.Inventories.Recipe
{
    public class FurnaceRecipe : IRecipe
    {
        public int ID
        {
            get
            {
                return CraftingDataPacket.ENTRY_FURNACE;
            }
        }

        public void Write(MCBEBinary stream)
        {
            throw new System.NotImplementedException();
        }
    }
}
