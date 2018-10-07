// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Di;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Models;

namespace Lyman.Receivers
{
    /// <summary>
    /// ツモ要求の受信機能を提供します。
    /// </summary>
    public sealed class DrawRequestReceiver : IReceivable<DrawRequest, DrawResponse>
    {
        /// <summary>
        /// ツモ要求の受信処理を行います。
        /// </summary>
        /// <returns>ツモ応答</returns>
        /// <param name="request">ツモ要求</param>
        public DrawResponse Receive(DrawRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<DrawResponse>();
            var context = request.Context;

            // 対象の牌を取得
            response.Tile = context.Walls[request.Position.Wind.ToInt()][request.Position.Rank.ToInt()][request.Position.Index];

            // 壁から対象の牌を削除
            context.Walls[request.Position.Wind.ToInt()][request.Position.Rank.ToInt()][request.Position.Index] = Tile.Kind.Undefined.ToUint();
            response.Context = context;

            // 次ツモの情報を作成
            response.NextPosition = request.Position.Next();

            return response;
        }
    }
}
