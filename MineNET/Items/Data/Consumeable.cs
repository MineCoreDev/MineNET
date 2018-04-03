using MineNET.Entities.Players;

namespace MineNET.Items.Data
{
    interface Consumeable
    {
        void OnConsume(Player player);
    }
}
