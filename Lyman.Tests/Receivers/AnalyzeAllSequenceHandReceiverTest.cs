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
    /// AnalyzeAllSequenceHandReceiverのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class AnalyzeAllSequenceHandReceiverTest : BaseUnitTest<AnalyzeAllSequenceHandReceiver>
    {
        /// <summary>
        /// 001:平和の成立が判定できる
        /// </summary>
        [TestMethod]
        public void 平和の成立が判定できる()
        {
            // 001: 
            var request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            request.Room = room;
            request.WinWind = Wind.Index.North;
            request.WinTile = Tile.BuildTile("3萬");
            var context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("2索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索"), Tile.BuildTile("7索"), Tile.BuildTile("7筒") };
            context.Pungs = new uint[] { };
            request.WinHandsContext = context;
            var actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSequenceHand, actual.Yaku);
            Assert.IsTrue(actual.HasCompleted);

            // 002: 頭が風牌
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            request.Room = room;
            request.WinWind = Wind.Index.North;
            request.WinTile = Tile.BuildTile("南");
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("2索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索"), Tile.BuildTile("7索"), Tile.BuildTile("7筒") };
            context.Pungs = new uint[] { };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSequenceHand, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);
        }

        /// <summary>
        /// 002:平和の不成立が判定できる
        /// </summary>
        [TestMethod]
        public void 平和の不成立が判定できる()
        {
            // 001: 刻子(コーツ)が含まれている
            var request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            request.Room = room;
            request.WinWind = Wind.Index.North;
            request.WinTile = Tile.BuildTile("3萬");
            var context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("2索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索"), Tile.BuildTile("7索") };
            context.Pungs = new uint[] { Tile.BuildTile("7筒") };
            request.WinHandsContext = context;
            var actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSequenceHand, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 002: 頭が三元牌
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            request.Room = room;
            request.WinWind = Wind.Index.North;
            request.WinTile = Tile.BuildTile("白");
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("2索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索"), Tile.BuildTile("7索"), Tile.BuildTile("7筒") };
            context.Pungs = new uint[] { };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSequenceHand, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 003: 頭が場風牌
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            request.Room = room;
            request.WinWind = Wind.Index.North;
            request.WinTile = Tile.BuildTile("東");
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("2索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索"), Tile.BuildTile("7索"), Tile.BuildTile("7筒") };
            context.Pungs = new uint[] { };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSequenceHand, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 004: 頭が自風牌
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            request.Room = room;
            request.WinWind = Wind.Index.North;
            request.WinTile = Tile.BuildTile("北");
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("2索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索"), Tile.BuildTile("7索"), Tile.BuildTile("7筒") };
            context.Pungs = new uint[] { };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSequenceHand, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);

            // 005: 和了牌の待ち方が両面待ちじゃない
            request = DiProvider.GetContainer().GetInstance<AnalyzeYakuRequest>();
            room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            request.Room = room;
            request.WinWind = Wind.Index.North;
            request.WinTile = Tile.BuildTile("2萬");
            context = DiProvider.GetContainer().GetInstance<WinHandsContext>();
            context.Head = Tile.BuildTile("2索");
            context.Chows = new[] { Tile.BuildTile("2萬"), Tile.BuildTile("6索"), Tile.BuildTile("7索"), Tile.BuildTile("7筒") };
            context.Pungs = new uint[] { };
            request.WinHandsContext = context;
            actual = this.Target.Receive(request);
            Assert.AreEqual(Yaku.AllSequenceHand, actual.Yaku);
            Assert.IsFalse(actual.HasCompleted);
        }
    }
}
