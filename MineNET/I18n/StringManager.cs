using System.Resources;
using MineNET.I18n.Interfaces;

namespace MineNET.I18n
{
    public static class StringManager
    {
        private static ResourceManager _resourceManager;

        static StringManager()
        {
            _resourceManager =
                new ResourceManager("MineNET.I18n.Strings_ja-JP", typeof(StringManager).Assembly);
        }

        public static string GetString(string key)
        {
            try
            {
                return _resourceManager.GetString(key);
            }
            catch
            {
                return string.Format(GetString("i18n.key.notFound"), key);
            }
        }

        public static string GetString(string key, params object[] args)
        {
            try
            {
                return string.Format(GetString(key), args);
            }
            catch
            {
                return string.Format(GetString("i18n.key.notFound"), key);
            }
        }

        public static ITextContainer GetTextContainer(string key)
        {
            return new TextContainer(GetString(key));
        }

        public static ITextContainer GetTextContainer(string key, params object[] args)
        {
            return new TextContainer(GetString(key), args);
        }
    }
}