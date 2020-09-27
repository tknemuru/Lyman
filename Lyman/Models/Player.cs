using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Lyman.Di;

namespace Lyman.Models
{
    /// <summary>
    /// プレイヤ
    /// </summary>
    public sealed class Player
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Player()
        {
            this.Score = 25000;
            this.Reach = false;
        }

        /// <summary>
        /// 識別キー
        /// </summary>
        /// <value>The key.</value>
        public Guid Key { get; set; }

        /// <summary>
        /// 風
        /// </summary>
        public Wind.Index Wind { get; set; }

        /// <summary>
        /// プレイヤ種別
        /// </summary>
        public PlayerType PlayerType { get; set; }

        /// <summary>
        /// プレイヤ名
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// スコア
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// リーチ
        /// </summary>
        public bool Reach { get; set; }

        /// <summary>
        /// コネクションID
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// ディープコピーを行います。
        /// </summary>
        /// <returns>ディープコピーしたインスタンス</returns>
        public Player DeepCopy()
        {
            var player = DiProvider.GetContainer().GetInstance<Player>();
            player.Key = this.Key;
            player.Wind = this.Wind;
            player.PlayerType = this.PlayerType;
            player.Name = this.Name;
            player.Score = this.Score;
            player.Reach = this.Reach;
            player.ConnectionId = this.ConnectionId;
            return player;
        }

        /// <summary>
        /// 文字列に変換します。
        /// </summary>
        /// <returns>文字列化したフィールド状態</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Key: {this.Key}");
            sb.AppendLine($"Wind: {this.Wind}");
            sb.AppendLine($"PlayerType: {this.PlayerType}");
            sb.AppendLine($"Name: {this.Name}");
            sb.AppendLine($"Score: {this.Score}");
            sb.AppendLine($"Reach: {this.Reach}");
            sb.AppendLine($"ConnectionId: {this.ConnectionId}");
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

            var player = (Player)obj;
            var equals = new Dictionary<string, bool>();

            equals.Add("Key", this.Key == player.Key);
            equals.Add("Wind", this.Wind == player.Wind);
            equals.Add("PlayerType", this.PlayerType == player.PlayerType);
            equals.Add("Name", this.Name == player.Name);
            equals.Add("Score", this.Score == player.Score);
            equals.Add("Reach", this.Reach == player.Reach);
            equals.Add("ConnectionId", this.ConnectionId == player.ConnectionId);
            return equals.All(e => e.Value);
        }

        /// <summary>
        /// 既定のハッシュ関数として機能します。
        /// </summary>
        /// <returns>現在のオブジェクトのハッシュ コード。</returns>
        public override int GetHashCode()
        {
            return this.Key.GetHashCode() ^ this.Wind.GetHashCode() ^ this.PlayerType.GetHashCode() ^ this.Name.GetHashCode() ^
                this.Score.GetHashCode() ^ this.Reach.GetHashCode() ^ this.ConnectionId.GetHashCode();
        }
    }
}
