using Lyman.Di;
using Lyman.Helpers;
using Lyman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Converters;

namespace Lyman.Tests
{
    /// <summary>
    /// ContextToTextConverterのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class ContextToTextConverterTest : BaseUnitTest<ContextToTextConverter>
    {
        /// <summary>
        /// 001:手牌の書き込みができる
        /// </summary>
        [TestMethod]
        public void 手牌の書き込みができる()
        {
            // 001:全ての風が存在している
            var context = DiProvider.GetContainer().GetInstance<FieldContext>();
            Wind.ForEach((wind) =>
            {
                context.Hands[wind.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
                context.Hands[wind.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
                context.Hands[wind.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
                context.Hands[wind.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
                context.Hands[wind.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
                context.Hands[wind.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
                context.Hands[wind.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
                context.Hands[wind.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
                context.Hands[wind.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
                context.Hands[wind.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
                context.Hands[wind.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
                context.Hands[wind.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
                context.Hands[wind.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
            });
            var actual = this.Target.Convert(context);
            var expected = IEnumerableHelper.IEnumerableToString(FileHelper.ReadTextLines(this.GetResourcePath(1, 1, ResourceType.Out)));
            Assert.AreEqual(expected, actual);

            // 002:一部の風のみ存在する
            context = DiProvider.GetContainer().GetInstance<FieldContext>();
            context.Hands[Wind.Index.South.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            context.Hands[Wind.Index.South.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            context.Hands[Wind.Index.South.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            context.Hands[Wind.Index.South.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            context.Hands[Wind.Index.South.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            context.Hands[Wind.Index.South.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            context.Hands[Wind.Index.South.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            context.Hands[Wind.Index.South.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            context.Hands[Wind.Index.South.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            context.Hands[Wind.Index.South.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            context.Hands[Wind.Index.South.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            context.Hands[Wind.Index.South.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
            context.Hands[Wind.Index.South.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
            actual = this.Target.Convert(context);
            expected = IEnumerableHelper.IEnumerableToString(FileHelper.ReadTextLines(this.GetResourcePath(1, 2, ResourceType.Out)));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 002:壁牌の書き込みができる
        /// </summary>
        [TestMethod]
        public void 壁牌の書き込みができる()
        {
            // 001:全ての風が存在している
            var context = DiProvider.GetContainer().GetInstance<FieldContext>();
            Wind.ForEach(wind =>
            {
                Wall.ForEachRank(rank =>
                {
                    context.Walls[wind.ToInt()][rank.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
                    context.Walls[wind.ToInt()][rank.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
                    context.Walls[wind.ToInt()][rank.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
                    context.Walls[wind.ToInt()][rank.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
                    context.Walls[wind.ToInt()][rank.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
                    context.Walls[wind.ToInt()][rank.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
                    context.Walls[wind.ToInt()][rank.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
                    context.Walls[wind.ToInt()][rank.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
                    context.Walls[wind.ToInt()][rank.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
                    context.Walls[wind.ToInt()][rank.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
                    context.Walls[wind.ToInt()][rank.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
                    context.Walls[wind.ToInt()][rank.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
                    context.Walls[wind.ToInt()][rank.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
                    context.Walls[wind.ToInt()][rank.ToInt()][13] = Tile.BuildTile(Tile.Kind.East);
                    context.Walls[wind.ToInt()][rank.ToInt()][14] = Tile.BuildTile(Tile.Kind.West);
                    context.Walls[wind.ToInt()][rank.ToInt()][15] = Tile.BuildTile(Tile.Kind.South);
                    context.Walls[wind.ToInt()][rank.ToInt()][16] = Tile.BuildTile(Tile.Kind.North);
                });
            });
            var actual = this.Target.Convert(context);
            var expected = IEnumerableHelper.IEnumerableToString(FileHelper.ReadTextLines(this.GetResourcePath(2, 1, ResourceType.Out)));
            Assert.AreEqual(expected, actual);

            // 002:一部の風が存在している
            context = DiProvider.GetContainer().GetInstance<FieldContext>();
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][13] = Tile.BuildTile(Tile.Kind.East);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][14] = Tile.BuildTile(Tile.Kind.West);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][15] = Tile.BuildTile(Tile.Kind.South);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][16] = Tile.BuildTile(Tile.Kind.North);
            actual = this.Target.Convert(context);
            expected = IEnumerableHelper.IEnumerableToString(FileHelper.ReadTextLines(this.GetResourcePath(2, 2, ResourceType.Out)));
            Assert.AreEqual(expected, actual);

            // 003:一部の牌のみ存在する
            context = DiProvider.GetContainer().GetInstance<FieldContext>();
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            context.Walls[Wind.Index.North.ToInt()][Wall.Rank.Upper.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            actual = this.Target.Convert(context);
            expected = IEnumerableHelper.IEnumerableToString(FileHelper.ReadTextLines(this.GetResourcePath(2, 3, ResourceType.Out)));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 003:河の書き込みができる
        /// </summary>
        [TestMethod]
        public void 河の書き込みができる()
        {
            // 001:全ての風が存在している
            var context = DiProvider.GetContainer().GetInstance<FieldContext>();
            Wind.ForEach((wind) =>
            {
                context.Rivers[wind.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
                context.Rivers[wind.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
                context.Rivers[wind.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
                context.Rivers[wind.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
                context.Rivers[wind.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
                context.Rivers[wind.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
                context.Rivers[wind.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
                context.Rivers[wind.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
                context.Rivers[wind.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
                context.Rivers[wind.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
                context.Rivers[wind.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
                context.Rivers[wind.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
                context.Rivers[wind.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
                context.Rivers[wind.ToInt()][13] = Tile.BuildTile(Tile.Kind.East);
                context.Rivers[wind.ToInt()][14] = Tile.BuildTile(Tile.Kind.West);
                context.Rivers[wind.ToInt()][15] = Tile.BuildTile(Tile.Kind.South);
                context.Rivers[wind.ToInt()][16] = Tile.BuildTile(Tile.Kind.North);
                context.Rivers[wind.ToInt()][17] = Tile.BuildTile(Tile.Kind.Characters, 1);
                context.Rivers[wind.ToInt()][18] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
                context.Rivers[wind.ToInt()][19] = Tile.BuildTile(Tile.Kind.Circles, 3);
                context.Rivers[wind.ToInt()][20] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            });
            var actual = this.Target.Convert(context);
            var expected = IEnumerableHelper.IEnumerableToString(FileHelper.ReadTextLines(this.GetResourcePath(3, 1, ResourceType.Out)));
            Assert.AreEqual(expected, actual);

            // 002:一部の風のみ存在する
            context = DiProvider.GetContainer().GetInstance<FieldContext>();
            context.Rivers[Wind.Index.South.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            context.Rivers[Wind.Index.South.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            context.Rivers[Wind.Index.South.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            context.Rivers[Wind.Index.South.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            context.Rivers[Wind.Index.South.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            context.Rivers[Wind.Index.South.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            context.Rivers[Wind.Index.South.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            context.Rivers[Wind.Index.South.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            context.Rivers[Wind.Index.South.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            context.Rivers[Wind.Index.South.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            context.Rivers[Wind.Index.South.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            context.Rivers[Wind.Index.South.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
            context.Rivers[Wind.Index.South.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
            context.Rivers[Wind.Index.South.ToInt()][13] = Tile.BuildTile(Tile.Kind.East);
            context.Rivers[Wind.Index.South.ToInt()][14] = Tile.BuildTile(Tile.Kind.West);
            context.Rivers[Wind.Index.South.ToInt()][15] = Tile.BuildTile(Tile.Kind.South);
            context.Rivers[Wind.Index.South.ToInt()][16] = Tile.BuildTile(Tile.Kind.North);
            context.Rivers[Wind.Index.South.ToInt()][17] = Tile.BuildTile(Tile.Kind.Characters, 1);
            context.Rivers[Wind.Index.South.ToInt()][18] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            context.Rivers[Wind.Index.South.ToInt()][19] = Tile.BuildTile(Tile.Kind.Circles, 3);
            context.Rivers[Wind.Index.South.ToInt()][20] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            actual = this.Target.Convert(context);
            expected = IEnumerableHelper.IEnumerableToString(FileHelper.ReadTextLines(this.GetResourcePath(3, 2, ResourceType.Out)));
            Assert.AreEqual(expected, actual);

            // 003:一部の牌のみ存在する
            context = DiProvider.GetContainer().GetInstance<FieldContext>();
            context.Rivers[Wind.Index.South.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            context.Rivers[Wind.Index.South.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            context.Rivers[Wind.Index.South.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            context.Rivers[Wind.Index.South.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            context.Rivers[Wind.Index.South.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            context.Rivers[Wind.Index.South.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            context.Rivers[Wind.Index.South.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            context.Rivers[Wind.Index.South.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            context.Rivers[Wind.Index.South.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            context.Rivers[Wind.Index.South.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            context.Rivers[Wind.Index.South.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            context.Rivers[Wind.Index.South.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
            context.Rivers[Wind.Index.South.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
            actual = this.Target.Convert(context);
            expected = IEnumerableHelper.IEnumerableToString(FileHelper.ReadTextLines(this.GetResourcePath(3, 3, ResourceType.Out)));
            Assert.AreEqual(expected, actual);

            // 004:牌が一つも存在しない
            context = DiProvider.GetContainer().GetInstance<FieldContext>();
            actual = this.Target.Convert(context);
            Assert.AreEqual(string.Empty, actual);
        }
    }
}
