namespace MineNET.Worlds.Formats.WorldDataFormats
{
    public interface IWorldDataFormat
    {
        void Save(World world);
        void Load(World world);
        void Create(World world);
    }
}
