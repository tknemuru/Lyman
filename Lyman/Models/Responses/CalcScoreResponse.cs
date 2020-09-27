using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Managers;

namespace Lyman.Models.Responses
{
    /// <summary>
    /// 点数計算応答
    /// </summary> 
    public class CalcScoreResponse : RoomAttachedResponse
    {
        /// <summary>
        /// 点数の差分
        /// </summary>
        public Dictionary<Wind.Index, int> ScoreDiffs { get; set; }
    }
}
