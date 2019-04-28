using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using Fingerprint.Optimalisation;
using Fingerprint.Functions;

namespace Fingerprint
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bitmap inputBMP;
        Bitmap outputBMP;
        bool isInputLoaded, isInputBinarised, isInputSkeletized, isMinutionsDetected;
        FastBitmap fastInput;
        FastBitmap fastOutput;
        public MainWindow()
        {
            InitializeComponent();
            isInputLoaded = false;
            isInputBinarised = false;
            isInputSkeletized = false;
            isMinutionsDetected = false;
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.DefaultExt = ".png";
            ofd.Title = "Wybierz obrazek";
            ofd.Filter = "Pliki JPEG (*.jpg; *.jpeg)|*.jpg;*.jpeg|Pliki PNG (*.png)|*.png|Pliki BMP (*.bmp)|*.bmp|Pliki TIFF (*.tiff)|*.tiff|Pliki SVG (*.svg)|*.svg";

            Nullable<Boolean> result = ofd.ShowDialog();
            if (result == true)
            {
                inputBMP = new Bitmap(ofd.FileName);
            }

            var memory = new MemoryStream();
            inputBMP.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
            memory.Position = 0;
            var bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.StreamSource = memory;
            bimage.CacheOption = BitmapCacheOption.OnLoad;
            bimage.EndInit();
            inputImage.Source = bimage;

            isInputLoaded = true;
            fastInput = new FastBitmap(inputBMP);
        }

        private void BinButton_Click(object sender, RoutedEventArgs e)
        {
            if (!fastInput.isLocked)
            {
                fastInput.Lock();
            }
            SetOutputSource(Binarisation.Run(fastInput).getBitmap());
            isInputBinarised = true;
        }

        private void KmmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!fastInput.isLocked)
            {
                fastInput.Lock();
            }
            isInputBinarised = true;
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {

            isInputBinarised = true;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SetOutputSource(Bitmap bitmap)
        {
            var memory = new MemoryStream();
            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
            memory.Position = 0;
            var bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.StreamSource = memory;
            bimage.CacheOption = BitmapCacheOption.OnLoad;
            bimage.EndInit();
            outputImage.Source = bimage;
        }
    }
}
