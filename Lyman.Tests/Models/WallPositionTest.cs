using Lyman.Di;
using Lyman.Helpers;
using Lyman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Converters;

namespace Lyman.Tests.Models
{
    /// <summary>
    /// WallPositionTest のテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class WallPositionTest
    {
        /// <summary>
        /// 001:次の位置に移動できる
        /// </summary>
        [TestMethod]
        public void 次の位置に移動できる()
        {
            // 001:同じ風/同じインデックス
            var actual = DiProvider.GetContainer().GetInstance<WallPosition>();
            actual.Wind = Wind.Index.East;
            actual.Rank = Wall.Rank.Upper;
            actual.Index = 16;
            actual.Next();
            var expected = DiProvider.GetContainer().GetInstance<WallPosition>();
            expected.Wind = Wind.Index.East;
            expected.Rank = Wall.Rank.Lower;
            expected.Index = 16;
            Assert.AreEqual(expected, actual);

            // 002:同じ風/異なるインデックス
            actual.Next();
            expected.Rank = Wall.Rank.Upper;
            expected.Index = 15;
            Assert.AreEqual(expected, actual);

            // 003:異なる風
            actual.Rank = Wall.Rank.Lower;
            actual.Index = 0;
            actual.Next();
            expected.Wind = Wind.Index.South;
            expected.Rank = Wall.Rank.Upper;
            expected.Index = 16;
            Assert.AreEqual(expected, actual);
        }
    }
}
