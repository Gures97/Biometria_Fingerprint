using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.Binarisation
{
    class BersenMethod
    {
        public static Bitmap Run(Bitmap bmp, int setThreshold, int setContrast)
        {
            Bitmap retVal = new Bitmap(bmp.Width, bmp.Height);
            var colorTable = BmpToTable(bmp);
            for (int x = 0; x < retVal.Width; x++)
            {
                for (int y = 0; y < retVal.Height; y++)
                {
                    int[] mask = getMask(colorTable, x, y, 3);
                    int max = 0;
                    int min = 255;
                    foreach(int c in mask)
                    {
                        if(c > max)
                        {
                            max = c;
                        }
                        if(c < min)
                        {
                            min = c;
                        }
                    }
                    int prog = (max + min) / 2;
                    int kontrast = (max - min) /2;
                    Color newColor;
                    if(kontrast < setContrast)
                    {
                        newColor = (prog >= setThreshold) ? Color.White : Color.Black;
                    }
                    else
                    {
                        newColor = (colorTable[x,y] >= setThreshold) ? Color.White : Color.Black;
                    }
                    retVal.SetPixel(x, y, newColor);
                }
            }

            return retVal;
        }

        public static int[] getMask(byte[,] bmp, int x, int y, int size)
        {
            List<int> retVal = new List<int>();
            for(int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    try
                    {
                        retVal.Add(bmp[x + i - 1, y + j - 1]);
                    }
                    catch
                    {

                    }
                }
            }
            return retVal.ToArray();
        }

        public static byte[,] BmpToTable(Bitmap bmp)
        {
            byte[,] result = new byte[bmp.Width,bmp.Height];

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    result[x, y] = bmp.GetPixel(x, y).B;
                }
            }

            return result;
        }
    }
}
