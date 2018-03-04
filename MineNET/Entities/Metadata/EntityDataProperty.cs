namespace MineNET.Entities.Metadata
{
    public abstract class EntityDataProperty<T> : EntityData
    {
        public T Data { get; set; }

        public EntityDataProperty(int id, T data) : base(id)
        {
            this.Data = data;
        }
    }
}
