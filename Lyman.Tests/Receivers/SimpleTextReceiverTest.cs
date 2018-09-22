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
        }
    }
}
