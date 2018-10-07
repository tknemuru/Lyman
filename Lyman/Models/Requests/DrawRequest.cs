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
        public WallPosition Position { get; set; }

        /// <summary>
        /// 要求種別の初期化を行います。
        /// </summary>
        protected override void InitRequestType()
        {
            this.RequestType = RequestType.Draw;
        }
    }
}
