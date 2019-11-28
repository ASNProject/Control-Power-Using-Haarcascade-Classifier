using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.CvEnum;

namespace Project_Metopen_2018_2
{
    public partial class Form1 : Form
    {
        private Capture capture = null;
        private bool captureInProgress = false;
        Image<Bgr, Byte> frame = null;
        Image<Bgr, Byte> frame1 = null;
        private Image<Gray, byte> grayscale;
        private HaarCascade haar;
        private HaarCascade haar1;
        string datasensor; 

        public Form1()
        {
            InitializeComponent();
            try
            {
                capture = new Capture();
                frame = null;
                frame1 = null;
                capture.ImageGrabbed += ProcessFrame;
                

            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }

        private void ReleaseData()
        {
            if (capture != null)
                capture.Dispose();
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            frame = this.capture.RetrieveBgrFrame();
            frame1 = this.capture.RetrieveBgrFrame();
            imageBox1.Image = frame1;
            imageBox1.Image = frame;
            grayscale = frame.Convert<Gray, Byte>();

            haar = new HaarCascade("haarcascade_frontalface_default.xml");
            var faces = grayscale.DetectHaarCascade(haar, 1.5, 5, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(25, 25))[0];
            if (faces.Length == 0)
            {
                //textBox1.Text = faces.Length.ToString();
                timer1.Enabled = true;
                timer1.Start();
                //timer2.Enabled = false;
            }

            haar1 = new HaarCascade("haarcascade_fullbody.xml");
            var faces1 = grayscale.DetectHaarCascade(haar1, 1.5, 5, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(25, 25))[0];
            if (faces1.Length == 0)
            {
                //program here
            }

            if (duration == 0)
            {
                timer1.Stop();
               // timer2.Stop();
                //timer2.Enabled = false;
                timer1.Enabled = false;
                label2.Text = ("POWER OFF");
                serialPort1.Write("9");
                //duration = 60;

            }

            // }
            //if (faces.Length >= 0)
            // {
            //    textBox1.Text = faces.Length.ToString();
            //     serialPort1.Write("1");
            // }


            foreach (var face in faces)
            {
                frame.Draw(face.rect, new Bgr(Color.Green), 3);
                serialPort1.Write("1");
                duration = 60;
                //timer1.Start();
                //timer1.Enabled = false;
                label2.Text = ("POWER ON");


            }

            foreach(var face in faces1)
            {
                frame1.Draw(face.rect, new Bgr(Color.Red), 3);
            }
          
            imageBox1.Image = frame;
            imageBox1.Image = frame1; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (capture != null)
            {
                if (captureInProgress)
                {
                    Application.Idle -= ProcessFrame;
                }
                else
                {
                    Application.Idle += ProcessFrame;
                }
                captureInProgress = !captureInProgress;
            }


           serialPort1.Open(); //hapus yang ini
           //serialPort2.Open(); 
        }
        int duration = 60;
        private void timer1_Tick(object sender, EventArgs e)
        {
            duration--;
            label1.Text = duration.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
           if (capture != null)
            {
             if (captureInProgress)
              {
                    Application.Idle -= ProcessFrame;
                    this.capture.Stop();
                    timer1.Stop();
                    timer1.Enabled = false;
                    serialPort1.Write("8");
                  button1.Text = "Camera Mode";
              }
                else
                {
                    Application.Idle += ProcessFrame;
                    this.capture.Stop();
                    timer1.Stop();
                    timer1.Enabled = false;
                    serialPort1.Write("9");
                    //this.capture.Start();
                    // timer1.Enabled = true;
                    //timer1.Start();
                    button1.Text = "Always ON";
                    

                    

             }
                captureInProgress = !captureInProgress;
           }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
           // ProcessFrame(sender, null);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Arief Setyo Nugroho " +
                "Kebumen 30 April 1996 " +
                "Teknik Elektro " +
                "Universitas Ahmad Dahlan " +
                "2015");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                serialPort1.Write("2");
            }
            else
            {
                serialPort1.Write("3");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
 
                try
                {
                    label7.Text = serialPort2.ReadLine();
                }
                catch
                {
                    MessageBox.Show("Not Connected");
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 tampil = new Form2();
            tampil.Show();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                serialPort1.Write("4");
            }
            else
            {
                serialPort1.Write("5");
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                serialPort1.Write("6");
            }
            else
            {
                serialPort1.Write("7");
            }
        }
    }
    
    
}
