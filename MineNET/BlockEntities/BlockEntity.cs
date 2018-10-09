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
    public abstract class BlockEntity : IPosition
    {
        /// <summary>
        /// この <see cref="BlockEntity"/> のNBTデータです。 
        /// </summary>
        public CompoundTag NamedTag { get; protected set; }

        /// <summary>
        /// 定義されている <see cref="BlockEntity"/> を生成します。
        /// </summary>
        /// <param name="type"><see cref="BlockEntity"/> のID</param>
        /// <param name="chunk"><see cref="BlockEntity"/> を設置するチャンク</param>
        /// <param name="nbt"><see cref="BlockEntity"/></param>
        /// <returns>生成した <see cref="BlockEntity"/></returns>
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

        /// <summary>
        /// <see cref="BlockEntity"/> クラスの新しいインスタンスを作成します
        /// </summary>
        /// <param name="chunk"> <see cref="BlockEntity"/> を作成するための <see cref="Worlds.Chunk"/> </param>
        /// <param name="nbt"> <see cref="BlockEntity"/> を作成するための <see cref="CompoundTag"/> </param>
        public BlockEntity(Chunk chunk, CompoundTag nbt)
        {
            this.Chunk = chunk;
            this.World = chunk.World;
            if (nbt == null)
            {
                throw new ArgumentNullException(nameof(nbt));
            }

            this.X = nbt.GetInt("x");
            this.Y = nbt.GetInt("y");
            this.Z = nbt.GetInt("z");

            this.NamedTag = nbt;
        }

        /// <summary>
        /// <see cref="BlockEntity"/> のX座標
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// <see cref="BlockEntity"/> のY座標
        /// </summary>
        public float Y { get; set; }
        /// <summary>
        /// <see cref="BlockEntity"/> のZ座標
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// <see cref="BlockEntity"/> の存在する <see cref="Worlds.World"/>
        /// </summary>
        public World World { get; set; }
        /// <summary>
        /// <see cref="BlockEntity"/> の存在する <see cref="Worlds.Chunk"/>
        /// </summary>
        public Chunk Chunk { get; set; }

        /// <summary>
        /// <see cref="BlockEntity"/> の名前
        /// </summary>
        public abstract string Name
        {
            get;
        }
        
        internal virtual void OnUpdate()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void SaveNBT()
        {

        }
    }
}