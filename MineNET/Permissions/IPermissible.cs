using System.Collections.Generic;
using MineNET.Plugins;

namespace MineNET.Permissions
{
    public interface IPermissible : IServerOperator
    {
        bool IsPermissionSet(string name);

        bool IsPermissionSet(Permission perm);

        bool HasPermission(string name);

        bool HasPermission(Permission perm);

        PermissionAttachment AddAttachment(IPlugin plugin, string name, bool value);

        PermissionAttachment AddAttachment(IPlugin plugin);

        void RemoveAttachment(PermissionAttachment attachment);

        void RecalculatePermissions();

        List<PermissionAttachmentInfo> GetEffectivePermissions();
    }
}
