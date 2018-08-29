using System.Globalization;
using System.Resources;

namespace MineNET.Text
{
    public class CultureTextContainer : ITextContainer
    {
        public string Key { get; }
        public object[] Args { get; }
        public CultureInfo Culture { get; }

        public CultureTextContainer(string key)
        {
            this.Key = key;
            this.Culture = CultureInfo.CurrentCulture;
        }

        public CultureTextContainer(string key, params object[] args)
        {
            this.Key = key;
            this.Args = args;
            this.Culture = CultureInfo.CurrentCulture;
        }

        public CultureTextContainer(CultureInfo culture, string key, params object[] args)
        {
            this.Key = key;
            this.Args = args;
            this.Culture = culture;
        }

        public virtual string GetText()
        {
            ResourceManager manager = new ResourceManager("MineNET.Properties.Resources", typeof(CultureTextContainer).Assembly);
            return manager.GetString(this.Key, this.Culture);
        }
    }
}
