using Lyman.Di;
using Lyman.Helpers;
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
    /// UpdateConnectionIdのテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class UpdateConnectionIdReceiverTest : BaseUnitTest<UpdateConnectionIdReceiver>
    {
        /// <summary>
        /// 001:コネクションIDが更新できる
        /// </summary>
        [TestMethod]
        public void コネクションIDが更新できる()
        {
            // 001:
            var request = DiProvider.GetContainer().GetInstance<UpdateConnectionIdRequest>();
            request.Player = DiProvider.GetContainer().GetInstance<Player>();
            request.ConnectionId = "cid";
            var expected = DiProvider.GetContainer().GetInstance<Player>();
            expected.ConnectionId = "cid";
            var actual = this.Target.Receive(request).Player;
            this.AssertEqualsPlayer(expected, actual);
        }
    }
}
