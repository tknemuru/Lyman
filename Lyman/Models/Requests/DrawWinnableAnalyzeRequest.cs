// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// ツモ可能性分析要求
    /// </summary>
    public class DrawWinnableAnalyzeRequest : FieldAttachedRequest
    {
        /// <summary>
        /// ツモ牌
        /// </summary>
        /// <value>ツモ牌</value>
        public uint DrawTile { get; set; }
    }
}
