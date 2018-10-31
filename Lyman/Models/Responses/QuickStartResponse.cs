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
        /// 部屋のキー
        /// </summary>
        public Guid RoomKey { get; set; }

        /// <summary>
        /// 部屋名
        /// </summary>
        /// <value>The name.</value>
        public string RoomName { get; set; }

        /// <summary>
        /// 風
        /// </summary>
        /// <value>The wind.</value>
        public Wind.Index WindIndex { get; set; }

        /// <summary>
        /// 風（日本語名）
        /// </summary>
        /// <value>The wind.</value>
        public string Wind { get; set; }

        /// <summary>
        /// プレイヤの識別キー
        /// </summary>
        /// <value>The player key.</value>
        public Guid PlayerKey { get; set; }

        /// <summary>
        /// プレイヤ名
        /// </summary>
        /// <value>The name of the player.</value>
        public string PlayerName { get; set; }

        /// <summary>
        /// 部屋の状態
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// プレイヤ
        /// </summary>
        public Dictionary<Wind.Index, string> Players { get; set; }

        /// <summary>
        /// 手牌
        /// </summary>
        /// <value>The hand.</value>
        public IEnumerable<uint> Hand { get; set; }

        /// <summary>
        /// 河牌
        /// </summary>
        public IEnumerable<IEnumerable<uint>> Rivers { get; set; }
    }
}
