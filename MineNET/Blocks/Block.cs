using System;
using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Blocks
{
    /// <summary>
    /// Minecraft に存在するブロックを提供するクラス。
    /// </summary>
    public class Block : ICloneable<Block>
    {
        /// <summary>
        /// 定義されている　<see cref="Block"/> をデータ値から取得します。
        /// </summary>
        /// <param name="id"><see cref="Block"/> のID</param>
        /// <param name="meta"><see cref="Block"/> のメタデータ</param>
        /// <returns>取得した <see cref="Block"/></returns>
        public static Block Get(int id, int meta = 0)
        {
            if (MineNET_Registries.Block.ContainsKey(id))
            {
                Block b = MineNET_Registries.Block[id].Clone();
                b.Damage = meta;
                return b;
            }

            return new BlockAir();
        }

        /// <summary>
        /// 定義されている <see cref="Block"/> を定義された名前から取得します。
        /// </summary>
        /// <param name="name"><see cref="Block"/> の定義された名前</param>
        /// <returns>取得した <see cref="Block"/></returns>
        public static Block Get(string name)
        {
            string[] data = name.Replace("minecraft:", "").Replace(" ", "_").ToUpper().Split(':');
            int id = 0;
            int meta = 0;

            if (data.Length == 1)
            {
                int.TryParse(data[0], out id);
            }

            if (data.Length == 2)
            {
                int.TryParse(data[0], out id);
                int.TryParse(data[1], out meta);
            }

            try
            {
                BlockIDs factory = new BlockIDs();
                id = (int) factory.GetType().GetField(data[0]).GetValue(factory);
            }
            catch
            {

            }

            Block block = Block.Get(id);

            return block;
        }

        /// <summary>
        /// <see cref="Block"/> のID。
        /// </summary>
        public int ID { get; }
        /// <summary>
        /// <see cref="Block"/> のメタデータ。
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// <see cref="Block"/> の位置。
        /// </summary>
        public Position Position { get; internal set; }

        /// <summary>
        /// <see cref="Block"/> の名前。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// <see cref="Block"/> の硬さ。
        /// </summary>
        public float Hardness { get; set; } = 0;
        /// <summary>
        /// <see cref="Block"/> の耐性。
        /// </summary>
        public float Resistance { get; set; } = 0;

        /// <summary>
        /// <see cref="Block"/> の光源レベル。
        /// </summary>
        public float LightLevel { get; set; } = 0;
        /// <summary>
        /// <see cref="Block"/> の透明度。
        /// </summary>
        public float LightOpacity { get; set; } = 0;

        /// <summary>
        /// <see cref="Block"/> の <see cref="World"/> 上での <see cref="BlockColor"/>。
        /// </summary>
        public Color MapColor { get; set; } = BlockColor.Air;

        /// <summary>
        /// <see cref="Block"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="id"><see cref="Block"/> のID</param>
        /// <param name="name"><see cref="Block"/> のメタデータ</param>
        public Block(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        /// <summary>
        /// <see cref="Block"/> のランタイムID。
        /// </summary>
        public int RuntimeId
        {
            get
            {
                return GlobalBlockPalette.GetRuntimeID(this.ID, this.Damage);
            }
        }

        /// <summary>
        /// <see cref="Block"/> のインスタンスを複製します。
        /// </summary>
        /// <returns>複製された<see cref="Block"/>クラスのインスタンス</returns>
        public Block Clone()
        {
            return (Block) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// <see cref="Block"/> の当たり判定。
        /// </summary>
        public virtual AxisAlignedBB BoundingBox { get; } = AxisAlignedBB.None;

        /// <summary>
        /// <see cref="Block"/> が透明である場合は true を返します。
        /// </summary>
        public virtual bool IsTransparent
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// <see cref="Block"/> が個体である場合は true を返します。
        /// </summary>
        public virtual bool IsSolid
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// <see cref="Block"/> が更新された時に呼び出されます。
        /// </summary>
        /// <param name="type">
        /// 詳細は <see cref="World"/> クラスを参照してください。
        /// </param>
        public void UpdateTick(int type)
        {
        }

        /// <summary>
        /// この <see cref="Block"/> が <see cref="Values.Position"/> を持っている場合 true を返します。
        /// </summary>
        /// <returns></returns>
        public bool HasPosition()
        {
            return this.Position != null;
        }

        /// <summary>
        /// この <see cref="Block"/> のサイドにある <see cref="Block"/> を取得します。
        /// </summary>
        /// <param name="face"><see cref="BlockFace"/> データ</param>
        /// <returns></returns>
        public Block GetSideBlock(BlockFace face)
        {
            if (this.HasPosition())
            {
                return this.Position.World.GetBlock((Vector3) this.Position + face.GetPosition());
            }
            return null;
        }

        /// <summary>
        /// この <see cref="Block"/> を置くことができる場合 true を返します。
        /// </summary>
        public virtual bool CanBePlaced
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// この <see cref="Block"/> を置き換えることができる場合 true を返します。
        /// </summary>
        public virtual bool CanBeReplaced
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// この <see cref="Block"/> を破壊できる場合 true を返します。
        /// </summary>
        public virtual bool CanBreak
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// この <see cref="Block"/> に触れることができる場合 true を返します。
        /// </summary>
        public virtual bool CanBeActivated
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// この <see cref="Block"/> が配置された時に呼び出されます。
        /// </summary>
        /// <param name="clicked">クリックした <see cref="Block"/></param>
        /// <param name="replace">置き換える <see cref="Block"/></param>
        /// <param name="face">クリックした <see cref="BlockFace"/></param>
        /// <param name="clickPos">クリックした <see cref="Values.Position"/></param>
        /// <param name="player">配置した <see cref="Player"/></param>
        /// <param name="item">クリックした時に持っていた <see cref="Items.ItemStack"/></param>
        /// <returns></returns>
        public virtual bool Place(Block clicked, Block replace, BlockFace face, Vector3 clickPos, Player player, ItemStack item)
        {
            this.Position.World.SetBlock((Vector3) this.Position, this, true);
            return true;
        }

        /// <summary>
        /// この <see cref="Block"/> が破壊された時に呼び出されます。
        /// </summary>
        /// <param name="player">破壊した <see cref="Player"/></param>
        /// <param name="item">破壊した時に持っていた <see cref="Items.ItemStack"/></param>
        /// <returns></returns>
        public virtual bool Break(Player player, ItemStack item)
        {
            this.Position.World.SetBlock((Vector3) this.Position, new BlockAir(), true);
            return true;
        }

        /// <summary>
        /// この <see cref="Block"/> にクリックされた時に呼び出されます。
        /// </summary>
        /// <param name="player">クリック <see cref="Player"/></param>
        /// <param name="item">クリックした時に持っていた <see cref="Items.ItemStack"/></param>
        /// <returns></returns>
        public virtual bool Activate(Player player, ItemStack item)
        {
            return false;
        }

        /// <summary>
        /// この <see cref="Block"/> が破壊された時にドロップする <see cref="Items.ItemStack"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual ItemStack[] GetDrops(ItemStack item)
        {
            if (this.ID < 1)
            {
                return new ItemStack[] { new ItemStack(Item.Get(BlockIDs.AIR)) };
            }
            return new ItemStack[] { this.ItemStack };
        }

        /// <summary>
        /// この <see cref="Block"/> に関連した <see cref="Items.ItemStack"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public virtual ItemStack ItemStack
        {
            get
            {
                return new ItemStack(Item.Get(this.ID), this.Damage);
            }
        }

    }
}
