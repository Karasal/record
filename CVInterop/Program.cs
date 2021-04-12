using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;

namespace CVInterop
{
    class Program
    {
        private VideoCapture _capture = null;
        private bool _captureInProgress;
        private Mat _frame;
        private Mat _grayFrame;
        private Mat _smallGrayFrame;
        private Mat _cannyFrame;
        
        static void Main(string[] args)
        {

            Program new_program = new Program();

            /*
            Console.WriteLine("Hello World!");
            CvInvoke.Init();
            String win1 = "Test Window";

            CvInvoke.NamedWindow(win1);

            Mat img = new Mat(200, 400, DepthType.Cv8U, 3);
            img.SetTo(new Bgr(255, 0, 0).MCvScalar);

            CvInvoke.PutText(
                img,
                "hello Wolrd",
                new System.Drawing.Point(10, 80),
                FontFace.HersheyComplex,
                1.0,
                new Bgr(0, 255, 0).MCvScalar);

            CvInvoke.Imshow(win1, img);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();
            

            CvInvoke.UseOpenCL = false;
            try
            {
                new_program._capture = new VideoCapture();

            }
            catch (NullReferenceException excpt)
            {
                Console.WriteLine(excpt.Message);
            }

            new_program._frame = new Mat();
            new_program._grayFrame = new Mat();

            new_program._capture.Retrieve(new_program._frame, 0);
            */

            int imgwidth = 1440;
            int imheight = 1080;

            string Cam1folderPath = @"C:\Users\Zorro\Desktop\shared_vm\VMTestSpin\VMTestSpin\bin\x64\Debug\rawoutput\21015253";

            string[] Cam1fileNames = System.IO.Directory.GetFiles(Cam1folderPath);

            double fps = 200.0;

            System.Drawing.Size draws = new System.Drawing.Size(imgwidth, imheight);

            var videoWriter = new Emgu.CV.VideoWriter(@"output.mp4", 200, draws, true);

            var videoWriterSec = new Emgu.CV.VideoWriter(@"second.avi", Emgu.CV.VideoWriter.Fourcc('M', 'J', 'P', 'G'), 200.0, draws, true);

            // public VideoWriter(string fileName, int compressionCode, double fps, Size size, bool isColor);


            // public VideoWriter(string fileName, int fps, Size size, bool isColor);

            

            foreach (string imgPath in Cam1fileNames) // 14 ms
            {
                

                byte[] readTExt = System.IO.File.ReadAllBytes(imgPath); // 0 ms
                
                // byte[,] resizedByte = new byte[1080, 1440];

                byte[] tempByte = new byte[1440 * 1080]; // 0 ms

                Array.Copy(readTExt, tempByte, 1440 * 1080); // 1 ms


                /*
                for (int j = 0; j < tempByte.Length; j++) // 5 ms
                {
                    tempByte[j] = readTExt[j];
                }
                */


                /*
                for (int i = 0; i < imheight; i++)
                {
                    for (int j = 0; j < imgwidth; j++)
                    {
                        resizedByte[i, j] = readTExt[i * 1440 + j];
                    }
                }
                */

                // Emgu.CV.Mat newMat = new Emgu.CV.Mat(readTExt);

                //Image<Gray, byte> depthImage = new Image<Gray, byte>(1440*1080);

                //Image < Emgu.CV.Structure., Byte> captureImage = new Image<Bgr, byte>(imgPath);

                // Image<Bgr, byte> resizedImage = captureImage.Resize(imgwidth, imheight, Emgu.CV.CvEnum.Inter.Area);

                // Emgu.CV.Util.VectorOfByte vectorOfByte = new Emgu.CV.Util.VectorOfByte(readTExt);

                // Image<Emgu.CV.Structure., byte> img = new Image<Emgu.C, byte>(resizedByte);
                
                Emgu.CV.Image<Gray, byte> depthImage = new Image<Gray, byte>(1440, 1080); // 2 ms

                
                depthImage.Bytes = tempByte; // 4 ms
                Emgu.CV.Mat colorodimage = new Emgu.CV.Mat(imheight, imgwidth, DepthType.Cv16U, 1); // 0 ms
                // Emgu.CV.Mat img = new Emgu.CV.Mat(1080, 1440, DepthType.Cv8S, 1);

                
                CvInvoke.CvtColor(depthImage, colorodimage, ColorConversion.BayerBg2Bgr); // 3 ms


                // byte[] tempdata = new byte[1555200];

                // Random rnd = new Random();

                // rnd.NextBytes(tempdata);

                // Emgu.CV.Mat resizedMat = new Emgu.CV.Mat(imheight, imgwidth, DepthType.Cv16U, 1);

                // CvInvoke.Imdecode(tempdata, ImreadModes.Grayscale, resizedMat);

                // System.Drawing.Size imSize = new System.Drawing.Size(imgwidth, imheight);

                // CvInvoke.Resize((IInputArray)vectorOfByte.GetInputArray(), resizedMat, imSize); 

                // Emgu.CV.Image<> image = new Emgu.CV.Image(imgwidth, imheight);
                // CvInvoke.Imdecode(readTExt, Emgu.CV.CvEnum.ImreadModes.Grayscale, img);
                System.Diagnostics.Stopwatch stopwatch1 = System.Diagnostics.Stopwatch.StartNew();
                
                videoWriterSec.Write(colorodimage); // 11 ms
                stopwatch1.Stop();
                Console.WriteLine(stopwatch1.ElapsedMilliseconds.ToString());
            }

            
            videoWriter.Dispose();

        }
    }
}
