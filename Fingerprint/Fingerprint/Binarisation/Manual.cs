using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.Binarisation
{
    class Manual
    {
        public static Bitmap Run(Bitmap bmp, int setThreshold)
        {
            Bitmap retVal = new Bitmap(bmp.Width, bmp.Height);

            for (int x = 0; x < retVal.Width; x++)
            {
                for (int y = 0; y < retVal.Height; y++)
                {
                    if(bmp.GetPixel(x,y).R > setThreshold)
                    {
                        retVal.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        retVal.SetPixel(x, y, Color.Black);
                    }
                }
            }

            return retVal;
        }
    }
}
