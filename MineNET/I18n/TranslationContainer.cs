using System.Globalization;

namespace MineNET.I18n.Interfaces
{
    public class TranslationContainer : TextContainer
    {
        public CultureInfo CultureInfo { get; }

        public TranslationContainer(string text, params object[] args) : base(Replace(text), args)
        {
            CultureInfo = CultureInfo.CurrentCulture;
        }

        public TranslationContainer(string text, CultureInfo cultureInfo, params object[] args) : base(Replace(text),
            args)
        {
            CultureInfo = cultureInfo;
        }

        private static string Replace(string text)
        {
            return StringManager.GetString(text);
        }
    }
}