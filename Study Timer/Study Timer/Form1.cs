using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Study_Timer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        float study_mins;
        float break_mins;
        float study_secs;
        float break_secs;
        //Point formPos;
        //Point timeLabelPos;
        //Point barPos;
        //Point barSize;
        //Point formSize;
        //Point timeLabelSize;

        //SystemColors[] colors;

        private void Form1_Load(object sender, EventArgs e)
        {
            Progressbar.Width= 0;
            Title.Text = "Study";
            
        }
        SoundPlayer anouncementSound = new SoundPlayer(@"C:\Users\khali\Downloads\call-to-attention-123107.wav");
        private void Form1_Resize(object sender, EventArgs e)
        {

        }
        int sessions = 0;
        float lost_time = 0;
        float remainder;
        private void timer1_Tick(object sender, EventArgs e)
        {
            remainder = panel1.Width % 60;
            lost_time += remainder / 60;
            if (lost_time >= 1)
            {
                Progressbar.Width += Convert.ToInt16(panel1.Width / 60) + 1;
                lost_time--;
            }
            else
            {
                Progressbar.Width += Convert.ToInt16(panel1.Width / 60);
            }
            if (Progressbar.Width >= panel1.Width)
            {
                anouncementSound.Play();
                if (Title.Text == "Study")
                {
                    sessions += 1;
                    session_label.Text = "Completed Study Sessions: " + Convert.ToString(sessions);
                    timer1.Interval = Convert.ToInt16(1000*break_secs/60);
                    Title.Text = "Break";
                }
                else
                {
                    Title.Text = "Study";
                    timer1.Interval = Convert.ToInt16(1000*study_secs/60);
                }
                Progressbar.Width = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            bool textbox1 = int.TryParse(richTextBox1.Text, out _);
            bool textbox2 = int.TryParse(richTextBox1.Text, out _);
            if (textbox1 && textbox2 && Convert.ToInt16(richTextBox1.Text) != 0 && Convert.ToInt16(richTextBox2.Text) != 0)
            {
                Progressbar.Width = 0;
                study_mins = Convert.ToInt16(richTextBox1.Text);
                break_mins = Convert.ToInt16(richTextBox2.Text);
                study_secs = 60 * study_mins; // study time in seconds
                break_secs = 60 * break_mins; // break time in seconds

                timer1.Interval = Convert.ToInt16(1000*study_secs/60);
                timer1.Enabled= true;
                infoLabel.Text = "";
            }
            else
            {
                infoLabel.Text = "the textboxes have not been filled correctly";
            }
            
        }
    }
}
