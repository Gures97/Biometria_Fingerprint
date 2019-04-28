using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zadanie1.Binarisation
{
    class OtsuMethod
    {
        public static Bitmap Run(Bitmap bmp)
        {
            double max = 0; //maksymalna wartość 
            int threshold = 0; //ustalony prog
            var hist = new Histogram(bmp); //histogram
            var histArray = hist.BW; //tablica z licznością poszczegolnych kolorow
            var total = hist.BW.Sum(); // maksymalna liczba pixeli
            var dystrybuanta = hist.getBWDistrib(); //dystrybuanta histogramu
            List<double> list = new List<double>();

            double wB = 0,wF;
            double sumaB = 0;

            double sumaTotal = 0;
            for(int i = 0; i < 256; i++)
            {
                sumaTotal += i * histArray[i];
            }

            for (int i = 0; i < 256; i++)
            {
                wB += histArray[i];
                wF = total - wB;
                if (wB == 0 || wF == 0)
                    continue;
                sumaB += i * histArray[i];
                var mF = (sumaTotal - sumaB) / wF;

                var srodek = wB * wF * ((sumaB / wB) - mF) * ((sumaB / wB) - mF);
                if(srodek > max)
                {
                    max = srodek;
                    threshold = i;
                }
            }

            return Manual.Run(bmp, threshold);
        }
    }
}
