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
    /// 捨牌要求の応答機能を提供します。
    /// </summary>
    public class DiscardRequestReceiver : IReceivable<DiscardRequest, DiscardResponse>
    {
        /// <summary>
        /// 捨牌要求の受信処理を行います。
        /// </summary>
        /// <returns>捨牌応答</returns>
        /// <param name="request">捨牌要求</param>
        public DiscardResponse Receive(DiscardRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<DiscardResponse>();
            var context = request.Context;

            // 捨牌を河に追加
            context.Rivers[request.Wind.ToInt()].Add(request.Tile);

            // 手牌から牌を削除
            var index = context.Hands[request.Wind.ToInt()].LastIndexOf(request.Tile);
            context.Hands[request.Wind.ToInt()].RemoveAt(index);

            response.Context = context;
            response.Wind = request.Wind;
            response.Tile = request.Tile;
            return response;
        }
    }
}
