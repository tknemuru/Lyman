using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Di;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Helpers;
using Lyman.Models;
using System.Diagnostics;

namespace Lyman.Receivers
{
    /// <summary>
    /// 配牌要求の受信機能を提供します。
    /// </summary>
    public abstract class DealtTilesReceiver : IReceivable<DealtTilesRequest, DealtTilesResponse>
    {
        /// <summary>
        /// 全ての牌のリスト
        /// </summary>
        protected static readonly IEnumerable<uint> Tiles = BuildTiles();

        /// <summary>
        /// 配牌要求の受信処理を行います。
        /// </summary>
        /// <returns>配牌応答</returns>
        /// <param name="request">配牌要求</param>
        public DealtTilesResponse Receive(DealtTilesRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<DealtTilesResponse>();
            var context = DiProvider.GetContainer().GetInstance<FieldContext>();
            response.Context = context;

            // 牌を混ぜる
            var tiles = this.ShuffleTiles();

            // TODO:あとでちゃんと実装
            // 親決め
            var dice = this.ShakeParentDecisionDice();
            var parent = Wind.Index.East;

            // 開門
            dice = this.ShakeOpenGateDecisionDice();
            var openGatePosition = DiProvider.GetContainer().GetInstance<WallPosition>();
            openGatePosition.Wind = parent.Prev(dice - 1);
            openGatePosition.Index = Wall.Length - dice - 1;
            openGatePosition.Rank = Wall.Rank.Upper;
            context.OpenGatePosition = openGatePosition;

            // 壁牌
            var start = 0;
            Wind.ForEach(wind =>
            {
                Wall.ForEachRank(rank =>
                {
                    context.Walls[wind.ToInt()][rank.ToInt()] = tiles.Skip(start).Take(Wall.Length).ToArray();
                    start += Wall.Length;
                });
            });

            // 手牌
            var range = 0;
            var position = openGatePosition.DeepCopy();
            Wind.ForEach(wind =>
            {
                range = this.IsLeader(wind) ? Hand.DrawLength : Hand.Length;
                for (var i = 0; i < range; i++)
                {
                    var tile = context.GetWallTile(position);
                    Debug.Assert(tile.GetKind() != Tile.Kind.Undefined && tile.GetKind() != Tile.Kind.Empty, $"牌が未定義か空は有り得ません。{position}");
                    context.Hands[wind.ToInt()].Add(tile);
                    context.SetWallTile(position, Tile.BuildTile(Tile.Kind.Empty));
                    position.Next();
                }
            });

            return response;
        }

        /// <summary>
        /// 親を決めるサイコロを振ります。
        /// </summary>
        /// <returns>サイコロの目の合計</returns>
        protected abstract int ShakeParentDecisionDice();

        /// <summary>
        /// 開門位置を決めるサイコロを振ります。
        /// </summary>
        /// <returns>サイコロの目の合計</returns>
        protected abstract int ShakeOpenGateDecisionDice();

        /// <summary>
        /// 牌をシャッフルします。
        /// </summary>
        protected abstract IEnumerable<uint> ShuffleTiles();

        /// <summary>
        /// 親かどうかを返却します。
        /// </summary>
        /// <returns>親かどうか</returns>
        /// <param name="wind">風</param>
        private bool IsLeader(Wind.Index wind)
        {
            return wind == Wind.Index.East;
        }

        /// <summary>
        /// 全ての牌のリストを組み立てます
        /// </summary>
        /// <returns>全ての牌のリスト</returns>
        private static IEnumerable<uint> BuildTiles()
        {
            var numbers = Enumerable.Range(1, 9);
            var count = Enumerable.Range(0, 4);
            var kinds = IEnumerableHelper.GetEnums<Tile.Kind>().Where(k => k != Tile.Kind.Undefined);
            var suitsTiles = kinds.Where(k => k.GetGroup() == Tile.Group.Suits).
                                  SelectMany(k => numbers, (k, n) => $"{n}{Tile.JapaneseName.Get(k)}").
                                  SelectMany(t => count, (t, c) => t);
            var honoursTiles = kinds.Where(k => k.GetGroup() == Tile.Group.Honours).
                                    SelectMany(k => count, (k, i) => Tile.JapaneseName.Get(k));
            var tiles = suitsTiles.Concat(honoursTiles);
            return tiles.Select(Tile.BuildTile);
        }
    }
}
