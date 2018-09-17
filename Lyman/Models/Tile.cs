﻿using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;

namespace Lyman.Models
{
    /// <summary>
    /// 牌
    /// </summary>
    public static class Tile
    {
        /// <summary>
        /// 種類
        /// </summary>
        public enum Kind : uint
        {
            /// <summary>
            /// 未定義
            /// </summary>
            Undefined,

            /// <summary>
            /// 萬子
            /// </summary>
            Characters,

            /// <summary>
            /// 筒子
            /// </summary>
            Circles,

            /// <summary>
            /// 索子
            /// </summary>
            Bamboos,

            /// <summary>
            /// 白
            /// </summary>
            WhiteDragon,

            /// <summary>
            /// 発
            /// </summary>
            GreenDragon,

            /// <summary>
            /// 中
            /// </summary>
            RedDragon,

            /// <summary>
            /// 東
            /// </summary>
            East,

            /// <summary>
            /// 南
            /// </summary>
            South,

            /// <summary>
            /// 西
            /// </summary>
            West,

            /// <summary>
            /// 北
            /// </summary>
            North,
        }

        /// <summary>
        /// グループ
        /// </summary>
        public enum Group
        {
            /// <summary>
            /// 未定義
            /// </summary>
            Undefined,

            /// <summary>
            /// 数牌(シューパイ)
            /// </summary>
            Suits,

            /// <summary>
            /// 字牌(ジハイ)
            /// </summary>
            Honours,
        }

        /// <summary>
        /// 日本語名称辞書
        /// </summary>
        public static readonly TwoWayDictionary<Kind, string> JapaneseName = new TwoWayDictionary<Kind, string>
        (
            new[]
            {
                Kind.Bamboos,
                Kind.Characters,
                Kind.Circles,
                Kind.East,
                Kind.GreenDragon,
                Kind.North,
                Kind.RedDragon,
                Kind.South,
                Kind.West,
                Kind.WhiteDragon,
            },
            new[]
            {
                "索",
                "萬",
                "筒",
                "東",
                "発",
                "北",
                "中",
                "南",
                "西",
                "白",
            }
        );

        /// <summary>
        /// 数領域のシフト量
        /// </summary>
        private const int NumberShift = 4;

        /// <summary>
        /// 赤ドラのシフト量
        /// </summary>
        private const int RedFiveShift = 8;

        /// <summary>
        /// 種類のマスク値
        /// </summary>
        private const uint KindMask = 0b1111;

        /// <summary>
        /// 数のリスト
        /// </summary>
        private static readonly IEnumerable<int> Numbers = Enumerable.Range(1, 9);

        /// <summary>
        /// 牌の種別を取得します。
        /// </summary>
        /// <returns>牌の種別</returns>
        /// <param name="kind">牌</param>
        public static Group GetGroup(this Kind kind)
        {
            Group group = Group.Undefined;
            switch (kind)
            {
                case Kind.Bamboos:
                case Kind.Characters:
                case Kind.Circles:
                    group = Group.Suits;
                    break;
                case Kind.East:
                case Kind.GreenDragon:
                case Kind.North:
                case Kind.RedDragon:
                case Kind.South:
                case Kind.West:
                case Kind.WhiteDragon:
                    group = Group.Honours;
                    break;
                default:
                    throw new ArgumentException("牌が不正です。");
            }
            return group;
        }

        /// <summary>
        /// 牌の種類を取得します。
        /// </summary>
        /// <returns>牌の種類</returns>
        /// <param name="tile">牌</param>
        public static Kind GetKind(this uint tile)
        {
            return (Kind)(tile & KindMask);
        }

        /// <summary>
        /// 牌を組み立てます。
        /// </summary>
        /// <returns>牌</returns>
        /// <param name="kind">種類</param>
        /// <param name="number">数</param>
        /// <param name="isRed">赤ドラかどうか</param>
        public static uint BuildTile(Kind kind, int number = 0, bool isRed = false)
        {
            Debug.Assert((kind.GetGroup() == Group.Honours && number == 0) ||
             (kind.GetGroup() == Group.Suits && Numbers.Contains(number)),
             $"数が不正です。{number}");
            return (uint)kind | ((uint)number << Tile.NumberShift) | ((isRed ? 1u : 0u) << Tile.RedFiveShift);
        }

        /// <summary>
        /// 牌を組み立てます。
        /// </summary>
        /// <returns>牌</returns>
        /// <param name="str">牌を示す文字列</param>
        public static uint BuildTile(string str)
        {
            Debug.Assert(str.Length == 1 || str.Length == 2, $"文字列の長さが不正です。{str}");
            var _str = str.Length == 2 ? str : "0" + str;
            var kind = JapaneseName.Get(_str.Substring(1, 1));
            var num = int.Parse(_str.Substring(0, 1));
            return BuildTile(kind, num);
        }
    }
}
