using System.Resources;

namespace MineNET.GUI
{
    public static class LanguageService
    {
        public static string LangCode { get; internal set; }
        public static ResourceManager Manager { get; private set; }

        internal static void LanguageServiceInit()
        {
            Manager = new ResourceManager("MineNET.GUI.Resources.Lang." + LangCode, typeof(LanguageService).Assembly);
        }

        public static string GetString(string key)
        {
            return Manager.GetString(key);
        }
    }
}
