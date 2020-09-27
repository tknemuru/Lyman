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

        /// <summary>
        /// 局終了
        /// </summary>
        Finish
    }

    /// <summary>
    /// 部屋状態の拡張機能を提供します。
    /// </summary>
    public static class RoomStateExt
    {
        /// <summary>
        /// インデックスを数値に変換します。
        /// </summary>
        /// <returns>数値</returns>
        /// <param name="index">インデックス</param>
        public static int ToInt(this RoomState index)
        {
            return (int)index;
        }
    }
}
