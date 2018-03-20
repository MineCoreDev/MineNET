using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MineNET.Blocks;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds.Formats.WorldSaveFormats;

namespace MineNET.Worlds
{
    public class World
    {
        public Dictionary<Tuple<int, int>, Chunk> chunks = new Dictionary<Tuple<int, int>, Chunk>();

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

        public IEnumerable<Chunk> LoadChunks(Vector2 chunkXZ, int radius)
        {
            lock (chunks)
            {
                Dictionary<Tuple<int, int>, double> newOrders = new Dictionary<Tuple<int, int>, double>();

                double radiusSquared = Math.Pow(radius, 2);
                Vector2 center = chunkXZ;

                for (int x = -radius; x <= radius; ++x)
                {
                    for (int z = -radius; z <= radius; ++z)
                    {
                        int distance = (x * x) + (z * z);
                        if (distance > radiusSquared)
                        {
                            continue;
                        }
                        int chunkX = (int) (x + center.X);
                        int chunkZ = (int) (z + center.Y);
                        Tuple<int, int> index = new Tuple<int, int>(chunkX, chunkZ);
                        newOrders[index] = distance;
                    }
                }

                foreach (Tuple<int, int> chunkKey in chunks.Keys.ToArray())
                {
                    if (!newOrders.ContainsKey(chunkKey))
                    {
                        //this.Format.SetChunk(chunks[chunkKey]);//TODO:
                        chunks.Remove(chunkKey);
                    }
                }

                foreach (var pair in newOrders.OrderBy(pair => pair.Value))
                {
                    if (chunks.ContainsKey(pair.Key)) continue;

                    Chunk chunk = null;
                    try
                    {
                        chunk = this.Format.GetChunk(pair.Key.Item1, pair.Key.Item2);
                        chunks.Add(pair.Key, chunk);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                    }

                    yield return chunk;
                }
            }
        }
    }
}
