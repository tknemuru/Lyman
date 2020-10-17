using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Managers;

namespace Lyman.Models.Requests
{
    /// <summary>
    /// 役分析要求
    /// </summary>
    public class AnalyzeYakuRequest : RoomAttachedRequest
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AnalyzeYakuRequest()
        {
            this.WinWind = Wind.Index.Undefined;
        }

        /// <summary>
        /// あがりの牌
        /// </summary>
        public uint WinTile { get; set; }

        /// <summary>
        /// あがるプレイヤの風
        /// </summary>
        public Wind.Index WinWind { get; set; }

        /// <summary>
        /// あがりの型情報
        /// </summary>
        public WinHandsContext WinHandsContext { get; set; }
    }
}
