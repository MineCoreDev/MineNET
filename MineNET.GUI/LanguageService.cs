using System.Resources;

namespace MineNET.GUI
{
    public static class LanguageService
    {
        public static string LangCode { get; internal set; }
        public static ResourceManager Manager { get; private set; }

        static LanguageService()
        {
            Manager = new ResourceManager("MineNET.GUI.Resources.Lang.ja_JP", typeof(LanguageService).Assembly);
        }

        internal static void LanguageServiceInit()
        {
            Manager = new ResourceManager("MineNET.GUI.Resources.Lang." + LangCode, typeof(LanguageService).Assembly);
        }

        public static string GetString(string key)
        {
            return Manager.GetString(key);
        }

        public static string GetFormatString(string key, params object[] args)
        {
            return string.Format(Manager.GetString(key), args);
        }
    }
}
