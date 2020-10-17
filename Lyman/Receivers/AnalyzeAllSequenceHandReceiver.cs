using System.Linq;
using System;
using System.Collections.Generic;
using Lyman.Di;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    /// <summary>
    /// 平和（ピンフ）の分析機能を提供します。
    /// </summary>
    public class AnalyzeAllSequenceHandReceiver : IYakuAnalyzable
    {
        /// <summary>
        /// 平和（ピンフ）分析要求の受信処理を実行します。
        /// </summary>
        /// <returns>平和（ピンフ）分析応答</returns>
        /// <param name="request">平和（ピンフ）分析要求</param>
        public AnalyzeYakuResponse Receive(AnalyzeYakuRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<AnalyzeYakuResponse>();
            var room = request.Room;
            response.Yaku = Yaku.AllSequenceHand;
            var context = request.WinHandsContext;
            // ４面子が順子（連番で揃えた面子）
            if (!(context.Chows.Count() >= 4 && context.PungsAndKongs.Count() <= 0))
            {
                return response;
            }

            // 頭は役牌（三元牌、場風牌、自風牌）以外
            var windKind = context.Head.GetKind();
            if (Tile.IsDragons(context.Head) ||
                EqualsWind(windKind, room.Round) ||
                EqualsWind(windKind, request.WinWind))
            {
                return response;
            }

            // 和了牌の待ち方は両面待ち
            var win = request.WinTile;
            var isBothSides = context.Chows.Any(chow => chow.GetKind() == win.GetKind() && (chow.GetNumber() + 1) == win.GetNumber());
            if (!isBothSides)
            {
                return response;
            }
            response.HasCompleted = true;
            return response;
        }

        /// <summary>
        /// 風牌の風と指定した風が同一かどうか
        /// </summary>
        /// <param name="kind">牌の種類</param>
        /// <param name="wind">風</param>
        /// <returns>風牌の風と指定した風が同一かどうか</returns>
        private bool EqualsWind(Tile.Kind kind,  Wind.Index wind)
        {
            return (kind == Tile.Kind.East && wind == Wind.Index.East ||
                kind == Tile.Kind.South && wind == Wind.Index.South ||
                kind == Tile.Kind.West && wind == Wind.Index.West ||
                kind == Tile.Kind.North && wind == Wind.Index.North);
        }
    }
}