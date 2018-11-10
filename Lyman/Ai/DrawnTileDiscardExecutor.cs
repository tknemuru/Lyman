using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Models;

namespace Lyman.Ai
{
    /// <summary>
    /// 常にツモ牌を捨てる機能を提供します。
    /// </summary>
    public class DrawnTileDiscardExecutor : IDiscardable
    {
        /// <summary>
        /// 捨牌を実行します。
        /// </summary>
        /// <returns>捨てた牌</returns>
        /// <param name="context">フィールド状態</param>
        /// <param name="wind">風</param>
        public uint Discard(FieldContext context, Wind.Index wind)
        {
            return context.Hands[wind.ToInt()].Last();
        }
    }
}
