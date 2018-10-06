// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    /// <summary>
    /// ツモ要求の受信機能を提供します。
    /// </summary>
    public sealed class DrawRequestReceiver : IReceivable<DrawRequest, DrawResponse>
    {
        /// <summary>
        /// ツモ要求の受信処理を行います。
        /// </summary>
        /// <returns>ツモ応答</returns>
        /// <param name="request">ツモ要求</param>
        public DrawResponse Receive(DrawRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
