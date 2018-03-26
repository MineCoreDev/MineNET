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
        public virtual string Permission { get; } = null;
        public virtual PlayerPermissions CommandPermission { get; } = PlayerPermissions.VISITOR;
        public virtual int Flag { get; } = 0;
        public virtual CommandOverload[] CommandOverloads { get; } = new CommandOverload[] { new CommandOverload(new CommandParameterString("args")) };

        public abstract bool Execute(CommandSender sender, params string[] args);

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
                //TODO:
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
