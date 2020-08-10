using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
namespace Lyman.Models
{
    /// <summary>
    /// 河での位置
    /// </summary>
    public sealed class RiverPosition
    {
        /// <summary>
        /// 風
        /// </summary>
        public Wind.Index Wind { get; set; }

        /// <summary>
        /// インデックス
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 指定のオブジェクトが現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <param name="obj">比較対象のオブジェクト</param>
        /// <returns>指定のオブジェクトが現在のオブジェクトと等しいかどうか</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var position = (RiverPosition)obj;
            var equals = new Dictionary<string, bool>();

            equals.Add("Wind", this.Wind == position.Wind);
            equals.Add("Index", this.Index == position.Index);

            return equals.All(e => e.Value);
        }

        /// <summary>
        /// 既定のハッシュ関数として機能します。
        /// </summary>
        /// <returns>現在のオブジェクトのハッシュ コード。</returns>
        public override int GetHashCode()
        {
            return this.Wind.GetHashCode() ^ this.Index.GetHashCode();
        }

        /// <summary>
        /// オブジェクトを文字列に変換します。
        /// </summary>
        /// <returns>文字列化したオブジェクト</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"風:{this.Wind}");
            sb.AppendLine($"インデックス:{this.Index}");
            return sb.ToString();
        }
    }
}
