using System;

namespace MineNET.Events.EntityEvents
{
    public sealed class EntityEvent
    {
        public event EventHandler<EntityAnimationEventArgs> EntityAnimation;
        public void OnEntityAnimation(object sender, EntityAnimationEventArgs e)
        {
            this.EntityAnimation?.Invoke(sender, e);
        }
    }
}
