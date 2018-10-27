using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Managers;
using Lyman.Di;

namespace Lyman.Receivers
{
    /// <summary>
    /// 部屋検索要求の受信機能を提供します。
    /// </summary>
    public class SearchRoomReceiver : IReceivable<SearchRoomRequest, IEnumerable<SearchRoomResponse>>
    {
        /// <summary>
        /// 部屋検索要求の受信処理を実行します。
        /// </summary>
        /// <returns>部屋検索応答</returns>
        /// <param name="request">部屋検索要求</param>
        public IEnumerable<SearchRoomResponse> Receive(SearchRoomRequest request)
        {
            return RoomManager.Rooms.Select(r => CreateResponse(r.Key, r.Value.Name));
        }

        /// <summary>
        /// 部屋検索応答の応答情報を作成します。
        /// </summary>
        /// <returns>部屋検索応答の応答情報</returns>
        /// <param name="key">部屋のキー</param>
        /// <param name="name">部屋名</param>
        private static SearchRoomResponse CreateResponse(Guid key, string name)
        {
            var response = DiProvider.GetContainer().GetInstance<SearchRoomResponse>();
            response.Key = key;
            response.Name = name;
            return response;
        }
    }
}
