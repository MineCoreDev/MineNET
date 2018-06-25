using MineNET.Items;

namespace MineNET.Init
{
    public sealed class ItemInit
    {
        public static ItemInit In { get; private set; }

        public ItemInit()
        {
            ItemInit.In = this;
        }

        public void Init()
        {
        }

        public void Add(int id, Item item)
        {
            MineNET_Registries.Item.Add(id, item);
        }

        public void Dispose()
        {
            ItemInit.In = null;
        }
    }
}
