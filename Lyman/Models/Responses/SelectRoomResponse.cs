using System.Collections.Generic;
using System.Linq;
using System;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// 部屋取得応答
    /// </summary>
    public class SelectRoomResponse : Response
    {
        /// <summary>
        /// 部屋名
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// 部屋の状態
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// ターン
        /// </summary>
        /// <value>The turn.</value>
        public Wind.Index Turn { get; set; }

        /// <summary>
        /// プレイヤ
        /// </summary>
        public Dictionary<int, Player> Players { get; set; }

        /// <summary>
        /// 手牌
        /// </summary>
        /// <value>The hand.</value>
        public IEnumerable<uint> Hand { get; set; }

        /// <summary>
        /// 河牌
        /// </summary>
        public IEnumerable<IEnumerable<uint>> Rivers { get; set; }

        /// <summary>
        /// 最後に捨牌した河の位置
        /// </summary>
        public RiverPosition LastDiscardPosition { get; set; }

        /// <summary>
        /// リーチ可能性情報
        /// </summary>
        public ReachableAnalyzeResponse ReachableInfo { get; set; }

        /// <summary>
        /// ロン可能性情報
        /// </summary>
        public RonableAnalyzeResponse RonableInfo { get; set; }
    }
}
