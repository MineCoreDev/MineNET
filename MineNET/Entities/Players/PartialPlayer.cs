using System.IO;
using System.Threading.Tasks;
using MineNET.Inventories;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds;
using MineNET.Worlds.Formats.WorldSaveFormats;

namespace MineNET.Entities.Players
{
    public partial class Player
    {
        private PlayerInventory inventory;

        public Player()
        {
            this.inventory = new PlayerInventory(this);

            this.ShowNameTag = true;
            this.AlwaysShowNameTag = true;

            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_CAN_CLIMB);

            this.LoadChunk();
        }

        private void LoadData()
        {
            string path = $"{Server.ExecutePath}\\players\\{this.Name}.dat";
            if (!File.Exists(path))
            {
                NBTIO.WriteGZIPFile(path, new CompoundTag(), NBTEndian.BIG_ENDIAN);
            }
            this.namedTag = NBTIO.ReadGZIPFile(path, NBTEndian.BIG_ENDIAN);
        }

        private int FixRadius(int radius)
        {
            int maxRequest = Server.ServerConfig.ViewDistance;
            if (radius > maxRequest) radius = maxRequest;
            return radius;
        }

        internal override void OnUpdate()
        {

        }

        public async void LoadChunk()
        {
            while (!Server.Instance.IsShutdown())
            {
                if (this.IsLogined)
                {
                    /*await Task.Run(() =>
                    {
                        World w = new World();
                        w.Format = new RegionWorldSaveFormat("test");
                        w.LoadChunk(this, ((int) this.X) >> 4, ((int) this.Z) >> 4, this.RequestChunkRadius);
                    });*/
                    Logger.Info(GetChunkVector().ToString());
                    await Task.Delay(10000);
                }
                await Task.Delay(1);
            }
        }
    }
}
