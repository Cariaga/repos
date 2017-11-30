using Lime.Protocol;

using Newtonsoft.Json;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
using FMUtils.Screenshot;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO.Compression;
using System.Reactive;
using System.Reactive.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Window window;
        public MainWindow()
        {
            InitializeComponent();
            Server();
            Client();
/*
 * 
            window = new Window() //make sure the window is invisible
            {
                Width = SystemParameters.PrimaryScreenWidth,
                Height = SystemParameters.PrimaryScreenHeight,

                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
                Opacity = 0.01F,
                Background = System.Windows.Media.Brushes.Black,
                Foreground = System.Windows.Media.Brushes.Black,
                AllowsTransparency = true,
                ShowInTaskbar = false,
                ShowActivated = true,
                Topmost=true
                
            };
            window.Show();*/
//https://stackoverflow.com/questions/2842667/how-to-create-a-semi-transparent-window-in-wpf-that-allows-mouse-events-to-pass
            CopyScreen();
        }
        private static ComposedScreenshot CopyScreen()
        {
          return  new ComposedScreenshot(new System.Drawing.Rectangle(0, 0, (int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight));
        }
        SimpleTcpClient client = new SimpleTcpClient();
        void Client()
        {
            client.Connect("127.0.0.1", 8910);
            IDisposable subscription = Observable.Interval(TimeSpan.FromMilliseconds(500))
                .Subscribe(x => execute());
        }
        public void execute()
        {
            var ComposedScreenShot = CopyScreen().ComposedScreenshotImage;//bitmap screen shooted
            var bitstring = BitmapToString(ComposedScreenShot);
            var replyMsg = client.WriteLineAndGetReply(bitstring, TimeSpan.FromMilliseconds(500));
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (window!=null)
                {
                    System.Windows.Point pointToWindow = Mouse.GetPosition(window);
                    System.Windows.Point pointToScreen = PointToScreen(pointToWindow);

                    MousePos.Content = "Client Mouse Position :" + pointToScreen.ToString();
                }
             
            });
        }
        public string BitmapToString(Bitmap bitmapx)
        {
          
            using (var ms = new MemoryStream())
            {
                Bitmap resized = new Bitmap(bitmapx, new System.Drawing.Size(bitmapx.Width / 2, bitmapx.Height / 2));
               

                var bitmap = resized;

                //--------set up
                System.Drawing.Imaging.Encoder myEncoder;
                EncoderParameters myEncoderParameters;

                myEncoder = System.Drawing.Imaging.Encoder.Quality;
               
                myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter;
                ImageCodecInfo myImageCodecInfo;
                myImageCodecInfo = GetEncoderInfo("image/jpeg");
                myEncoderParameter = new EncoderParameter(myEncoder, 90L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                //----------save

                bitmap.Save(ms,myImageCodecInfo, myEncoderParameters);
                var SigBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
               Console.WriteLine(ms.Length);
              
                return SigBase64;
            }
        }


        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        public static ImageSource LoadFromBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;
            var image = new BitmapImage();
           
            using (var mem = new MemoryStream(bytes))
            {
         
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
          
                image.StreamSource = mem;
              
                image.EndInit();

            }
            return image;

        }

        int len = 0;
        void Server()
        {
            var server = new SimpleTcpServer().Start(8910);

         //  var listeningIps = server.GetListeningIPs();
           var listeningV4Ips = server.GetListeningIPs().Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

            server.Delimiter = 0x13;
            server.DelimiterDataReceived += async (sender, msg) =>
            {
               
                msg.ReplyLine("ok");
                if (msg.MessageString!="")
                {
                    var x = await msg.AsCompletedTask();
                    byte[] toBytes =  Convert.FromBase64String(msg.MessageString);
                    Application.Current.Dispatcher.Invoke(() => {
                        var image = LoadFromBytes(toBytes);
                        if (image!=null)
                        {
                            bittest.Source = image;
                        }
                        else
                        {
                            Console.WriteLine("Emplty Image");
                        }
                       
                    });
                }
                else
                {
                    Console.WriteLine("empty msg");
                }
            };
        }
       

    }
}
