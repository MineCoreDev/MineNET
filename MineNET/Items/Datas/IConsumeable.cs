using MineNET.Entities;
using MineNET.Entities.Players;

namespace MineNET.Items
{
    public interface IConsumeable
    {
        void OnConsume(Player player, ItemStack food);

        Effect[] AdditionalEffects { get; }

        Item Residue { get; }
    }
}
