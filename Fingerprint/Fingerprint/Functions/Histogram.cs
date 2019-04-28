using Fingerprint.Optimalisation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fingerprint.Functions
{
    class Histogram
    {
        public int[] BW, R, G, B;

        public Histogram(FastBitmap bitmap)
        {
            BW = new int[256];
            R = new int[256];
            G = new int[256];
            B = new int[256];

            for (int x = 0; x < bitmap.bmp.Width; x++)
            {
                for (int y = 0; y < bitmap.bmp.Height; y++)
                {
                    Color c = bitmap.GetPixel(x, y);
                    R[c.R]++;
                    G[c.G]++;
                    B[c.B]++;
                    BW[(c.R + c.G + c.B) / 3]++;
                }
            }
        }

        public double[] getBWDistrib()
        {
            double[] result = new double[BW.Length];
            for (int i = 0; i < result.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j <= i; j++)
                {
                    sum += BW[j];
                }
                result[i] = sum / BW.Sum();
            }
            return result;
        }

        public double getAvgBW()
        {
            double result = 0;
            for (int i = 0; i < BW.Length; i++)
            {
                result += i * BW[i];
            }
            return result / BW.Sum();
        }
    }
}
