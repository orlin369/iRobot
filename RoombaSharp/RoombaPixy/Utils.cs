/*
 MIT License

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial SerialPortions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RoombaPixy
{
    public static class Utils
    {

        /// <summary>
        /// Resize bitmap images.
        /// </summary>
        /// <param name="imgToResize">Source image.</param>
        /// <param name="size">Output size.</param>
        /// <returns>Resized new bitmap.</returns>
        public static Bitmap ResizeImage(Bitmap sourceImage, Size size)
        {
            Size destSize = Resize(sourceImage.Size, size);

            Bitmap bitmapImage = new Bitmap(destSize.Width, destSize.Height);
            Graphics graphics = Graphics.FromImage(bitmapImage);

            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(sourceImage, 0, 0, destSize.Width, destSize.Height);
            graphics.Dispose();

            return bitmapImage;
        }

        //public static Bitmap CenterImage (Bitmap sourceImage, Size size)
        //{
        //    return new Bitmap();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceRectangle"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Rectangle OffsetRectangle(Rectangle sourceRectangle, Size capturedImageSize, Size baseSize)
        {
            Point offset = new Point((baseSize.Width - capturedImageSize.Width) / 2, (baseSize.Height - capturedImageSize.Height) / 2);
            Point offsetLocation = new Point(sourceRectangle.X + offset.X, sourceRectangle.Y + offset.Y);

            return new Rectangle(offsetLocation, sourceRectangle.Size);
        }
        public static bool IfNotLaser(Size rectangleSize)
        {
            return rectangleSize.Height > 70 || rectangleSize.Width > 70
                || rectangleSize.Height < 2 || rectangleSize.Width < 2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="baseCordinate"></param>
        /// <returns></returns>
        public static Size Resize(Size source, Size baseCordinate)
        {
            int sourceWidth = source.Width;
            int sourceHeight = source.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = baseCordinate.Width / (float)sourceWidth;
            nPercentH = baseCordinate.Height / (float)sourceHeight;

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
            }
            else
            {
                nPercent = nPercentW;
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            //
            if(float.IsInfinity(nPercent))
            {
                destWidth = 0;
                destHeight = 0;
            }

            return new Size(destWidth, destHeight);
        }
    }
}