using MineNET.Values;
using System;

namespace MineNET.Blocks
{
    /// <summary>
    /// Minecraft に存在するブロックを提供するクラス。
    /// </summary>
    public class Block : ICloneable<Block>
    {
        /// <summary>
        /// 定義されているブロックをデータ値から取得します。
        /// </summary>
        /// <param name="id">ブロックのID</param>
        /// <param name="meta">ブロックのメタデータ</param>
        /// <returns>取得したブロック</returns>
        public static Block Get(int id, int meta = 0)
        {
            if (MineNET_Registries.Block.ContainsKey(id))
            {
                Block b = MineNET_Registries.Block[id];
                b.Damage = meta;
                return b;
            }

            return new BlockAir();
        }

        /// <summary>
        /// 定義されているブロックを名前から取得します。
        /// </summary>
        /// <param name="name">ブロックの名前</param>
        /// <returns>取得したブロック</returns>
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
        /// ブロックのID
        /// </summary>
        public int ID { get; }
        /// <summary>
        /// ブロックのメタデータ
        /// </summary>
        public int Damage { get; set; }

        public Position Position { get; internal set; }

        /// <summary>
        /// ブロックの名前
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// ブロックの硬さ
        /// </summary>
        public float Hardness { get; set; } = 0;
        /// <summary>
        /// ブロックの耐性
        /// </summary>
        public float Resistance { get; set; } = 0;

        /// <summary>
        /// ブロックの光源レベル
        /// </summary>
        public float LightLevel { get; set; } = 0;
        /// <summary>
        /// ブロックの透明度
        /// </summary>
        public float LightOpacity { get; set; } = 0;

        /// <summary>
        /// ブロックのマップ上での色
        /// </summary>
        public Color MapColor { get; set; } = BlockColor.Air;

        public Block(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        /// <summary>
        /// ブロックのインスタンスを複製します。
        /// </summary>
        /// <returns></returns>
        public Block Clone()
        {
            return (Block) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// ブロックの当たり判定
        /// </summary>
        public virtual AxisAlignedBB BoundingBox { get; } = AxisAlignedBB.None;


        public virtual bool IsTransparent
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsSolid
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// ブロックが更新された時に呼び出されるメソッド
        /// </summary>
        /// <param name="type"></param>
        public void UpdateTick(int type)
        {
        }
    }
}
