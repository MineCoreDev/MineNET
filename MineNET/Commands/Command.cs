using System;

namespace MineNET.Commands
{
    public abstract class Command : ICloneable<Command>
    {
        public abstract string Name { get; }
        public virtual string Description { get; } = "";
        public virtual string[] Aliases { get; } = null;
        public virtual string Permission { get; } = null;
        //TODO:
        public virtual int Flag { get; } = 0;
        //TODO:

        public abstract bool OnExecute(CommandSender sender, params string[] args);

        public Command Clone()
        {
            return (Command) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
