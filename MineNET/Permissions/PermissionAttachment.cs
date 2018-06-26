using System.Collections.Generic;
using MineNET.Plugins;

namespace MineNET.Permissions
{
    public class PermissionAttachment
    {
        public IPermissionRemovedExecutor Removed { get; set; }
        private Dictionary<string, bool> Permissions { get; } = new Dictionary<string, bool>();
        public IPermissible Permissible { get; }
        public IPlugin Plugin { get; }

        public PermissionAttachment(IPlugin plugin, IPermissible permissible)
        {
            this.Permissible = permissible;
            this.Plugin = plugin;
        }

        public Dictionary<string, bool> GetPermissions()
        {
            return new Dictionary<string, bool>(this.Permissions);
        }

        public void SetPermission(string name, bool value)
        {
            this.Permissions[name.ToLower()] = value;
            this.Permissible.RecalculatePermissions();
        }

        public void SetPermission(Permission perm, bool value)
        {
            this.SetPermission(perm.Name, value);
        }

        public void UnsetPermission(string name)
        {
            this.Permissions.Remove(name.ToLower());
            this.Permissible.RecalculatePermissions();
        }

        public void UnsetPermission(Permission perm)
        {
            this.UnsetPermission(perm.Name);
        }

        public void Remove()
        {
            this.Permissible.RemoveAttachment(this);
        }
    }
}
