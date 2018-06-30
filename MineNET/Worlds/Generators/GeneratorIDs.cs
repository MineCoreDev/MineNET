using System;

namespace MineNET.Worlds.Generators
{
    public sealed class GeneratorIDs
    {
        public const int Old = 0;
        public const int Infinite = 1;
        public const int Flat = 2;

        public static string GetString(int type)
        {
            if (type == Old)
            {
                return "OLD";
            }
            else if (type == Infinite)
            {
                return "INFINITE";
            }
            else if (type == Flat)
            {
                return "FLAT";
            }
            else
            {
                throw new ArgumentOutOfRangeException(type + " is not found!");
            }
        }
    }
}
