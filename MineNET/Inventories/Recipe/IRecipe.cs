using MineNET.Utils;

namespace MineNET.Inventories.Recipe
{
    public interface IRecipe
    {
        int ID { get; }

        void Write(MCBEBinary stream);
    }
}
