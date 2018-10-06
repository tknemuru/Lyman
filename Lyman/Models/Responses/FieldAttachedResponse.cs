// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// フィールド状態が付随した応答
    /// </summary>
    public abstract class FieldAttachedResponse : Response
    {
        /// <summary>
        /// フィールド状態
        /// </summary>
        public FieldContext Context { get; set; }
    }
}
