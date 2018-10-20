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
    /// Wind のテスト機能を提供します。
    /// </summary>
    [TestClass]
    public class WindTest
    {
        /// <summary>
        /// 001:次の風に移動できる
        /// </summary>
        [TestMethod]
        public void 次の風に移動できる()
        {
            // 001:単発
            var wind = Wind.Index.East;
            wind = wind.Next();
            Assert.AreEqual(Wind.Index.South, wind);
            wind = wind.Next();
            Assert.AreEqual(Wind.Index.West, wind);
            wind = wind.Next();
            Assert.AreEqual(Wind.Index.North, wind);
            wind = wind.Next();
            Assert.AreEqual(Wind.Index.East, wind);

            // 002:複数回
            wind = wind.Next(2);
            Assert.AreEqual(Wind.Index.West, wind);
        }

        /// <summary>
        /// 002:前の風に移動できる
        /// </summary>
        [TestMethod]
        public void 前の風に移動できる()
        {
            // 001:単発
            var wind = Wind.Index.East;
            wind = wind.Prev();
            Assert.AreEqual(Wind.Index.North, wind);
            wind = wind.Prev();
            Assert.AreEqual(Wind.Index.West, wind);
            wind = wind.Prev();
            Assert.AreEqual(Wind.Index.South, wind);
            wind = wind.Prev();
            Assert.AreEqual(Wind.Index.East, wind);

            // 002:複数回
            wind = wind.Prev(2);
            Assert.AreEqual(Wind.Index.West, wind);
        }
    }
}
