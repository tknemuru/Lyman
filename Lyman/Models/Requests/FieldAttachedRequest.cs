// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// フィールド状態が付随した要求
    /// </summary>
    public abstract class FieldAttachedRequest : Request
    {
        /// <summary>
        /// フィールド状態
        /// </summary>
        public FieldContext Context { get; set; }
    }
}
