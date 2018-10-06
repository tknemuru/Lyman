using Lyman.Converters;
using Lyman.Di;
using Lyman.Helpers;
using Lyman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System.Collections.Generic;
// using System.Linq;
using System;

namespace Lyman.Tests
{
    /// <summary>
    /// TextToContextConverterのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class TextToContextConverterTest : BaseUnitTest<TextToContextConverter>
    {
        /// <summary>
        /// 001:手牌の読み込みができる
        /// </summary>
        [TestMethod]
        public void 手牌の読み込みができる()
        {
            // 001:全ての風が存在する
            var input = FileHelper.ReadTextLines(this.GetResourcePath(1, 1, ResourceType.In));
            var actual = this.Target.Convert(input);
            var expected = DiProvider.GetContainer().GetInstance<FieldContext>();
            Wind.ForEach((wind) =>
            {
                expected.Hands[wind.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
                expected.Hands[wind.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
                expected.Hands[wind.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
                expected.Hands[wind.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
                expected.Hands[wind.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
                expected.Hands[wind.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
                expected.Hands[wind.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
                expected.Hands[wind.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
                expected.Hands[wind.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
                expected.Hands[wind.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
                expected.Hands[wind.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
                expected.Hands[wind.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
                expected.Hands[wind.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
            });
            Assert.AreEqual(expected, actual);

            // 002:一部の風のみ存在する
            input = FileHelper.ReadTextLines(this.GetResourcePath(1, 2, ResourceType.In));
            actual = this.Target.Convert(input);
            expected = DiProvider.GetContainer().GetInstance<FieldContext>();
            expected.Hands[Wind.Index.South.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            expected.Hands[Wind.Index.South.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            expected.Hands[Wind.Index.South.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            expected.Hands[Wind.Index.South.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            expected.Hands[Wind.Index.South.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            expected.Hands[Wind.Index.South.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            expected.Hands[Wind.Index.South.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            expected.Hands[Wind.Index.South.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            expected.Hands[Wind.Index.South.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            expected.Hands[Wind.Index.South.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            expected.Hands[Wind.Index.South.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            expected.Hands[Wind.Index.South.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
            expected.Hands[Wind.Index.South.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 002:壁牌の読み込みができる
        /// </summary>
        [TestMethod]
        public void 壁牌の読み込みができる()
        {
            // 001:全ての風が存在する
            var input = FileHelper.ReadTextLines(this.GetResourcePath(2, 1, ResourceType.In));
            var actual = this.Target.Convert(input);
            var expected = DiProvider.GetContainer().GetInstance<FieldContext>();
            Wind.ForEach(wind =>
            {
                Wall.ForEachRank(rank =>
                {
                    expected.Walls[wind.ToInt()][rank.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
                    expected.Walls[wind.ToInt()][rank.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
                    expected.Walls[wind.ToInt()][rank.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
                    expected.Walls[wind.ToInt()][rank.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
                    expected.Walls[wind.ToInt()][rank.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
                    expected.Walls[wind.ToInt()][rank.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
                    expected.Walls[wind.ToInt()][rank.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
                    expected.Walls[wind.ToInt()][rank.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
                    expected.Walls[wind.ToInt()][rank.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
                    expected.Walls[wind.ToInt()][rank.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
                    expected.Walls[wind.ToInt()][rank.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
                    expected.Walls[wind.ToInt()][rank.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
                    expected.Walls[wind.ToInt()][rank.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
                    expected.Walls[wind.ToInt()][rank.ToInt()][13] = Tile.BuildTile(Tile.Kind.East);
                    expected.Walls[wind.ToInt()][rank.ToInt()][14] = Tile.BuildTile(Tile.Kind.West);
                    expected.Walls[wind.ToInt()][rank.ToInt()][15] = Tile.BuildTile(Tile.Kind.South);
                    expected.Walls[wind.ToInt()][rank.ToInt()][16] = Tile.BuildTile(Tile.Kind.North);
                });
            });
            Assert.AreEqual(expected, actual);

            // 002:一部の風のみ存在する
            input = FileHelper.ReadTextLines(this.GetResourcePath(2, 2, ResourceType.In));
            actual = this.Target.Convert(input);
            expected = DiProvider.GetContainer().GetInstance<FieldContext>();
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][13] = Tile.BuildTile(Tile.Kind.East);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][14] = Tile.BuildTile(Tile.Kind.West);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][15] = Tile.BuildTile(Tile.Kind.South);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][16] = Tile.BuildTile(Tile.Kind.North);
            Assert.AreEqual(expected, actual);

            // 003:一部の牌のみ存在する
            input = FileHelper.ReadTextLines(this.GetResourcePath(2, 3, ResourceType.In));
            actual = this.Target.Convert(input);
            expected = DiProvider.GetContainer().GetInstance<FieldContext>();
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            expected.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 003:河の読み込みができる
        /// </summary>
        [TestMethod]
        public void 河の読み込みができる()
        {
            // 001:全ての風が存在する
            var input = FileHelper.ReadTextLines(this.GetResourcePath(3, 1, ResourceType.In));
            var actual = this.Target.Convert(input);
            var expected = DiProvider.GetContainer().GetInstance<FieldContext>();
            Wind.ForEach((wind) =>
            {
                expected.Rivers[wind.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
                expected.Rivers[wind.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
                expected.Rivers[wind.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
                expected.Rivers[wind.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
                expected.Rivers[wind.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
                expected.Rivers[wind.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
                expected.Rivers[wind.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
                expected.Rivers[wind.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
                expected.Rivers[wind.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
                expected.Rivers[wind.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
                expected.Rivers[wind.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
                expected.Rivers[wind.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
                expected.Rivers[wind.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
                expected.Rivers[wind.ToInt()][13] = Tile.BuildTile(Tile.Kind.East);
                expected.Rivers[wind.ToInt()][14] = Tile.BuildTile(Tile.Kind.West);
                expected.Rivers[wind.ToInt()][15] = Tile.BuildTile(Tile.Kind.South);
                expected.Rivers[wind.ToInt()][16] = Tile.BuildTile(Tile.Kind.North);
                expected.Rivers[wind.ToInt()][17] = Tile.BuildTile(Tile.Kind.Characters, 1);
                expected.Rivers[wind.ToInt()][18] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
                expected.Rivers[wind.ToInt()][19] = Tile.BuildTile(Tile.Kind.Circles, 3);
                expected.Rivers[wind.ToInt()][20] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            });
            Assert.AreEqual(expected, actual);

            // 002:一部の風のみ存在する
            input = FileHelper.ReadTextLines(this.GetResourcePath(3, 2, ResourceType.In));
            actual = this.Target.Convert(input);
            expected = DiProvider.GetContainer().GetInstance<FieldContext>();
            expected.Rivers[Wind.Index.South.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            expected.Rivers[Wind.Index.South.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            expected.Rivers[Wind.Index.South.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            expected.Rivers[Wind.Index.South.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            expected.Rivers[Wind.Index.South.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            expected.Rivers[Wind.Index.South.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            expected.Rivers[Wind.Index.South.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            expected.Rivers[Wind.Index.South.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            expected.Rivers[Wind.Index.South.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            expected.Rivers[Wind.Index.South.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            expected.Rivers[Wind.Index.South.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            expected.Rivers[Wind.Index.South.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
            expected.Rivers[Wind.Index.South.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
            expected.Rivers[Wind.Index.South.ToInt()][13] = Tile.BuildTile(Tile.Kind.East);
            expected.Rivers[Wind.Index.South.ToInt()][14] = Tile.BuildTile(Tile.Kind.West);
            expected.Rivers[Wind.Index.South.ToInt()][15] = Tile.BuildTile(Tile.Kind.South);
            expected.Rivers[Wind.Index.South.ToInt()][16] = Tile.BuildTile(Tile.Kind.North);
            expected.Rivers[Wind.Index.South.ToInt()][17] = Tile.BuildTile(Tile.Kind.Characters, 1);
            expected.Rivers[Wind.Index.South.ToInt()][18] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            expected.Rivers[Wind.Index.South.ToInt()][19] = Tile.BuildTile(Tile.Kind.Circles, 3);
            expected.Rivers[Wind.Index.South.ToInt()][20] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            Assert.AreEqual(expected, actual);

            // 003:一部の牌のみ存在している
            input = FileHelper.ReadTextLines(this.GetResourcePath(3, 3, ResourceType.In));
            actual = this.Target.Convert(input);
            expected = DiProvider.GetContainer().GetInstance<FieldContext>();
            expected.Rivers[Wind.Index.South.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            expected.Rivers[Wind.Index.South.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            expected.Rivers[Wind.Index.South.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            expected.Rivers[Wind.Index.South.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            expected.Rivers[Wind.Index.South.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            expected.Rivers[Wind.Index.South.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            expected.Rivers[Wind.Index.South.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            expected.Rivers[Wind.Index.South.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            expected.Rivers[Wind.Index.South.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            expected.Rivers[Wind.Index.South.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            expected.Rivers[Wind.Index.South.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            expected.Rivers[Wind.Index.South.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
            expected.Rivers[Wind.Index.South.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
            Assert.AreEqual(expected, actual);

            // 004:牌が一つも存在しない
            input = FileHelper.ReadTextLines(this.GetResourcePath(3, 4, ResourceType.In));
            actual = this.Target.Convert(input);
            expected = DiProvider.GetContainer().GetInstance<FieldContext>();
            Assert.AreEqual(expected, actual);
        }
    }
}
