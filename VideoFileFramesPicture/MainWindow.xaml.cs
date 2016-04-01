using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading;

//using AForge;
//using AForge.Video;
using AForge.Video.FFMPEG;
using System.Drawing.Imaging;

namespace VideoFileFramesPicture
{
    public partial class MainWindow : Window
    {
        static string videoFilePath = "";
        static int pixelsPerFrame = 10;
        static Thread ProcessingThread;
        static string programName = "Video Spectrometer";

        public MainWindow()
        {
            InitializeComponent();
        }


        void SaveFramesPicture()
        {
            if(videoFilePath == "")
            {
                MessageBox.Show("Сначала выберите видеофайл.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            VideoFileReader reader = new VideoFileReader();

            try
            {
                reader.Open(videoFilePath);
            }
            catch
            {
                MessageBox.Show("Данный формат файла не поддерживается.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                
                return;
            }

            var framesCount = reader.FrameCount;

            if(framesCount == 0)
            {
                MessageBox.Show("Не удалось получить общее количество кадров в видео файле.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                
                return;                
            }

            SetUI(false);


            Dispatcher.BeginInvoke(new Action(delegate
            {
                Progress_ProgressBar.Maximum = framesCount;
            }));

            var optimalSize = GetOptimalWidthAndHeighFromPixelsCount((int)framesCount * pixelsPerFrame);

            Bitmap bmp = new Bitmap(optimalSize.Width, optimalSize.Height);

            
            
            var currFrame = 1;
            var currPixel = 0;



            System.Drawing.Color meanColor = System.Drawing.Color.Black;
            Bitmap videoFrame;


            for(var currX = 0; currX < bmp.Width; currX++)
            {
                for (var currY = 0; currY < bmp.Height; currY++)
                {
                    if (currPixel % pixelsPerFrame == 0)
                        if (currFrame <= framesCount)
                        {
                            Dispatcher.BeginInvoke(new Action(delegate
                            {
                                Progress_ProgressBar.Value = currFrame;

                                var percent = (int)(currFrame / (framesCount / 100));

                                Title = $"[{percent}%] {programName}";

                                Progress_Label.Content = $"{percent}%";
                            }));

                            videoFrame = reader.ReadVideoFrame();

                            if (videoFrame != null)
                            {
                                meanColor = CalculateAverageColor(videoFrame);

                                videoFrame.Dispose();
                            }
                            else
                                meanColor = System.Drawing.Color.Black;

                            currFrame++;                            
                        }
                        else
                            meanColor = System.Drawing.Color.Black;

                    bmp.SetPixel(currX, currY, meanColor);
                    currPixel = (currPixel + 1) % pixelsPerFrame;
                }
            }

            var resultingImageFilename = AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.GetFileNameWithoutExtension(videoFilePath) + $"_{pixelsPerFrame}ppf.bmp";

            bmp.Save(resultingImageFilename);

            MessageBox.Show($"Сохранено в \"{System.IO.Path.GetFileNameWithoutExtension(videoFilePath) + ".bmp\""}", "Готово!", MessageBoxButton.OK, MessageBoxImage.Information);

            //}
            reader.Close();

            SetUI(true);

            Dispatcher.BeginInvoke(new Action(delegate
            {
                Title = $"{programName}";

                Progress_ProgressBar.Value = 0;
                Progress_Label.Content = $"";
            }));
        }



        void SetUI(bool isEnabled)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                Process_Button.IsEnabled = isEnabled;
                Process_Button.Content = isEnabled? "Сохранить \"спектр\"" : "Идёт обработка...";

                PixelsPerFrame_IntegerUpDown.IsEnabled = isEnabled;
                ChooseFile_Button.IsEnabled = isEnabled;
            }));
        }


        private void ChooseFile_Button_Click(object sender, RoutedEventArgs e)
        {
            var file_opening_dialog = new Microsoft.Win32.OpenFileDialog
            {
                CheckFileExists = true
            };

            if (file_opening_dialog.ShowDialog(this) != true) return;


            videoFilePath = file_opening_dialog.FileName;
            var fileName = file_opening_dialog.SafeFileName;


            FileName_Label.Content = fileName;
        }

        private void Process_Button_Click(object sender, RoutedEventArgs e)
        {
            pixelsPerFrame = (int)PixelsPerFrame_IntegerUpDown.Value;

            ProcessingThread = new Thread(new ThreadStart(SaveFramesPicture));
            ProcessingThread.IsBackground = true;
            ProcessingThread.Start();
        }


        static System.Drawing.Color CalculateAverageColor(Bitmap bm)
        {
            int width = bm.Width;
            int height = bm.Height;
            int red = 0;
            int green = 0;
            int blue = 0;

            int[] totals = new int[] { 0, 0, 0 };
            int bppModifier = bm.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ? 3 : 4;

            BitmapData srcData = bm.LockBits(new System.Drawing.Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, bm.PixelFormat);
            int stride = srcData.Stride;
            IntPtr Scan0 = srcData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int idx = (y * stride) + x * bppModifier;
                        red = p[idx + 2];
                        green = p[idx + 1];
                        blue = p[idx];

                        totals[2] += red;
                        totals[1] += green;
                        totals[0] += blue;
                    }
                }
            }

            int count = width * height;
            int avgR = (int)(totals[2] / count);
            int avgG = (int)(totals[1] / count);
            int avgB = (int)(totals[0] / count);

            return System.Drawing.Color.FromArgb(avgR, avgG, avgB);
        }


        static System.Drawing.Size GetOptimalWidthAndHeighFromPixelsCount(int pixelsCount)
        {
            var size = new System.Drawing.Size(3, 1);

            while(size.Width * size.Height < pixelsCount)
            {
                size.Width += 3;
                size.Height += 1;
            }

            while ((size.Width - 1) * size.Height >= pixelsCount)
                size.Width -= 1;

            return size;
        }
    }
}
