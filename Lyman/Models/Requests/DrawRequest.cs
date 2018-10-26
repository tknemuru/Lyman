// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// ツモ要求
    /// </summary>
    public sealed class DrawRequest : FieldAttachedRequest
    {
        /// <summary>
        /// ツモを行う壁牌の位置
        /// </summary>
        /// <value>The position.</value>
        public WallPosition Position { get; set; }
    }
}
