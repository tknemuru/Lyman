using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using Lyman.Models;

namespace Lyman.Senders
{
    public class SimpleTextSender : ISendable<FieldContext, string>
    {
        /// <summary>
        /// 状態を文字列に変換し送信します。
        /// </summary>
        /// <param name="context">フィールドの状態</param>
        /// <returns>フィールドの状態を表した文字列</returns>
        public string Send(FieldContext context)
        {
            return this.HandToString(context);
        }

        /// <summary>
        /// 手牌を文字列に変換します。
        /// </summary>
        /// <returns>文字列化した手牌</returns>
        /// <param name="context">フィールド状態</param>
        private string HandToString(FieldContext context)
        {
            var hand = new StringBuilder();
            for (var wind = 0; wind < Wind.Length; wind++)
            {
                if (context.Hands[wind].All(tile => tile <= 0))
                {
                    continue;
                }

                hand.Append($"{SimpleText.Key.Hand}{SimpleText.ValueSeparator}{Wind.JapaneseName.Get((Wind.Index)wind)}{SimpleText.KeyValueSeparator}");
                Hand.ForEach(i =>
                {
                    if (i > 0)
                    {
                        hand.Append(SimpleText.ValueSeparator);
                    }
                    hand.Append(context.Hands[wind][i].ToStringTile());
                });
                hand.AppendLine(string.Empty);
            }
            return hand.ToString();
        }
    }
}
