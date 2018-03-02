namespace MineNET.Worlds.Data
{
    public class GameRule<T> : GameRuleBase
    {
        public T Value { set; get; }

        public GameRule(string ruleName, T value) : base(ruleName)
        {
            Value = value;
        }
    }
}
