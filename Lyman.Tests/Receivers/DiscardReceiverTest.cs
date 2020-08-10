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
    /// DiscardReceiverのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class DiscardReceiverTest : BaseUnitTest<DiscardReceiver>
    {
        /// <summary>
        /// 001:捨牌ができる
        /// </summary>
        [TestMethod]
        public void 捨牌ができる()
        {
            // 001:東/ツモ牌
            var request = DiProvider.GetContainer().GetInstance<DiscardRequest>();
            request.Context = this.LoadFieldContext(1, 1, ResourceType.In);
            request.Wind = Wind.Index.East;
            request.Tile = Tile.BuildTile("1萬");
            var expected = DiProvider.GetContainer().GetInstance<DiscardResponse>();
            expected.Wind = Wind.Index.East;
            expected.Tile = Tile.BuildTile("1萬");
            expected.Context = this.LoadFieldContext(1, 1, ResourceType.Out);
            var actual = this.Target.Receive(request);
            this.AssertAreEqual(expected, actual);

            // 002:北/非ツモ牌
            request = DiProvider.GetContainer().GetInstance<DiscardRequest>();
            request.Context = this.LoadFieldContext(1, 2, ResourceType.In);
            request.Wind = Wind.Index.North;
            request.Tile = Tile.BuildTile("発");
            expected = DiProvider.GetContainer().GetInstance<DiscardResponse>();
            expected.Wind = Wind.Index.North;
            expected.Tile = Tile.BuildTile("発");
            expected.Context = this.LoadFieldContext(1, 2, ResourceType.Out);
            actual = this.Target.Receive(request);
            this.AssertAreEqual(expected, actual);
        }

        /// <summary>
        /// 002:最後の捨牌の位置を記録できる
        /// </summary>
        [TestMethod]
        public void 最後の捨牌の位置を記録できる()
        {
            var key = Guid.NewGuid();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            RoomManager.Add(key, room);
            var request = DiProvider.GetContainer().GetInstance<DiscardRequest>();
            request.RoomKey = key;
            request.Context = this.LoadFieldContext(2, 1, ResourceType.In);
            request.Wind = Wind.Index.East;
            request.Tile = Tile.BuildTile("1萬");
            var expected = DiProvider.GetContainer().GetInstance<RiverPosition>();
            expected.Wind = Wind.Index.East;
            expected.Index = 8;
            this.Target.Receive(request);
            Assert.AreEqual(expected, room.LastDiscardPosition);
        }

            /// <summary>
            /// 妥当性を検証します。
            /// </summary>
            /// <param name="expected">期待する状態</param>
            /// <param name="actual">実際の状態</param>
            private void AssertAreEqual(DiscardResponse expected, DiscardResponse actual)
        {
            Assert.AreEqual(expected.Wind, actual.Wind);
            Assert.AreEqual(expected.Tile, actual.Tile);
            this.AssertEqualsFieldContext(expected.Context, actual.Context);
        }
    }
}
