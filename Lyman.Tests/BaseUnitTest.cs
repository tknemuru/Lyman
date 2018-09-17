// using System.Collections.Generic;
// using System.Linq;
using System;
using System.Text;
using Lyman.Di;

namespace Lyman.Tests
{
    /// <summary>
    /// 基底ユニットテストクラス
    /// </summary>
    public abstract class BaseUnitTest<T> where T :  class
    {
        /// <summary>
        /// テスト対象のインスタンス
        /// </summary>
        /// <value>The target.</value>
        protected T Target { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected BaseUnitTest()
        {
            this.Target = DiProvider.GetContainer().GetInstance<T>();
        }

        /// <summary>
        /// リソースのパスを取得します。
        /// </summary>
        /// <returns>リソースパス</returns>
        /// <param name="index">インデックス</param>
        /// <param name="childIndex">子インデックス</param>
        /// <param name="type">リソース種別</param>
        /// <param name="extension">拡張子</param>
        protected string GetResourcePath(int index, int childIndex, ResourceType type, string extension = "txt")
        {
            return $"../../Resources/{this.Target.GetType().Name}/{index.ToString().PadLeft(3, '0')}-{childIndex.ToString().PadLeft(3, '0')}-{type.ToString().ToLower()}.{extension}";
        }
    }
}
