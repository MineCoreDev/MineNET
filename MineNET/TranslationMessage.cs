using MineNET.Utils;

namespace MineNET
{
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
}
