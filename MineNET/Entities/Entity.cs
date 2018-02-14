namespace MineNET.Entities
{
    public abstract class Entity
    {
        protected long id;

        public Entity()
        {

        }

        public long GetId
        {
            get
            {
                return this.id;
            }
        }
    }
}
