using System;
using System.Collections.Generic;
using MineNET.Blocks;
using MineNET.IO;
using MineNET.Worlds.Biomes;

namespace MineNET.Worlds.Generators.Flat
{
    public class FlatGenerator : GeneratorBase
    {
        private class FlatOptions
        {
            public int version;
            public BlockLayers blockLayers = new BlockLayers();
        }

        private class BlockLayers
        {
            public List<BlockLayer> layers = new List<BlockLayer>();
        }

        private struct BlockLayer
        {
            public Block block;
            public int height;
        }

        public override string Name => "FLAT";

        public string FlatOptionsString { get; private set; } = null;

        FlatOptions flatOption = null;

        public FlatGenerator()
        {
            this.FlatOptionsString = Server.Instance.ServerProperty.FlatGeneratorOptions;
            this.ReadFlatOption();
        }

        public FlatGenerator(string option)
        {
            this.FlatOptionsString = option;
            this.ReadFlatOption();
        }

        public override void GenerationBasicTerrain(Chunk chunk)
        {
            if (this.flatOption != null)
            {
                BlockLayers layers = this.flatOption.blockLayers;
                for (int i = 0; i < 16; ++i)//X
                {
                    for (int j = 0; j < 16; ++j)//Z
                    {
                        int y = 0;
                        for (int c = 0; c < layers.layers.Count; ++c)
                        {
                            BlockLayer layer = layers.layers[c];
                            for (int k = 0; k < layer.height; ++k)//Y
                            {
                                if (World.MAX_HEIGHT == y)
                                {
                                    Logger.Warn("World MaxHeight 256");
                                    break;
                                }
                                chunk.SetBlock(i, y, j, (byte) layer.block.ID);
                                chunk.SetMetadata(i, y, j, (byte) layer.block.Damage);
                                chunk.SetBiome(i, j, BiomeIDs.Plains);
                                y++;
                            }
                        }
                    }
                }
            }
            else
            {
                SubChunk flat = new SubChunk();
                for (int i = 0; i < 16; ++i)//X
                {
                    for (int j = 0; j < 16; ++j)//Z
                    {
                        for (int k = 0; k < 16; ++k)//Y
                        {
                            if (k == 0)
                            {
                                flat.SetBlock(i, k, j, 7);
                            }
                            else if (k == 1 || k == 2)
                            {
                                flat.SetBlock(i, k, j, 3);
                            }
                            else if (k == 3)
                            {
                                flat.SetBlock(i, k, j, 2);
                            }
                        }
                    }
                }

                chunk.SubChunks[0] = flat;
            }
        }

        public override void GenerationOnTheGroundNaturalObjects(Chunk chunk)
        {
            //Tree Test...
            //Vector2 offset = new Vector2(8, 8);
            //Vector3 pos = new Vector3((chunk.X << 4) + offset.X, 0, (chunk.Z << 4) + offset.Y);
            //pos.Y = chunk.GetBlockHighest(offset) + 1;
            //new OakTree().GenerationStruct(chunk.World, pos);
        }

        public override void GenerationUnderGroundNaturalObjects(Chunk chunk)
        {
            //throw new NotImplementedException();
        }

        public void ReadFlatOption()
        {
            if (!string.IsNullOrEmpty(this.FlatOptionsString))
            {
                try
                {
                    this.flatOption = new FlatOptions();
                    string[] args = this.FlatOptionsString.Split(';');
                    if (args.Length > 1)
                    {
                        this.flatOption.version = int.Parse(args[0]);

                        string[] layerData = args[1].Split(',');//support format <2*1:0> or <minecraft:stone:1>
                        BlockLayers layers = this.flatOption.blockLayers = new BlockLayers();
                        for (int i = 0; i < layerData.Length; ++i)
                        {
                            string[] blockData = layerData[i].Split('*');
                            if (blockData.Length == 2)
                            {
                                int layerCount = int.Parse(blockData[0]);
                                BlockLayer layer = new BlockLayer();
                                layer.block = Block.Get(blockData[1]);
                                layer.height = layerCount;
                                layers.layers.Add(layer);
                            }
                            else if (blockData.Length == 1)
                            {
                                BlockLayer layer = new BlockLayer();
                                layer.block = Block.Get(blockData[0]);
                                layer.height = 1;
                                layers.layers.Add(layer);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        if (layers.layers.Count == 0)
                        {
                            Logger.Warn("FlatOptions Error");
                            this.flatOption = null;
                        }
                    }
                    else
                    {
                        Logger.Warn("FlatOptions Error");
                        this.flatOption = null;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Logger.Warn("FlatOptions Error");
                    this.flatOption = null;
                }
            }
        }
    }
}
