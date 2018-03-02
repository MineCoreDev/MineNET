using MineNET.NBT.Tags;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds;
using System;
using System.Collections.Generic;

namespace MineNET.BlockEntities
{
    public abstract class BlockEntity : ICloneable<BlockEntity>, IPosition
    {
        private static Dictionary<string, Type> registry = new Dictionary<string, Type>();

        public CompoundTag namedTag;

        public static void RegisterBlockEntity(BlockEntity block)
        {
            Type type = block.GetType();
            BlockEntity.registry[block.Name] = type;
        }

        public static BlockEntity CreateBlockEntity(string name, World world, CompoundTag nbt)
        {
            if (!BlockEntity.registry.ContainsKey(name))
            {
                return null;
            }
            Type type = BlockEntity.registry[name];
            BlockEntity entity = (BlockEntity) System.Activator.CreateInstance(type, new Object[] { world, nbt });
            return entity;
        }

        public BlockEntity(Position position, CompoundTag nbt = null)
        {
            this.X = position.X;
            this.Y = position.Y;
            this.Z = position.Z;
            this.World = position.World;
            if (nbt == null)
            {
                nbt = new CompoundTag();
                nbt.PutInt("x", (int) this.X);
                nbt.PutInt("y", (int) this.Y);
                nbt.PutInt("z", (int) this.Z);

            }
        }

        public BlockEntity(World world, CompoundTag nbt)
        {
            this.World = world;

            this.X = nbt.GetInt("x");
            this.Y = nbt.GetInt("y");
            this.Z = nbt.GetInt("z");

            this.namedTag = nbt;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public World World { get; set; }

        public abstract string Name
        {
            get;
        }

        public BlockEntity Clone()
        {
            return (BlockEntity) Clone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        public void OnUpdate()
        {

        }
    }
}
