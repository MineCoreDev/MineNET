namespace MineNET.Worlds.Data
{
    public abstract class GameRuleBase
    {
        public string Name { get; set; }

        public GameRuleBase(string ruleName)
        {
            Name = ruleName;
        }
    }
}
