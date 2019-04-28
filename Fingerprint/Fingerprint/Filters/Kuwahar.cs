using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.Filters
{
    class Kuwahar
    {
        public static Bitmap Run(Bitmap bmp)
        {
            Bitmap retVal = new Bitmap(bmp.Width, bmp.Height);
            int newRed, newGreen, newBlue;

            for (int x = 0; x < retVal.Width; x++)
            {
                for (int y = 0; y < retVal.Height; y++)
                {
                    RGBMask rgbmask = new RGBMask(bmp, x, y, 5);
                    //pierwszy segment
                    int sredniaR1 = 0, sredniaG1 = 0, sredniaB1 = 0;
                    for(int i = 0; i< 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            sredniaR1 += rgbmask.maskR[i, j];
                            sredniaG1 += rgbmask.maskG[i, j];
                            sredniaB1 += rgbmask.maskB[i, j];
                        }
                    }
                    sredniaR1 /= 9;
                    sredniaG1 /= 9;
                    sredniaB1 /= 9;
                    int wariancjaR1 = 0, wariancjaG1 = 0, wariancjaB1 = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            wariancjaR1 += sredniaR1 - (int)Math.Pow(rgbmask.maskR[i, j],2);
                            wariancjaG1 += sredniaG1 - (int)Math.Pow(rgbmask.maskG[i, j], 2);
                            wariancjaB1 += sredniaB1 - (int)Math.Pow(rgbmask.maskB[i, j], 2);
                        }
                    }
                    wariancjaR1 /= 9;
                    wariancjaG1 /= 9;
                    wariancjaB1 /= 9;

                    //drugi segment
                    int sredniaR2 = 0, sredniaG2 = 0, sredniaB2 = 0;
                    for (int i = 2; i < 5; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            sredniaR2 += rgbmask.maskR[i, j];
                            sredniaG2 += rgbmask.maskG[i, j];
                            sredniaB2 += rgbmask.maskB[i, j];
                        }
                    }
                    sredniaR2 /= 9;
                    sredniaG2 /= 9;
                    sredniaB2 /= 9;
                    int wariancjaR2 = 0, wariancjaG2 = 0, wariancjaB2 = 0;
                    for (int i = 2; i < 5; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            wariancjaR2 += sredniaR2 - (int)Math.Pow(rgbmask.maskR[i, j], 2);
                            wariancjaG2 += sredniaG2 - (int)Math.Pow(rgbmask.maskG[i, j], 2);
                            wariancjaB2 += sredniaB2 - (int)Math.Pow(rgbmask.maskB[i, j], 2);
                        }
                    }
                    wariancjaR2 /= 9;
                    wariancjaG2 /= 9;
                    wariancjaB2 /= 9;

                    //trzeci segment
                    int sredniaR3 = 0, sredniaG3 = 0, sredniaB3 = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 2; j < 5; j++)
                        {
                            sredniaR3 += rgbmask.maskR[i, j];
                            sredniaG3 += rgbmask.maskG[i, j];
                            sredniaB3 += rgbmask.maskB[i, j];
                        }
                    }
                    sredniaR3 /= 9;
                    sredniaG3 /= 9;
                    sredniaB3 /= 9;
                    int wariancjaR3 = 0, wariancjaG3 = 0, wariancjaB3 = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 2; j < 5; j++)
                        {
                            wariancjaR3 += sredniaR3 - (int)Math.Pow(rgbmask.maskR[i, j], 2);
                            wariancjaG3 += sredniaG3 - (int)Math.Pow(rgbmask.maskG[i, j], 2);
                            wariancjaB3 += sredniaB3 - (int)Math.Pow(rgbmask.maskB[i, j], 2);
                        }
                    }
                    wariancjaR3 /= 9;
                    wariancjaG3 /= 9;
                    wariancjaB3 /= 9;

                    //czwarty segment
                    int sredniaR4 = 0, sredniaG4 = 0, sredniaB4 = 0;
                    for (int i = 2; i < 5; i++)
                    {
                        for (int j = 2; j < 5; j++)
                        {
                            sredniaR4 += rgbmask.maskR[i, j];
                            sredniaG4 += rgbmask.maskG[i, j];
                            sredniaB4 += rgbmask.maskB[i, j];
                        }
                    }
                    sredniaR4 /= 9;
                    sredniaG4 /= 9;
                    sredniaB4 /= 9;
                    int wariancjaR4 = 0, wariancjaG4 = 0, wariancjaB4 = 0;
                    for (int i = 2; i < 5; i++)
                    {
                        for (int j = 2; j < 5; j++)
                        {
                            wariancjaR4 += sredniaR4 - (int)Math.Pow(rgbmask.maskR[i, j], 2);
                            wariancjaG4 += sredniaG4 - (int)Math.Pow(rgbmask.maskG[i, j], 2);
                            wariancjaB4 += sredniaB4 - (int)Math.Pow(rgbmask.maskB[i, j], 2);
                        }
                    }
                    wariancjaR4 /= 9;
                    wariancjaG4 /= 9;
                    wariancjaB4 /= 9;
                    //decyzja
                    var wariancjeR = new int[]{ wariancjaR1, wariancjaR2, wariancjaR3, wariancjaR4 };
                    var wariancjeListR = new List<int>(wariancjeR);
                    switch (wariancjeListR.IndexOf(wariancjeR.Min()))
                    {
                        case 0:
                            newRed = sredniaR1;
                            break;
                        case 1:
                            newRed = sredniaR2;
                            break;
                        case 2:
                            newRed = sredniaR3;
                            break;
                        case 3:
                            newRed = sredniaR4;
                            break;
                        default:
                            newRed = sredniaR1;
                            break;
                    }

                    var wariancjeG = new int[] { wariancjaG1, wariancjaG2, wariancjaG3, wariancjaG4 };
                    var wariancjeListG = new List<int>(wariancjeG);
                    switch (wariancjeListG.IndexOf(wariancjeG.Min()))
                    {
                        case 0:
                            newGreen = sredniaG1;
                            break;
                        case 1:
                            newGreen = sredniaG2;
                            break;
                        case 2:
                            newGreen = sredniaG3;
                            break;
                        case 3:
                            newGreen = sredniaG4;
                            break;
                        default:
                            newGreen = sredniaG1;
                            break;
                    }

                    var wariancjeB = new int[] { wariancjaB1, wariancjaB2, wariancjaB3, wariancjaB4 };
                    var wariancjeListB = new List<int>(wariancjeB);
                    switch (wariancjeListB.IndexOf(wariancjeB.Min()))
                    {
                        case 0:
                            newBlue = sredniaB1;
                            break;
                        case 1:
                            newBlue = sredniaB2;
                            break;
                        case 2:
                            newBlue = sredniaB3;
                            break;
                        case 3:
                            newBlue = sredniaB4;
                            break;
                        default:
                            newBlue = sredniaB1;
                            break;
                    }

                    retVal.SetPixel(x, y, Color.FromArgb(newRed, newGreen, newBlue));
                }
            }

            return retVal;
        }
    }
}
