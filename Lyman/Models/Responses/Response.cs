// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Models.Requests;

namespace Lyman.Models.Responses
{
    /// <summary>
    /// 応答
    /// </summary>
    public abstract class Response
    {
        /// <summary>
        /// 要求種別
        /// </summary>
        public RequestType RequestType { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected Response() => this.InitRequestType();

        /// <summary>
        /// 要求種別を初期化します。
        /// </summary>
        protected abstract void InitRequestType();
    }
}
