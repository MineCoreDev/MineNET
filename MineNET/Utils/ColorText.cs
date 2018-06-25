namespace MineNET.Utils
{
    public struct ColorText
    {
        public const string Code = "§";

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
            return $"{Code}{this.value}";
        }
    }
}
