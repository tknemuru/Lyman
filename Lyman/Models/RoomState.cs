// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models
{
    /// <summary>
    /// 部屋の状況
    /// </summary>
    public enum RoomState
    {
        /// <summary>
        /// 未定義
        /// </summary>
        Undefined,

        /// <summary>
        /// 入室中
        /// </summary>
        Entering,

        /// <summary>
        /// 入室完了
        /// </summary>
        Entered,

        /// <summary>
        /// 配牌完了
        /// </summary>
        Dealted,
    }
}
