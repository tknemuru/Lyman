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
    /// ReachReceiverのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class ReachReceiverTest : BaseUnitTest<ReachReceiver>
    {
        /// <summary>
        /// 001:リーチができる
        /// </summary>
        [TestMethod]
        public void リーチができる()
        {
            // 001:
            var request = DiProvider.GetContainer().GetInstance<PlayerAttachedRequest>();
            request.Player = DiProvider.GetContainer().GetInstance<Player>();
            var expected = DiProvider.GetContainer().GetInstance<Player>();
            expected.Score = 24000;
            expected.Reach = true;
            var actual = this.Target.Receive(request).Player;
            this.AssertEqualsPlayer(expected, actual);
        }
    }
}
