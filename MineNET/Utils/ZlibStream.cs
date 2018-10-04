using System.IO;
using System.IO.Compression;

namespace MineNET.Utils
{
    /// <summary>
    /// チェックサムを追加した <see cref="DeflateStream"/> を提供します。
    /// </summary>
    public class ZlibStream : DeflateStream
    {
        private uint adler32 = 1;

        private const int mod = 65521;//mod

        /// <summary>
        /// チェックサムの結果
        /// </summary>
        public int Checksum => (int) this.adler32;

        private uint Update(uint adler, byte[] s, int offset, int count)
        {
            uint l = (ushort) adler;
            ulong h = adler >> 16;
            int p = 0;
            for (; p < (count & 7); ++p)
            {
                l += s[offset + p];
                h += l;
            }

            for (; p < count; p += 8)
            {
                int idx = offset + p;
                l += s[idx];
                h += l;
                l += s[idx + 1];
                h += l;
                l += s[idx + 2];
                h += l;
                l += s[idx + 3];
                h += l;
                l += s[idx + 4];
                h += l;
                l += s[idx + 5];
                h += l;
                l += s[idx + 6];
                h += l;
                l += s[idx + 7];
                h += l;
            }

            return (uint) (((h % mod) << 16) | (l % mod));
        }

        /// <summary>
        ///   指定したストリームと圧縮レベルを使用して、<see cref="ZlibStream" /> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="stream">圧縮するストリーム。</param>
        /// <param name="level">
        ///   ストリームの圧縮時に速度または圧縮の効率性を強調するかどうかを示す列挙値の 1 つ。
        /// </param>
        /// <param name="leaveOpen">
        ///   <see cref="T:System.IO.Compression.DeflateStream" /> オブジェクトを破棄した後にストリーム オブジェクトを開いたままにする場合は <see langword="true" />、それ以外の場合は <see langword="false" />。
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="stream" /> は <see langword="null" /> です。
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///   ストリームは、圧縮などの書き込み操作をサポートしていません。
        ///    (、 <see cref="P:System.IO.Stream.CanWrite" /> ストリーム オブジェクトのプロパティは、 <see langword="false" />.)
        /// </exception>
        public ZlibStream(Stream stream, CompressionLevel level, bool leaveOpen) : base(stream, level, leaveOpen)
        {
        }

        /// <summary>
        ///   指定したストリームと圧縮モードを使用して、<see cref="ZlibStream" /> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="stream">圧縮または圧縮解除するストリーム。</param>
        /// <param name="mode">ストリームを圧縮するか圧縮解除するかを示す列挙値の 1 つ。</param>
        /// <param name="leaveOpen">
        ///   <see cref="T:System.IO.Compression.DeflateStream" /> オブジェクトを破棄した後にストリーム オブジェクトを開いたままにする場合は <see langword="true" />、それ以外の場合は <see langword="false" />。
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="stream" /> は <see langword="null" /> です。
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="mode" /> が有効な <see cref="T:System.IO.Compression.CompressionMode" /> 値ではありません。
        /// 
        ///   または
        /// 
        ///   <see cref="T:System.IO.Compression.CompressionMode" /><see cref="F:System.IO.Compression.CompressionMode.Compress" />  と <see cref="P:System.IO.Stream.CanWrite" /> は <see langword="false" />です。
        /// 
        ///   または
        /// 
        ///   <see cref="T:System.IO.Compression.CompressionMode" /><see cref="F:System.IO.Compression.CompressionMode.Decompress" />  と <see cref="P:System.IO.Stream.CanRead" /> は <see langword="false" />です。
        /// </exception>
        public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen) : base(stream, mode, leaveOpen)
        {
        }

        /// <summary>圧縮されたバイトを、指定したバイト配列から基になるストリームに書き込みます。</summary>
        /// <param name="array">圧縮するデータを格納しているバッファー。</param>
        /// <param name="offset">
        ///   バイトの読み取り元となる <paramref name="array" /> 内のバイト オフセット。
        /// </param>
        /// <param name="count">書き込む最大バイト数。</param>
        public override void Write(byte[] array, int offset, int count)
        {
            this.adler32 = Update(this.adler32, array, offset, count);
            base.Write(array, offset, count);
        }
    }
}
