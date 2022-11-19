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
    public partial class Form3 : Form
    {
        List<Label> labels;
        public Form3()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            labels = new List<Label>()
            {
                label2,label3,label4,label5,label6,label7,label8,label9,label10,label11
            };
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            if (File.Exists("highscore.ser")) {
                Stream stream = new FileStream("highscore.ser", FileMode.Open, FileAccess.Read);
                List<int> newscore = (List<int>)formatter.Deserialize(stream);
                foreach (var scoresandlabels in newscore.Zip(labels, Tuple.Create))
                {
                    scoresandlabels.Item2.Text = scoresandlabels.Item1.ToString();
                }
                stream.Close();
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            this.Close();            
        }
    }
}
