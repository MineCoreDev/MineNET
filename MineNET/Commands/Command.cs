using System;
using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;

namespace MineNET.Commands
{
    public abstract class Command : ICloneable<Command>
    {
        public abstract string Name { get; }
        public virtual string Description { get; } = "";
        public virtual string[] Aliases { get; } = null;
        public virtual string Permission { get; } = null;
        public virtual PlayerPermissions PermissionLevel { get; } = PlayerPermissions.VISITOR;
        public virtual int Flag { get; } = 0;
        public virtual CommandOverload[] CommandOverloads { get; } = new CommandOverload[] { new CommandOverload(new CommandParameterString("args")) };

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
