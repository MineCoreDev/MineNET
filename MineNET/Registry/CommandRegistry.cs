using MineNET.Commands;
using System.Collections.Generic;

namespace MineNET.Registry
{
    public class CommandRegistry : DictionaryRegistryBase<string, Command>
    {
        public Dictionary<string, Command> ToDictionary()
        {
            return this.Dictionary;
        }
    }
}
