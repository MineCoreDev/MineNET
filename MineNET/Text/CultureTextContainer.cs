using System.Globalization;
using System.Resources;

namespace MineNET.Text
{
    public class CultureTextContainer : ITextContainer
    {
        public string Key { get; }
        public object[] Args { get; } = null;
        public CultureInfo Culture { get; }

        public CultureTextContainer(string key)
        {
            this.Key = key;
            this.Culture = Properties.Resources.Culture;
        }

        public CultureTextContainer(string key, params object[] args)
        {
            this.Key = key;
            this.Args = args;
            this.Culture = Properties.Resources.Culture;
        }

        public CultureTextContainer(CultureInfo culture, string key, params object[] args)
        {
            this.Key = key;
            this.Args = args;
            this.Culture = culture;
        }

        public virtual string GetText()
        {
            ResourceManager manager = Properties.Resources.ResourceManager;
            string value = manager.GetString(Key_Dot_Replace(this.Key), this.Culture);
            if (this.Args != null)
            {
                if (value != null)
                {
                    if (this.Args.Length > 0)
                    {
                        value = string.Format(value, this.Args);
                    }
                }
                else
                {
                    value = "Null";
                }
            }

            return value;
        }

        private string Key_Dot_Replace(string key)
        {
            if (key.Contains("."))
            {
                return key.Replace('.', '_');
            }

            return key;
        }
    }
}
