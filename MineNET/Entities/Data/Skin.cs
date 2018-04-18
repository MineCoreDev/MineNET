namespace MineNET.Entities.Data
{
    public class Skin
    {
        public string SkinId { get; set; }
        public byte[] SkinData { get; set; }
        public byte[] CapeData { get; set; }
        public string GeometryName { get; set; }
        public string GeometryData { get; set; }

        public Skin(string skinId, byte[] skinData, byte[] capeData, string geometryName, string geometryData)
        {
            this.SkinId = skinId;
            this.SkinData = skinData;
            this.CapeData = capeData;
            this.GeometryName = geometryName;
            this.GeometryData = geometryData;
        }
    }
}
