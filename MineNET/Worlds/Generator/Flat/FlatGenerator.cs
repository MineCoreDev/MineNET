using System.Collections.Generic;
using MineNET.Blocks;
using MineNET.Utils;

namespace MineNET.Worlds.Generator.Flat
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

        public override string Name
        {
            get
            {
                return "FLAT";
            }
        }

        public string FlatOptionsString { get; set; } = null;

        FlatOptions flatOption;

        public override void GenerationBasicTerrain(Chunk chunk)
        {
            if (this.ReadFlatOption())
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
                            for (int k = y; k < layer.height; ++k)//Y
                            {
                                chunk.SetBlock(i, k, j, (byte) layer.block.ID);
                                chunk.SetMetadata(i, k, j, (byte) layer.block.Damage);
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
            //throw new NotImplementedException();
        }

        public override void GenerationUnderGroundNaturalObjects(Chunk chunk)
        {
            //throw new NotImplementedException();
        }

        public bool ReadFlatOption()
        {
            if (!string.IsNullOrEmpty(this.FlatOptionsString))
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
                        Logger.Notice("FlatOptions Error");
                        return false;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
