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
    /// AnalyzeAllSimplesReceiverのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class AnalyzeAllSimplesReceiverTest : BaseUnitTest<AnalyzeAllSimplesReceiver>
    {
        /// <summary>
        /// 001:タンヤオの成立が判定できる
        /// </summary>
        [TestMethod]
        public void タンヤオの成立が判定できる()
        {
            // 001: 雀頭が2
            var request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            var context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("2索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索") };
            context.Pungs = new[] { Tile.BuildTile("2筒"), Tile.BuildTile("8筒") };
            request.WinHandsContext = context;
            var actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsTrue(actual.HasCompleted);

            // 002: 雀頭が8
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("8索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索") };
            context.Pungs = new[] { Tile.BuildTile("2筒"), Tile.BuildTile("8筒") };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsTrue(actual.HasCompleted);

            // 003: 順子(シュンツ)のみ
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("8索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索"), Tile.BuildTile("2筒"), Tile.BuildTile("2筒") };
            context.Pungs = new uint[] { };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsTrue(actual.HasCompleted);

            // 004: 刻子(コーツ)と槓子(カンツ)のみ
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("8索");
            context.Chows = new uint[] { };
            context.Pungs = new [] { Tile.BuildTile("2萬"), Tile.BuildTile("6索"), Tile.BuildTile("2筒") };
            context.Kongs = new[] { Tile.BuildTile("3筒") };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsTrue(actual.HasCompleted);
        }

        /// <summary>
        /// 002:タンヤオの不成立が判定できる
        /// </summary>
        [TestMethod]
        public void タンヤオの不成立が判定できる()
        {
            // 001: 雀頭が1
            var request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            var context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("1索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索") };
            context.Pungs = new[] { Tile.BuildTile("2筒"), Tile.BuildTile("8筒") };
            request.WinHandsContext = context;
            var actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 002: 雀頭が9
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("9索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索") };
            context.Pungs = new[] { Tile.BuildTile("2筒"), Tile.BuildTile("8筒") };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 003: 雀頭が字牌
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("中");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索") };
            context.Pungs = new[] { Tile.BuildTile("2筒"), Tile.BuildTile("8筒") };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 004: 順子(シュンツ)が1
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("8索");
            context.Chows = new[] { Tile.BuildTile("1萬"), Tile.BuildTile("6索") };
            context.Pungs = new[] { Tile.BuildTile("2筒"), Tile.BuildTile("8筒") };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 004: 順子(シュンツ)が7
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("8索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("7索") };
            context.Pungs = new[] { Tile.BuildTile("2筒"), Tile.BuildTile("8筒") };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 005: 刻子(コーツ)が1
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("8索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索") };
            context.Pungs = new[] { Tile.BuildTile("1筒"), Tile.BuildTile("8筒") };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 006: 刻子(コーツ)が9
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("8索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索") };
            context.Pungs = new[] { Tile.BuildTile("2筒"), Tile.BuildTile("9筒") };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 007: 刻子(コーツ)が字牌
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("8索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索") };
            context.Pungs = new[] { Tile.BuildTile("東"), Tile.BuildTile("8筒") };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 008: 槓子(カンツ)が字牌
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("8索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索") };
            context.Pungs = new[] { Tile.BuildTile("8筒") };
            context.Kongs = new[] { Tile.BuildTile("東") };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSimples, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);
        }
    }
}
