namespace MineNET.Items
{
    public class ItemUnknown : Item
    {
        private int id;
        private string name;

        public ItemUnknown(int id) : this(id, "Unknown")
        {
            
        }

        public ItemUnknown(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public override int ID
        {
            get
            {
                return this.id;
            }
        }

        public override string GetName(int damage)
        {
            return this.name;
        }
    }
}
