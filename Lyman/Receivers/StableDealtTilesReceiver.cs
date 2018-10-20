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
    /// 配牌要求の受信機能を提供します。サイコロの目を指定することができます。
    /// </summary>
    public class StableDealtTilesReceiver : DealtTilesReceiver
    {
        /// <summary>
        /// 親を決めるサイコロの目
        /// </summary>
        /// <value>The parent decision dice.</value>
        public int ParentDecisionDice { get; set; } = 2;

        /// <summary>
        /// 開門を決めるサイコロの目
        /// </summary>
        /// <value>The open gate decision dice.</value>
        public int OpenGateDecisionDice { get; set; } = 2;

        /// <summary>
        /// 牌をシャッフルします。
        /// </summary>
        protected override IEnumerable<uint> ShuffleTiles()
        {
            return Tiles;
        }

        /// <summary>
        /// 親を決めるサイコロを振ります。
        /// </summary>
        /// <returns>サイコロの目の合計</returns>
        protected override int ShakeParentDecisionDice()
        {
            return this.ParentDecisionDice;
        }

        /// <summary>
        /// 開門位置を決めるサイコロを振ります。
        /// </summary>
        /// <returns>サイコロの目の合計</returns>
        protected override int ShakeOpenGateDecisionDice()
        {
            return this.OpenGateDecisionDice;
        }
    }
}
