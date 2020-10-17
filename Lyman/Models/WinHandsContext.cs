using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;

namespace Lyman.Models
{
    /// <summary>
    /// あがりの型情報
    /// </summary>
    public class WinHandsContext
    {
        /// <summary>
        /// 雀頭
        /// </summary>
        public uint Head { get; set; }

        /// <summary>
        /// 順子(シュンツ)
        /// </summary>
        public IEnumerable<uint> Chows { get; set; }

        /// <summary>
        /// 刻子(コーツ)
        /// </summary>
        public IEnumerable<uint> Pungs { get; set; }

        /// <summary>
        /// 槓子(カンツ)
        /// </summary>
        public IEnumerable<uint> Kongs { get; set; }

        /// <summary>
        /// 刻子(コーツ)と槓子(カンツ)
        /// </summary>
        public IEnumerable<uint> PungsAndKongs
        {
            get
            {
                return this.Pungs.Concat(this.Kongs);
            }
        }

        /// <summary>
        /// 雀頭と全ての面子
        /// </summary>
        public IEnumerable<uint> HeadAndAllSet
        {
            get
            {
                return this.Chows.Concat(this.Pungs.Concat(this.Kongs))
                    .Append(this.Head);
            }
        }

        /// <summary>
        /// あがることができるかどうか
        /// </summary>
        /// <returns>あがることができるかどうか</returns>
        public bool Winnable {
            get
            {
                if (this.Head == 0)
                {
                    return false;
                }
                var setCount = this.Chows.Count() + this.Pungs.Count() + this.Kongs.Count();
                return setCount >= 4;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WinHandsContext()
        {
            this.Head = 0;
            this.Chows = new List<uint>();
            this.Pungs = new List<uint>();
            this.Kongs = new List<uint>();
        }

        /// <summary>
        /// 文字列に変換します。
        /// </summary>
        /// <returns>文字列化したフィールド状態</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine($"Head: {this.Head.ToStringTile()}");
            sb.Append("Chows: ");
            foreach(var tile in this.Chows)
            {
                sb.Append($"{tile.ToStringTile()}|");
            }
            sb.AppendLine("");
            sb.Append("Pungs: ");
            foreach (var tile in this.Pungs)
            {
                sb.Append($"{tile.ToStringTile()}|");
            }
            sb.AppendLine("");
            sb.Append("Kongs: ");
            foreach (var tile in this.Kongs)
            {
                sb.Append($"{tile.ToStringTile()}|");
            }
            sb.AppendLine("");
            return sb.ToString();
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

            var context = (WinHandsContext)obj;
            var equals = new Dictionary<string, bool>();

            equals.Add($"Head", this.Head == context.Head);

            var countEqual = this.Chows.Count() == context.Chows.Count();
            equals.Add($"ChowsCount", countEqual);
            if (countEqual)
            {
                var length = this.Chows.Count();
                for (var i = 0; i < length; i++)
                {
                    equals.Add($"Chows{i}", this.Chows.ElementAt(i) == context.Chows.ElementAt(i));
                }
            }

            countEqual = this.Pungs.Count() == context.Pungs.Count();
            equals.Add($"PungsCount", countEqual);
            if (countEqual)
            {
                var length = this.Pungs.Count();
                for (var i = 0; i < length; i++)
                {
                    equals.Add($"Pungs{i}", this.Pungs.ElementAt(i) == context.Pungs.ElementAt(i));
                }
            }

            countEqual = this.Kongs.Count() == context.Kongs.Count();
            equals.Add($"KongsCount", countEqual);
            if (countEqual)
            {
                var length = this.Kongs.Count();
                for (var i = 0; i < length; i++)
                {
                    equals.Add($"Kongs{i}", this.Kongs.ElementAt(i) == context.Kongs.ElementAt(i));
                }
            }

            return equals.All(e => e.Value);
        }

        /// <summary>
        /// 既定のハッシュ関数として機能します。
        /// </summary>
        /// <returns>現在のオブジェクトのハッシュ コード。</returns>
        public override int GetHashCode()
        {
            return this.Head.GetHashCode() ^ this.Chows.GetHashCode() ^ this.Pungs.GetHashCode() ^ this.Kongs.GetHashCode();
        }
    }
}
