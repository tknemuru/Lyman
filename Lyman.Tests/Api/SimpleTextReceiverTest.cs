using Lyman.Api.Receivers;
using Lyman.Di;
using Lyman.Helpers;
using Lyman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System.Collections.Generic;
// using System.Linq;
using System;

namespace Lyman.Tests.Api
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
            expected.Hands[Wind.Index.East.ToInt()][0] = Tile.BuildTile(Tile.Kind.East);
            expected.Hands[Wind.Index.East.ToInt()][1] = Tile.BuildTile(Tile.Kind.West);
            expected.Hands[Wind.Index.East.ToInt()][2] = Tile.BuildTile(Tile.Kind.South);
            expected.Hands[Wind.Index.East.ToInt()][3] = Tile.BuildTile(Tile.Kind.North);
            expected.Hands[Wind.Index.East.ToInt()][4] = Tile.BuildTile(Tile.Kind.Characters, 1);
            expected.Hands[Wind.Index.East.ToInt()][5] = Tile.BuildTile(Tile.Kind.Bamboos, 2);
            expected.Hands[Wind.Index.East.ToInt()][6] = Tile.BuildTile(Tile.Kind.Circles, 3);
            expected.Hands[Wind.Index.East.ToInt()][7] = Tile.BuildTile(Tile.Kind.WhiteDragon);
            expected.Hands[Wind.Index.East.ToInt()][8] = Tile.BuildTile(Tile.Kind.GreenDragon);
            expected.Hands[Wind.Index.East.ToInt()][9] = Tile.BuildTile(Tile.Kind.RedDragon);
            expected.Hands[Wind.Index.East.ToInt()][10] = Tile.BuildTile(Tile.Kind.Characters, 4);
            expected.Hands[Wind.Index.East.ToInt()][11] = Tile.BuildTile(Tile.Kind.Bamboos, 5);
            expected.Hands[Wind.Index.East.ToInt()][12] = Tile.BuildTile(Tile.Kind.Circles, 6);

            Assert.AreEqual(expected, actual);
        }
    }
}
