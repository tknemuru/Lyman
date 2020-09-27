// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// コネクションID更新要求
    /// </summary>
    public class UpdateConnectionIdRequest : PlayerAttachedRequest
    {
        /// <summary>
        /// コネクションID
        /// </summary>
        public string ConnectionId { get; set; }
    }
}
