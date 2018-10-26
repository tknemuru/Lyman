// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// 入室応答
    /// </summary>
    public class EnterRoomResponse : Response
    {
        /// <summary>
        /// 風
        /// </summary>
        /// <value>The wind.</value>
        public Wind.Index Wind { get; set; }
    }
}
