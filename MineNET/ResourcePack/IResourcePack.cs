namespace MineNET.ResourcePack
{
    public interface IResourcePack
    {
        string GetPackId();
        string GetPackVersion();
        ulong GetPackSize();
    }
}
