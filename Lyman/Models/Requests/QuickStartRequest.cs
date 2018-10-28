using System.Collections.Generic;
using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// 即時ゲーム開始要求
    /// </summary>
    public class QuickStartRequest : Request
    {
        /// <summary>
        /// 部屋作成要求
        /// </summary>
        /// <value>The create room request.</value>
        public CreateRoomRequest CreateRoomRequest { get; set; }

        /// <summary>
        /// 入室要求
        /// </summary>
        /// <value>The enter room requests.</value>
        public IEnumerable<EnterRoomRequest> EnterRoomRequests { get; set; }

        /// <summary>
        /// 配牌要求
        /// </summary>
        /// <value>The dealt tiles request.</value>
        public DealtTilesRequest DealtTilesRequest { get; set; }

        /// <summary>
        /// 部屋取得要求
        /// </summary>
        public SelectRoomRequest SelectRoomRequest { get; set; }
    }
}
