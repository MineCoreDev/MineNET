using System;
using MineNET.Utils;

namespace MineNET.Commands
{
    public abstract class Command : ICloneable
    {
        public abstract string Name
        {
            get;
        }

        public abstract string Description
        {
            get;
        }

        public abstract string Alias
        {
            get;
        }

        public virtual string[] SubAlias
        {
            get
            {
                return new string[0];
            }
        }

        public abstract bool Execute(CommandSender sender, params string[] args);

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        internal string LangDescription()
        {
            return LangManager.GetString($"command_{this.Name}_description");
        }
    }
}
