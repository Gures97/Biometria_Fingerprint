using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Zadanie1.Filters
{
    class Convolution
    {
        public static Bitmap Run(Bitmap bmp, int maskSize, int[,] mask,ProgressBar bar)
        {
            bar.Maximum = bmp.Width * bmp.Height;
            bar.Minimum = 0;
            bar.Value = 0;
            Bitmap retVal = new Bitmap(bmp.Width, bmp.Height);
            int newRed, newGreen, newBlue;

            for (int x = 0; x < retVal.Width; x++)
            {
                for (int y = 0; y < retVal.Height; y++)
                {
                    RGBMask rgbmask = new RGBMask(bmp, x, y, 3);
                    rgbmask.convolution(mask);
                    //R
                    newRed = 0;
                    foreach (int i in rgbmask.maskR)
                    {
                        newRed += i;
                    }
                    //G
                    newGreen = 0;
                    foreach (int i in rgbmask.maskG)
                    {
                        newGreen += i;
                    }
                    //B
                    newBlue = 0;
                    foreach (int i in rgbmask.maskB)
                    {
                        newBlue += i;
                    }

                    newRed = (newRed > 255) ? 255 : newRed;
                    newRed = (newRed < 0) ? 0 : newRed;

                    newGreen = (newGreen > 255) ? 255 : newGreen;
                    newGreen = (newGreen < 0) ? 0 : newGreen;

                    newBlue = (newBlue > 255) ? 255 : newBlue;
                    newBlue = (newBlue < 0) ? 0 : newBlue;
                    retVal.SetPixel(x, y, Color.FromArgb(newRed, newGreen, newBlue));
                    bar.Value++;
                }
            }

            return retVal;
        }
    }
}
