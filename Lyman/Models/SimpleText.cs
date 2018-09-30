using System.Collections.Generic;
using System.Linq;
using System;
namespace Lyman.Models
{
    /// <summary>
    /// 標準テキストファイル
    /// </summary>
    public static class SimpleText
    {
        /// <summary>
        /// キーと値を分離する文字
        /// </summary>
        public const char KeyValueSeparator = ':';

        /// <summary>
        /// 値を分離する文字
        /// </summary>
        public const char ValueSeparator = '|';

        /// <summary>
        /// キー
        /// </summary>
        public static class Key
        {
            /// <summary>
            /// 手牌
            /// </summary>
            public const string Hand = "手牌";

            /// <summary>
            /// 壁牌
            /// </summary>
            public const string Wall = "壁牌";

            /// <summary>
            /// 河
            /// </summary>
            public const string River = "河";
        }
    }
}
