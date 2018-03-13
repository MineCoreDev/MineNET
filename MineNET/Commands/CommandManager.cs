﻿using System.Collections.Generic;
using MineNET.Commands.Data;
using MineNET.Commands.Defaults;
using MineNET.Entities.Players;

namespace MineNET.Commands
{
    public sealed class CommandManager
    {
        public ICommandHandler CommandHandler { get; set; }

        public Dictionary<string, Command> CommandList { get; set; } = new Dictionary<string, Command>();

        public CommandManager()
        {
            this.Init();
        }

        private void Init()
        {
            this.RegisterCommands();
            this.CommandHandler = new CommandHandler(this);
        }

        public void HandleConsoleCommand(string msg)
        {
            string[] args = msg.Split(' ');
            string cmd = args[0];

            if (args.Length != 1)
            {
                List<string> tmp = new List<string>(args);
                tmp.RemoveAt(0);
                args = tmp.ToArray();
            }
            else
            {
                args = new string[0];
            }

            this.CommandHandler.CommandHandle(new ConsoleSender(), cmd, args);
        }

        public void HandlePlayerCommand(Player player, string msg)
        {
            string[] args = msg.Split(' ');
            string cmd = args[0];

            if (args.Length != 1)
            {
                List<string> tmp = new List<string>(args);
                tmp.RemoveAt(0);
                args = tmp.ToArray();
            }
            else
            {
                args = new string[0];
            }

            this.CommandHandler.CommandHandle(player, cmd, args);
        }

        public Command GetCommand(string cmd)
        {
            if (this.CommandList.ContainsKey(cmd))
            {
                return this.CommandList[cmd];
            }
            return null;
        }

        public void RegisterCommand(Command command)
        {
            if (!this.CommandList.ContainsKey(command.Name))
            {
                this.CommandList.Add(command.Name, command.Clone());
            }

            /*
            if (command.Aliases != null)
            {
                for (int i = 0; i < command.Aliases.Length; ++i)
                {
                    if (!this.CommandList.ContainsKey(command.Aliases[i]))
                    {
                        this.CommandList.Add(command.Aliases[i], command.Clone());
                    }
                }
            }
            */
        }

        public void RemoveCommand(string alias)
        {
            if (this.CommandList.ContainsKey(alias))
            {
                this.CommandList.Remove(alias);
            }
        }

        public void RemoveAllCommand()
        {
            this.CommandList.Clear();
        }

        private void RegisterCommands()
        {
            RegisterCommand(new StopCommand());
        }
    }
}
