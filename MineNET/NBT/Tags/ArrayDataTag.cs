namespace MineNET.NBT.Tags
{
    public abstract class ArrayDataTag<T> : Tag
    {
        private T[] data;

        public ArrayDataTag(string name, T[] data) : base(name)
        {
            this.data = data;
        }

        public T[] Data
        {
            get
            {
                return this.data;
            }

            set
            {
                this.data = value;
            }
        }
    }
}
