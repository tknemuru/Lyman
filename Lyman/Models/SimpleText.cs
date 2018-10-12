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
            /// 開門位置
            /// </summary>
            public const string OpenGatePosition = "開門位置";

            /// <summary>
            /// 開門位置の風
            /// </summary>
            public const string OpenGatePostionWind = "開門位置|風";

            /// <summary>
            /// 開門位置のインデックス
            /// </summary>
            public const string OpenGatePostionIndex = "開門位置|インデックス";

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
