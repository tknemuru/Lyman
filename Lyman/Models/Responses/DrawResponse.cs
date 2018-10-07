﻿// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Models.Requests;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// ツモ応答
    /// </summary>
    public sealed class DrawResponse : FieldAttachedResponse
    {
        /// <summary>
        /// 牌
        /// </summary>
        public uint Tile { get; set; }

        /// <summary>
        /// 次ツモの位置
        /// </summary>
        /// <value>The next position.</value>
        public WallPosition NextPosition { get; set; }

        /// <summary>
        /// 要求種別の初期化を行います。
        /// </summary>
        protected override void InitRequestType()
        {
            this.RequestType = RequestType.Draw;
        }
    }
}
