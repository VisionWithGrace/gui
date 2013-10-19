using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Kinect;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace gui
{
    public partial class Form1 : Form
    {
        Bitmap view;
        Rectangle[] rectangles;
        int selected;
        int frame_count;
        ComputerVision cv;
        Pen redPen = new Pen(Color.Red, 3);
        Pen yellowPen = new Pen(Color.Yellow, 5);

        public Form1()
        {
            InitializeComponent();
        }

        public void ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            frame_count++;

            ColorImageFrame colorFrame = e.OpenColorImageFrame();
            if (colorFrame == null)
            {
                return;
            }
            view = ColorImageFrameToBitmap(colorFrame);
            Bitmap view_with_boxes = new Bitmap(view);

            // Add boxes
            using (var graphics = Graphics.FromImage(view_with_boxes))
            {
                for (int i = 0; i < rectangles.Length; i++)
                {
                    if (i == selected)
                        graphics.DrawRectangle(yellowPen, rectangles[i]);
                    else
                        graphics.DrawRectangle(redPen, rectangles[i]);
                }
            }

            if (frame_count > 50)
            {
                selected = (selected + 1) % rectangles.Length;
                frame_count = 0;
            }

            this.objectDetectedLabel.Text = Convert.ToString(frame_count);

            this.mainDisplay.Image = view_with_boxes;
        }

        private static Bitmap ColorImageFrameToBitmap(ColorImageFrame colorFrame)
        {
            byte[] pixelBuffer = new byte[colorFrame.PixelDataLength];
            colorFrame.CopyPixelDataTo(pixelBuffer);

            Bitmap bitmapFrame = new Bitmap(colorFrame.Width, colorFrame.Height,
                PixelFormat.Format32bppRgb);


            BitmapData bitmapData = bitmapFrame.LockBits(new Rectangle(0, 0,
                                             colorFrame.Width, colorFrame.Height),
            ImageLockMode.WriteOnly, bitmapFrame.PixelFormat);

            IntPtr intPointer = bitmapData.Scan0;
            Marshal.Copy(pixelBuffer, 0, intPointer, colorFrame.PixelDataLength);

            bitmapFrame.UnlockBits(bitmapData);
            return bitmapFrame;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cv = new ComputerVision();
            cv.set_handler(new EventHandler<ColorImageFrameReadyEventArgs>(this.ColorFrameReady));
            rectangles = cv.getBoxes();
            selected = 0;
            frame_count = 0;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            rectangles = cv.getBoxes();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            show_selected_object();
        }

        private void show_selected_object()

        {
            Bitmap zoomView = new Bitmap(rectangles[selected].Width, rectangles[selected].Height);
            using (var graphics = Graphics.FromImage(zoomView))
            {
                graphics.DrawImage(view, new Rectangle(0, 0, zoomView.Width, zoomView.Height), rectangles[selected], GraphicsUnit.Pixel);
            }

            this.closeUpDisplay.Image = zoomView;
        }

    }
}