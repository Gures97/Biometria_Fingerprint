using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.HistogramOps
{
    class LogBrighten
    {
        public static Bitmap Run(Bitmap bmp)
        {
            Bitmap retVal = new Bitmap(bmp.Width, bmp.Height);

            double cValue = 255/Math.Log(256);

            for (int x = 0; x < retVal.Width; x++)
            {
                for (int y = 0; y < retVal.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int valueR = c.R;
                    int valueG = c.G;
                    int valueB = c.B;
                    int newR = (int)(cValue * Math.Log(valueR+1));
                    int newG = (int)(cValue * Math.Log(valueG+1));
                    int newB = (int)(cValue * Math.Log(valueB+1));
                    retVal.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }

            return retVal;
        }
    }
}
