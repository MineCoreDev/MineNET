using System.Resources;

namespace MineNET.Utils
{
    public struct ColorText
    {
        public const string CODE = "§";

        private char value;

        public static ColorText RED
        {
            get
            {
                return new ColorText('c');
            }
        }

        public ColorText(char value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return $"{CODE}{this.value}";
        }
    }

    public sealed class TranslationMessage
    {
        public string TranslationKey { get; }

        public object[] TranslationFills { get; } = null;

        public ColorText Header { get; } = new ColorText('f');

        public TranslationMessage(string key)
        {
            this.TranslationKey = key;
        }

        public TranslationMessage(string key, params object[] args)
        {
            this.TranslationKey = key;
            this.TranslationFills = args;
        }

        public TranslationMessage(ColorText header, string key, params object[] args)
        {
            this.Header = header;
            this.TranslationKey = key;
            this.TranslationFills = args;
        }
    }

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
