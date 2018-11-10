// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Di;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    /// <summary>
    /// AI捨牌の受信機能を提供します。
    /// </summary>
    public class AiDiscardReceiver : IReceivable<AiDiscardRequest, AiDiscardResponse>
    {
        /// <summary>
        /// AI捨牌の受信処理を実行します。
        /// </summary>
        /// <returns>AI捨牌応答</returns>
        /// <param name="request">AI捨牌要求</param>
        public AiDiscardResponse Receive(AiDiscardRequest request)
        {
            var tile = request.Discardable.Discard(request.Context, request.Wind);
            var _request = DiProvider.GetContainer().GetInstance<DiscardRequest>();
            _request.Context = request.Context;
            _request.Wind = request.Wind;
            _request.Tile = tile;
            var _response = DiProvider.GetContainer().GetInstance<DiscardReceiver>().Receive(_request);
            var response = DiProvider.GetContainer().GetInstance<AiDiscardResponse>();
            response.Context = _response.Context;
            response.Wind = _response.Wind;
            response.Tile = _response.Tile;
            return response;
        }
    }
}
