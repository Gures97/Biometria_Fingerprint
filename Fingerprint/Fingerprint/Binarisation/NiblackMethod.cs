using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.Binarisation
{
    class NiblackMethod
    {
        unsafe public static Bitmap Run(Bitmap bmp, int maskSize, double thresholdValue)
        {
            Bitmap retVal = new Bitmap(bmp);
            var bmpLock = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadOnly, bmp.PixelFormat);
            var retLock = retVal.LockBits(new Rectangle(Point.Empty, retVal.Size), ImageLockMode.WriteOnly, retVal.PixelFormat);

            byte* bmpLockP = (byte*)bmpLock.Scan0.ToPointer();
            byte* retLockP = (byte*)retLock.Scan0.ToPointer();

            //var colorTable = BersenMethod.BmpToTable(bmp);
            for (int x = 0; x < retVal.Width; x++)
            {
                for (int y = 0; y < retVal.Height; y++)
                {
                    byte[] mask = getMask(bmpLockP, bmpLock, x, y, maskSize);
                    int avg = 0;
                    double stdDev = 0;
                    foreach (int c in mask)
                    {
                        avg += c;
                        stdDev += c * c;
                    }
                    avg /= mask.Length;
                    stdDev /= mask.Length;
                    stdDev -= Math.Pow(avg, 2);
                    stdDev = Math.Sqrt(stdDev);
                    double threshold = avg + (thresholdValue * stdDev);
                    byte newColor;
                    if (bmpLockP[x * 4 + (y * bmpLock.Stride)] < threshold)
                    {
                        newColor = 0x00;
                    }
                    else
                    {
                        newColor = 0xFF;
                    }
                    //retVal.SetPixel(x, y, newColor);
                    retLockP[x*4 + y * retLock.Stride] = newColor;
                    retLockP[x*4 + y * retLock.Stride + 1] = newColor;
                    retLockP[x*4 + y * retLock.Stride + 2] = newColor;

                }
            }

            bmp.UnlockBits(bmpLock);
            retVal.UnlockBits(retLock);
            return retVal;
        }

        public unsafe static byte[] getMask(byte* bmp, BitmapData bmpLock, int x, int y, int maskSize)
        {
            byte[] retVal = new byte[maskSize*maskSize];

            for (int i = 0; i < maskSize; i++)
            {
                for (int j = 0; j < maskSize; j++)
                {
                    int curX = x + i - (maskSize / 2), curY = y + j - (maskSize / 2);
                    if (curX >= bmpLock.Width || curX < 0)
                        curX = x;
                    if (curY >= bmpLock.Height || curY < 0)
                        curY = y;
                    try
                    {
                        retVal[i * maskSize + j] = bmp[curX * 4 + (curY * bmpLock.Stride)];
                    }
                    catch
                    {
                        retVal[i * maskSize + j] = 0;
                    }
                }
            }

            return retVal;
        }
    }
}
