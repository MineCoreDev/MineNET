namespace MineNET.Worlds.Rule
{
    public abstract class GameRuleBase
    {
        public string Name { get; set; }

        public GameRuleBase(string ruleName)
        {
            this.Name = ruleName;
        }
    }
}
