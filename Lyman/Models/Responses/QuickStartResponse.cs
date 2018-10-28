using System.Collections.Generic;
using System.Linq;
using System;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// 即時ゲーム開始応答
    /// </summary>
    public class QuickStartResponse : Response
    {
        /// <summary>
        /// 部屋作成応答
        /// </summary>
        //public CreateRoomResponse CreateRoomResponse { get; set; }

        /// <summary>
        /// 入室応答
        /// </summary>
        //public IEnumerable<EnterRoomResponse> EnterRoomResponses { get; set; }

        /// <summary>
        /// 配牌応答
        /// </summary>
        /// <value>The dealt tiles response.</value>
        //public DealtTilesResponse DealtTilesResponse { get; set; }

        /// <summary>
        /// 部屋取得応答
        /// </summary>
        /// <value>The select room response.</value>
        //public SelectRoomResponse SelectRoomResponse { get; set; }

        /// <summary>
        /// 手牌
        /// </summary>
        /// <value>The hand.</value>
        public IEnumerable<uint> Hand { get; set; }
    }
}
