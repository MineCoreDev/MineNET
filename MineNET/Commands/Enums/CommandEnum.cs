namespace MineNET.Commands.Enums
{
    public class CommandEnum
    {
        public string Name { get; set; }
        public string[] Values { get; set; }

        public CommandEnum(string name, params string[] values)
        {
            this.Name = name;
            if (values == null)
            {
                values = new string[0];
            }
            this.Values = values;
        }
    }
}
