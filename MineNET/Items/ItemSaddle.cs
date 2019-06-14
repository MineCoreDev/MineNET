namespace MineNET.Items
{
    public class ItemSaddle : Item
    {
        public override int ID { get; } = ItemIDs.SADDLE;

        public override string GetName(int damage)
        {
            return "Saddle";
        }

        public override byte MaxStackSize
        {
            get
            {
                return 1;
            }
        }
    }
}
