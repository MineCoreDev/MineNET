namespace MineNET.GUI.Data
{
    public class Language
    {
        public string Name { get; }
        public string Code { get; }

        public Language(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }

        public override string ToString()
        {
            return $"{this.Name}({this.Code})";
        }
    }
}
