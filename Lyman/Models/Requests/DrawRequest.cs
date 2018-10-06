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
        /// 要求種別の初期化を行います。
        /// </summary>
        protected override void InitRequestType()
        {
            this.RequestType = RequestType.Draw;
        }
    }
}
