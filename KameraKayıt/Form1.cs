using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NumSharp;
using SharpCV;
using static SharpCV.Binding;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace KameraKayıt
{
    
    public partial class Form1 : Form
    {
        static double height1 = 1080;
        static double width1 = 1440;

        static OpenCvSharp.Size opsize = new OpenCvSharp.Size(width1, height1);
        static double fps = 200;
        OpenCvSharp.VideoWriter videokayit = new OpenCvSharp.VideoWriter("kayit1.avi", OpenCvSharp.FourCC.PIM1, fps, opsize); 
        string DosyaYolu;
         //x264 2dk 3 sn
         //xvıd 2dk 6 sn
         //wvc1 1dk 25 sn videosu yok 
         //wmv3 1 dk 34 sn videosu yok
         // wmv2 2dk 8 sn videosu var 10 sn 25 sn gibi oynuyor
         // wmv2 2dk 10 sn  videous var 25 sn sürüyor 

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int height = 1080;
            int width = 1440;
            
            string path = DosyaYolu;

            string[] files = System.IO.Directory.GetFiles(path);

          
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Fotoğraf işlensin mi?", "ÇIKIŞ", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {   
                int ij = 300;
                string  Key = ij.ToString("D6");
                
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
                    var color = SharpCV.Binding.cv2.cvtColor(dst, SharpCV.ColorConversionCodes.COLOR_BayerBG2BGR);
                    SharpCV.Binding.cv2.imwrite("output/" + filename + ".jpg", color);


                    OpenCvSharp.Mat matis = OpenCvSharp.Cv2.ImRead("output/" + filename + ".jpg");
                    //OpenCvSharp.Cv2.ImRead
                    //OpenCvSharp.InputArray Ioarry = new OpenCvSharp.InputArray(dst);

                    videokayit.Write(matis);
                }
                videokayit.Release();
            }
            else
            {
                MessageBox.Show("İşlem yapılmadı");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog _fbd = new FolderBrowserDialog();
            if (_fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                DosyaYolu = (_fbd.SelectedPath);
            
                //listBox1.DataSource = Populate(_fbd.SelectedPath);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Programdan çıkılsın mı?", "ÇIKIŞ", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Çıkış yapılmadı");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                using (OpenCvSharp.VideoCapture v = new OpenCvSharp.VideoCapture(0))
                using (OpenCvSharp.Mat img = new OpenCvSharp.Mat())
                {
                    while (true)
                    {
                        v.Read(img);
                        pictureBox1.Image = BitmapConverter.ToBitmap(img);
                    }
                }
            });
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        //private List<string> Populate(string Path)
        //{
        //    List<string> _items = new List<string>();
        //    string[] _dirs = Directory.GetDirectories(Path, "*.*", SearchOption.AllDirectories);
        //    foreach (var item in _dirs)
        //    {
        //        _items.Add((new DirectoryInfo(item)).Name);
        //        string[] _files = Directory.GetFiles(item, "*.*");
        //        foreach (var item2 in _files)
        //            _items.Add((new FileInfo(item2)).Name);
        //    }
        //    return _items;
        //}


        //OpenFileDialog file = new OpenFileDialog();

        //OpenFileDialog file = new OpenFileDialog();
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog file = new OpenFileDialog();
        //    file.ShowDialog();
        //}
    }
}
