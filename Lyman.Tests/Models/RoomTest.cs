using Lyman.Di;
using Lyman.Helpers;
using Lyman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Lyman.Tests.Models
{
    /// <summary>
    /// Room のテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class RoomTest
    {
        /// <summary>
        /// 001:空いている風が取得できる
        /// </summary>
        [TestMethod]
        public void 空いている風が取得できる()
        {
            // 001:全て空き
            var room = DiProvider.GetContainer().GetInstance<Room>();
            var actual = room.GetAvailableWinds().Count();
            Assert.AreEqual(4, actual);

            // 002:3人空き
            room.AddPlayer(Wind.Index.East, PlayerType.Human, "aaa");
            actual = room.GetAvailableWinds().Count();
            Assert.AreEqual(3, actual);

            // 003:空き無し
            room.AddPlayer(Wind.Index.North, PlayerType.Human, "bbb");
            room.AddPlayer(Wind.Index.South, PlayerType.Human, "ccc");
            room.AddPlayer(Wind.Index.West, PlayerType.Human, "ddd");
            actual = room.GetAvailableWinds().Count();
            Assert.AreEqual(0, actual);
        }
    }
}
