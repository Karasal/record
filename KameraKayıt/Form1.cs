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
using System.Diagnostics;

namespace KameraKayıt
{
    


    public partial class Form1 : Form
    {
        static double height1 = 1080;
        static double width1 = 1440;

        static OpenCvSharp.Size opsize = new OpenCvSharp.Size(width1, height1);
        static double fps = 200;
        OpenCvSharp.VideoWriter videokayit = new OpenCvSharp.VideoWriter("kayitmjpg.mkv", OpenCvSharp.FourCC.DIVX, fps, opsize); 
        string DosyaYolu;
         //x264 2dk 3 sn vido açılmıyor
         //xvıd 2dk 6 sn resim atlıyor dıvx de resim atlıyor mp4 
         //wvc1 1dk 25 sn videosu yok 
         //wmv3 1 dk 34 sn videosu yok
         // wmv2 2dk 8 sn videosu var 10 sn 25 sn gibi oynuyor
         // wmv1 2dk 10 sn  videosu var 25 sn sürüyor 
         // pım1 2.20 sn videosu çalışmıyor
         //msvc 1 dk 30sn videosu çalışmıyor 
         //mss2 1dk 30 sn video açılmıyor
         //mss1 1dk 30 sn video açılmıyor
         //mpg4 1dk30sn video açlımıyor
         //mpg4 1dk40sn mp4 de açılıyor 
         //h264 1dk34sn 
         //h265 1dk35sn 
         //h263 1dk30sn
         //mjpg 2dk10sn avi açıyor
         //dıvx 2dk  mkv video açılıyor resim atlıyor  


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
                //Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
                //                                            //your sample code
                //System.Threading.Thread.Sleep(500);
                //stopwatch.Stop();
                //Console.WriteLine(stopwatch.ElapsedMilliseconds);
                foreach (string img in files)
                {
                    Stopwatch stopwatch4 = Stopwatch.StartNew();
                    byte[] Rowimg = System.IO.File.ReadAllBytes(img); //0,9 milisecond
                    
                    byte[,] Data = new byte[1080, 1440];
                    var indexer1 = new Mat
                    string filename = System.IO.Path.GetFileNameWithoutExtension(img);
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    for (int i = 0; i < height; i++) // 9 milisecond
                    {

                        for (int j = 0; j < width; j++)
                        {
                            Data[i, j] = Rowimg[i * 1440 + j];  
                        }
                    }
                    stopwatch.Stop();


                    OpenCvSharp.Mat mat = new OpenCvSharp.Mat(@"C:\Users\furka\OneDrive\Masaüstü\GAMES\indir.jpg");       //LoadMode.Color);

                    var mat3 = new Mat<Vec3b>(mat); // cv::Mat_<cv::Vec3b>
                    var indexer = mat3.GetIndexer();

                    for (int y = 0; y < mat.Height; y++)
                    {
                        for (int x = 0; x < mat.Width; x++)
                        {
                            Vec3b color1 = indexer[y, x];
                            byte temp = color1.Item0;
                            color1.Item0 = color1.Item2; // B <- R
                            color1.Item2 = temp;        // R <- B
                            indexer[y, x] = color1;
                        }
                    }





                    Stopwatch stopwatch1 = Stopwatch.StartNew();
                    SharpCV.Mat dst = new SharpCV.Mat(Data); //92 milisecond
                    stopwatch1.Stop();
                    Stopwatch stopwatch11 = Stopwatch.StartNew();
                    var color = SharpCV.Binding.cv2.cvtColor(dst, SharpCV.ColorConversionCodes.COLOR_BayerBG2BGR); // 5 milisecond
                    stopwatch11.Stop();
                    Stopwatch stopwatch111 = Stopwatch.StartNew();
                    SharpCV.Binding.cv2.imwrite("output/" + filename + ".jpg", color); // 21 milisecond
                    stopwatch111.Stop();

                    // Console.WriteLine(stopwatch.ElapsedMilliseconds);

                    Stopwatch stopwatch2 = Stopwatch.StartNew();
                    OpenCvSharp.Mat matis = OpenCvSharp.Cv2.ImRead("output/" + filename + ".jpg"); //16 milsecond
                    //OpenCvSharp.Cv2.ImRead
                    //OpenCvSharp.InputArray Ioarry = new OpenCvSharp.InputArray(dst);
                    stopwatch2.Stop(); 
                    
                    Stopwatch stopwatch3 = Stopwatch.StartNew();
                    videokayit.Write(matis);  //17 milisecend
                    stopwatch3.Stop();
                    

                    stopwatch4.Stop();
                    
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
