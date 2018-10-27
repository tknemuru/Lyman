using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Helpers;
using System.Diagnostics;
using Lyman.Di;

namespace Lyman.Models
{
    /// <summary>
    /// 部屋
    /// </summary>
    public class Room
    {
        /// <summary>
        /// 部屋名
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// 状態
        /// </summary>
        public RoomState State { get
            {
                var state = RoomState.Undefined;
                if (this.Players.Count() < Wind.Length)
                {
                    state = RoomState.Entering;
                } else if (this.Context.Hands[Wind.Index.East.ToInt()].Count() < Hand.Length)
                {
                    state = RoomState.Entered;
                } else {
                    state = RoomState.Dealted;
                }
                return state;
            }
        }

        /// <summary>
        /// フィールド状態
        /// </summary>
        /// <value>The context.</value>
        public FieldContext Context { get; set; }

        /// <summary>
        /// プレイヤ
        /// </summary>
        public Dictionary<Wind.Index, Player> Players { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Room()
        {
            this.Players = new Dictionary<Wind.Index, Player>();
            this.Context = DiProvider.GetContainer().GetInstance<FieldContext>();
        }

        /// <summary>
        /// プレイヤを追加します。
        /// </summary>
        /// <param name="wind">風</param>
        /// <param name="name">プレイヤ名</param>
        public Guid AddPlayer(Wind.Index wind, string name)
        {
            Debug.Assert(wind != Wind.Index.Undefined, "風が不正です。");
            var player = DiProvider.GetContainer().GetInstance<Player>();
            player.Key = Guid.NewGuid();
            player.Name = name;
            this.Players.Add(wind, player);
            return player.Key;
        }

        /// <summary>
        /// 使用可能な風のリストを取得します。
        /// </summary>
        /// <returns>使用可能な風のリスト</returns>
        public IEnumerable<Wind.Index> GetAvailableWinds()
        {
            return IEnumerableHelper.GetEnums<Wind.Index>().
                                    Where(w => w != Wind.Index.Undefined).
                                    Except(this.Players.Select(p => p.Key));
        }

        /// <summary>
        /// プレイヤを取得します。
        /// </summary>
        /// <returns>プレイヤ</returns>
        /// <param name="key">プレイヤの識別キー</param>
        public KeyValuePair<Wind.Index, Player> GetPlayer(Guid key)
        {
            return this.Players.First(p => p.Value.Key == key);
        }
    }
}
