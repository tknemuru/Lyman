using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Models;
using System.Diagnostics;
using Lyman.Di;
using System.Diagnostics.Contracts;

namespace Lyman.Converters
{
    /// <summary>
    /// Lymanにおける標準テキスト形式文字列をフィールド状態に変換する機能を提供します。
    /// </summary>
    public sealed class TextToTilesConverter : IConvertible<string, IEnumerable<uint>>
    {
        /// <summary>
        /// 牌の配列へ変換します。
        /// </summary>
        /// <returns>牌の配列</returns>
        /// <param name="str">牌の配列を示す文字列</param>
        public IEnumerable<uint> Convert(string str)
        {
            return str.Split(SimpleText.ValueSeparator).Where(s => s != string.Empty).Select(Tile.BuildTile);
        }
    }
}
