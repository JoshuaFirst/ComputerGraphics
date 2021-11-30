using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics1
{
    class BitmapSizeChanger
    {
        public static Bitmap EnlargeImage(Bitmap bitmap)
        {
            var newBitmap = new Bitmap(bitmap.Width * 2, bitmap.Height * 2);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    newBitmap.SetPixel(x * 2, y * 2, bitmap.GetPixel(x, y));
                }
            }

            for (int x = 1; x < newBitmap.Width; x += 2)
            {
                for (int y = 1; y < newBitmap.Height; y += 2)
                {
                    newBitmap.SetPixel(x, y, GetDiagonallyInterpolatedPixelColor(newBitmap, x, y));
                    newBitmap.SetPixel(x - 1, y, GetInterpolatedPixelColor(newBitmap, x - 1, y));
                    newBitmap.SetPixel(x, y - 1, GetInterpolatedPixelColor(newBitmap, x, y - 1));
                }
            }

            return newBitmap;
        }

        private static Color GetInterpolatedPixelColor(Bitmap bitmap, int x, int y)
        {
            var adjacentPixelsCount = 0;

            var interpolatedPixelRgb = new RGB(0, 0, 0);

            if (y > 0)
            {
                var topPixel = bitmap.GetPixel(x, y - 1);
                interpolatedPixelRgb += new RGB(topPixel.R, topPixel.G, topPixel.B);
                adjacentPixelsCount++;
            }

            if (y + 1 < bitmap.Height)
            {
                var downPixel = bitmap.GetPixel(x, y + 1);
                interpolatedPixelRgb += new RGB(downPixel.R, downPixel.G, downPixel.B);
                adjacentPixelsCount++;
            }

            if (x > 0)
            {
                var leftPixel = bitmap.GetPixel(x - 1, y);
                interpolatedPixelRgb += new RGB(leftPixel.R, leftPixel.G, leftPixel.B);
                adjacentPixelsCount++;
            }

            if (x + 1 < bitmap.Width)
            {
                var leftPixel = bitmap.GetPixel(x + 1, y);
                interpolatedPixelRgb += new RGB(leftPixel.R, leftPixel.G, leftPixel.B);
                adjacentPixelsCount++;
            }

            interpolatedPixelRgb /= adjacentPixelsCount;

            return Color.FromArgb(interpolatedPixelRgb.R, interpolatedPixelRgb.G, interpolatedPixelRgb.B);
        }

        private static Color GetDiagonallyInterpolatedPixelColor(Bitmap bitmap, int x, int y)
        {
            var adjacentPixelsCount = 0;

            var interpolatedPixelRgb = new RGB(0, 0, 0);

            if (0 < x && x - 1 < bitmap.Width && 0 < y && y - 1 < bitmap.Height)
            {
                var leftTopPixel = bitmap.GetPixel(x - 1, y - 1);
                interpolatedPixelRgb += new RGB(leftTopPixel.R, leftTopPixel.G, leftTopPixel.B);
                adjacentPixelsCount++;
            }

            if (0 < x && x - 1 < bitmap.Width && 0 < y && y + 1 < bitmap.Height)
            {
                var leftDownPixel = bitmap.GetPixel(x - 1, y + 1);
                interpolatedPixelRgb += new RGB(leftDownPixel.R, leftDownPixel.G, leftDownPixel.B);
                adjacentPixelsCount++;
            }

            if (0 < x && x + 1 < bitmap.Width && 0 < y && y - 1 < bitmap.Height)
            {
                var rightTopPixel = bitmap.GetPixel(x + 1, y - 1);
                interpolatedPixelRgb += new RGB(rightTopPixel.R, rightTopPixel.G, rightTopPixel.B);
                adjacentPixelsCount++;
            }

            if (0 < x && x + 1 < bitmap.Width && 0 < y && y + 1 < bitmap.Height)
            {
                var rightDownPixel = bitmap.GetPixel(x + 1, y + 1);
                interpolatedPixelRgb += new RGB(rightDownPixel.R, rightDownPixel.G, rightDownPixel.B);
                adjacentPixelsCount++;
            }

            interpolatedPixelRgb /= adjacentPixelsCount;

            return Color.FromArgb(interpolatedPixelRgb.R, interpolatedPixelRgb.G, interpolatedPixelRgb.B);
        }
    }
}
