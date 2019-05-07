using Fingerprint.Optimalisation;
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
        public static int[] foursTable = { 3, 6, 12, 24, 48, 96, 192, 129,
                                           7, 14, 28, 56, 112, 224, 193, 131,
                                           15, 30, 60, 120, 240, 225, 195, 135};

        public static int[] deletionTable = { 3, 5, 7, 12, 13, 14, 15, 20,
                                              21, 22,23,28,29,30,31,48,
                                              52,53,54,55,56,60,61,62,
                                              63,65,67,69,71,77,79,80,
                                              81,83,84,85,86,87,88,89,
                                              91,92,93,94,95,97,99,101,
                                              103,109,111,112,113,115,116,117,
                                              118,119,120,121,123,124,125,126,
                                              127,131,133,135,141,143,149,151,
                                              157,159,181,183,189,191,192,193,
                                              195,197,199,205,207,208,209,211,
                                              212,213,214,215,216,217,219,220,
                                              221,222,223,224,225,227,229,231,
                                              237,239,240,241, 243,244,245,246,
                                              247,248,249,251,252,253,254,255};
        static bool working;

        public static FastBitmap Run(FastBitmap bmp)
        {
            if (!bmp.isLocked)
            {
                bmp.Lock();
            }
            working = true;
            Set1s(bmp);
            while (working)
            {
                working = false;
                Check2s(bmp);
                Check3s(bmp);
                CheckAndDelete4s(bmp);
                Clean2s(bmp);
                And3s(bmp);
            }
            Cleanup(bmp);

            return bmp;
        }

        public static void Set1s(FastBitmap bmp)
        {
            for (int x = 0; x < bmp.bmp.Width; x++)
            {
                for (int y = 0; y < bmp.bmp.Height; y++)
                {
                    if(bmp.GetPixel(x,y).ToArgb() == Color.Black.ToArgb())
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(50, 50, 50));
                    }
                }
            }
        }

        public static void Check2s(FastBitmap bmp)
        {
            for (int x = 0; x < bmp.bmp.Width; x++)
            {
                for (int y = 0; y < bmp.bmp.Height; y++)
                {
                    if (bmp.GetPixel(x, y).ToArgb() != Color.White.ToArgb())
                    {
                        Color[] mask = bmp.Get3x3Mask(x, y);
                        if( mask[1].ToArgb() == Color.White.ToArgb() ||
                            mask[3].ToArgb() == Color.White.ToArgb() || 
                            mask[5].ToArgb() == Color.White.ToArgb() || 
                            mask[7].ToArgb() == Color.White.ToArgb())
                            bmp.SetPixel(x, y, Color.FromArgb(0, 255, 0));
                    }
                }
            }
        }

        public static void Check3s(FastBitmap bmp)
        {
            for (int x = 0; x < bmp.bmp.Width; x++)
            {
                for (int y = 0; y < bmp.bmp.Height; y++)
                {
                    if (bmp.GetPixel(x, y).ToArgb() == Color.FromArgb(50, 50, 50).ToArgb())
                    {
                        Color[] mask = bmp.Get3x3Mask(x, y);
                        bool contains = false;

                        foreach(Color c in mask)
                        {
                            if (c.ToArgb() == Color.White.ToArgb())
                                contains = true;
                        }

                        if (contains)
                            bmp.SetPixel(x, y, Color.FromArgb(0, 0, 255));
                    }
                }
            }
        }

        public static void CheckAndDelete4s(FastBitmap bmp)
        {
            for (int x = 0; x < bmp.bmp.Width; x++)
            {
                for (int y = 0; y < bmp.bmp.Height; y++)
                {
                    if (bmp.GetPixel(x, y).ToArgb() != Color.White.ToArgb())
                    {
                        if (foursTable.Contains(GetValueOfPixel(bmp, x, y))) {
                            working = true;
                            bmp.SetPixel(x, y, Color.White);
                        }
                    }
                }
            }
        }

        public static void Clean2s(FastBitmap bmp)
        {
            for (int x = 0; x < bmp.bmp.Width; x++)
            {
                for (int y = 0; y < bmp.bmp.Height; y++)
                {
                    if (bmp.GetPixel(x, y).ToArgb() == Color.FromArgb(0, 255, 0).ToArgb())
                    {
                        if (deletionTable.Contains(GetValueOfPixel(bmp, x, y))) { 
                            bmp.SetPixel(x, y, Color.White);
                            working = true;
                        }
                    else
                        bmp.SetPixel(x, y, Color.FromArgb(50, 50, 50));
                    }
                }
            }
        }
        public static void And3s(FastBitmap bmp)
        {
            for (int x = 0; x < bmp.bmp.Width; x++)
            {
                for (int y = 0; y < bmp.bmp.Height; y++)
                {
                    if (bmp.GetPixel(x, y).ToArgb() == Color.FromArgb(0, 0, 255).ToArgb())
                    {
                        if (deletionTable.Contains(GetValueOfPixel(bmp, x, y)))
                        { 
                            bmp.SetPixel(x, y, Color.White);
                            working = true;
                        }
                        else
                            bmp.SetPixel(x, y, Color.FromArgb(50, 50, 50));
                    }
                }
            }
        }

        public static void Cleanup(FastBitmap bmp)
        {
            for (int x = 0; x < bmp.bmp.Width; x++)
            {
                for (int y = 0; y < bmp.bmp.Height; y++)
                {
                    if (bmp.GetPixel(x, y).ToArgb() != Color.White.ToArgb())
                    {
                        bmp.SetPixel(x, y, Color.Black);
                    }
                }
            }
        }

        public static int GetValueOfPixel(FastBitmap bmp, int x, int y)
        {
            Color[] mask = bmp.Get3x3Mask(x, y);
            int value = 0;
            value += mask[1].ToArgb() == Color.White.ToArgb() ? 0 : 1;
            value += mask[2].ToArgb() == Color.White.ToArgb() ? 0 : 2;
            value += mask[5].ToArgb() == Color.White.ToArgb() ? 0 : 4;
            value += mask[8].ToArgb() == Color.White.ToArgb() ? 0 : 8;
            value += mask[7].ToArgb() == Color.White.ToArgb() ? 0 : 16;
            value += mask[6].ToArgb() == Color.White.ToArgb() ? 0 : 32;
            value += mask[3].ToArgb() == Color.White.ToArgb() ? 0 : 64;
            value += mask[0].ToArgb() == Color.White.ToArgb() ? 0 : 128;
            return value;
        }
    }
}
