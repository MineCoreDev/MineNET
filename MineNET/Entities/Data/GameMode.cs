namespace MineNET.Entities.Data
{
    public enum GameMode
    {
        Survival = 0,
        Creative = 1,
        Adventure = 2,
        Spectator = 3
    }

    public static class GameModeExtention
    {
        public static int GameModeToInt(this GameMode gameMode)
        {
            return (int) gameMode;
        }

        public static string GameModeToString(this GameMode gameMode)
        {
            if (gameMode == GameMode.Survival)
            {
                return "Survival";
            }
            else if (gameMode == GameMode.Creative)
            {
                return "Creative";
            }
            else if (gameMode == GameMode.Adventure)
            {
                return "Adventure";
            }
            else
            {
                return "Spectator";
            }
        }
    }
}
