using MineNET.Entities.Data;
using MineNET.Entities.Players;

namespace MineNET.Items.Data
{
    public interface IConsumeable
    {
        void OnConsume(Player player);

        Effect[] AdditionalEffects
        {
            get;
        }
    }
}
