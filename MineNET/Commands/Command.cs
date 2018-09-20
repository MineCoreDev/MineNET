using System;
using System.Collections.Generic;
using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Text;
using MineNET.Utils;

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
                players.Add(online[new System.Random().Next(online.Length)]);
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
                Player player = Server.Instance.GetPlayer(selector);
                if (player == null)
                {
                    return null;
                }
                else
                {
                    players.Add(player);
                }
            }

            return players.ToArray();
        }

        public Entity[] GetEntityFromSelector(string selector, CommandSender sender)
        {
            List<Entity> entities = new List<Entity>();
            if (selector == "@a")
            {
                return Server.Instance.GetPlayers();
            }
            else if (selector == "@e")
            {
                //TODO
            }
            else if (selector == "@p")
            {
                //TODO:
                if (sender.IsPlayer)
                {
                    entities.Add((Player) sender);
                }
            }
            else if (selector == "@r")
            {
                Player[] online = Server.Instance.GetPlayers();
                entities.Add(online[new System.Random().Next(online.Length)]);
            }
            else if (selector == "@s")
            {
                if (sender.IsPlayer)
                {
                    entities.Add((Player) sender);
                }
            }
            else
            {
                Player player = Server.Instance.GetPlayer(selector);
                if (player == null)
                {
                    return null;
                }
                else
                {
                    entities.Add(player);
                }
            }

            return entities.ToArray();
        }

        public void SendNoTargetMessage(CommandSender sender)
        {
            sender.SendMessage(new TranslationContainer(TextFormat.RED, "commands.generic.noTargetMatch"));
        }

        public void SendTargetNotPlayerMessage(CommandSender sender)
        {
            sender.SendMessage(new TranslationContainer(TextFormat.RED, "commands.generic.targetNotPlayer"));
        }

        public void SendSyntaxMessage(CommandSender sender)
        {
            sender.SendMessage(new TranslationContainer(TextFormat.RED, "commands.generic.syntax")); //TODO
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
