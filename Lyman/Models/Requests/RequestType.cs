// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// 命令種別
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// 部屋作成
        /// </summary>
        CreateRoom,

        /// <summary>
        /// ツモ
        /// </summary>
        Draw,

        /// <summary>
        /// 捨牌
        /// </summary>
        Discard,

        /// <summary>
        /// 配牌
        /// </summary>
        DealtTiles,
    }
}
