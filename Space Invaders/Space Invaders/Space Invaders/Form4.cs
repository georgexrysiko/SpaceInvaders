using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class Form4 : Form
    {
        Skin skin = new Skin();
        SerDeser sd = new SerDeser();
        public Form4()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            skin.chooseSkin = 1;
            sd.Serialize("skin.ser", skin);
            label3.Text = "You chose spaceship 1";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            skin.chooseSkin = 2;
            sd.Serialize("skin.ser", skin);
            label3.Text = "You chose spaceship 2";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            skin.chooseSkin = 3;
            sd.Serialize("skin.ser", skin);
            label3.Text = "You chose spaceship 3";
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            skin.chooseSkin = 4;
            sd.Serialize("skin.ser", skin);
            label3.Text = "You chose spaceship 4";
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            skin.chooseEnemySkin = 1;
            sd.Serialize("enemyskin.ser", skin);
            label5.Text = "You chose enemy spaceship 1";
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            skin.chooseEnemySkin = 2;
            sd.Serialize("enemyskin.ser", skin);
            label5.Text = "You chose enemy spaceship 2";
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            skin.chooseEnemySkin = 3;
            sd.Serialize("enemyskin.ser", skin);
            label5.Text = "You chose enemy spaceship 3";
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }
    }
}
