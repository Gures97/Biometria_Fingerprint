using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.HistogramOps
{
    class PowDarken
    {
        public static Bitmap Run(Bitmap bmp)
        {
            Bitmap retVal = new Bitmap(bmp.Width, bmp.Height);

            double cValue = 1.0 / 65025.0;

            for (int x = 0; x < retVal.Width; x++)
            {
                for (int y = 0; y < retVal.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int valueR = c.R;
                    int valueG = c.G;
                    int valueB = c.B;
                    int newR = (int)(cValue * Math.Pow(valueR, 3));
                    int newG = (int)(cValue * Math.Pow(valueG, 3));
                    int newB = (int)(cValue * Math.Pow(valueB, 3));
                    retVal.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }

            return retVal;
        }
    }
}
