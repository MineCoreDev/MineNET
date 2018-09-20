using MineNET.Commands.Defaults;
using System;

namespace MineNET.Commands
{
    public class CommandManager : IDisposable
    {
        #region Property & Field
        public ICommandHandler CommandHandler { get; set; }
        #endregion

        #region Ctor
        public CommandManager()
        {
            this.CommandHandler = new CommandHandler(this);
            this.RegisterDefaultCommands();
        }
        #endregion

        #region Get Command Method
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
        #endregion

        #region Register / UnRegister Command Method
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
        #endregion

        #region Default Command Register Method
        private void RegisterDefaultCommands()
        {
            this.RegisterCommand(new StopCommand());
            this.RegisterCommand(new GiveCommand());
            this.RegisterCommand(new GameModeCommand());
        }
        #endregion

        #region Dispose Method
        public void Dispose()
        {
            this.CommandHandler = null;
        }
        #endregion
    }
}
