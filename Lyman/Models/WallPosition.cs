using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Diagnostics;
using Lyman.Di;

namespace Lyman.Models
{
    /// <summary>
    /// 壁での位置
    /// </summary>
    public sealed class WallPosition
    {
        /// <summary>
        /// 種別
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// 未確定
            /// </summary>
            Undefined,

            /// <summary>
            /// 通常
            /// </summary>
            Default,

            /// <summary>
            /// 海底牌
            /// </summary>
            SeaFloor,

            /// <summary>
            /// 王牌
            /// </summary>
            Dead,
        }

        /// <summary>
        /// 風
        /// </summary>
        public Wind.Index Wind { get; set; }

        /// <summary>
        /// 段
        /// </summary>
        /// <value>The rank.</value>
        public Wall.Rank Rank { get; set; }

        /// <summary>
        /// インデックス
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }

        /// <summary>
        /// オブジェクトのディープコピーを行います。
        /// </summary>
        /// <returns>ディーピコピーされたオブジェクト</returns>
        public WallPosition DeepCopy()
        {
            var obj = DiProvider.GetContainer().GetInstance<WallPosition>();
            obj.Wind = this.Wind;
            obj.Rank = this.Rank;
            obj.Index = this.Index;
            return obj;
        }

        /// <summary>
        /// 次の位置を取得します。
        /// </summary>
        public WallPosition Next()
        {
            Debug.Assert(this.Index >= 0 && this.Index < Wall.Length, $"インデックスが不正です。{this.Index}");
            var requiredChangeWind = (this.Index == 0 && this.Rank == Wall.Rank.Lower);
            if (!requiredChangeWind)
            {
                if (this.Rank == Wall.Rank.Lower)
                {
                    this.Index--;
                }
                this.Rank = this.Rank.Next();
                return this;
            }

            this.Wind = this.Wind.Next();
            this.Rank = this.Rank.Next();
            this.Index = Wall.Length - 1;
            return this;
        }

        /// <summary>
        /// 位置の種別を取得します。
        /// </summary>
        /// <returns>位置の種別</returns>
        /// <param name="openGatePosition">開門位置</param>
        public Type GetPositionType(WallPosition openGatePosition)
        {
            Debug.Assert(openGatePosition != null && openGatePosition.Wind != Models.Wind.Index.Undefined && openGatePosition.Index > 0, $"開門位置が不正です。{openGatePosition}");
            var type = Type.Default;
            if (this.Wind == openGatePosition.Wind.Prev() && this.Index == 0 && this.Rank == Wall.Rank.Lower)
            {
                type = Type.SeaFloor;
            }
            if (this.Wind == openGatePosition.Wind && this.Index > openGatePosition.Index)
            {
                type = Type.Dead;
            }
            return type;
        }

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

            var position = (WallPosition)obj;
            var equals = new Dictionary<string, bool>();

            equals.Add("Wind", this.Wind == position.Wind);
            equals.Add("Rank", this.Rank == position.Rank);
            equals.Add("Index", this.Index == position.Index);

            return equals.All(e => e.Value);
        }

        /// <summary>
        /// 既定のハッシュ関数として機能します。
        /// </summary>
        /// <returns>現在のオブジェクトのハッシュ コード。</returns>
        public override int GetHashCode()
        {
            return this.Wind.GetHashCode() ^ this.Rank.GetHashCode() ^ this.Index.GetHashCode();
        }

        /// <summary>
        /// オブジェクトを文字列に変換します。
        /// </summary>
        /// <returns>文字列化したオブジェクト</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"風:{this.Wind}");
            sb.AppendLine($"段:{this.Rank}");
            sb.AppendLine($"インデックス:{this.Index}");
            return sb.ToString();
        }
    }
}
