namespace MineNET.Entities.Data
{
    public class Skin
    {
        public Skin(string skinId, string skinData, string capeData, string geometryName, string geometryData)
        {
            this.SkinId = skinId;
            this.SkinData = skinData;
            this.CapeData = capeData;
            this.GeometryName = geometryName;
            this.GeometryData = geometryData;
        }

        public string SkinId { get; set; }
        public string SkinData { get; set; }
        public string CapeData { get; set; }
        public string GeometryName { get; set; }
        public string GeometryData { get; set; }
    }
}
