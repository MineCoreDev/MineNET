namespace MineNET.Entities
{
    public abstract class Entity
    {
        protected long id;

        public Entity()
        {

        }

        public long GetID
        {
            get
            {
                return this.id;
            }
        }
    }
}
