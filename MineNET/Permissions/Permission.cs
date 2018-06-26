using System.Collections.Generic;

namespace MineNET.Permissions
{
    public class Permission
    {
        public const string OP = "op";
        public const string NOT_OP = "notop";
        public const string TRUE = "true";
        public const string FALSE = "false";

        public const string DEFAULT_PERMISSION = Permission.OP;

        public static bool GetValue(string perm, bool op)
        {
            switch (perm)
            {
                case Permission.TRUE:
                    return true;

                case Permission.FALSE:
                    return false;

                case Permission.OP:
                    return op;

                case Permission.NOT_OP:
                    return !op;

                default:
                    return false;
            }
        }

        public string Name { get; }
        public string Description { get; set; }
        public string DefaultValue { get; set; } = Permission.DEFAULT_PERMISSION;
        public Dictionary<string, bool> Children { get; set; } = new Dictionary<string, bool>();

        public Permission(string name, string description = "", string defaultValue = "", Dictionary<string, bool> children = null)
        {
            this.Name = name;
            this.Description = description;
            this.DefaultValue = this.DefaultValue;
            this.Children = children;
        }

        public List<IPermissible> Permissibles
        {
            get
            {
                return null; //TODO : PluginManager.GetPermissionSubscriptions(this.Name);
            }
        }

        public void RecalculatePermissibles()
        {
            List<IPermissible> perms = this.Permissibles;
            // TODO : PluginManager.RecalculatePermissionDefaults(this);

            for (int i = 0; i < perms.Count; ++i)
            {
                perms[i].RecalculatePermissions();
            }
        }

        public void AddParent(Permission perm, bool value)
        {
            perm.Children[this.Name] = value;
            perm.RecalculatePermissibles();
        }
    }
}
