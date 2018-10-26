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

namespace Lyman.Tests
{
    /// <summary>
    /// DrawReceiverのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class DrawReceiverTest : BaseUnitTest<DrawReceiver>
    {
        /// <summary>
        /// 001:ツモができる
        /// </summary>
        [TestMethod]
        public void ツモができる()
        {
            // 001:北/上/フルに壁が存在
            var request = DiProvider.GetContainer().GetInstance<DrawRequest>();
            request.Context = this.LoadFieldContext(1, 1, ResourceType.In);
            request.Position = DiProvider.GetContainer().GetInstance<WallPosition>();
            request.Position.Wind = Wind.Index.North;
            request.Position.Rank = Wall.Rank.Upper;
            request.Position.Index = 16;
            var expected = DiProvider.GetContainer().GetInstance<DrawResponse>();
            expected.Tile = Tile.BuildTile("北");
            expected.Context = this.LoadFieldContext(1, 1, ResourceType.Out);
            expected.NextPosition = DiProvider.GetContainer().GetInstance<WallPosition>();
            expected.NextPosition.Wind = Wind.Index.North;
            expected.NextPosition.Rank = Wall.Rank.Lower;
            expected.NextPosition.Index = 16;
            var actual = this.Target.Receive(request);
            Assert.AreEqual(WallPosition.Type.Default, expected.NextPosition.GetPositionType(request.Context.OpenGatePosition));
            this.AssertAreEqual(expected, actual);

            // 002:西/下/壁の一部が存在
            request = DiProvider.GetContainer().GetInstance<DrawRequest>();
            request.Context = this.LoadFieldContext(1, 2, ResourceType.In);
            request.Position = DiProvider.GetContainer().GetInstance<WallPosition>();
            request.Position.Wind = Wind.Index.West;
            request.Position.Rank = Wall.Rank.Lower;
            request.Position.Index = 6;
            expected = DiProvider.GetContainer().GetInstance<DrawResponse>();
            expected.Tile = Tile.BuildTile("3筒");
            expected.Context = this.LoadFieldContext(1, 2, ResourceType.Out);
            expected.NextPosition = DiProvider.GetContainer().GetInstance<WallPosition>();
            expected.NextPosition.Wind = Wind.Index.West;
            expected.NextPosition.Rank = Wall.Rank.Upper;
            expected.NextPosition.Index = 5;
            actual = this.Target.Receive(request);
            Assert.AreEqual(WallPosition.Type.Default, expected.NextPosition.GetPositionType(request.Context.OpenGatePosition));
            this.AssertAreEqual(expected, actual);

            // 003:西から北に切り替わる
            request = DiProvider.GetContainer().GetInstance<DrawRequest>();
            request.Context = this.LoadFieldContext(1, 3, ResourceType.In);
            request.Position = DiProvider.GetContainer().GetInstance<WallPosition>();
            request.Position.Wind = Wind.Index.West;
            request.Position.Rank = Wall.Rank.Lower;
            request.Position.Index = 0;
            expected = DiProvider.GetContainer().GetInstance<DrawResponse>();
            expected.Tile = Tile.BuildTile("1萬");
            expected.Context = this.LoadFieldContext(1, 3, ResourceType.Out);
            expected.NextPosition = DiProvider.GetContainer().GetInstance<WallPosition>();
            expected.NextPosition.Wind = Wind.Index.North;
            expected.NextPosition.Rank = Wall.Rank.Upper;
            expected.NextPosition.Index = 16;
            actual = this.Target.Receive(request);
            Assert.AreEqual(WallPosition.Type.Default, expected.NextPosition.GetPositionType(request.Context.OpenGatePosition));
            this.AssertAreEqual(expected, actual);

            // 004:次ツモが海底牌
            request = DiProvider.GetContainer().GetInstance<DrawRequest>();
            request.Context = this.LoadFieldContext(1, 4, ResourceType.In);
            request.Position = DiProvider.GetContainer().GetInstance<WallPosition>();
            request.Position.Wind = Wind.Index.North;
            request.Position.Rank = Wall.Rank.Upper;
            request.Position.Index = 0;
            expected = DiProvider.GetContainer().GetInstance<DrawResponse>();
            expected.Tile = Tile.BuildTile("4萬");
            expected.Context = this.LoadFieldContext(1, 4, ResourceType.Out);
            expected.NextPosition = DiProvider.GetContainer().GetInstance<WallPosition>();
            expected.NextPosition.Wind = Wind.Index.North;
            expected.NextPosition.Rank = Wall.Rank.Lower;
            expected.NextPosition.Index = 0;
            actual = this.Target.Receive(request);
            Assert.AreEqual(WallPosition.Type.SeaFloor, expected.NextPosition.GetPositionType(request.Context.OpenGatePosition));
            this.AssertAreEqual(expected, actual);

            // 005:次ツモが王牌
            request = DiProvider.GetContainer().GetInstance<DrawRequest>();
            request.Context = this.LoadFieldContext(1, 5, ResourceType.In);
            request.Position = DiProvider.GetContainer().GetInstance<WallPosition>();
            request.Position.Wind = Wind.Index.North;
            request.Position.Rank = Wall.Rank.Lower;
            request.Position.Index = 0;
            expected = DiProvider.GetContainer().GetInstance<DrawResponse>();
            expected.Tile = Tile.BuildTile("6索");
            expected.Context = this.LoadFieldContext(1, 5, ResourceType.Out);
            expected.NextPosition = DiProvider.GetContainer().GetInstance<WallPosition>();
            expected.NextPosition.Wind = Wind.Index.East;
            expected.NextPosition.Rank = Wall.Rank.Upper;
            expected.NextPosition.Index = 16;
            actual = this.Target.Receive(request);
            Assert.AreEqual(WallPosition.Type.Dead, expected.NextPosition.GetPositionType(request.Context.OpenGatePosition));
            this.AssertAreEqual(expected, actual);
        }

        /// <summary>
        /// 妥当性を検証します。
        /// </summary>
        /// <param name="expected">期待する状態</param>
        /// <param name="actual">実際の状態</param>
        private void AssertAreEqual(DrawResponse expected, DrawResponse actual)
        {
            Assert.AreEqual(expected.Tile, actual.Tile);
            Assert.AreEqual(expected.Context, actual.Context);
            Assert.AreEqual(expected.NextPosition, actual.NextPosition);
        }
    }
}
