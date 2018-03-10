using System;
using System.Collections.Generic;
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
        public List<CommandParameterManager> CommandParameterManagers { get; } = new List<CommandParameterManager>();

        public Command()
        {
            //Server.Instance.CommandManager.RegisterCommand(this);
        }

        public abstract bool Execute(CommandSender sender, params string[] args);

        public void AddParameters(CommandParameterManager manager)
        {
            this.CommandParameterManagers.Add(manager);
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
