using System.Collections.Generic;
using System.Linq;
using System;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// プレイヤ取得応答
    /// </summary>
    public class SelectPlayerResponse : Response
    {
        /// <summary>
        /// プレイヤ
        /// </summary>
        public IEnumerable<string> Players { get; set; }
    }
}
