using System.Resources;

namespace MineNET.Utils
{
    public sealed class LangManager
    {
        static string lang = "ja_JP";
        public static string Lang
        {
            get
            {
                return lang;
            }

            set
            {
                lang = value;
            }
        }

        static ResourceManager manager;
        public static ResourceManager Manager
        {
            get
            {
                if (manager == null)
                {
                    try
                    {
                        manager = new ResourceManager("MineNET.Resources.Lang." + lang, typeof(LangManager).Assembly);
                    }
                    catch (System.Exception exception)
                    {
                        Logger.Info(exception.Message);
                        Logger.Error($"NotFoundLang <{lang}>");
                        manager = new ResourceManager("MineNET.Resources.Lang.ja_JP", typeof(LangManager).Assembly);
                    }
                }

                return manager;
            }

            set
            {
                manager = value;
            }
        }

        public static string GetString(string key)
        {
            string msg = null;
            try
            {
                msg = Manager.GetString(key);
                if (msg != null)
                {
                    return msg;
                }
                throw new System.NullReferenceException(string.Format(Manager.GetString("server_language_key_error"), key));
            }
            catch (System.Exception exception)
            {
                Logger.Info(exception.ToString());
                Logger.Error($"NotFoundLang <{lang}>");
                manager = new ResourceManager("MineNET.Resources.Lang.ja_JP", typeof(LangManager).Assembly);
            }

            msg = Manager.GetString(key);
            if (msg != null)
            {
                return msg;
            }

            return string.Format(Manager.GetString("server_language_key_error"), key);
        }
    }
}
