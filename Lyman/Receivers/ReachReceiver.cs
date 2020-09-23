// using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using Lyman.Di;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Receivers;

namespace Lyman.Receivers
{
    /// <summary>
    /// リーチ要求の受信機能を提供します。
    /// </summary>
    public class ReachReceiver : IReceivable<FieldAttachedRequest, bool>
    {
        /// <summary>
        /// リーチ要求の受信処理を実行します。
        /// </summary>
        /// <returns>リーチ応答</returns>
        /// <param name="request">リーチ要求</param>
        public bool Receive(FieldAttachedRequest request)
        {
            return true;
        }
    }
}
