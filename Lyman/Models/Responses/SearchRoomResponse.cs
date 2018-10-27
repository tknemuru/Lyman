using System.Collections.Generic;
using System.Linq;
using System;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// 部屋検索応答
    /// </summary>
    public class SearchRoomResponse : Response
    {
        /// <summary>
        /// 部屋のキー
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// 部屋名
        /// </summary>
        public string Name { get; set; }
    }
}
