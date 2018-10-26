using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Models;

namespace Lyman.Managers
{
    /// <summary>
    /// 部屋の管理機能を提供します。
    /// </summary>
    public static class RoomManager
    {
        /// <summary>
        /// 部屋
        /// </summary>
        /// <value>The rooms.</value>
        private static Dictionary<Guid, Room> Rooms { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static RoomManager()
        {
            Rooms = new Dictionary<Guid, Room>();
        }

        /// <summary>
        /// 部屋を追加します。
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="room">部屋</param>
        public static void Add(Guid key, Room room)
        {
            Rooms.Add(key, room);
        }

        /// <summary>
        /// 部屋を取得します。
        /// </summary>
        /// <returns>部屋</returns>
        /// <param name="key">部屋のキー</param>
        public static Room Get(Guid key)
        {
            return Rooms[key];
        }
    }
}
