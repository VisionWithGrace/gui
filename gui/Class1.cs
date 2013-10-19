using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace gui
{
    class ComputerVision
    {
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();

        Bitmap kinectView;
        Rectangle[] Recs;
        int numRecs;
        public ComputerVision()
        {
        }
        public Bitmap getImage()
        {
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Title = "Select a picture file";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                kinectView = new Bitmap(openFileDialog1.FileName);
            }
            return kinectView;
        }
        public Rectangle[] getBoxes()
        {
            Random tempRand = new Random(new Random().Next());
            numRecs = (tempRand.Next() % 4) + 2;
            Recs = new Rectangle[numRecs];
            int width = kinectView.Width;
            int height = kinectView.Height;
            int x, y, z, w;
            for (int i = 0; i < numRecs; i++)
            {
                x = tempRand.Next() % (width - 160);
                y = tempRand.Next() % (height - 160);
                z = (tempRand.Next() % 120) + 40;
                w = (tempRand.Next() % 120) + 40;
                Recs[i] = new Rectangle(x, y, z, w);
            }
            return Recs;
        }
        public int getnumBoxes() { return numRecs; }
    }
}