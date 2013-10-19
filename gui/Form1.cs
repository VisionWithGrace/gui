using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gui
{
    public partial class Form1 : Form
    {
        Bitmap plainView;
        Bitmap boxedView;
        Bitmap zoomView;
        Rectangle[] Recs;
        int numRecs;
        int selected;
        ComputerVision cv;
        Pen redPen = new Pen(Color.Red, 3);
        Pen yellowPen = new Pen(Color.Yellow, 5);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cv = new ComputerVision();
            plainView = cv.getImage();
            Recs = cv.getBoxes();
            numRecs = cv.getnumBoxes();

            boxedView = new Bitmap(plainView);

            using (var graphics = Graphics.FromImage(boxedView))
            {
                for (int i = 0; i < numRecs; i++)
                {
                    graphics.DrawRectangle(redPen, Recs[i]);
                }
            }
            selected = 0;
            mainDisplay.Image = boxedView;
        }

        private void objectNameText_TextChanged(object sender, EventArgs e)
        {

        }

        private void objectDetectedLabel_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        // Select Button onClick
        private void selectButton_Click(object sender, EventArgs e)
        {

        }

        // Scan Button onClick
        private void scanButton_Click(object sender, EventArgs e)
        {
            Bitmap tempBitmap = new Bitmap(boxedView);

            using (var graphics = Graphics.FromImage(tempBitmap))
            {
                graphics.DrawRectangle(yellowPen, Recs[selected]);
            }

            mainDisplay.Image = tempBitmap;


            zoomView = new Bitmap(Recs[selected].Width, Recs[selected].Height);
            using (var graphics = Graphics.FromImage(zoomView))
            {
                graphics.DrawImage(plainView, new Rectangle(0, 0, zoomView.Width, zoomView.Height), Recs[selected], GraphicsUnit.Pixel);
            }

            closeUpDisplay.Image = zoomView;

            selected++;
            if (selected >= numRecs) selected = 0;
        }

        // Discard Button onClick
        private void discardButton_Click(object sender, EventArgs e)
        {

        }

        // Refresh Button onClick
        private void refreshButton_Click(object sender, EventArgs e)
        {
            //cv = new ComputerVision();
            //plainView = cv.getImage();
            Recs = cv.getBoxes();
            numRecs = cv.getnumBoxes();

            boxedView = new Bitmap(plainView);

            using (var graphics = Graphics.FromImage(boxedView))
            {
                for (int i = 0; i < numRecs; i++)
                {
                    graphics.DrawRectangle(redPen, Recs[i]);
                }
            }
            selected = 0;
            mainDisplay.Image = boxedView;
        }
    }
}