// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Models;

namespace Lyman.Ai
{
    /// <summary>
    /// 捨牌の機能を提供します。
    /// </summary>
    public interface IDiscardable
    {
        /// <summary>
        /// 捨牌を実行します。
        /// </summary>
        /// <returns>捨てた牌</returns>
        /// <param name="context">フィールド状態</param>
        /// <param name="wind">風</param>
        uint Discard(FieldContext context, Wind.Index wind);
    }
}
