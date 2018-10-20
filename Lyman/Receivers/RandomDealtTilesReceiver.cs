using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Di;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Helpers;
using Lyman.Models;

namespace Lyman.Receivers
{
    /// <summary>
    /// 配牌要求の受信機能を提供します。牌の並び・サイコロの目はランダムです。
    /// </summary>
    public class RandomDealtTilesReceiver : DealtTilesReceiver
    {
        /// <summary>
        /// 牌をシャッフルします。
        /// </summary>
        protected override IEnumerable<uint> ShuffleTiles()
        {
            return Tiles.OrderBy(t => Guid.NewGuid());
        }

        /// <summary>
        /// 親を決めるサイコロを振ります。
        /// </summary>
        /// <returns>サイコロの目の合計</returns>
        protected override int ShakeParentDecisionDice()
        {
            return this.ShakeDice();
        }

        /// <summary>
        /// 開門位置を決めるサイコロを振ります。
        /// </summary>
        /// <returns>サイコロの目の合計</returns>
        protected override int ShakeOpenGateDecisionDice()
        {
            return this.ShakeDice();
        }

        /// <summary>
        /// サイコロを振ります。
        /// </summary>
        /// <returns>サイコロの目の合計</returns>
        private int ShakeDice()
        {
            return Enumerable.Range(2, 11).OrderBy(i => Guid.NewGuid()).First();
        }
    }
}
