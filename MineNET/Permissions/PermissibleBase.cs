using System.Collections.Generic;
using System.Linq;
using MineNET.Plugins;

namespace MineNET.Permissions
{
    public class PermissibleBase : IPermissible
    {
        private IServerOperator Opable { get; }
        private IPermissible Parent { get; }
        private List<PermissionAttachment> Attachments { get; } = new List<PermissionAttachment>();
        private Dictionary<string, PermissionAttachmentInfo> Permissions { get; } = new Dictionary<string, PermissionAttachmentInfo>();

        public PermissibleBase(IServerOperator opable)
        {
            this.Opable = opable;

            if (opable is IPermissible)
            {
                this.Parent = (IPermissible) opable;
            }

            this.RecalculatePermissions();
        }

        public bool Op
        {
            get
            {
                if (this.Opable == null)
                {
                    return false;
                }
                return this.Opable.Op;
            }

            set
            {
                if (this.Opable != null)
                {
                    this.Opable.Op = value;
                }
            }
        }

        public bool IsPermissionSet(string name)
        {
            return this.Permissions.ContainsKey(name.ToLower());
        }

        public bool IsPermissionSet(Permission perm)
        {
            return this.IsPermissionSet(perm.Name);
        }

        public bool HasPermission(string name)
        {
            name = name.ToLower();
            if (this.IsPermissionSet(name))
            {
                return this.Permissions[name].Value;
            }
            else
            {
                Permission perm = null; //TODO : PluginManager.GerPermission(name);

                if (perm != null)
                {
                    return Permission.GetValue(perm.DefaultValue, this.Op);
                }
                else
                {
                    return Permission.GetValue(Permission.DEFAULT_PERMISSION, this.Op);
                }
            }
        }

        public bool HasPermission(Permission perm)
        {
            string name = perm.Name.ToLower();
            if (this.IsPermissionSet(name))
            {
                return this.Permissions[name].Value;
            }
            return Permission.GetValue(perm.DefaultValue, this.Op);
        }

        public PermissionAttachment AddAttachment(IPlugin plugin, string name, bool value)
        {
            PermissionAttachment result = this.AddAttachment(plugin);
            result.SetPermission(name, value);

            this.RecalculatePermissions();
            return result;
        }

        public PermissionAttachment AddAttachment(IPlugin plugin)
        {
            PermissionAttachment result = new PermissionAttachment(plugin, this.Parent);
            this.Attachments.Add(result);

            this.RecalculatePermissions();
            return result;
        }

        public void RemoveAttachment(PermissionAttachment attachment)
        {
            if (this.Attachments.Contains(attachment))
            {
                this.Attachments.Remove(attachment);
                IPermissionRemovedExecutor ex = attachment.Removed;
                if (ex != null)
                {
                    ex.AttachmentRemoved(attachment);
                }
                this.RecalculatePermissions();
            }
        }

        public void RecalculatePermissions()
        {
            this.ClearPermissions();
            Dictionary<string, Permission> defaults = new Dictionary<string, Permission>(); //TODO : PluginManager
            //TODO : PluginManager.SubscribeToDefaltPerms();

            foreach (Permission perm in defaults.Values)
            {
                string name = perm.Name;
                this.Permissions[name] = new PermissionAttachmentInfo(this.Parent != null ? this.Parent : this, name, null, true);
                //TODO : PluginManager.SubscribeToDefaltPerms();
                this.CalculateChildPermissions(perm.Children, false, null);
            }

            for (int i = 0; i < this.Attachments.Count; ++i)
            {
                PermissionAttachment attachment = this.Attachments[i];
                this.CalculateChildPermissions(attachment.GetPermissions(), false, attachment);
            }
        }

        public void ClearPermissions()
        {
            //TODO : PluginManager.UnsubscribeFromDefaultPerms();

            this.Permissions.Clear();
        }

        private void CalculateChildPermissions(Dictionary<string, bool> children, bool invert, PermissionAttachment attachment)
        {
            foreach (string name in children.Keys)
            {
                Permission perm = null; //TODO : PluginManager.GetPermission(name);
                bool value = (children[name] ^ invert);
                this.Permissions[name] = new PermissionAttachmentInfo(this.Parent != null ? this.Parent : this, name, attachment, value);
                //TODO : PluginManager.SubscribeToPermission();

                if (perm != null)
                {
                    this.CalculateChildPermissions(perm.Children, !value, attachment);
                }
            }
        }

        public List<PermissionAttachmentInfo> GetEffectivePermissions()
        {
            return this.Permissions.Values.ToArray().ToList();
        }
    }
}
