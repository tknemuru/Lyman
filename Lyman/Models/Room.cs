using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// 最大局数
        /// </summary>
        public const int MaxGameCount = 4;

        /// <summary>
        /// 部屋のキー
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// 部屋名
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// 状態
        /// </summary>
        public RoomState State { get; set; }

        /// <summary>
        /// 荘
        /// </summary>
        public Wind.Index Round { get; set; }

        /// <summary>
        /// 局数
        /// </summary>
        public int GameCount { get; set; }

        /// <summary>
        /// 本場数
        /// </summary>
        public int HomeCount { get; set; }

        /// <summary>
        /// ツモ数
        /// </summary>
        public int DrawCount { get; set; }

        /// <summary>
        /// 親
        /// </summary>
        public Wind.Index Parent { get; set; }

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
        public Dictionary<Wind.Index, Player> Players { get; set; }

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
            this.Round = Wind.Index.East;
            this.Players = new Dictionary<Wind.Index, Player>();
            this.GameCount = 1;
            this.HomeCount = 0;
            this.ClearHomeContext();
        }

        /// <summary>
        /// 場の状態をクリアします。
        /// </summary>
        public void ClearHomeContext()
        {
            this.DrawCount = 0;
            this.Context = DiProvider.GetContainer().GetInstance<FieldContext>();
            this.NextPosition = DiProvider.GetContainer().GetInstance<WallPosition>();
            this.LastDiscardPosition = DiProvider.GetContainer().GetInstance<RiverPosition>();
        }

        /// <summary>
        /// プレイヤを追加します。
        /// </summary>
        /// <param name="wind">風</param>
        /// <param name="playerType">風</param>
        /// <param name="name">プレイヤ名</param>
        /// <param name="connectionId">コネクションID</param>
        public Guid AddPlayer(Wind.Index wind, PlayerType playerType, string name, string connectionId = null)
        {
            Debug.Assert(wind != Wind.Index.Undefined, "風が不正です。");
            var player = DiProvider.GetContainer().GetInstance<Player>();
            player.Key = Guid.NewGuid();
            player.Wind = wind;
            player.PlayerType = playerType;
            player.Name = name;
            player.ConnectionId = connectionId;
            this.Players[wind] = player;
            return player.Key;
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

        /// <summary>
        /// 文字列に変換します。
        /// </summary>
        /// <returns>文字列化したフィールド状態</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Key: {this.Key}");
            sb.AppendLine($"Name: {this.Name}");
            sb.AppendLine($"Round: {this.Round}");
            sb.AppendLine($"GameCount: {this.GameCount}");
            sb.AppendLine($"HomeCount: {this.HomeCount}");
            sb.AppendLine($"DrawCount: {this.DrawCount}");
            sb.AppendLine($"Parent: {this.Parent}");
            sb.AppendLine($"State: {this.State}");
            foreach (var player in this.Players)
            {
                sb.AppendLine($"Players{player.Key}: {player.Value}");
            }
            sb.AppendLine($"Turn: {this.Turn}");
            sb.AppendLine($"LastDiscardPosition: {this.LastDiscardPosition}");
            sb.AppendLine($"NextPosition: {this.NextPosition}");
            sb.AppendLine($"Context: {this.Context}");
            return sb.ToString();
        }

        /// <summary>
        /// 指定のオブジェクトが現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <param name="obj">比較対象のオブジェクト</param>
        /// <returns>指定のオブジェクトが現在のオブジェクトと等しいかどうか</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var room = (Room)obj;
            var equals = new Dictionary<string, bool>();

            equals.Add("Key", this.Key == room.Key);
            equals.Add("Name", this.Name == room.Name);
            equals.Add("Round", this.Round == room.Round);
            equals.Add("GameCount", this.GameCount == room.GameCount);
            equals.Add("HomeCount", this.HomeCount == room.HomeCount);
            equals.Add("DrawCount", this.DrawCount == room.DrawCount);
            equals.Add("Parent", this.Parent == room.Parent);
            equals.Add("LastDiscardPosition", this.LastDiscardPosition.Equals(room.LastDiscardPosition));
            equals.Add("NextPosition", this.NextPosition.Equals(room.NextPosition));
            equals.Add("State", this.State == room.State);
            equals.Add("Turn", this.Turn == room.Turn);
            equals.Add("Context", this.Context.Equals(room.Context));
            if (this.Players == null && room.Players == null)
            {
                equals.Add("Players", true);
            } else if (this.Players == null || room.Players == null)
            {
                equals.Add("Players", false);
            } else
            {
                if (this.Players.Count != room.Players.Count)
                {
                    equals.Add("Players", false);
                } else
                {
                    foreach (var player in this.Players)
                    {
                        var wind = player.Key;
                        if (!room.Players.ContainsKey(wind))
                        {
                            equals.Add($"Players{wind}", false);
                            continue;
                        }
                        equals.Add($"Players{wind}", player.Equals(room.Players[wind]));
                    }
                }
            }

            return equals.All(e => e.Value);
        }

        /// <summary>
        /// 既定のハッシュ関数として機能します。
        /// </summary>
        /// <returns>現在のオブジェクトのハッシュ コード。</returns>
        public override int GetHashCode()
        {
            return this.Context.GetHashCode() ^ this.Key.GetHashCode() ^ this.LastDiscardPosition.GetHashCode() ^
                this.Round.GetHashCode() ^ this.GameCount.GetHashCode() ^ this.HomeCount.GetHashCode() ^ this.DrawCount.GetHashCode() ^ this.Parent.GetHashCode() ^
                this.Name.GetHashCode() ^ this.NextPosition.GetHashCode() ^ this.Players.GetHashCode() ^
                this.State.GetHashCode() ^ this.Turn.GetHashCode();
        }
    }
}
