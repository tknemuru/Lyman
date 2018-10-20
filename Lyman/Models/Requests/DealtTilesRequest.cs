// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// 配牌要求
    /// </summary>
    public class DealtTilesRequest : FieldAttachedRequest
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
