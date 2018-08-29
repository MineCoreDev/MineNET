using System.Globalization;
using MineNET.Utils;

namespace MineNET.Text
{
    public sealed class TranslationContainer : CultureTextContainer
    {
        public TextFormat Header { get; } = new TextFormat('f');

        public TranslationContainer(string key) : base(key)
        {

        }

        public TranslationContainer(string key, params object[] args) : base(key, args)
        {

        }

        public TranslationContainer(CultureInfo culture, string key, params object[] args) : base(culture, key, args)
        {

        }

        public TranslationContainer(TextFormat header, string key, params object[] args) : base(key, args)
        {
            this.Header = header;
        }

        public TranslationContainer(TextFormat header, CultureInfo culture, string key, params object[] args) : base(
            culture, key, args)
        {
            this.Header = header;
        }

        public override string GetText()
        {
            return this.Header + base.GetText();
        }
    }
}
