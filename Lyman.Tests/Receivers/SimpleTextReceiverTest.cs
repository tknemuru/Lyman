using Lyman.Receivers;
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
    /// SimpleTextReceiverのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class SimpleTextReceiverTest : BaseUnitTest<SimpleTextReceiver>
    {
        /// <summary>
        /// 001:手牌の読み込みができる
        /// </summary>
        [TestMethod]
        public void 手牌の読み込みができる()
        {
            // 001:全ての風が存在する
            var input = FileHelper.ReadTextLines(this.GetResourcePath(1, 1, ResourceType.In));
            var actual = this.Target.Receive(input);
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
            actual = this.Target.Receive(input);
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
            var actual = this.Target.Receive(input);
            var expected = DiProvider.GetContainer().GetInstance<FieldContext>();
            Wind.ForEach((wind) =>
            {
                expected.Walls[wind.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
                expected.Walls[wind.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
                expected.Walls[wind.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
                expected.Walls[wind.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
                expected.Walls[wind.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
                expected.Walls[wind.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
                expected.Walls[wind.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
                expected.Walls[wind.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
                expected.Walls[wind.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
                expected.Walls[wind.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
                expected.Walls[wind.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
                expected.Walls[wind.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
                expected.Walls[wind.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
                expected.Walls[wind.ToInt()][13] = Tile.BuildTile(Tile.Kind.East);
                expected.Walls[wind.ToInt()][14] = Tile.BuildTile(Tile.Kind.West);
                expected.Walls[wind.ToInt()][15] = Tile.BuildTile(Tile.Kind.South);
                expected.Walls[wind.ToInt()][16] = Tile.BuildTile(Tile.Kind.North);
            });
            Assert.AreEqual(expected, actual);

            // 002:一部の風のみ存在する
            input = FileHelper.ReadTextLines(this.GetResourcePath(2, 2, ResourceType.In));
            actual = this.Target.Receive(input);
            expected = DiProvider.GetContainer().GetInstance<FieldContext>();
            expected.Walls[Wind.Index.North.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            expected.Walls[Wind.Index.North.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            expected.Walls[Wind.Index.North.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            expected.Walls[Wind.Index.North.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            expected.Walls[Wind.Index.North.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            expected.Walls[Wind.Index.North.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            expected.Walls[Wind.Index.North.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            expected.Walls[Wind.Index.North.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            expected.Walls[Wind.Index.North.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            expected.Walls[Wind.Index.North.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            expected.Walls[Wind.Index.North.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            expected.Walls[Wind.Index.North.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5, true);
            expected.Walls[Wind.Index.North.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);
            expected.Walls[Wind.Index.North.ToInt()][13] = Tile.BuildTile(Tile.Kind.East);
            expected.Walls[Wind.Index.North.ToInt()][14] = Tile.BuildTile(Tile.Kind.West);
            expected.Walls[Wind.Index.North.ToInt()][15] = Tile.BuildTile(Tile.Kind.South);
            expected.Walls[Wind.Index.North.ToInt()][16] = Tile.BuildTile(Tile.Kind.North);
            Assert.AreEqual(expected, actual);

            // 003:一部の牌のみ存在する
            input = FileHelper.ReadTextLines(this.GetResourcePath(2, 3, ResourceType.In));
            actual = this.Target.Receive(input);
            expected = DiProvider.GetContainer().GetInstance<FieldContext>();
            expected.Walls[Wind.Index.North.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            expected.Walls[Wind.Index.North.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            expected.Walls[Wind.Index.North.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            expected.Walls[Wind.Index.North.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            expected.Walls[Wind.Index.North.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            expected.Walls[Wind.Index.North.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            expected.Walls[Wind.Index.North.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            expected.Walls[Wind.Index.North.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            expected.Walls[Wind.Index.North.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            expected.Walls[Wind.Index.North.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            expected.Walls[Wind.Index.North.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            Assert.AreEqual(expected, actual);

        }
    }
}
