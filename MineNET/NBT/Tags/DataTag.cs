namespace MineNET.NBT.Tags
{
    public abstract class DataTag<T> : Tag
    {
        public DataTag(string name, T data) : base(name)
        {
            this.Data = data;
        }

        public T Data { get; set; }
    }
}
