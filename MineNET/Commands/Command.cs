using System;
using System.Collections.Generic;
using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities.Players;
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

        public Player[] GetPlayerFromSelector(string selector, CommandSender sender)
        {
            List<Player> players = new List<Player>();
            if (selector == "@a")
            {
                return Server.Instance.GetPlayers();
            }
            else if (selector == "@e")
            {

            }
            else if (selector == "@p")
            {
                //TODO
                if (sender.IsPlayer)
                {
                    players.Add((Player) sender);
                }
            }
            else if (selector == "@r")
            {
                Player[] online = Server.Instance.GetPlayers();
                players.Add(online[new Random().Next(online.Length)]);
            }
            else if (selector == "@s")
            {
                if (sender.IsPlayer)
                {
                    players.Add((Player) sender);
                }
            }
            else
            {
                players.Add(Server.Instance.GetPlayer(selector));
            }
            return players.ToArray();
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
