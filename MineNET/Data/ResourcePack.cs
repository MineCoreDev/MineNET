namespace MineNET.Data
{
    public interface ResourcePack
    {
        string GetPackName();
        string GetPackId();
        string GetPackVersion();
        int GetPackSize();
        byte[] GetSha256();
        byte[] GetPackChunk(int off, int len);
    }
}
