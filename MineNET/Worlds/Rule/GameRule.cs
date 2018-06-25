namespace MineNET.Worlds.Rule
{
    public class GameRule<T> : GameRuleBase
    {
        public T Value { set; get; }

        public GameRule(string ruleName, T value) : base(ruleName)
        {
            this.Value = value;
        }
    }
}
