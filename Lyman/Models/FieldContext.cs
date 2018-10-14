using System;
using System.Collections.Generic;
using System.Linq;
using Lyman.Di;

namespace Lyman.Models
{
    /// <summary>
    /// フィールドの状態
    /// </summary>
    public sealed class FieldContext
    {
        /// <summary>
        /// 開門位置
        /// </summary>
        public WallPosition OpenGatePosition { get; set; }

        /// <summary>
        /// 手牌
        /// </summary>
        public List<uint>[] Hands { get; set; }

        /// <summary>
        /// 壁牌
        /// </summary>
        public uint[][][] Walls { get; set; }

        /// <summary>
        /// 河
        /// </summary>
        /// <value>The rivers.</value>
        public List<uint>[] Rivers { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FieldContext()
        {
            this.OpenGatePosition = DiProvider.GetContainer().GetInstance<WallPosition>();
            this.OpenGatePosition.Wind = Wind.Index.Undefined;
            this.OpenGatePosition.Rank = Wall.Rank.Upper;
            this.OpenGatePosition.Index = -1;
            this.Hands = new List<uint>[Wind.Length];
            this.Walls = new uint[Wind.Length][][];
            this.Rivers = new List<uint>[Wind.Length];

            Wind.ForEach(wind =>
            {
                this.Hands[wind.ToInt()] = new List<uint>();
                this.Walls[wind.ToInt()] = new uint[Wall.RankLength][];
                Wall.ForEachRank(rank =>
                {
                    this.Walls[wind.ToInt()][rank.ToInt()] = new uint[Wall.Length];
                });
                this.Rivers[wind.ToInt()] = new List<uint>();
            });
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

            var context = (FieldContext)obj;
            var equals = new Dictionary<string, bool>();

            equals.Add("OpenGatePosition", this.OpenGatePosition.Equals(context.OpenGatePosition));

            Wind.ForEach((wind) =>
            {
                var countEqual = this.Hands[wind.ToInt()].Count() == context.Hands[wind.ToInt()].Count();
                equals.Add($"HandsCount{wind}", countEqual);
                if (countEqual)
                {
                    for (var i = 0; i < this.Hands[wind.ToInt()].Count(); i++)
                    {
                        equals.Add($"Hands{wind}{i}", this.Hands[wind.ToInt()][i] == context.Hands[wind.ToInt()][i]);
                    }
                }

                countEqual = this.Rivers[wind.ToInt()].Count() == context.Rivers[wind.ToInt()].Count();
                equals.Add($"RiversCount{wind}", countEqual);
                if (countEqual)
                {
                    for (var i = 0; i < this.Rivers[wind.ToInt()].Count(); i++)
                    {
                        equals.Add($"Rivers{wind}{i}", this.Rivers[wind.ToInt()][i] == context.Rivers[wind.ToInt()][i]);
                    }
                }
            });

            Wall.ForEach((wind, rank, i) =>
            {
                equals.Add($"Walls{wind}{rank}{i}", this.Walls[wind.ToInt()][rank.ToInt()][i] == context.Walls[wind.ToInt()][rank.ToInt()][i]);
            });

            return equals.All(e => e.Value);
        }

        /// <summary>
        /// 既定のハッシュ関数として機能します。
        /// </summary>
        /// <returns>現在のオブジェクトのハッシュ コード。</returns>
        public override int GetHashCode()
        {
            return this.OpenGatePosition.GetHashCode() ^ this.Hands.GetHashCode() ^ this.Walls.GetHashCode() ^ this.Rivers.GetHashCode();
        }
    }
}
