using System.Resources;

namespace MineNET
{
    public static class LanguageService
    {
        public static string LangCode { get; }
        public static ResourceManager Manager { get; private set; }

        static LanguageService()
        {
            LangCode = "ja_JP";
            Manager = new ResourceManager("MineNET.Resources.Lang." + LangCode, typeof(LanguageService).Assembly);
        }

        public static string GetString(string key)
        {
            return Manager.GetString(key);
        }
    }
}
