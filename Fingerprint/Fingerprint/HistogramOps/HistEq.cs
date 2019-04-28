using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.HistogramOps
{
    class HistEq
    {
        public static double[] dystrybuanta(int[] histogram)
        {
            double[] retVal = new double[256];
            for(int i = 0; i < 256; i++)
            {
                for(int k = 0; k<=i;k++)
                {
                    retVal[i] += histogram[k];
                }
                retVal[i] /= histogram.Sum();
            }
            return retVal;
        }
        public static Bitmap Run(Bitmap bmp)
        {
            //1. Dystrybuanta
            Histogram hist = new Histogram(bmp);
            double[] dystrybuantaR = dystrybuanta(hist.R);
            double[] dystrybuantaG = dystrybuanta(hist.G);
            double[] dystrybuantaB = dystrybuanta(hist.B);
            //2. LookUp Table
            LUT lookupR = new LUT();
            LUT lookupG = new LUT();
            LUT lookupB = new LUT();
            for (int i = 0; i < 256; i++)
            {
                lookupR[i] = (int)(((dystrybuantaR[i] - dystrybuantaR[0]) / (1 - dystrybuantaR[0]))*(dystrybuantaR.Length-1));
                lookupR[i] = (lookupR[i] > 255)? 255 : lookupR[i];
                lookupR[i] = (lookupR[i] < 0) ? 0 : lookupR[i];

                lookupG[i] = (int)(((dystrybuantaG[i] - dystrybuantaG[0]) / (1 - dystrybuantaG[0])) * (dystrybuantaG.Length - 1));
                lookupG[i] = (lookupG[i] > 255) ? 255 : lookupG[i];
                lookupG[i] = (lookupG[i] < 0) ? 0 : lookupG[i];

                lookupB[i] = (int)(((dystrybuantaB[i] - dystrybuantaB[0]) / (1 - dystrybuantaB[0])) * (dystrybuantaB.Length - 1));
                lookupB[i] = (lookupB[i] > 255) ? 255 : lookupB[i];
                lookupB[i] = (lookupB[i] < 0) ? 0 : lookupB[i];
            }
            //3. Wprowadzanie zmian
            Bitmap retVal = new Bitmap(bmp.Width, bmp.Height);

            for(int x = 0;x < retVal.Width; x++)
            {
                for (int y = 0; y < retVal.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int valueR = c.R;
                    int valueG = c.G;
                    int valueB = c.B;
                    int newR = (int)lookupR[valueR];
                    int newG = (int)lookupG[valueG];
                    int newB = (int)lookupB[valueB];
                    retVal.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }

            return retVal;
        }
    }
}
