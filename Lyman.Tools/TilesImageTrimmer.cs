using System.Collections.Generic;
using System.Linq;
using System;
using System.Drawing;
using Lyman.Helpers;
using Lyman.Models;

namespace Lyman.Tools
{
    /// <summary>
    /// 牌画像の切り取り機能を提供します。
    /// </summary>
    public class TilesImageTrimmer
    {
        /// <summary>
        /// 読み込むファイルのパス
        /// </summary>
        private const string ImagePath = "./Resources/Images/tiles.png";

        /// <summary>
        /// 牌の横幅
        /// </summary>
        private const int Width = 66;

        /// <summary>
        /// 牌の縦幅
        /// </summary>
        private const int Height = 100;

        /// <summary>
        /// 水平方向の余白
        /// </summary>
        private const int HorizontalSpace = 11;

        /// <summary>
        /// 垂直方向の余白
        /// </summary>
        private const int VerticalSpace = 19;

        /// <summary>
        /// 牌リスト
        /// </summary>
        private static readonly List<List<string>> Tiles = BuildTiles();

        /// <summary>
        /// 牌画像の切り取りを実行します。
        /// </summary>ß
        public void Execute()
        {
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            if (!System.IO.File.Exists(ImagePath))
            {
                throw new Exception("ファイルが存在しません。");
            }

            using (var inImage = new Bitmap(ImagePath))
            {
                var x = 0;
                var y = 0;
                foreach (var tileLine in Tiles)
                {
                    foreach (var fileName in tileLine)
                    {
                        var inRect = new Rectangle(x, y, Width, Height);
                        var outImage = new Bitmap(Width, Height, inImage.PixelFormat);
                        var outRect = new Rectangle(0, 0, outImage.Width, outImage.Height);
                        var g = Graphics.FromImage(outImage);
                        g.DrawImage(inImage, outRect, inRect, GraphicsUnit.Pixel);
                        string outFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(ImagePath) + "/tiles", fileName);
                        outImage.Save(outFile, System.Drawing.Imaging.ImageFormat.Png);
                        x += Width + HorizontalSpace;
                    }
                    x = 0;
                    y += Height + VerticalSpace;
                }
            }
        }

        /// <summary>
        /// 牌リストを組み立てます。
        /// </summary>
        /// <returns>牌リスト</returns>
        private static List<List<string>> BuildTiles()
        {
            var tiles = new List<List<string>>();
            var children = new List<string>();
            children.Add(BuildTileFileName(Tile.Kind.East));
            children.Add(BuildTileFileName(Tile.Kind.South));
            children.Add(BuildTileFileName(Tile.Kind.West));
            children.Add(BuildTileFileName(Tile.Kind.North));
            children.Add(BuildTileFileName(Tile.Kind.WhiteDragon));
            children.Add(BuildTileFileName(Tile.Kind.GreenDragon));
            children.Add(BuildTileFileName(Tile.Kind.RedDragon));
            tiles.Add(children);
            var suits = new[] { Tile.Kind.Characters, Tile.Kind.Bamboos, Tile.Kind.Circles };
            foreach (var suit in suits)
            {
                children = new List<string>();
                for (var i = 0; i < 9; i++)
                {
                    children.Add(BuildTileFileName(suit, i + 1));
                    if (i == 4)
                    {
                        children.Add(BuildTileFileName(suit, i + 1, true));
                    }
                }
                tiles.Add(children);
            }
            return tiles;
        }

        /// <summary>
        /// ファイル名を組み立てます。
        /// </summary>
        /// <returns>ファイル名</returns>
        /// <param name="kind">種類</param>
        /// <param name="number">数</param>
        /// <param name="isRed">赤ドラかどうか</param>
        private static string BuildTileFileName(Tile.Kind kind, int number = 0, bool isRed = false)
        {
            var _isRed = isRed ? 1 : 0;
            return $"{Tile.BuildTile(kind, number, isRed)}.png";
        }
    }
}
