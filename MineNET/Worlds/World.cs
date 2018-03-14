using System.IO;
using MineNET.Blocks;
using MineNET.Entities.Players;
using MineNET.Values;
using MineNET.Worlds.Formats.WorldSaveFormats;

namespace MineNET.Worlds
{
    public class World
    {
        public static World CreateWorld(string worldName)
        {
            return World.CreateWorld(worldName, new MineNETWorldSaveFormat());
        }

        public static World CreateWorld(string worldName, IWorldSaveFormat format)
        {
            World world = new World();
            world.Format = format;
            world.Format.WorldData.Create(world);
            world.Format.WorldData.Save(world);

            return world;
        }

        public static World LoadWorld(string worldName)
        {
            return World.CreateWorld(worldName, new MineNETWorldSaveFormat());
        }

        public static World LoadWorld(string worldName, IWorldSaveFormat format)
        {
            if (World.Exists(worldName))
            {
                World world = new World();
                world.Format = format;
                world.Format.WorldData.Load(world);

                return world;
            }
            else
            {
                return CreateWorld(worldName);
            }
        }

        public static bool Exists(string worldName)
        {
            if (Directory.Exists($"{Server.ExecutePath}\\worlds\\{worldName}"))
            {
                if (File.Exists($"{Server.ExecutePath}\\worlds\\{worldName}\\level.dat"))
                {
                    return true;
                }
            }

            return false;
        }

        public IWorldSaveFormat Format { get; set; }
        public string Name { get; }

        public Block GetBlock(Vector3 pos)
        {
            //TODO
            return Block.Get(0);
        }

        public void SetBlock(Vector3 pos, Block block)
        {
            //TODO
        }

        public void LoadChunk(Player player, int chunkX, int chunkZ, int requestRadius)
        {
            for (int i = (chunkX - requestRadius); i < (chunkX + requestRadius); ++i)
            {
                for (int j = (chunkZ - requestRadius); j < (chunkZ - requestRadius); ++j)
                {
                    this.Format.GetChunk(i, j).SendChunk(player);
                }
            }
        }
    }
}
