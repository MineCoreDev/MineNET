using MineNET.Text;

namespace MineNET.Utils
{
    public static class TextContainerUtils
    {
        public static string ToString(string key)
        {
            return new CultureTextContainer(key).GetText();
        }

        public static string ToString(string key, object[] args)
        {
            return new CultureTextContainer(key, args).GetText();
        }
    }
}