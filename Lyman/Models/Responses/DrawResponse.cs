// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Models.Requests;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// ツモ応答
    /// </summary>
    public sealed class DrawResponse : FieldAttachedResponse
    {
        /// <summary>
        /// 牌
        /// </summary>
        public uint Tile { get; set; }

        /// <summary>
        /// 次ツモの風
        /// </summary>
        public Wind.Index NextWind { get; set; }

        /// <summary>
        /// 次ツモの段
        /// </summary>
        /// <value>The rank.</value>
        public Wall.Rank NextRank { get; set; }

        /// <summary>
        /// 次ツモのインデックス
        /// </summary>
        /// <value>The index.</value>
        public int NextIndex { get; set; }

        /// <summary>
        /// 次ツモが海底牌かどうか
        /// </summary>
        public bool NextSeafloor { get; set; }

        /// <summary>
        /// 要求種別の初期化を行います。
        /// </summary>
        protected override void InitRequestType()
        {
            this.RequestType = RequestType.Draw;
        }
    }
}
