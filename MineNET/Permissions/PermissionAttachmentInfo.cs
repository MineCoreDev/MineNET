namespace MineNET.Permissions
{
    public class PermissionAttachmentInfo
    {
        public IPermissible Permissible { get; }
        public string Permission { get; }
        public PermissionAttachment Attachment { get; }
        public bool Value { get; }

        public PermissionAttachmentInfo(IPermissible permissible, string permission, PermissionAttachment attachment, bool value)
        {
            this.Permissible = permissible;
            this.Permission = permission;
            this.Attachment = attachment;
            this.Value = value;
        }
    }
}
