using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Fingerprint.Optimalisation
{
    unsafe class FastBitmap
    {
        public Bitmap bmp { get; private set; }
        BitmapData bmpLock;
        public bool isLocked { get; private set; }
        byte* bmpLockP;

        public unsafe FastBitmap(Bitmap bmp)
        {
            this.bmp = bmp;

            Lock();
        }

        public unsafe void Lock()
        {
            bmpLock = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadOnly, bmp.PixelFormat);
            bmpLockP = (byte*)bmpLock.Scan0.ToPointer();
            isLocked = true;
        }

        public Bitmap getBitmap()
        {
            if (isLocked)
            { 
                bmp.UnlockBits(bmpLock);
                bmpLockP = null;
                isLocked = false;
            }
            return bmp;
        }

        public Color GetPixel(int x, int y)
        {
            if(x < 0 || y < 0 || x >= bmp.Width || y >= bmp.Height)
            {
                throw new Exception("Punkt poza obrazem!");
            }
            if (!isLocked)
            {
                throw new Exception("Obraz nie jest zablokowany!");
            }
            int r, g, b;
            b = bmpLockP[x * 4 + y * bmpLock.Stride];
            g = bmpLockP[x * 4 + y * bmpLock.Stride + 1];
            r = bmpLockP[x * 4 + y * bmpLock.Stride + 2];

            return Color.FromArgb(r, g, b);
        }

        public void SetPixel(int x, int y,Color c)
        {
            if (x < 0 || y < 0 || x >= bmp.Width || y >= bmp.Height)
            {
                throw new Exception("Punkt poza obrazem!");
            }
            if (!isLocked)
            {
                throw new Exception("Obraz nie jest zablokowany!");
            }
            bmpLockP[x * 4 + y * bmpLock.Stride] = c.B;
            bmpLockP[x * 4 + y * bmpLock.Stride + 1] = c.G;
            bmpLockP[x * 4 + y * bmpLock.Stride + 2] = c.R;
        }

        public Color[] Get3x3Mask(int x, int y)
        {
            Color[] retVal = new Color[9];

            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    try
                    {
                        retVal[3 * i + j] = GetPixel(x + i - 1, y + j - 1);
                    }
                    catch
                    {
                        retVal[3*i+j] = Color.White;
                    }
                }
            }

            return retVal;
        }
    }
}
