using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    /// <summary>
    /// 役分析機能を提供します。
    /// </summary>
    public interface IYakuAnalyzable : IReceivable<List<uint>, YakuAnalyzeResponse>
    {
    }
}
