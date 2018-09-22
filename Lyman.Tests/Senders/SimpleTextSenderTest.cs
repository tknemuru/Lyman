using Lyman.Di;
using Lyman.Helpers;
using Lyman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Senders;

namespace Lyman.Tests
{
    /// <summary>
    /// SimpleTextSenderのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class SimpleTextSenderTest : BaseUnitTest<SimpleTextSender>
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
            var actual = this.Target.Send(context);
            var expected = StringHelper.IEnumerableToString(FileHelper.ReadTextLines(this.GetResourcePath(1, 1, ResourceType.Out)));
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
            actual = this.Target.Send(context);
            expected = StringHelper.IEnumerableToString(FileHelper.ReadTextLines(this.GetResourcePath(1, 2, ResourceType.Out)));
            Assert.AreEqual(expected, actual);
        }
    }
}
