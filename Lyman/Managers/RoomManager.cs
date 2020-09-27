using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Models;
using Lyman.Di;

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
        public static Dictionary<Guid, Room> Rooms { get; private set; }

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

        /// <summary>
        /// 部屋をセットします。
        /// </summary>
        /// <param name="key">部屋のキー</param>
        /// <param name="room">部屋</param>
        public static void Set(Guid key, Room room)
        {
            Rooms[key] = room;
        }

        /// <summary>
        /// 部屋を取得します。
        /// </summary>
        /// <returns>部屋</returns>
        /// <param name="key">部屋のキー</param>
        public static Room GetOrDefault(Guid key)
        {
            if (!Rooms.ContainsKey(key))
            {
                // 存在しない場合はデフォルトの部屋を返す
                return DiProvider.GetContainer().GetInstance<Room>();
            }
            return Rooms[key];
        }
    }
}
