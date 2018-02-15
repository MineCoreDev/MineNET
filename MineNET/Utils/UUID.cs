using System;

namespace MineNET.Utils
{
    public sealed class UUID
    {
        public static Guid CreateUUID()
        {
            return Guid.NewGuid();
        }

        public static Guid ToUUID(string value)
        {
            return Guid.Parse(value);
        }
    }
}
