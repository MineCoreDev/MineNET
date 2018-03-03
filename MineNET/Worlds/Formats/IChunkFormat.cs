namespace MineNET.Worlds.Formats
{
    public interface IChunkFormat
    {
        string NetworkSerialize();
        string Serialize();
        string Deserialize();
    }
}
