// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Models.Requests;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// 配牌応答
    /// </summary>
    public class DealtTilesResponse : FieldAttachedResponse
    {
        /// <summary>
        /// 要求種別の初期化を行います。
        /// </summary>
        protected override void InitRequestType()
        {
            this.RequestType = RequestType.DealtTiles;
        }
    }
}
