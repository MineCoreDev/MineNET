namespace MineNET.Text
{
    public interface ITextContainer
    {
        string Key { get; }
        object[] Args { get; }

        string GetText();
    }
}