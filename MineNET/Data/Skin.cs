namespace MineNET.Data
{
    public class Skin
    {
        public Skin(string skinId, string skinData, string capeData, string geometryName, string geometryData)
        {
            this.skinId = skinId;
            this.skinData = skinData;
            this.capeData = capeData;
            this.geometryName = geometryName;
            this.geometryData = geometryData;
        }

        string skinId;
        public string SkinId
        {
            get
            {
                return this.skinId;
            }

            set
            {
                this.skinId = value;
            }
        }

        string skinData;
        public string SkinData
        {
            get
            {
                return this.skinData;
            }

            set
            {
                this.skinData = value;
            }
        }

        string capeData;
        public string CapeData
        {
            get
            {
                return this.capeData;
            }

            set
            {
                this.capeData = value;
            }
        }

        string geometryName;
        public string GeometryName
        {
            get
            {
                return this.geometryName;
            }

            set
            {
                this.geometryName = value;
            }
        }

        string geometryData;
        public string GeometryData
        {
            get
            {
                return this.geometryData;
            }

            set
            {
                this.geometryData = value;
            }
        }
    }
}
