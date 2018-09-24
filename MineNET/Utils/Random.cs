namespace MineNET.Utils
{
    public static class Random
    {
        private static readonly System.Random _randomBase = new System.Random();

        public static long CreateRandomID()
        {
            long val = ((long) _randomBase.Next(int.MinValue, int.MaxValue) << 8);
            val += _randomBase.Next(int.MinValue, int.MaxValue);
            return val;
        }

        public static System.Random GetRandom()
        {
            return _randomBase;
        }
    }
}