using System;
using System.Collections.Generic;
using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities;
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

        public CommandOverload[] EnptyCommandOverloads
        {
            get
            {
                return new CommandOverload[]
                {
                    new CommandOverload(),
                };
            }
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
                Player player = Server.Instance.GetPlayer(selector);
                if (player == null)
                {
                    sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.generic.player.notFound"));
                    return null;
                }
                else
                {
                    players.Add(player);
                }
            }

            if (players.Count < 1)
            {
                sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.generic.noTargetMatch"));
                return null;
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
                entities.Add(online[new Random().Next(online.Length)]);
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
                    sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.generic.player.notFound"));
                    return null;
                }
                else
                {
                    entities.Add(player);
                }
            }

            if (entities.Count < 1)
            {
                sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.generic.noTargetMatch"));
                return null;
            }

            return entities.ToArray();
        }

        public void SendTargetNotPlayerMessage(CommandSender sender)
        {
            sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.generic.targetNotPlayer"));
        }

        public void SendSyntaxMessage(CommandSender sender)
        {
            sender.SendMessage("構文エラー"); //TODO
        }

        public string GetTranslationDescription(string messageKey)
        {
            return LangManager.GetString(messageKey);
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
