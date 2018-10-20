using System;
using MineNET.Commands.Defaults;

namespace MineNET.Commands
{
    public class CommandManager : IDisposable
    {
        public ICommandHandler CommandHandler { get; set; }

        public CommandManager()
        {
            this.CommandHandler = new CommandHandler(this);
            this.RegisterDefaultCommands();
        }

        public Command GetCommand(string cmd)
        {
            if (MineNET_Registries.Command.ContainsKey(cmd))
            {
                return MineNET_Registries.Command[cmd];
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

        public bool RegisterCommand(Command cmd)
        {
            bool result = false;
            if (!MineNET_Registries.Command.ContainsKey(cmd.Name))
            {
                MineNET_Registries.Command.Add(cmd.Name, cmd);
                result = true;
            }

            if (cmd.Aliases != null)
            {
                for (int i = 0; i < cmd.Aliases.Length; ++i)
                {
                    if (!MineNET_Registries.Command.ContainsKey(cmd.Aliases[i]))
                    {
                        MineNET_Registries.Command.Add(cmd.Aliases[i], cmd);
                    }
                }

                result = true;
            }

            return result;
        }

        public bool UnRegisterCommand(string cmdName)
        {
            if (MineNET_Registries.Command.ContainsKey(cmdName))
            {
                MineNET_Registries.Command.Remove(cmdName);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UnRegisterCommand(Command cmd)
        {
            bool result = this.UnRegisterCommand(cmd.Name);

            if (cmd.Aliases != null)
            {
                for (int i = 0; i < cmd.Aliases.Length; ++i)
                {
                    if (!MineNET_Registries.Command.ContainsKey(cmd.Aliases[i]))
                    {
                        MineNET_Registries.Command.Remove(cmd.Aliases[i]);
                    }
                }

                result = true;
            }

            return result;
        }

        private void RegisterDefaultCommands()
        {
            this.RegisterCommand(new EffectCommand());
            this.RegisterCommand(new EnchantCommand());
            this.RegisterCommand(new GameModeCommand());
            this.RegisterCommand(new GiveCommand());
            this.RegisterCommand(new ListCommand());
            this.RegisterCommand(new MeCommand());
            this.RegisterCommand(new SayCommand());
            this.RegisterCommand(new StopCommand());
            this.RegisterCommand(new TellCommand());
            this.RegisterCommand(new XpCommand());
        }

        public void Dispose()
        {
            this.CommandHandler = null;
        }
    }
}