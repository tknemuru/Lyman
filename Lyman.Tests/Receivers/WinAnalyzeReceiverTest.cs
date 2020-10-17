using Lyman.Di;
using Lyman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Receivers;

namespace Lyman.Tests
{
    /// <summary>
    /// WinAnalyzeReceiverのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class WinAnalyzeReceiverTest : BaseUnitTest<WinAnalyzeReceiver>
    {
        /// <summary>
        /// 001:単一のあがりが判定できる
        /// </summary>
        [TestMethod]
        public void 単一のあがりが判定できる()
        {
            // 001:
            var request = this.LoadTiles(1, 1, ResourceType.In);
            var expected = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            expected.Head = Tile.BuildTile("東");
            expected.Chows = new[] { Tile.BuildTile("3萬"), Tile.BuildTile("5索") };
            expected.Pungs = new[] { Tile.BuildTile("3筒"), Tile.BuildTile("中") };
            var actual = this.Target.Receive(request);
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(expected, actual.First());
            Assert.IsTrue(actual.First().Winnable);
        }

        /// <summary>
        /// 002:順子優先で重なりのあるあがりが判定できる
        /// </summary>
        [TestMethod]
        public void 順子優先で重なりのあるあがりが判定できる()
        {
            // 001: 
            var request = this.LoadTiles(2, 1, ResourceType.In);
            var expected = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            expected.Head = Tile.BuildTile("5萬");
            expected.Chows = new[] { Tile.BuildTile("3萬"), Tile.BuildTile("5萬") };
            expected.Pungs = new[] { Tile.BuildTile("3筒"), Tile.BuildTile("中") };
            var actual = this.Target.Receive(request);
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(expected, actual.First());
            Assert.IsTrue(actual.First().Winnable);
        }

        /// <summary>
        /// 003:刻子優先で重なりのあるあがりが判定できる
        /// </summary>
        [TestMethod]
        public void 刻子優先で重なりのあるあがりが判定できる()
        {
            // 001: 
            var request = this.LoadTiles(3, 1, ResourceType.In);
            var expected = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            expected.Head = Tile.BuildTile("3筒");
            expected.Chows = new[] { Tile.BuildTile("3萬"), Tile.BuildTile("6萬") };
            expected.Pungs = new[] { Tile.BuildTile("5萬"), Tile.BuildTile("中") };
            var actual = this.Target.Receive(request);
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(expected, actual.First());
            Assert.IsTrue(actual.First().Winnable);
        }

        /// <summary>
        /// 004:刻子二連続であがりが判定できる
        /// </summary>
        [TestMethod]
        public void 刻子二連続であがりが判定できる()
        {
            // 001: 
            var request = this.LoadTiles(4, 1, ResourceType.In);
            var expected = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            expected.Head = Tile.BuildTile("3筒");
            expected.Chows = new[] { Tile.BuildTile("3萬") };
            expected.Pungs = new[] { Tile.BuildTile("5萬"), Tile.BuildTile("6萬"), Tile.BuildTile("中") };
            var actual = this.Target.Receive(request);
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(expected, actual.First());
            Assert.IsTrue(actual.First().Winnable);
        }

        /// <summary>
        /// 005:同一順子であがりが判定できる
        /// </summary>
        [TestMethod]
        public void 同一順子であがりが判定できる()
        {
            // 001: 2つ
            var request = this.LoadTiles(5, 1, ResourceType.In);
            var expected = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            expected.Head = Tile.BuildTile("5萬");
            expected.Chows = new[] { Tile.BuildTile("3萬"), Tile.BuildTile("3萬"), Tile.BuildTile("3筒") };
            expected.Pungs = new[] { Tile.BuildTile("3筒") };
            var actual = this.Target.Receive(request);
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(expected, actual.First());
            Assert.IsTrue(actual.First().Winnable);
        }

        /// <summary>
        /// 006:あがりの解釈が二種類ある判定ができる
        /// </summary>
        [TestMethod]
        public void あがりの解釈が二種類ある判定ができる()
        {
            // 001:
            var request = this.LoadTiles(6, 1, ResourceType.In);
            var expected = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            var actual = this.Target.Receive(request);
            Assert.AreEqual(2, actual.Count());
            var ac = actual.ElementAt(0);
            expected.Head = Tile.BuildTile("3筒");
            expected.Chows = new[] { Tile.BuildTile("3萬"), Tile.BuildTile("3萬"), Tile.BuildTile("3萬"), Tile.BuildTile("3筒") };
            expected.Pungs = new uint[] { };
            Assert.AreEqual(expected, ac);
            Assert.IsTrue(ac.Winnable);
            ac = actual.ElementAt(1);
            expected.Head = Tile.BuildTile("3筒");
            expected.Chows = new[] { Tile.BuildTile("3筒") };
            expected.Pungs = new[] { Tile.BuildTile("3萬"), Tile.BuildTile("4萬"), Tile.BuildTile("5萬") };
            Assert.AreEqual(expected, ac);
            Assert.IsTrue(ac.Winnable);
        }

        /// <summary>
        /// 007:雀頭がなくてあがれない
        /// </summary>
        [TestMethod]
        public void 雀頭がなくてあがれない()
        {
            // 001:
            var request = this.LoadTiles(7, 1, ResourceType.In);
            var actual = this.Target.Receive(request);
            Assert.AreEqual(0, actual.Count());
        }

        /// <summary>
        /// 008:面子がなくてあがれない
        /// </summary>
        [TestMethod]
        public void 面子がなくてあがれない()
        {
            // 001:
            var request = this.LoadTiles(8, 1, ResourceType.In);
            var actual = this.Target.Receive(request);
            Assert.AreEqual(0, actual.Count());
        }

        /// <summary>
        /// 009:字牌だけであがり判定ができる
        /// </summary>
        [TestMethod]
        public void 字牌だけであがり判定ができる()
        {
            // 001: 
            var request = this.LoadTiles(9, 1, ResourceType.In);
            var expected = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            expected.Head = Tile.BuildTile("北");
            expected.Pungs = new[] { Tile.BuildTile("中"), Tile.BuildTile("東"), Tile.BuildTile("南"), Tile.BuildTile("西") };
            var actual = this.Target.Receive(request);
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(expected, actual.First());
            Assert.IsTrue(actual.First().Winnable);
        }
    }
}
