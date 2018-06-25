namespace MineNET.Worlds
{
    public enum Difficulty
    {
        Peaceful,
        Easy,
        Normal,
        Hard
    }

    public static class DifficultyExtention
    {
        public static int GetIndex(this Difficulty difficulty)
        {
            return (int) difficulty;
        }
    }
}
