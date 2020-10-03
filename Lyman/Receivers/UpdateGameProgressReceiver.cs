using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Di;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    /// <summary>
    /// 進行更新要求の受信機能を提供します。
    /// </summary>
    public class UpdateGameProgressReceiver : IReceivable<UpdateGameProgressRequest, RoomAttachedResponse>
    {
        /// <summary>
        /// 進行更新要求の受信処理を実行します。
        /// </summary>
        /// <returns>進行更新応答</returns>
        /// <param name="request">進行更新要求</param>
        public RoomAttachedResponse Receive(UpdateGameProgressRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<RoomAttachedResponse>();
            var room = request.Room;
            response.Room = room;

            // 場の終了？
            if (!this.HasFinishedHome(room, request.WinWind))
            {
                // 継続
                room.Turn = room.Turn.Next();
                room.DrawCount++;
                return response;
            }

            // 場の状態をクリア
            room.ClearHomeContext();
            room.State = RoomState.SingleGameFinish;

            // 親継続？
            if (this.RequiredKeepParent(room, request.ParentWaitingHand, request.WinWind))
            {
                // 場をカウントアップ
                room.HomeCount++;
                return response;
            }

            // 親をチェンジ
            room.Parent = room.Parent.Next();

            // 半荘の終了？
            if (!this.HasFinishedHalfRound(room))
            {
                // 次の局に移る
                room.GameCount++;
                room.HomeCount = 0;
                return response;
            }

            // 東場？
            if (room.Round == Wind.Index.East)
            {
                // 南場に移る
                room.GameCount = 1;
                room.HomeCount = 0;
                room.Round = Wind.Index.South;
                return response;
            }

            // ゲーム終了
            room.State = RoomState.AllGameFinish;
            return response;
        }

        /// <summary>
        /// 場の終了かどうか
        /// </summary>
        /// <param name="room">部屋</param>
        /// <param name="winWind">アガったプレイヤの風</param>
        /// <returns>場の終了かどうか</returns>
        private bool HasFinishedHome(Room room, Wind.Index winWind)
        {
            // アガったプレイヤがいるかどうか
            if (winWind != Wind.Index.Undefined)
            {
                return true;
            }
            // 流局かどうか
            var drawnGame = room.Context.Walls.All(wind => wind.All(rank => rank.All(tile => tile == Tile.Kind.Undefined.ToUint())));
            return drawnGame;
        }

        /// <summary>
        /// 親継続かどうか
        /// </summary>
        /// <param name="room">部屋</param>
        /// <param name="parentWaitngHand">親のテンパイ有無</param>
        /// <param name="winWind">アガったプレイヤの風</param>
        /// <returns>親継続かどうか</returns>
        private bool RequiredKeepParent(Room room, bool parentWaitngHand, Wind.Index winWind)
        {
            return parentWaitngHand || winWind == room.Parent;
        }

        /// <summary>
        /// 半荘の終了かどうか
        /// </summary>
        /// <param name="room">部屋</param>
        /// <returns>半荘の終了かどうか</returns>
        private bool HasFinishedHalfRound(Room room)
        {
            return room.GameCount >= Room.MaxGameCount;
        }
      }
}
