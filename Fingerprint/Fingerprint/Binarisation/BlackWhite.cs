using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.Binarisation
{
    class BlackWhite
    {
        public static Bitmap Run(Bitmap bmp)
        {
            Bitmap retVal = new Bitmap(bmp.Width, bmp.Height);

            for (int x = 0; x < retVal.Width; x++)
            {
                for (int y = 0; y < retVal.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int valueR = c.R;
                    int valueG = c.G;
                    int valueB = c.B;
                    int newValue = (valueR + valueG + valueB) / 3;
                    retVal.SetPixel(x, y, Color.FromArgb(valueG, valueG, valueG));
                }
            }

            return retVal;
        }
    }
}
