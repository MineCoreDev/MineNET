using MineNET.Network.MinecraftPackets;

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

        public void Write(MinecraftPacket stream)
        {
            throw new System.NotImplementedException();
        }
    }
}
