using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using Lyman.Models;

namespace Lyman.Converters
{
    /// <summary>
    /// フィールド状態をLymanにおける標準テキスト形式文字列に変換する機能を提供します。
    /// </summary>
    public class ContextToTextConverter : IConvertible<FieldContext, string>
    {
        /// <summary>
        /// 状態を文字列に変換します。
        /// </summary>
        /// <param name="context">フィールドの状態</param>
        /// <returns>フィールドの状態を表した文字列</returns>
        public string Convert(FieldContext context)
        {
            var contextSb = new StringBuilder();
            contextSb.Append(this.OpenGatePositionToString(context));
            contextSb.Append(this.HandToString(context));
            contextSb.Append(this.WallToString(context));
            contextSb.Append(this.RiverToString(context));
            return contextSb.ToString();
        }

        /// <summary>
        /// 開門位置を文字列に変換します。
        /// </summary>
        /// <returns>文字列化した開門位置</returns>
        /// <param name="context">フィールド状態</param>
        private string OpenGatePositionToString(FieldContext context)
        {
            if (context.OpenGatePosition.Wind == Wind.Index.Undefined ||
               context.OpenGatePosition.Index < 0)
            {
                return string.Empty;
            }

            var position = new StringBuilder();
            position.AppendLine($"{SimpleText.Key.OpenGatePostionWind}{SimpleText.KeyValueSeparator}{Wind.JapaneseName.Get(context.OpenGatePosition.Wind)}");
            position.AppendLine($"{SimpleText.Key.OpenGatePostionIndex}{SimpleText.KeyValueSeparator}{context.OpenGatePosition.Index}");
            return position.ToString();
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
                if (context.Hands[wind].All(tile => tile <= Tile.Kind.Undefined.ToUint()))
                {
                    continue;
                }

                hand.Append($"{SimpleText.Key.Hand}{SimpleText.ValueSeparator}{Wind.JapaneseName.Get((Wind.Index)wind)}{SimpleText.KeyValueSeparator}");
                var count = context.Hands[wind].Count();
                for (var i = 0; i < count; i++)
                {
                    if (context.Hands[wind][i].GetKind() != Tile.Kind.Undefined)
                    {
                        if (i > 0)
                        {
                            hand.Append(SimpleText.ValueSeparator);
                        }
                        hand.Append(context.Hands[wind][i].ToStringTile());
                    }
                }
                hand.AppendLine(string.Empty);
            }
            return hand.ToString();
        }

        /// <summary>
        /// 河を文字列に変換します。
        /// </summary>
        /// <returns>文字列化した河</returns>
        /// <param name="context">フィールド状態</param>
        private string RiverToString(FieldContext context)
        {
            var river = new StringBuilder();
            for (var wind = 0; wind < Wind.Length; wind++)
            {
                if (context.Rivers[wind].All(tile => tile.GetKind() == Tile.Kind.Undefined))
                {
                    continue;
                }

                river.Append($"{SimpleText.Key.River}{SimpleText.ValueSeparator}{Wind.JapaneseName.Get((Wind.Index)wind)}{SimpleText.KeyValueSeparator}");
                var count = context.Rivers[wind].Count();
                for (var i = 0; i < count; i++)
                {
                    if (context.Rivers[wind][i].GetKind() == Tile.Kind.Undefined)
                    {
                        break;
                    }
                    if (i > 0)
                    {
                        river.Append(SimpleText.ValueSeparator);
                    }
                    river.Append(context.Rivers[wind][i].ToStringTile());
                }
                river.AppendLine(string.Empty);
            }
            return river.ToString();
        }

        /// <summary>
        /// 壁牌を文字列に変換します。
        /// </summary>
        /// <returns>文字列化した壁牌</returns>
        /// <param name="context">フィールド状態</param>
        private string WallToString(FieldContext context)
        {
            var wall = new StringBuilder();
            for (var wind = 0; wind < Wind.Length; wind++)
            {
                for (var rank = 0; rank < Wall.RankLength; rank++)
                {
                    if (context.Walls[wind][rank].All(tile => tile.GetKind() == Tile.Kind.Undefined))
                    {
                        continue;
                    }

                    wall.Append(SimpleText.Key.Wall);
                    wall.Append(SimpleText.ValueSeparator);
                    wall.Append(Wind.JapaneseName.Get((Wind.Index)wind));
                    wall.Append(SimpleText.ValueSeparator);
                    wall.Append(Wall.RankJapaneseName.Get((Wall.Rank)rank));
                    wall.Append(SimpleText.KeyValueSeparator);
                    for (var i = 0; i < Wall.Length; i++)
                    {
                        if (context.Walls[wind][rank][i].GetKind() == Tile.Kind.Undefined)
                        {
                            break;
                        }
                        if (i > 0)
                        {
                            wall.Append(SimpleText.ValueSeparator);
                        }
                        wall.Append(context.Walls[wind][rank][i].ToStringTile());
                    }
                    wall.AppendLine(string.Empty);
                }
            }
            return wall.ToString();
        }
    }
}
