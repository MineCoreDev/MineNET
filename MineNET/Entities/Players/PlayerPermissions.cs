namespace MineNET.Entities.Players
{
    public enum PlayerPermissions
    {
        VISITOR = 0,
        MEMBER = 1,
        OPERATOR = 2,
        CUSTOM = 4
    }

    public static class PlayerPermissionExtensions
    {
        public static int GetIndex(this PlayerPermissions permission)
        {
            return (int) permission;
        }
    }
}
