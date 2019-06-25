using MineNET.I18n.Interfaces;

namespace MineNET.I18n
{
    public class TextContainer : ITextContainer
    {
        public string Text { get; }

        public TextContainer(string text, params object[] args)
        {
            Text = string.Format(text, args);
        }

        public static implicit operator string(TextContainer container)
        {
            return container.Text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}