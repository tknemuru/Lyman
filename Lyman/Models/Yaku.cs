// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models
{
    /// <summary>
    /// 役
    /// </summary>
    public enum Yaku
    {
        /// <summary>
        /// リーチ
        /// </summary>
        Reach,

        /// <summary>
        /// タンヤオ
        /// </summary>
        AllSimples,

        /// <summary>
        /// 平和（ピンフ）
        /// </summary>
        AllSequenceHand,

        /// <summary>
        /// 一盃口（イーペイコー）
        /// </summary>
        TwoIdenticalSequences,

        /// <summary>
        /// 七対子（チートイツ）
        /// </summary>
        SevenPairs
    }
}
