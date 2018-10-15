using MineNET.Entities;

namespace MineNET.Events.EntityEvents
{
    public class EntityAnimationEventArgs : EntityEventArgs, ICancelable
    {
        public int Action { get; }
        public bool IsCancel { get; set; }

        public EntityAnimationEventArgs(Entity entity, int action) : base(entity)
        {
            this.Action = action;
        }
    }
}
