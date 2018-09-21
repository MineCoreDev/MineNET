using MineNET.Items;

namespace MineNET.Init
{
    public sealed class ItemInit
    {
        public static ItemInit In { get; private set; }

        public ItemInit()
        {
            ItemInit.In = this;
            this.Init();

            Item.LoadCreativeItems();
        }

        public void Init()
        {
        }

        public void Add(Item item)
        {
            MineNET_Registries.Item.Add(item.ID, item);
        }

        public void Dispose()
        {
            ItemInit.In = null;
        }
    }
}
