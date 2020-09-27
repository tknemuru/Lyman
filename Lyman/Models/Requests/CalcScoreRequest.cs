// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Managers;

namespace Lyman.Models.Requests
{
    /// <summary>
    /// 点数計算要求
    /// </summary>
    public class CalcScoreRequest : RoomAttachedRequest
    {
        /// <summary>
        /// アガったプレイヤの識別キー
        /// </summary>
        /// <value>The player key.</value>
        public Guid PlayerKey { get; set; }

        /// <summary>
        /// ロンされたプレイヤの風
        /// </summary>
        public Wind.Index RonTargetWind { get; set; }
    }
}
