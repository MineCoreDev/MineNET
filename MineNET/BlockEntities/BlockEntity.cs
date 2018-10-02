using System;
using System.Collections.Generic;
using MineNET.NBT.Tags;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.BlockEntities
{
    /// <summary>
    /// Minecraft に存在するタイルエンティティを提供する抽象クラス。
    /// </summary>
    public abstract class BlockEntity : ICloneable<BlockEntity>, IPosition
    {
        public CompoundTag NamedTag { get; protected set; }

        public static BlockEntity CreateBlockEntity(string type, Chunk chunk, CompoundTag nbt)
        {
            if (MineNET_Registries.BlockEntity.ContainsKey(type))
            {
                Type t = MineNET_Registries.BlockEntity[type];
                return MineNET_Registries.BlockEntity.GetExpression(type)(chunk, nbt);
            }
            else
            {
                throw new KeyNotFoundException(type);
            }
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
            }
            nbt.PutInt("x", (int) this.X);
            nbt.PutInt("y", (int) this.Y);
            nbt.PutInt("z", (int) this.Z);
            this.NamedTag = nbt;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public World World { get; set; }

        public abstract string Name
        {
            get;
        }

        public virtual BlockEntity Clone()
        {
            return (BlockEntity) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual void OnUpdate()
        {

        }

        public virtual void SaveNBT()
        {

        }
    }
}