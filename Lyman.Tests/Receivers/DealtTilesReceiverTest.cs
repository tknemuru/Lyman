using Lyman.Di;
using Lyman.Helpers;
using Lyman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Receivers;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Tests.Receivers
{
    /// <summary>
    /// DealtTilesReceiverのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class DealtTilesReceiverTest : BaseUnitTest<StableDealtTilesReceiver>
    {
        /// <summary>
        /// 001:配牌ができる
        /// </summary>
        [TestMethod]
        public void 配牌ができる()
        {
            this.Target.ParentDecisionDice = 3;
            this.Target.OpenGateDecisionDice = 4;
            var request = DiProvider.GetContainer().GetInstance<DealtTilesRequest>();
            var actual = this.Target.Receive(request);
            var expected = this.LoadFieldContext(1, 1, ResourceType.Out);
            this.AssertEqualsFieldContext(expected, actual.Context);
            var expectedPosition = DiProvider.GetContainer().GetInstance<WallPosition>();
            expectedPosition.Wind = Wind.Index.West;
            expectedPosition.Index = 3;
            expectedPosition.Rank = Wall.Rank.Lower;
            Assert.AreEqual(expectedPosition, actual.NextDrawPosition);
        }
    }
}
