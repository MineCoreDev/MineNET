namespace MineNET.Utils
{
    public static class Random
    {
        public static System.Random RandomBase { get; } = new System.Random();

        public static long CreateRandomID()
        {
            long val = ((long) Random.RandomBase.Next(int.MinValue, int.MaxValue) << 8);
            val += Random.RandomBase.Next(int.MinValue, int.MaxValue);
            return val;
        }
    }
}
