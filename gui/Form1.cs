using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gui
{
    public partial class Form1 : Form
    {
        Bitmap pic = new Bitmap(550, 300);

        Pen redPen = new Pen(Color.Red, 3);
        Pen yellowPen = new Pen(Color.Yellow, 5);
        Rectangle Rec = new Rectangle(30, 40, 50, 60);
        Rectangle[] Recs;
        int counter;

        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(pic);
            using (var graphics = Graphics.FromImage(temp))
            {
                graphics.DrawRectangle(yellowPen, Recs[counter]);
            }
            pictureBox1.Image = temp;
            counter++;
            if (counter >= 3) counter = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var newGraphics = Graphics.FromImage(pic))
            {
                newGraphics.FillRectangle(new SolidBrush(Color.Orange), 0, 0, 550, 300);
            }
            pictureBox1.Image = pic;
            Recs = new Rectangle[3];
            Recs[0] = new Rectangle(30, 40, 50, 60);
            Recs[1] = new Rectangle(200, 80, 100, 60);
            Recs[2] = new Rectangle(400, 40, 50, 30);
            using (var graphics = Graphics.FromImage(pic))
            {
                for (int i = 0; i < 3; i++)
                {
                    graphics.DrawRectangle(redPen, Recs[i]);
                }
            }
            pictureBox1.Image = pic;
            counter = 0;
        }
    }
}
