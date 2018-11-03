using System;
using System.Collections.Generic;
using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Text;
using MineNET.Utils;
using MineNET.Values;
using Random = MineNET.Utils.Random;

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
        public bool IsAliasesCommand { get; set; }

        public virtual CommandOverload[] CommandOverloads { get; } = new CommandOverload[]
            {new CommandOverload(new CommandParameterString("args"))};

        public abstract bool OnExecute(CommandSender sender, string command, params string[] args);

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
                if (sender is IPosition)
                {
                    IPosition originSender = (IPosition) sender;
                    Player[] near = originSender.World.GetPlayers();
                    Player min = null;
                    float minDist = int.MaxValue;
                    for (int i = 0; i < near.Length; ++i)
                    {
                        float nowDist = Vector3.Distance(near[i].ToVector3(), originSender.ToVector3());
                        if (minDist > nowDist)
                        {
                            minDist = nowDist;
                            min = near[i];
                        }
                    }

                    players.Add(min);
                }
            }
            else if (selector == "@r")
            {
                Player[] online = Server.Instance.GetPlayers();
                players.Add(online[Random.GetRandom().Next(online.Length)]);
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
                if (sender is IPosition)
                {
                    IPosition originSender = (IPosition) sender;
                    Player[] near = originSender.World.GetPlayers();
                    Player min = null;
                    float minDist = int.MaxValue;
                    for (int i = 0; i < near.Length; ++i)
                    {
                        float nowDist = Vector3.Distance(near[i].ToVector3(), originSender.ToVector3());
                        if (minDist > nowDist)
                        {
                            minDist = nowDist;
                            min = near[i];
                        }
                    }

                    entities.Add(min);
                }
            }
            else if (selector == "@r")
            {
                Player[] online = Server.Instance.GetPlayers();
                entities.Add(online[Random.GetRandom().Next(online.Length)]);
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

        public void SendSyntaxErrorMessage(CommandSender sender, string command, string[] args, int error)
        {
            string errorArgStr = args[error];
            string commandStr = $"/{command} ";
            string lastStr = "";
            for (int i = 0; i < args.Length; i++)
            {
                if (i == error)
                {
                    continue;
                }
                else if (i > error)
                {
                    lastStr += args[i] + " ";
                }
                else
                {
                    commandStr += args[i] + " ";
                }
            }

            if (lastStr.Length > 0)
            {
                lastStr = lastStr.Remove(lastStr.Length - 1);
            }

            sender.SendMessage(new TranslationContainer(TextFormat.RED, "commands.generic.syntax", commandStr, errorArgStr, lastStr));
        }

        public void SendLengthErrorMessage(CommandSender sender, string command, string[] args, int len)
        {
            string errorArgStr = "";
            string commandStr = $"/{command} ";
            for (int i = 0; i < args.Length; i++)
            {
                commandStr += args[i] + " ";
            }

            sender.SendMessage(new TranslationContainer(TextFormat.RED, "commands.generic.syntax", commandStr, errorArgStr, ""));
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}