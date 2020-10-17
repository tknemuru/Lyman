using Lyman.Di;
using Lyman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Receivers;
using Lyman.Models.Requests;

namespace Lyman.Tests
{
    /// <summary>
    /// AnalyzeTwoIdenticalSequencesReceiverのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class AnalyzeTwoIdenticalSequencesReceiverTest : BaseUnitTest<AnalyzeTwoIdenticalSequencesReceiver>
    {
        /// <summary>
        /// 001:一盃口の成立が判定できる
        /// </summary>
        [TestMethod]
        public void 一盃口の成立が判定できる()
        {
            // 001: 
            var request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            var context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("2索");
            context.Chows = new[] { Tile.BuildTile("6索"), Tile.BuildTile("6索"), Tile.BuildTile("7筒") };
            context.Pungs = new uint[] { Tile.BuildTile("2萬") };
            request.WinHandsContext = context;
            var actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.TwoIdenticalSequences, actual.Yaku);
            Assert.IsTrue(actual.HasCompleted);
        }

        /// <summary>
        /// 002:一盃口の不成立が判定できる
        /// </summary>
        [TestMethod]
        public void 一盃口の不成立が判定できる()
        {
            // 001: 順子無し
            var request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            var context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("2索");
            context.Chows = new uint[] { };
            context.Pungs = new uint[] { Tile.BuildTile("7筒"), Tile.BuildTile("2萬"), Tile.BuildTile("6索"), Tile.BuildTile("7索") };
            request.WinHandsContext = context;
            var actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.TwoIdenticalSequences, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 002: 一つ
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("2索");
            context.Chows = new[] { Tile.BuildTile("6索"), Tile.BuildTile("2萬"), Tile.BuildTile("7筒") };
            context.Pungs = new uint[] { Tile.BuildTile("2萬") };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.TwoIdenticalSequences, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 003: 三つ
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("2索");
            context.Chows = new[] { Tile.BuildTile("6索"), Tile.BuildTile("6索"), Tile.BuildTile("6索") };
            context.Pungs = new uint[] { Tile.BuildTile("2萬") };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.TwoIdenticalSequences, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);
        }
    }
}
