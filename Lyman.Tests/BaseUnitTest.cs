// using System.Collections.Generic;
// using System.Linq;
using System;
using System.Text;
using Lyman.Converters;
using Lyman.Di;
using Lyman.Helpers;
using Lyman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            return $"../../../Resources/{this.Target.GetType().Name}/{index.ToString().PadLeft(3, '0')}-{childIndex.ToString().PadLeft(3, '0')}-{type.ToString().ToLower()}.{extension}";
        }

        /// <summary>
        /// フィールド状態を読み込みます。
        /// </summary>
        /// <returns>フィールド状態</returns>
        /// <param name="index">インデックス</param>
        /// <param name="childIndex">子インデックス</param>
        /// <param name="type">リソース種別</param>
        /// <param name="extension">拡張子</param>
        protected FieldContext LoadFieldContext(int index, int childIndex, ResourceType type, string extension = "txt")
        {
            return DiProvider.GetContainer().GetInstance<TextToContextConverter>().Convert(FileHelper.ReadTextLines(this.GetResourcePath(index, childIndex, type, extension)));
        }

        /// <summary>
        /// フィールドの状態が同一であることを検証します。
        /// </summary>
        /// <param name="expected">期待値</param>
        /// <param name="actual">実際値</param>
        public void AssertEqualsFieldContext(FieldContext expected, FieldContext actual)
        {
            Assert.IsTrue(expected.Equals(actual),
                Environment.NewLine + "[expected] :" + Environment.NewLine + "{0}" + Environment.NewLine + " [actual] :" + Environment.NewLine + "{1}",
                          DiProvider.GetContainer().GetInstance<ContextToTextConverter>().Convert(expected), DiProvider.GetContainer().GetInstance<ContextToTextConverter>().Convert(actual));
        }

        /// <summary>
        /// プレイヤの状態が同一であることを検証します。
        /// </summary>
        /// <param name="expected">期待値</param>
        /// <param name="actual">実際値</param>
        public void AssertEqualsPlayer(Player expected, Player actual)
        {
            Assert.IsTrue(expected.Equals(actual),
                Environment.NewLine + "[expected] :" + Environment.NewLine + "{0}" + Environment.NewLine + " [actual] :" + Environment.NewLine + "{1}",
                          expected, actual);
        }
    }
}
