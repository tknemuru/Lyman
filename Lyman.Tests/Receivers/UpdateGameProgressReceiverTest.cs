using Lyman.Di;
using Lyman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Receivers;
using Lyman.Models.Requests;

namespace Lyman.Tests
{
    /// <summary>
    /// UpdateGameProgressReceiverのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class UpdateGameProgressReceiverTest : BaseUnitTest<UpdateGameProgressReceiver>
    {
        /// <summary>
        /// 001:ツモ数の更新ができる
        /// </summary>
        [TestMethod]
        public void ツモ数の更新ができる()
        {
            var context = this.LoadFieldContext(1, 1, ResourceType.In);

            // 001:0
            var request = DiProvider.GetContainer().GetInstance<UpdateGameProgressRequest>();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            room.Parent = Wind.Index.South;
            room.GameCount = 2;
            room.HomeCount = 2;
            room.DrawCount = 0;
            room.Turn = Wind.Index.East;
            room.State = RoomState.Dealted;
            room.Context = context;
            request.Room = room;
            var expected = DiProvider.GetContainer().GetInstance<Room>();
            expected.Round = Wind.Index.East;
            expected.Parent = Wind.Index.South;
            expected.GameCount = 2;
            expected.HomeCount = 2;
            expected.DrawCount = 1;
            expected.Turn = Wind.Index.South;
            expected.State = RoomState.Dealted;
            expected.Context = context;
            var actual = this.Target.Receive(request).Room;
            Assert.AreEqual(expected, actual);

            // 001:0以外
            request = DiProvider.GetContainer().GetInstance<UpdateGameProgressRequest>();
            room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            room.Parent = Wind.Index.South;
            room.GameCount = 2;
            room.HomeCount = 2;
            room.DrawCount = 10;
            room.Turn = Wind.Index.East;
            room.State = RoomState.Dealted;
            room.Context = context;
            request.Room = room;
            expected = DiProvider.GetContainer().GetInstance<Room>();
            expected.Round = Wind.Index.East;
            expected.Parent = Wind.Index.South;
            expected.GameCount = 2;
            expected.HomeCount = 2;
            expected.DrawCount = 11;
            expected.Turn = Wind.Index.South;
            expected.State = RoomState.Dealted;
            expected.Context = context;
            actual = this.Target.Receive(request).Room;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 002:親があがって場の更新ができる
        /// </summary>
        [TestMethod]
        public void 親があがって場の更新ができる()
        {
            var request = DiProvider.GetContainer().GetInstance<UpdateGameProgressRequest>();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            room.Parent = Wind.Index.East;
            room.GameCount = 2;
            room.HomeCount = 2;
            room.DrawCount = 10;
            room.Turn = Wind.Index.East;
            room.State = RoomState.Dealted;
            request.Room = room;
            request.WinWind = Wind.Index.East;
            var expected = DiProvider.GetContainer().GetInstance<Room>();
            expected.Round = Wind.Index.East;
            expected.Parent = Wind.Index.East;
            expected.GameCount = 2;
            expected.HomeCount = 3;
            expected.DrawCount = 0;
            expected.Turn = Wind.Index.East;
            expected.State = RoomState.SingleGameFinish;
            var actual = this.Target.Receive(request).Room;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 003:親がテンパイで場の更新ができる
        /// </summary>
        [TestMethod]
        public void 親がテンパイで場の更新ができる()
        {
            var request = DiProvider.GetContainer().GetInstance<UpdateGameProgressRequest>();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            room.Parent = Wind.Index.East;
            room.GameCount = 2;
            room.HomeCount = 2;
            room.DrawCount = 10;
            room.Turn = Wind.Index.East;
            room.State = RoomState.Dealted;
            request.Room = room;
            request.ParentWaitingHand = true;
            var expected = DiProvider.GetContainer().GetInstance<Room>();
            expected.Round = Wind.Index.East;
            expected.Parent = Wind.Index.East;
            expected.GameCount = 2;
            expected.HomeCount = 3;
            expected.DrawCount = 0;
            expected.Turn = Wind.Index.East;
            expected.State = RoomState.SingleGameFinish;
            var actual = this.Target.Receive(request).Room;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 004:親以外があがって半荘継続ができる
        /// </summary>
        [TestMethod]
        public void 親以外があがって半荘継続ができる()
        {
            var context = this.LoadFieldContext(1, 1, ResourceType.In);

            var request = DiProvider.GetContainer().GetInstance<UpdateGameProgressRequest>();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            room.Parent = Wind.Index.South;
            room.GameCount = 2;
            room.HomeCount = 2;
            room.DrawCount = 30;
            room.Turn = Wind.Index.East;
            room.State = RoomState.Dealted;
            room.Context = context;
            request.Room = room;
            request.WinWind = Wind.Index.North;
            var expected = DiProvider.GetContainer().GetInstance<Room>();
            expected.Round = Wind.Index.East;
            expected.Parent = Wind.Index.West;
            expected.GameCount = 3;
            expected.HomeCount = 0;
            expected.DrawCount = 0;
            expected.Turn = Wind.Index.East;
            expected.State = RoomState.SingleGameFinish;
            var actual = this.Target.Receive(request).Room;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 005:親がテンパイじゃなくて半荘継続ができる
        /// </summary>
        [TestMethod]
        public void 親がテンパイじゃなくて半荘継続ができる()
        {
            var request = DiProvider.GetContainer().GetInstance<UpdateGameProgressRequest>();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            room.Parent = Wind.Index.South;
            room.GameCount = 2;
            room.HomeCount = 2;
            room.DrawCount = 30;
            room.Turn = Wind.Index.East;
            room.State = RoomState.Dealted;
            request.Room = room;
            var expected = DiProvider.GetContainer().GetInstance<Room>();
            expected.Round = Wind.Index.East;
            expected.Parent = Wind.Index.West;
            expected.GameCount = 3;
            expected.HomeCount = 0;
            expected.DrawCount = 0;
            expected.Turn = Wind.Index.East;
            expected.State = RoomState.SingleGameFinish;
            var actual = this.Target.Receive(request).Room;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 006:親以外があがって南場に変わる
        /// </summary>
        [TestMethod]
        public void 親以外があがって南場に変わる()
        {
            var context = this.LoadFieldContext(1, 1, ResourceType.In);

            var request = DiProvider.GetContainer().GetInstance<UpdateGameProgressRequest>();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.East;
            room.Parent = Wind.Index.North;
            room.GameCount = 4;
            room.HomeCount = 0;
            room.DrawCount = 30;
            room.Turn = Wind.Index.East;
            room.State = RoomState.Dealted;
            room.Context = context;
            request.Room = room;
            request.WinWind = Wind.Index.East;
            var expected = DiProvider.GetContainer().GetInstance<Room>();
            expected.Round = Wind.Index.South;
            expected.Parent = Wind.Index.East;
            expected.GameCount = 1;
            expected.HomeCount = 0;
            expected.DrawCount = 0;
            expected.Turn = Wind.Index.East;
            expected.State = RoomState.SingleGameFinish;
            var actual = this.Target.Receive(request).Room;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 007:親以外があがって荘が終わる
        /// </summary>
        [TestMethod]
        public void 親以外があがって荘が終わる()
        {
            var context = this.LoadFieldContext(1, 1, ResourceType.In);

            var request = DiProvider.GetContainer().GetInstance<UpdateGameProgressRequest>();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            room.Round = Wind.Index.South;
            room.Parent = Wind.Index.North;
            room.GameCount = 4;
            room.HomeCount = 0;
            room.DrawCount = 30;
            room.Turn = Wind.Index.East;
            room.State = RoomState.Dealted;
            room.Context = context;
            request.Room = room;
            request.WinWind = Wind.Index.East;
            var expected = DiProvider.GetContainer().GetInstance<Room>();
            expected.Round = Wind.Index.South;
            expected.Parent = Wind.Index.East;
            expected.GameCount = 4;
            expected.HomeCount = 0;
            expected.DrawCount = 0;
            expected.Turn = Wind.Index.East;
            expected.State = RoomState.AllGameFinish;
            var actual = this.Target.Receive(request).Room;
            Assert.AreEqual(expected, actual);
        }
    }
}
