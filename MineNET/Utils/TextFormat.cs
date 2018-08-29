namespace MineNET.Utils
{
    public struct TextFormat
    {
        public const string Code = "§";

        private char value;

        public static TextFormat BLACK => new TextFormat('0');
        public static TextFormat DARK_BLUE => new TextFormat('1');
        public static TextFormat DARK_GREEN => new TextFormat('2');
        public static TextFormat DARK_AQUA => new TextFormat('3');
        public static TextFormat DARK_RED => new TextFormat('4');
        public static TextFormat DARK_PURPLE => new TextFormat('5');
        public static TextFormat GOLD => new TextFormat('6');
        public static TextFormat GRAY => new TextFormat('7');
        public static TextFormat BLACKDARK_GRAY => new TextFormat('8');
        public static TextFormat BLUE => new TextFormat('9');
        public static TextFormat GREEN => new TextFormat('a');
        public static TextFormat AQUA => new TextFormat('b');
        public static TextFormat RED => new TextFormat('c');
        public static TextFormat LIGHT_PURPLE => new TextFormat('d');
        public static TextFormat YELLOW => new TextFormat('e');
        public static TextFormat WHITE => new TextFormat('f');
        public static TextFormat OBFUSCATED => new TextFormat('k');
        public static TextFormat BOLD => new TextFormat('l');
        public static TextFormat STRIKETHROUGH => new TextFormat('m');
        public static TextFormat UNDERLINE => new TextFormat('n');
        public static TextFormat ITALIC => new TextFormat('o');
        public static TextFormat RESET => new TextFormat('r');

        public TextFormat(char value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return $"{Code}{this.value}";
        }
    }
}
