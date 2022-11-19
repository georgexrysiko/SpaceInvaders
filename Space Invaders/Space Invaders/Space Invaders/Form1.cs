using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invaders
{
    
    public partial class Form1 : Form
    {
        ScoreBoard scoreboard;

        List<PictureBox> goodbullets;
        List<PictureBox> enemybullets;
        Random r = new Random();
        int score = 0;
        int minutes = 2;
        int seconds = 0;
        int hp = 500;
        bool goLeft, goRight, goUp, goDown;
        int speed = 8;

        public Form1(int goodskin, int enemyskin)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            goodbullets = new List<PictureBox>();
            enemybullets = new List<PictureBox>();
            scoreboard = new ScoreBoard();

            if (goodskin == 1)
            {
                pictureBox1.Image = Space_Invaders.Properties.Resources.good;
            }
            else if (goodskin == 2)
            {
                pictureBox1.Image = Space_Invaders.Properties.Resources.spaceship2;
            }
            else if (goodskin == 3)
            {
                pictureBox1.Image = Space_Invaders.Properties.Resources.spaceship3;
            }
            else if(goodskin == 4)
            {
                pictureBox1.Image = Space_Invaders.Properties.Resources.spaceship4;
            }

            if (enemyskin == 1)
            {
                pictureBox2.Image = Space_Invaders.Properties.Resources.enemy;
            }
            else if(enemyskin == 2)
            {
                pictureBox2.Image = Space_Invaders.Properties.Resources.spaceship5;
            }
            else if (enemyskin == 3)
            {
                pictureBox2.Image = Space_Invaders.Properties.Resources.spaceship6;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            else if (e.KeyCode.ToString().Equals("Space"))
            {
                createPlayerBullet(pictureBox1.Location.X);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            else if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            else if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void maintimerEvent(object sender, EventArgs e)
        {
            if (goLeft == true && pictureBox1.Left > 0)
            {
                pictureBox1.Left -= speed;
            }
            if (goRight == true && pictureBox1.Right < 652)
            {
                pictureBox1.Left += speed;
            }
            if (goUp == true && pictureBox1.Top > 162)
            {
                pictureBox1.Top -= speed;
            }
            if (goDown == true && pictureBox1.Bottom < 855)
            {
                pictureBox1.Top += speed;
            }
        }
        
        
        private void createPlayerBullet(int startX)
        {
            PictureBox p = new PictureBox();
            p.Image = Space_Invaders.Properties.Resources.goodbullet;            
            p.Location = new Point(startX + 35, pictureBox1.Location.Y - 50);
            p.Size = new Size(30, 60);
            p.SizeMode = PictureBoxSizeMode.StretchImage;
            p.BackColor = Color.Transparent;
            Controls.Add(p);
            goodbullets.Add(p);
        }

        private void createEnemyBullet(int startX)
        {
            PictureBox p = new PictureBox();
            p.Image = Space_Invaders.Properties.Resources.enemybullet;
            p.Location = new Point(startX + 45, pictureBox2.Location.Y + 100);
            p.Size = new Size(30, 60);
            p.SizeMode = PictureBoxSizeMode.StretchImage;
            p.BackColor = Color.Transparent;
            Controls.Add(p);
            enemybullets.Add(p);
        }        
        
        
        private void timer1_Tick(object sender, EventArgs e)
        {               
            foreach (PictureBox p in goodbullets)
            {
                p.Location = new Point(p.Location.X, p.Location.Y - 10);
                if (p.Bounds.IntersectsWith(pictureBox2.Bounds))
                {
                      this.Controls.Remove(p);
                      
                      score = score + 5;
                      label1.Text = "SCORE: " + score.ToString();                      
                }            
            }
            
            foreach (PictureBox p in enemybullets)
            {
                p.Location = new Point(p.Location.X, p.Location.Y + 10);
                
                if (p.Bounds.IntersectsWith(pictureBox1.Bounds))
                {   
                    this.Controls.Remove(p);
                    hp = hp - 2;
                    healthpoints.Text = "HP: " + hp.ToString();
                }                
            }            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox2.Location = new Point(r.Next(Width - pictureBox2.Width), pictureBox2.Location.Y);            
            createEnemyBullet(pictureBox2.Location.X - 40);
            createEnemyBullet(pictureBox2.Location.X + 47);            
        }

        

        private void countdown_Tick(object sender, EventArgs e)
        {              
            if (seconds == -1)
            {   
                minutes--;
                seconds = 59;
            }
            label2.Text = "TIME: " + minutes.ToString() + ":" + seconds--.ToString();

            if (hp <= 0 && (minutes != 0 || seconds != 0))
            {
                countdown.Stop();
                timer1.Stop();
                timer2.Stop();
                maintimer.Stop();
                label4.Text = "YOU DIED";
                label4.ForeColor = Color.Red;

            }
            else if (minutes == 0 && seconds == -1 && hp != 0)
            {
                countdown.Stop();
                timer1.Stop();
                timer2.Stop();
                maintimer.Stop();

                label3.TextAlign = ContentAlignment.TopCenter;
                label3.Text = "CONGRATULATIONS";

                scoreboard.highscore.Add(score);                

                IFormatter formatter = new BinaryFormatter();                
                if (File.Exists("highscore.ser"))
                {
                    Stream stream = new FileStream("highscore.ser", FileMode.Open, FileAccess.Read);
                    List<int> newscore = (List<int>)formatter.Deserialize(stream);
                    foreach (int score in newscore)
                    {
                        scoreboard.highscore.Add(score);
                    }
                    scoreboard.highscore.Sort((x, y) => y.CompareTo(x));
                    stream.Close();
                    
                }
                IFormatter formatter1 = new BinaryFormatter();
                Stream stream1 = new FileStream("highscore.ser", FileMode.OpenOrCreate, FileAccess.Write);
                formatter1.Serialize(stream1, scoreboard.highscore);
                stream1.Close();
            }
        }

        private void healthpoints_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            healthpoints.Text = "HP: " + hp.ToString();
        }
    }
}
