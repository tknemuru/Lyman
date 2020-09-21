// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models
{
    /// <summary>
    /// プレイヤ
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 識別キー
        /// </summary>
        /// <value>The key.</value>
        public Guid Key { get; set; }

        /// <summary>
        /// プレイヤ名
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// コネクションID
        /// </summary>
        public string ConnectionId { get; set; }
    }
}
