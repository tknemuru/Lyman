// using System.Collections.Generic;
// using System.Linq;
using System;
using System.Diagnostics;

namespace Lyman.Di
{

    /// <summary>
    /// DIの生成機能を提供します。
    /// </summary>
    public static class DiProvider
    {
        /// <summary>
        /// DIコンテナ
        /// </summary>
        private static SimpleContainer MyContainer { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static DiProvider()
        {
            MyContainer = new SimpleContainer();
            Register();
#if DEBUG
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                Debug.Listeners.Add(new DebugListener());
            }
#endif
        }

        /// <summary>
        /// DIコンテナをセットします。
        /// </summary>
        /// <param name="container">DIコンテナ</param>
        public static void SetContainer(SimpleContainer container)
        {
            MyContainer = container;
        }

        /// <summary>
        /// DIコンテナを取得します。
        /// </summary>
        /// <returns></returns>
        public static SimpleContainer GetContainer()
        {
            if (MyContainer == null)
            {
                throw new InvalidOperationException("コンテナのインスタンスが生成されていません。");
            }
            return MyContainer;
        }

        /// <summary>
        /// 依存性の登録を行います。
        /// </summary>
        private static void Register()
        {
        }
    }
}
