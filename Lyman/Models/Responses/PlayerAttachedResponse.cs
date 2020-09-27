// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Managers;

namespace Lyman.Models.Responses
{
    /// <summary>
    /// プレイヤが付随した応答
    /// </summary> 
    public class PlayerAttachedResponse : Response
    {
        /// <summary>
        /// プレイヤ
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// プレイヤを引き離します。
        /// </summary>
        public void Detach(Guid roomKey)
        {
            var room = RoomManager.Get(roomKey);
            room.Players[this.Player.Wind] = this.Player;
            this.Player = null;
        }
    }
}
