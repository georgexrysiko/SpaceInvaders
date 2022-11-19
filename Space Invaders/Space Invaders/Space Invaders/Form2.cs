using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invaders
{
    public partial class Form2 : Form
    {
        int playerskin, enemyskin = 0;
        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            IFormatter formatter1 = new BinaryFormatter();
            if (File.Exists("skin.ser") && File.Exists("enemyskin.ser"))
            {
                Stream stream = new FileStream("skin.ser", FileMode.Open, FileAccess.Read);
                Skin newskin = (Skin)formatter.Deserialize(stream);
                stream.Close();
                Stream stream1 = new FileStream("enemyskin.ser", FileMode.Open, FileAccess.Read);
                Skin newskin1 = (Skin)formatter1.Deserialize(stream1);
                stream1.Close();
                playerskin = newskin.chooseSkin;
                enemyskin = newskin1.chooseEnemySkin;
            }
            //IFormatter formatter = new BinaryFormatter();         
            
            Form1 game = new Form1(playerskin,enemyskin);
            this.Hide();
            game.ShowDialog();
            this.Show();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 highscore = new Form3();
            this.Hide();
            highscore.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 settings = new Form4();
            this.Hide();
            settings.ShowDialog();
            this.Show();
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Black;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Black;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = Color.White;
        }
    }
}
