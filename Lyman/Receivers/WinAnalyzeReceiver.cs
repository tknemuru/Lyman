using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Models;
using Lyman.Helpers;
using Lyman.Di;

namespace Lyman.Receivers
{
    /// <summary>
    /// あがり可能性分析機能を提供します。
    /// </summary>
    public class WinAnalyzeReceiver : IReceivable<IEnumerable<uint>, IEnumerable<WinHandsContext>>
    {
        /// <summary>
        /// 面子パターン
        /// </summary>
        private static readonly IEnumerable<Dictionary<Tile.Kind, Set>> SetPatterns = BuildSetPatterns();

        /// <summary>
        /// あがり可能性分析要求の受信処理を実行します。
        /// </summary>
        /// <param name="request">手牌</param>
        /// <returns>分析結果</returns>
        public IEnumerable<WinHandsContext> Receive(IEnumerable<uint> request)
        {
            return this.Analyze(request);
        }

        /// <summary>
        /// あがり可能性の分析を行います。
        /// </summary>
        /// <param name="orgHands">手牌</param>
        /// <returns>あがり可能性の分析結果</returns>
        private IEnumerable<WinHandsContext> Analyze(IEnumerable<uint> orgHands)
        {
            List<WinHandsContext> winHands = new List<WinHandsContext>();
            WinHandsContext winHand;
            var hands = Tile.Order(orgHands);
            // 雀頭を抽出
            var heads = this.ExtractSameNumberHands(hands, 2);
            foreach (var head in heads)
            {
                winHand = DiProvider.GetContainer().GetInstance<WinHandsContext>();
                winHand.Head = head;
                var sets = this.RemoveExtractedHands(hands, 2, head);

                // 面子を作成
                IEnumerable<uint> extractedHands;
                foreach(var patternDic in SetPatterns)
                {
                    foreach(var pattern in patternDic)
                    {
                        var kind = pattern.Key;
                        var workSets = sets;
                        if (pattern.Value == Set.Chow)
                        {
                            // 順子(シュンツ)優先
                            // 順子(シュンツ)
                            extractedHands = this.ExtractChow(workSets, kind);
                            workSets = this.RemoveExtractedChows(workSets, extractedHands);
                            winHand.Chows = winHand.Chows.Concat(extractedHands);
                            // 刻子(コーツ)
                            extractedHands = this.ExtractPung(workSets, kind);
                            workSets = this.RemoveExtractedHands(workSets, 3, extractedHands);
                            winHand.Pungs = winHand.Pungs.Concat(extractedHands);
                        }
                        else
                        {
                            // 刻子(コーツ)優先
                            // 刻子(コーツ)
                            extractedHands = this.ExtractPung(workSets, kind);
                            workSets = this.RemoveExtractedHands(workSets, 3, extractedHands);
                            winHand.Pungs = winHand.Pungs.Concat(extractedHands);
                            // 順子(シュンツ)
                            extractedHands = this.ExtractChow(workSets, kind);
                            workSets = this.RemoveExtractedChows(workSets, extractedHands);
                            winHand.Chows = winHand.Chows.Concat(extractedHands);
                        }
                        // 槓子(カンツ)
                        //extractedHands = this.ExtractKong(workSets, kind);
                        //workSets = this.RemoveExtractedHands(workSets, 4, extractedHands);
                        //winHand.Kongs = winHand.Kongs.Concat(extractedHands);
                    }
                    // あがりの形になっていたら追加
                    if (winHand.Winnable && !winHands.Contains(winHand))
                    {
                        winHands.Add(winHand);
                    }
                    // 初期化
                    winHand = DiProvider.GetContainer().GetInstance<WinHandsContext>();
                    winHand.Head = head;
                }
            }
            return winHands;
        }

        /// <summary>
        /// 順子(シュンツ)を抽出します。
        /// </summary>
        /// <param name="orgHands">抽出元の手牌</param>
        /// <param name="kind">牌の種類</param>
        /// <returns>順子(シュンツ)のリスト</returns>
        private IEnumerable<uint> ExtractChow(IEnumerable<uint> orgHands, Tile.Kind kind)
        {
            var workSets = orgHands;
            var count = 0;
            IEnumerable<uint> chows = new List<uint>();
            // 数牌(シューパイ)以外は処理終了
            if (kind.GetGroup() != Tile.Group.Suits)
            {
                return chows;
            }

            while (count < 4)
            {
                var workChows = this.ExtractChowInternal(workSets, kind);
                if (workChows.Count() <= 0)
                {
                    break;
                }
                chows = chows.Concat(workChows);
                workSets = this.RemoveExtractedChows(workSets, workChows);
                count++;
            }
            return chows;
        }

        /// <summary>
        /// 順子(シュンツ)を抽出します。
        /// </summary>
        /// <param name="orgHands">抽出元の手牌</param>
        /// <param name="kind">牌の種類</param>
        /// <returns>順子(シュンツ)のリスト</returns>
        private IEnumerable<uint> ExtractChowInternal(IEnumerable<uint> orgHands, Tile.Kind kind)
        {
            var retChows = new List<uint>();
            var workChows = new List<uint>();
            var hands = orgHands.Where(h => h.GetKind() == kind);
            foreach (var h in hands)
            {
                if (workChows.Count() <= 0)
                {
                    workChows.Add(h);
                    continue;
                }
                var last = workChows.Last();
                // 同一数値の場合はスキップ
                if (last.GetNumber() == h.GetNumber())
                {
                    continue;
                }
                // 前回数値+1ならOK
                if (last.GetNumber() < Tile.MaxNumber && (last.GetNumber() + 1) == h.GetNumber())
                {
                    workChows.Add(h);
                } else
                {
                    workChows.Clear();
                    workChows.Add(h);
                }

                if (workChows.Count() == 3)
                {
                    retChows.Add(workChows.First());
                    workChows.Clear();
                }
            }
            return retChows;
        }

        /// <summary>
        /// 刻子(コーツ)を抽出します。
        /// </summary>
        /// <param name="orgHands">抽出元の手牌</param>
        /// <param name="kind">牌の種類</param>
        /// <returns>刻子(コーツ)のリスト</returns>
        private IEnumerable<uint> ExtractPung(IEnumerable<uint> orgHands, Tile.Kind kind)
        {
            return this.ExtractSameNumberHands(orgHands, 3, kind);
        }

        /// <summary>
        /// 槓子(カンツ)を抽出します。
        /// </summary>
        /// <param name="orgHands">抽出元の手牌</param>
        /// <param name="kind">牌の種類</param>
        /// <returns>槓子(カンツ)のリスト</returns>
        private IEnumerable<uint> ExtractKong(IEnumerable<uint> orgHands, Tile.Kind kind)
        {
            return this.ExtractSameNumberHands(orgHands, 4, kind);
        }

        /// <summary>
        /// 同一数字の手牌を抽出します。
        /// </summary>
        /// <param name="orgHands">抽出元の手牌</param>
        /// <param name="minCount">抽出条件（最低牌数）</param>
        /// <param name="kind">抽出条件（種類）</param>
        /// <returns>同一数字の手牌</returns>
        private IEnumerable<uint> ExtractSameNumberHands(IEnumerable<uint> orgHands, int minCount, Tile.Kind kind = Tile.Kind.Undefined)
        {
            var hands = orgHands
                .GroupBy(h => new { kind = h.GetKind(), num = h.GetNumber() })
                .Select(h => new { Kind = h.Key.kind, Num = h.Key.num, Count = h.Count() })
                .Where(h => (kind == Tile.Kind.Undefined || h.Kind == kind) && minCount <= h.Count)
                .Select(h => Tile.BuildTile(h.Kind, h.Num));
            return hands;
        }

        /// <summary>
        /// 手牌から抽出済の順子(シュンツ)を削除します。
        /// </summary>
        /// <param name="orgHands">抽出元の手牌</param>
        /// <param name="extHands">抽出済の順子(シュンツ)</param>
        /// <param name="maxCount">削除する最大数</param>
        /// <returns>抽出済の牌を削除した手牌</returns>
        private IEnumerable<uint> RemoveExtractedChows(IEnumerable<uint> orgHands, IEnumerable<uint> extHands)
        {
            var removedHands = orgHands;
            foreach(var ext in extHands)
            {
                for(var i = 0; i < 3; i++)
                {
                    var chow = Tile.BuildTile(ext.GetKind(), ext.GetNumber() + i);
                    removedHands = this.RemoveExtractedHands(removedHands, 1, chow);
                }
            }
            return removedHands;
        }

        /// <summary>
        /// 手牌から抽出済の牌を削除します。
        /// </summary>
        /// <param name="orgHands">抽出元の手牌</param>
        /// <param name="extHands">抽出済の牌</param>
        /// <param name="maxCount">削除する最大数</param>
        /// <returns>抽出済の牌を削除した手牌</returns>
        private IEnumerable<uint> RemoveExtractedHands(IEnumerable<uint> orgHands, int maxCount, uint extHands)
        {
            return this.RemoveExtractedHands(orgHands, maxCount, new uint[] { extHands });
        }

        /// <summary>
        /// 手牌から抽出済の牌を削除します。
        /// </summary>
        /// <param name="orgHands">抽出元の手牌</param>
        /// <param name="extHands">抽出済の牌</param>
        /// <param name="maxCount">削除する最大数</param>
        /// <returns>抽出済の牌を削除した手牌</returns>
        private IEnumerable<uint> RemoveExtractedHands(IEnumerable<uint> orgHands, int maxCount, IEnumerable<uint> extHands)
        {
            var removedHands = orgHands;
            foreach(var ext in extHands)
            {
                var count = 0;
                removedHands = removedHands
                    .Where(h =>
                    {
                        if (h != ext || count >= maxCount)
                        {
                            return true;
                        }
                        count++;
                        return false;
                    });
            }
            return removedHands.ToList();
        }

        /// <summary>
        /// 面子のパターンを組み立てます。
        /// </summary>
        /// <returns>面子のパターン</returns>
        private static IEnumerable<Dictionary<Tile.Kind, Set>> BuildSetPatterns()
        {
            var patterns = new List<Dictionary<Tile.Kind, Set>>();
            var suits = Tile.GetSuitsKinds();
            var allKinds = Tile.GetAllValidKinds();
            var maxCount = suits.Count();
            // 全て順子(シュンツ)のパターンを追加しておく
            patterns.Add(allKinds.ToDictionary(k => k, k => k.GetGroup() == Tile.Group.Suits ? Set.Chow : Set.Pung));
            for (var i = 0; i < maxCount; i++)
            {
                var chowsComb = MathHelper.Combination(suits, i);
                foreach (var chows in chowsComb)
                {
                    var p = allKinds.ToDictionary(k => k, k => chows.Contains(k) ? Set.Chow : Set.Pung);
                    patterns.Add(p);
                }
            }
            return patterns;
        }
    }
}
