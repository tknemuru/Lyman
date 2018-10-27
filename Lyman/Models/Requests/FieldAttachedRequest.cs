// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Managers;

namespace Lyman.Models.Requests
{
    /// <summary>
    /// フィールド状態が付随した要求
    /// </summary>
    public abstract class FieldAttachedRequest : Request
    {
        /// <summary>
        /// 部屋のキー
        /// </summary>
        /// <value>The room key.</value>
        public Guid RoomKey { get; set; }

        /// <summary>
        /// フィールド状態
        /// </summary>
        public FieldContext Context { get; set; }

        /// <summary>
        /// フィールド情報を紐付けます。
        /// </summary>
        public void AttachContext()
        {
            if (this.RoomKey == null)
            {
                return;
            }
            this.Context = RoomManager.Get(this.RoomKey).Context;
        }
    }
}
