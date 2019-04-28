using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.Filters
{
    class RGBMask
    {
        public int[,] maskR;
        public int[,] maskG;
        public int[,] maskB;

        public RGBMask(Bitmap bmp, int x, int y, int size)
        {
            maskR = new int[size, size];
            maskG = new int[size, size];
            maskB = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Color c;
                    try
                    {
                        c = bmp.GetPixel(
                        (x + i - (size / 2)),
                        (y + j - (size / 2)));
                    }
                    catch
                    {
                        c = new Color();
                    }
                    
                    maskR[i, j] = c.R;
                    maskG[i, j] = c.G;
                    maskB[i, j] = c.B;
                }
            }
        }

        public void convolution(int[,] mask)
        {
            for (int i = 0; i < mask.GetLength(0); i++)
            {
                for (int j = 0; j < mask.GetLength(1); j++)
                {
                    maskR[i, j] *= mask[i, j];
                    maskG[i, j] *= mask[i, j];
                    maskB[i, j] *= mask[i, j];
                }
            }
        }
        public int[] RToSortedArray()
        {
            List<int> retVal = new List<int>();
            foreach(int i in maskR)
            {
                retVal.Add(i);
            }
            retVal.Sort();
            return retVal.ToArray();
        }

        public int[] GToSortedArray()
        {
            List<int> retVal = new List<int>();
            foreach (int i in maskG)
            {
                retVal.Add(i);
            }
            retVal.Sort();
            return retVal.ToArray();
        }

        public int[] BToSortedArray()
        {
            List<int> retVal = new List<int>();
            foreach (int i in maskB)
            {
                retVal.Add(i);
            }
            retVal.Sort();
            return retVal.ToArray();
        }

    }
}
