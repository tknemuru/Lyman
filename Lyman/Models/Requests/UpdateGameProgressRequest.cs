using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Managers;

namespace Lyman.Models.Requests
{
    /// <summary>
    /// ゲーム進行更新要求
    /// </summary>
    public class UpdateGameProgressRequest : RoomAttachedRequest
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UpdateGameProgressRequest()
        {
            this.WinWind = Wind.Index.Undefined;
            this.ParentWaitingHand = false;
        }

        /// <summary>
        /// アガったプレイヤの風
        /// </summary>
        public Wind.Index WinWind { get; set; }

        /// <summary>
        /// 親のテンパイ有無
        /// </summary>
        public bool ParentWaitingHand { get; set; }
    }
}
