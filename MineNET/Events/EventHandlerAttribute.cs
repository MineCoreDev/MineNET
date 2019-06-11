using System;

namespace MineNET.Events
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventHandlerAttribute : Attribute
    {
        public EventHandlerAttribute()
        {
        }
    }
}