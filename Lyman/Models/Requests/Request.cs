// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// 要求
    /// </summary>
    public abstract class Request
    {
        /// <summary>
        /// 要求種別
        /// </summary>
        public RequestType RequestType { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected Request() => this.InitRequestType();

        /// <summary>
        /// 要求種別を初期化します。
        /// </summary>
        protected abstract void InitRequestType();
    }
}
