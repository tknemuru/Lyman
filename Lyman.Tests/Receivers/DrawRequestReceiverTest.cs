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
    /// DrawRequestReceiverのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class DrawRequestReceiverTest : BaseUnitTest<DrawRequestReceiver>
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
