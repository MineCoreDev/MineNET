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

        public bool TryGetCommand(string cmd, out Command command)
        {
            command = this.GetCommand(cmd);
            if (command != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RegisterCommand(params Command[] commands)
        {
            bool result = false;
            for (int i = 0; i < commands.Length; ++i)
            {
                result = this.RegisterCommand(commands[i]);
            }

            return result;
        }

        public bool RegisterCommand(Command command)
        {
            bool result = false;
            if (!this.CommandList.ContainsKey(command.Name))
            {
                this.CommandList.Add(command.Name, command);
                result = true;
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
                result = true;
            }

            return result;
        }

        public bool UnRegisterCommand(params string[] alias)
        {
            bool result = false;
            for (int i = 0; i < alias.Length; ++i)
            {
                result = this.UnRegisterCommand(alias[i]);
            }

            return result;
        }

        public bool UnRegisterCommand(string alias)
        {
            if (this.CommandList.ContainsKey(alias))
            {
                this.CommandList.Remove(alias);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UnRegisterCommand(params Command[] commands)
        {
            bool result = false;
            for (int i = 0; i < commands.Length; ++i)
            {
                result = this.UnRegisterCommand(commands[i]);
            }

            return result;
        }

        public bool UnRegisterCommand(Command command)
        {
            bool result = this.UnRegisterCommand(command.Name);

            if (command.Aliases != null)
            {
                for (int i = 0; i < command.Aliases.Length; ++i)
                {
                    if (!this.CommandAliases.ContainsKey(command.Aliases[i]))
                    {
                        this.CommandAliases.Remove(command.Aliases[i]);
                    }
                }
                result = true;
            }

            return result;
        }

        public void RemoveAllCommand()
        {
            this.CommandList.Clear();
        }

        private void RegisterCommands()
        {
            this.RegisterCommand(
                new EffectCommand(),
                new EnchantCommand(),
                new GameModeCommand(),
                new GiveCommand(),
                new HelpCommand(),
                new ListCommand(),
                new MeCommand(),
                new SayCommand(),
                new StopCommand(),
                new TellCommand(),
                new WhitelistCommand()
            );
        }
    }
}
