using MineNET.Network.MinecraftPackets;

namespace MineNET.Inventories.Recipe
{
    public interface IRecipe
    {
        int ID { get; }

        void Write(MinecraftPacket stream);
    }
}
