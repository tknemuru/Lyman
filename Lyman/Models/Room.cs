using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Helpers;
using System.Diagnostics;
using Lyman.Di;
using System.Collections.Concurrent;

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
        /// 次ツモの位置
        /// </summary>
        /// <value>The next position.</value>
        public WallPosition NextPosition { get; set; }

        /// <summary>
        /// フィールド状態
        /// </summary>
        /// <value>The context.</value>
        public FieldContext Context { get; set; }

        /// <summary>
        /// プレイヤ
        /// </summary>
        public ConcurrentDictionary<Wind.Index, Player> Players { get; private set; }

        /// <summary>
        /// ターン
        /// </summary>
        /// <value>The turn.</value>
        public Wind.Index Turn { get; set; } = Wind.Index.East;

        /// <summary>
        /// 最後に捨牌した河の位置
        /// </summary>
        public RiverPosition LastDiscardPosition { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Room()
        {
            this.Players = new ConcurrentDictionary<Wind.Index, Player>();
            this.Context = DiProvider.GetContainer().GetInstance<FieldContext>();
        }

        /// <summary>
        /// プレイヤを追加します。
        /// </summary>
        /// <param name="wind">風</param>
        /// <param name="name">プレイヤ名</param>
        /// <param name="connectionId">コネクションID</param>
        public Guid AddPlayer(Wind.Index wind, string name, string connectionId = null)
        {
            Debug.Assert(wind != Wind.Index.Undefined, "風が不正です。");
            var player = DiProvider.GetContainer().GetInstance<Player>();
            player.Key = Guid.NewGuid();
            player.Name = name;
            player.ConnectionId = connectionId;
            this.Players.AddOrUpdate(wind, player, (key, old) => player);
            return player.Key;
        }

        /// <summary>
        /// プレイヤのコネクションIDを更新します。
        /// </summary>
        /// <param name="key">プレイヤキー</param>
        /// <param name="connectionId">コネクションID</param>
        public void UpdatePlayerConnectionId(Guid playerKey, string connectionId)
        {
            var playerKeyValue = this.GetPlayer(playerKey);
            var wind = playerKeyValue.Key;
            var player = playerKeyValue.Value;
            var newPlayer = DiProvider.GetContainer().GetInstance<Player>();
            newPlayer.Key = player.Key;
            newPlayer.Name = Name;
            newPlayer.ConnectionId = connectionId;
            this.Players.AddOrUpdate(wind, newPlayer, (key, old) => newPlayer);
        }

        /// <summary>
        /// 最初のプレイヤかどうか
        /// </summary>
        /// <param name="key">プレイヤの識別キー</param>
        public bool IsFirstPlayer(Guid key)
        {
            if (!this.Players.Any())
            {
                return false;
            }

            return this.Players.First().Value.Key == key;
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

        /// <summary>
        /// SignalR接続済のプレイヤを取得します。
        /// </summary>
        /// <returns>プレイヤ</returns>
        /// <param name="key">プレイヤの識別キー</param>
        public IEnumerable<KeyValuePair<Wind.Index, Player>> GetConnectedPlayers()
        {
            return this.Players.Where(p => !string.IsNullOrEmpty(p.Value.ConnectionId));
        }
    }
}
