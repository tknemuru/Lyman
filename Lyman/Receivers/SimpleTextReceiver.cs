using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Models;
using System.Diagnostics;
using Lyman.Di;

namespace Lyman.Receivers
{
    /// <summary>
    /// Lymanにおける標準テキスト形式文字列の受信機能を提供します。
    /// </summary>
    public sealed class SimpleTextReceiver : IReceivable<IEnumerable<string>, FieldContext>
    {
        /// <summary>
        /// キーと値を分離する文字
        /// </summary>
        private const char KeyValueSeparator = ':';

        /// <summary>
        /// 値を分離する文字
        /// </summary>
        private const char ValueSeparator = '|';

        /// <summary>
        /// 標準テキストファイルを受信しフィールドの状態に変換します。
        /// </summary>
        /// <param name="source">標準テキストファイルパス</param>
        /// <returns>フィールドの状態</returns>
        public FieldContext Receive(IEnumerable<string> source)
        {
            var context = DiProvider.GetContainer().GetInstance<FieldContext>();

            foreach(var s in source)
            {
                var keyValue = s.Split(KeyValueSeparator);
                Debug.Assert(keyValue.Count() == 2, "要素数が不正です。");

                var keys = keyValue[0].Split(ValueSeparator);
                Debug.Assert(keys.Count() <= 2, "キーの数が不正です。");

                switch(keys[0])
                {
                    case SimpleText.Key.Hand:
                        context.Hands[Wind.JapaneseName.Get(keys[1]).ToInt()] = ParseHand(keyValue[1]);
                        break;
                    default:
                        throw new ArgumentException($"キーが不正です。{keys[0]}");
                }
            }
            return context;
        }

        /// <summary>
        /// 手牌への変換を行います。
        /// </summary>
        /// <returns>手牌</returns>
        /// <param name="str">手牌を示す文字列</param>
        private static uint[] ParseHand(string str)
        {
            var hand = str.Split(ValueSeparator).Select(Tile.BuildTile).ToArray();
            Debug.Assert(hand.Length == Hand.Length, $"手牌の数が不正です。{hand.Length}/{str}");
            return hand;
        }
    }
}
