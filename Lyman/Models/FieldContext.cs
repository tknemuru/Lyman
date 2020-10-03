using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Lyman.Converters;
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
        /// 指定した位置の壁牌を取得します。
        /// </summary>
        /// <returns>指定した位置の壁牌</returns>
        /// <param name="position">壁牌の位置</param>
        public uint GetWallTile(WallPosition position)
        {
            Debug.Assert(position.Wind != Wind.Index.Undefined, "風が未定義です。");
            Debug.Assert(position.Rank != Wall.Rank.Undefined, "段が未定義です。");
            Debug.Assert(position.Index >= 0 && position.Index < Wall.Length, "インデックスが不正です。");
            return this.Walls[position.Wind.ToInt()][position.Rank.ToInt()][position.Index];
        }

        /// <summary>
        /// 河から指定した位置の牌を取得します。
        /// </summary>
        /// <param name="position">河の位置</param>
        /// <returns>河の牌</returns>
        public uint GetRiverTile(RiverPosition position)
        {
            return this.Rivers[position.Wind.ToInt()][position.Index];
        }

        /// <summary>
        /// 指定した位置に牌を配置します。
        /// </summary>
        /// <param name="position">配置する位置</param>
        /// <param name="tile">牌</param>
        public void SetWallTile(WallPosition position, uint tile)
        {
            Debug.Assert(position.Wind != Wind.Index.Undefined, "風が未定義です。");
            Debug.Assert(position.Rank != Wall.Rank.Undefined, "段が未定義です。");
            Debug.Assert(position.Index >= 0 && position.Index < Wall.Length, "インデックスが不正です。");
            this.Walls[position.Wind.ToInt()][position.Rank.ToInt()][position.Index] = tile;
        }

        /// <summary>
        /// 文字列に変換します。
        /// </summary>
        /// <returns>文字列化したフィールド状態</returns>
        public override string ToString()
        {
            return DiProvider.GetContainer().GetInstance<ContextToTextConverter>().Convert(this);
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
