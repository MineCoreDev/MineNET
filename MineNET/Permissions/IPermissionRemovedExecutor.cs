namespace MineNET.Permissions
{
    public interface IPermissionRemovedExecutor
    {
        void AttachmentRemoved(PermissionAttachment attachment);
    }
}
