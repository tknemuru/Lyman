// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Managers;

namespace Lyman.Models.Requests
{
    /// <summary>
    /// フィールド状態が付随した要求
    /// </summary>
    public class FieldAttachedRequest : Request
    {
        /// <summary>
        /// 部屋のキー
        /// </summary>
        /// <value>The room key.</value>
        public Guid RoomKey { get; set; }

        /// <summary>
        /// プレイヤの識別キー
        /// </summary>
        /// <value>The player key.</value>
        public Guid PlayerKey { get; set; }

        /// <summary>
        /// フィールド状態
        /// </summary>
        public FieldContext Context { get; set; }

        /// <summary>
        /// プレイヤの風
        /// </summary>
        /// <value>The wind.</value>
        public Wind.Index Wind { get; set; }

        /// <summary>
        /// フィールド情報を紐付けます。
        /// </summary>
        public void Attach()
        {
            var room = RoomManager.Get(this.RoomKey);
            this.Context = room.Context;
            this.Wind = room.GetPlayer(this.PlayerKey).Key;
        }

        /// <summary>
        /// 他のリクエストインスタンスから情報を取り込みます。<br>
        /// 元の参照情報を維持します。
        /// </summary>
        /// <param name="from">取り込み元リクエストインスタンス</param>
        public void ShallowImport(FieldAttachedRequest from)
        {
            this.RoomKey = from.RoomKey;
            this.PlayerKey = from.PlayerKey;
            this.Context = from.Context;
            this.Wind = from.Wind;
        }
    }
}
