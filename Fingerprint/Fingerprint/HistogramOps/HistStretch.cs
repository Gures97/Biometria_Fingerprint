using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.HistogramOps
{
    class HistStretch
    {
        public static double[] dystrybuanta(int[] histogram)
        {
            double[] retVal = new double[256];
            for (int i = 0; i < 256; i++)
            {
                for (int k = 0; k <= i; k++)
                {
                    retVal[i] += histogram[k];
                }
                retVal[i] /= histogram.Sum();
            }
            return retVal;
        }

        public static Bitmap Run(Bitmap bmp)
        {
            //1. Wartości Max/Min
            int Rmin = 0,Rmax=255, Gmin = 0, Gmax = 255, Bmin = 0, Bmax = 255;
            Histogram hist = new Histogram(bmp);
            double[] dystrybuantaR = dystrybuanta(hist.R);
            double[] dystrybuantaG = dystrybuanta(hist.G);
            double[] dystrybuantaB = dystrybuanta(hist.B);
            //RED
            for(int i = 0; i< 256; i++)
            {
                if(dystrybuantaR[i] > 0.05)
                {
                    Rmin = i;
                    break;
                }
            }
            for (int i = 255; i >= 0; i--)
            {
                if (dystrybuantaR[i] < 0.95)
                {
                    Rmax = i;
                    break;
                }
            }
            //GREEN
            for (int i = 0; i < 256; i++)
            {
                if (dystrybuantaG[i] > 0.05)
                {
                    Gmin = i;
                    break;
                }
            }
            for (int i = 255; i >= 0; i--)
            {
                if (dystrybuantaG[i] < 0.95)
                {
                    Gmax = i;
                    break;
                }
            }
            //BLUE
            for (int i = 0; i < 256; i++)
            {
                if (dystrybuantaB[i] > 0.05)
                {
                    Bmin = i;
                    break;
                }
            }
            for (int i = 255; i >= 0; i--)
            {
                if (dystrybuantaB[i] < 0.95)
                {
                    Bmax = i;
                    break;
                }
            }

            //2. Wprowadzanie zmian
            Bitmap retVal = new Bitmap(bmp.Width, bmp.Height);

            for (int x = 0; x < retVal.Width; x++)
            {
                for (int y = 0; y < retVal.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int valueR = c.R;
                    int valueG = c.G;
                    int valueB = c.B;
                    int newR = (valueR - Rmin) * 256 / (Rmax - Rmin);
                    newR = (newR > 255) ? 255 : newR;
                    newR = (newR < 0) ? 0 : newR;
                    int newG = (valueG - Gmin)  * 256 / (Gmax - Gmin);
                    newG = (newG > 255) ? 255 : newG;
                    newG = (newG < 0) ? 0 : newG;
                    int newB = (valueB - Bmin)  * 256 / (Bmax - Bmin);
                    newB = (newB > 255) ? 255 : newB;
                    newB = (newB < 0) ? 0 : newB;
                    retVal.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }

            return retVal;
        }
    }
}
