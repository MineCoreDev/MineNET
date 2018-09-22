﻿using System.Reflection;
using MineNET.Blocks;
using MineNET.Items;

namespace MineNET.Init
{
    public sealed class ItemInit
    {
        public static ItemInit In { get; private set; }

        public ItemInit()
        {
            ItemInit.In = this;
            this.Init();

            Item.LoadCreativeItems();
        }

        public void Init()
        {
            foreach (int key in MineNET_Registries.Block.Keys)
            {
                if (MineNET_Registries.Block.TryGetValue(key, out Block block))
                {
                    this.Add(new ItemBlock(block));
                }
            }

            FieldInfo[] fields = new ItemIDs().GetType().GetFields(); //TODO
            for (int i = 0; i < fields.Length; ++i)
            {
                FieldInfo field = fields[i];
                this.Add(new Item(field.Name, (int) field.GetValue(null)));
            }
        }

        public void Add(Item item)
        {
            MineNET_Registries.Item.Add(item.ID, item);
        }

        public void Dispose()
        {
            ItemInit.In = null;
        }
    }
}
