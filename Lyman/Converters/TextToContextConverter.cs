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
    public sealed class TextToContextConverter : IConvertible<IEnumerable<string>, FieldContext>
    {
        /// <summary>
        /// 標準テキストファイルをフィールドの状態に変換します。
        /// </summary>
        /// <param name="source">標準テキストファイルパス</param>
        /// <returns>フィールドの状態</returns>
        public FieldContext Convert(IEnumerable<string> source)
        {
            var context = DiProvider.GetContainer().GetInstance<FieldContext>();

            foreach(var s in source)
            {
                var keyValue = s.Split(SimpleText.KeyValueSeparator);
                Debug.Assert(keyValue.Count() == 2, "要素数が不正です。");

                var keys = keyValue[0].Split(SimpleText.ValueSeparator);

                switch(keys[0])
                {
                    case SimpleText.Key.OpenGatePosition:
                        switch(keyValue[0])
                        {
                            case SimpleText.Key.OpenGatePostionWind:
                                context.OpenGatePosition.Wind = Wind.JapaneseName.Get(keyValue[1]);
                                break;
                            case SimpleText.Key.OpenGatePostionIndex:
                                context.OpenGatePosition.Index = int.Parse(keyValue[1]);
                                break;
                            default:
                                throw new ArgumentException($"キーが不正です。{keyValue[0]}");
                        }
                        break;
                    case SimpleText.Key.Hand:
                        Debug.Assert(keys.Count() == Hand.KeyLength, "キーの数が不正です。");
                        context.Hands[Wind.JapaneseName.Get(keys[1]).ToInt()] = ParseHand(keyValue[1]);
                        break;
                    case SimpleText.Key.Wall:
                        Debug.Assert(keys.Count() == Wall.KeyLength, "キーの数が不正です。");
                        context.Walls[Wind.JapaneseName.Get(keys[Wall.Key.Wind.ToInt()]).ToInt()][Wall.RankJapaneseName.Get(keys[Wall.Key.Rank.ToInt()]).ToInt()] = ParseWall(keyValue[1]);
                        break;
                    case SimpleText.Key.River:
                        Debug.Assert(keys.Count() == River.KeyLength, "キーの数が不正です。");
                        context.Rivers[Wind.JapaneseName.Get(keys[1]).ToInt()] = ParseRiver(keyValue[1]);
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
            var hand = ParseTileArray(str);
            Debug.Assert(hand.Length == Hand.Length, $"手牌の数が不正です。{hand.Length}/{str}");
            return hand;
        }

        /// <summary>
        /// 壁牌への変換を行います。
        /// </summary>
        /// <returns>壁牌</returns>
        /// <param name="str">壁牌を示す文字列</param>
        private static uint[] ParseWall(string str)
        {
            var tiles = ParseTileArray(str);
            tiles = tiles.Concat(Enumerable.Range(0, Wall.Length - tiles.Length).Select(i => 0u)).ToArray();
            Debug.Assert(tiles.Length <= Wall.Length, $"壁牌の数が不正です。{tiles.Length}/{str}");
            return tiles;
        }

        /// <summary>
        /// 河への変換を行います。
        /// </summary>
        /// <returns>河</returns>
        /// <param name="str">河を示す文字列</param>
        private static uint[] ParseRiver(string str)
        {
            var tiles = ParseTileArray(str);
            tiles = tiles.Concat(Enumerable.Range(0, River.Length - tiles.Length).Select(i => 0u)).ToArray();
            Debug.Assert(tiles.Length <= River.Length, $"河の数が不正です。{tiles.Length}/{str}");
            return tiles;
        }

        /// <summary>
        /// 牌の配列へ変換します。
        /// </summary>
        /// <returns>牌の配列</returns>
        /// <param name="str">牌の配列を示す文字列</param>
        private static uint[] ParseTileArray(string str)
        {
            return str.Split(SimpleText.ValueSeparator).Where(s => s != string.Empty).Select(Tile.BuildTile).ToArray();
        }
    }
}
