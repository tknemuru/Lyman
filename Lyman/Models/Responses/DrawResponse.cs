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
        /// 次ツモの位置
        /// </summary>
        /// <value>The next position.</value>
        public WallPosition NextPosition { get; set; }

        /// <summary>
        /// ツモ上がり可能性情報
        /// </summary>
        public DrawWinnableAnalyzeResponse DrawWinnableInfo { get; set; }
    }
}
