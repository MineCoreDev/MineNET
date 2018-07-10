using System.Resources;

namespace MineNET
{
    public static class LanguageService
    {
        public static string LangCode { get; }
        public static ResourceManager Manager { get; private set; }

        static LanguageService()
        {
            LangCode = Server.Instance?.Config.Language ?? "ja_JP";
            Manager = new ResourceManager("MineNET.Resources.Lang." + LangCode, typeof(LanguageService).Assembly);
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
