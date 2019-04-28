using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fingerprint.Functions
{
    class KMM
    {
        public static Bitmap Run(Bitmap bmp)
        {
            Bitmap retVal = new Bitmap(bmp.Width, bmp.Height);

            for (int x = 0; x < retVal.Width; x++)
            {
                for (int y = 0; y < retVal.Height; y++)
                {
                    
                }
            }

            return retVal;
        }
    }
}
