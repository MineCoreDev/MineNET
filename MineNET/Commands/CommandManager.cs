using System.Collections.Generic;
using MineNET.Commands.Data;
using MineNET.Commands.Defaults;
using MineNET.Entities;

namespace MineNET.Commands
{
    public sealed class CommandManager
    {
        ICommandHandler commandHandler;
        public ICommandHandler CommandHandler
        {
            get
            {
                return commandHandler;
            }

            set
            {
                commandHandler = value;
            }
        }

        Dictionary<string, Command> commandList = new Dictionary<string, Command>();

        public CommandManager()
        {
            Init();
        }

        void Init()
        {
            RegisterCommands();
            commandHandler = new CommandHandler(this);
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

            commandHandler.CommandHandle(new ConsoleSender(), cmd, args);
        }

        internal void HandlePlayerCommand(Player player, string msg)
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

            commandHandler.CommandHandle(player, cmd, args);
        }

        public Command GetCommand(string cmd)
        {
            if (commandList.ContainsKey(cmd))
            {
                return commandList[cmd];
            }
            return null;
        }

        public void RegisterCommand(Command command)
        {
            if (!commandList.ContainsKey(command.Alias))
            {
                commandList.Add(command.Alias, (Command) command.Clone());
            }

            for (int i = 0; i < command.SubAlias.Length; ++i)
            {
                if (!commandList.ContainsKey(command.SubAlias[i]))
                {
                    commandList.Add(command.SubAlias[i], (Command) command.Clone());
                }
            }
        }

        public void RemoveCommand(string alias)
        {
            if (commandList.ContainsKey(alias))
            {
                commandList.Remove(alias);
            }
        }

        public void RemoveAllCommand()
        {
            commandList.Clear();
        }

        void RegisterCommands()
        {
            RegisterCommand(new StopCommand());
        }
    }
}
