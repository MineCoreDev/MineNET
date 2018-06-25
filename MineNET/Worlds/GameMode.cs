namespace MineNET.Worlds
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
        public static GameMode FromIndex(int index)
        {
            if (index == 0)
            {
                return GameMode.Survival;
            }
            else if (index == 1)
            {
                return GameMode.Creative;
            }
            else if (index == 2)
            {
                return GameMode.Adventure;
            }
            else if (index == 3)
            {
                return GameMode.Spectator;
            }
            return GameMode.Survival;
        }

        public static int GetIndex(this GameMode gameMode)
        {
            return (int) gameMode;
        }

        public static string GetGameModeString(this GameMode gameMode)
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
