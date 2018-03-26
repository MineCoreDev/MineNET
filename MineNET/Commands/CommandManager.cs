using System.Collections.Generic;
using MineNET.Commands.Data;
using MineNET.Commands.Defaults;
using MineNET.Entities.Players;
using MineNET.Events.PlayerEvents;
using MineNET.Events.ServerEvents;

namespace MineNET.Commands
{
    public sealed class CommandManager
    {
        public ICommandHandler CommandHandler { get; set; }

        public Dictionary<string, Command> CommandList { get; set; } = new Dictionary<string, Command>();
        public Dictionary<string, Command> CommandAliases { get; set; } = new Dictionary<string, Command>();

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
            CommandSender sender = new ConsoleSender();
            ServerCommandEventArgs serverCommandEvent = new ServerCommandEventArgs(sender, msg);
            ServerEvents.OnServerCommand(serverCommandEvent);
            if (serverCommandEvent.IsCancel)
            {
                return;
            }
            string[] args = serverCommandEvent.Message.Split(' ');
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

            this.CommandHandler.CommandHandle(sender, cmd, args);
        }

        public void HandlePlayerCommand(Player player, string msg)
        {
            PlayerCommandPreprocessEventArgs playerCommandPreprocessEvent = new PlayerCommandPreprocessEventArgs(player, msg);
            PlayerEvents.OnPlayerCommandPreprocess(playerCommandPreprocessEvent);
            if (playerCommandPreprocessEvent.IsCancel)
            {
                return;
            }
            string[] args = playerCommandPreprocessEvent.Message.Split(' ');
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
            if (this.CommandAliases.ContainsKey(cmd))
            {
                return this.CommandAliases[cmd];
            }
            return null;
        }

        public void RegisterCommand(Command command)
        {
            if (!this.CommandList.ContainsKey(command.Name))
            {
                this.CommandList.Add(command.Name, command);
            }

            if (command.Aliases != null)
            {
                for (int i = 0; i < command.Aliases.Length; ++i)
                {
                    if (!this.CommandAliases.ContainsKey(command.Aliases[i]))
                    {
                        this.CommandAliases.Add(command.Aliases[i], command);
                    }
                }
            }
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
            this.RegisterCommand(new GameModeCommand());
            this.RegisterCommand(new GiveCommand());
            this.RegisterCommand(new HelpCommand());
            this.RegisterCommand(new ListCommand());
            this.RegisterCommand(new MeCommand());
            this.RegisterCommand(new SayCommand());
            this.RegisterCommand(new StopCommand());
            this.RegisterCommand(new TellCommand());
        }
    }
}
