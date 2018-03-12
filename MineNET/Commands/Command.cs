using System;
using System.Collections.Generic;
using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Utils;

namespace MineNET.Commands
{
    public abstract class Command : ICloneable<Command>
    {
        public abstract string Name { get; }
        public virtual string Description { get; } = "";
        public virtual string[] Aliases { get; } = null;
        public virtual PlayerPermissions Permission { get; } = PlayerPermissions.VISITOR;
        public virtual int Flag { get; } = 0;
        public List<CommandOverload> CommandOverloads { get; set; } = new List<CommandOverload>();

        public Command()
        {
            this.CommandOverloads.Add(new CommandOverload(new CommandParameterString("args")));
        }

        public abstract bool Execute(CommandSender sender, params string[] args);

        public void AddOverloads(params CommandOverload[] overloads)
        {
            for (int i = 0; i < overloads.Length; ++i)
            {
                this.CommandOverloads.Add(overloads[i]);
            }
        }

        public void RemoveAllOverloads()
        {
            this.CommandOverloads = new List<CommandOverload>();
        }

        internal string LangDescription()
        {
            return LangManager.GetString($"command_{this.Name}_description");
        }

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
