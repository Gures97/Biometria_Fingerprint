using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.Filters
{
    class MedianFilter
    {
        public static Bitmap Run(Bitmap bmp, int maskSize)
        {
            Bitmap retVal = new Bitmap(bmp.Width, bmp.Height);
            int newRed, newGreen, newBlue;

            for (int x = 0; x < retVal.Width; x++)
            {
                for (int y = 0; y < retVal.Height; y++)
                {
                    RGBMask rgbmask = new RGBMask(bmp, x, y, maskSize);
                    //R
                    newRed = rgbmask.RToSortedArray()[rgbmask.maskR.Length / 2];
                    //G
                    newGreen = rgbmask.GToSortedArray()[rgbmask.maskG.Length / 2];
                    //B
                    newBlue = rgbmask.BToSortedArray()[rgbmask.maskB.Length / 2];

                    retVal.SetPixel(x, y, Color.FromArgb(newRed, newGreen, newBlue));
                }
            }

            return retVal;
        }
    }
}
