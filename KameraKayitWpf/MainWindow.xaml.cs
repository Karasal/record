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
using NumSharp;
using SharpCV;
using static SharpCV.Binding;

namespace KameraKayitWpf
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Hello World
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int height = 1080;
            int width = 1440;
            string path = "file";
            string path2 = "atat";
            // burasi duzeltilecek
            string[] files = System.IO.Directory.GetFiles(path);

            foreach (string img in files)
            {
                byte[] Rowimg = System.IO.File.ReadAllBytes(img);
                byte[,] Data = new byte[1080, 1440];
                string filename = System.IO.Path.GetFileNameWithoutExtension(img);
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Data[i, j] = Rowimg[i * 1440 + j];
                    }
                }

                SharpCV.Mat dst = new SharpCV.Mat(Data);
                var color = SharpCV.Binding.cv2.cvtColor(dst, ColorConversionCodes.COLOR_BayerBG2BGR);
                SharpCV.Binding.cv2.imwrite("output/" + filename + ".jpg", color);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            
            
            

        }
    }
}
