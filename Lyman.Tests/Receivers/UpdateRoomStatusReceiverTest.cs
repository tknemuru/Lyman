using Lyman.Di;
using Lyman.Helpers;
using Lyman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Converters;
using Lyman.Receivers;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Managers;

namespace Lyman.Tests
{
    /// <summary>
    /// UpdateRoomStatusReceiverのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class UpdateRoomStatusReceiverTest : BaseUnitTest<UpdateRoomStatusReceiver>
    {
        /// <summary>
        /// 001:部屋の状態が更新できる
        /// </summary>
        [TestMethod]
        public void 部屋の状態を入室中に更新できる()
        {
            // 001:0人
            var request = DiProvider.GetContainer().GetInstance<UpdateRoomStatusRequest>();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            request.Room = room;
            var expected = DiProvider.GetContainer().GetInstance<Room>();
            expected.State = RoomState.Entering;
            var actual = this.Target.Receive(request).Room;
            Assert.AreEqual(expected.State, actual.State);

            // 001:3人
            request = DiProvider.GetContainer().GetInstance<UpdateRoomStatusRequest>();
            room = DiProvider.GetContainer().GetInstance<Room>();
            room.Players.Add(Wind.Index.East, DiProvider.GetContainer().GetInstance<Player>());
            room.Players.Add(Wind.Index.North, DiProvider.GetContainer().GetInstance<Player>());
            room.Players.Add(Wind.Index.South, DiProvider.GetContainer().GetInstance<Player>());
            request.Room = room;
            expected = DiProvider.GetContainer().GetInstance<Room>();
            expected.State = RoomState.Entering;
            actual = this.Target.Receive(request).Room;
            Assert.AreEqual(expected.State, actual.State);
        }

        /// <summary>
        /// 002:部屋の状態を入室完了に更新できる
        /// </summary>
        public void 部屋の状態を入室完了に更新できる()
        {
            // 001:4人
            var request = DiProvider.GetContainer().GetInstance<UpdateRoomStatusRequest>();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            room.Players.Add(Wind.Index.East, DiProvider.GetContainer().GetInstance<Player>());
            room.Players.Add(Wind.Index.North, DiProvider.GetContainer().GetInstance<Player>());
            room.Players.Add(Wind.Index.South, DiProvider.GetContainer().GetInstance<Player>());
            room.Players.Add(Wind.Index.West, DiProvider.GetContainer().GetInstance<Player>());
            request.Room = room;
            var expected = DiProvider.GetContainer().GetInstance<Room>();
            expected.State = RoomState.Entered;
            var actual = this.Target.Receive(request).Room;
            Assert.AreEqual(expected.State, actual.State);
        }

        /// <summary>
        /// 003:部屋の状態を指定した状態に更新できる
        /// </summary>
        public void 部屋の状態を指定した状態に更新できる()
        {
            // 001:配牌完了
            var request = DiProvider.GetContainer().GetInstance<UpdateRoomStatusRequest>();
            request.RoomState = RoomState.Dealted;
            var room = DiProvider.GetContainer().GetInstance<Room>();
            request.Room = room;
            var expected = DiProvider.GetContainer().GetInstance<Room>();
            expected.State = RoomState.Dealted;
            var actual = this.Target.Receive(request).Room;
            Assert.AreEqual(expected.State, actual.State);
        }
    }
}
