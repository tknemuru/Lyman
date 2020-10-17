using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Receivers;
using Lyman.Di;

namespace Lyman.Providers
{
    /// <summary>
    /// 役分析機能提供機能を提供します。
    /// </summary>
    public sealed class YakuAnalyzerProvider
    {
        /// <summary>
        /// 役分析機能リスト
        /// </summary>
        private static List<IYakuAnalyzable> YakuAnalyzers = BuildYakuAnalyzers();

        /// <summary>
        /// 役分析機能リストを取得します。
        /// </summary>
        /// <returns>役分析機能リスト</returns>
        public List<IYakuAnalyzable> GetYakuAnalyzers()
        {
            return YakuAnalyzers;
        }

        /// <summary>
        /// 役分析機能のリストを組み立てます。
        /// </summary>
        /// <returns>役分析機能のリスト</returns>
        private static List<IYakuAnalyzable> BuildYakuAnalyzers()
        {
            var analyzers = new List<IYakuAnalyzable>();
            analyzers.Add(DiProvider.GetContainer().GetInstance<AnalyzeAllSimplesReceiver>());
            return analyzers;
        }
    }
}
