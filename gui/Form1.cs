﻿using System;
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
        Bitmap plainView;
        Bitmap boxedView;
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

        // Initialize Computer Vision and Kinect upon Load
        // Draw mockup GUI if no kinect is found
        private void Form1_Load(object sender, EventArgs e)
        {
            cv = new ComputerVision();

            // Set kinect handler
            if (cv.kinectFlag)
            {
                cv.set_handler(new EventHandler<ColorImageFrameReadyEventArgs>(this.ColorFrameReady));
            }

            // Draw imported image if no kinect is found
            else
            {
                plainView = cv.getImage();
                boxedView = new Bitmap(plainView);
                rectangles = cv.getBoxes();
                drawRectangles();
                mainDisplay.Image = boxedView;
            }
            frame_count = 0;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            show_selected_object();
        }

        // Display selected object in closeUpDisplay
        // DOESN'T WORK WITHOUT KINECT
        private void show_selected_object()
        {
            try
            {
                Bitmap zoomView = new Bitmap(rectangles[selected].Width, rectangles[selected].Height);

                using (var graphics = Graphics.FromImage(zoomView))
                {
                    graphics.DrawImage(view, new Rectangle(0, 0, zoomView.Width, zoomView.Height), rectangles[selected], GraphicsUnit.Pixel);
                }

                this.closeUpDisplay.Image = zoomView;
            }
            catch (Exception e)
            {
                // Do something
            }
        }

        // Rotate and highlight the rectangles, also display the cropped image in the closeupView
        private void scanButton_Click_1(object sender, EventArgs e)
        {
            Bitmap tempBitmap = new Bitmap(boxedView);

            // Highlight rectangle in yellow
            using (var graphics = Graphics.FromImage(tempBitmap))
            {
                graphics.DrawRectangle(yellowPen, rectangles[selected]);
            }

            mainDisplay.Image = tempBitmap;

            // Draw cropped image in closeupView
            Bitmap zoomView = new Bitmap(rectangles[selected].Width, rectangles[selected].Height);
            using (var graphics = Graphics.FromImage(zoomView))
            {
                graphics.DrawImage(plainView, new Rectangle(0, 0, zoomView.Width, zoomView.Height), rectangles[selected], GraphicsUnit.Pixel);
            }
            closeUpDisplay.Image = zoomView;

            selected++;
            if (selected >= cv.num_objects) selected = 0;
        }

        // Re-generate rectangles
        private void refreshButton_Click(object sender, EventArgs e)
        {
            rectangles = cv.getBoxes();
            boxedView = new Bitmap(plainView);
            drawRectangles();
            mainDisplay.Image = boxedView;
        }

        private void drawRectangles()
        {
            using (var graphics = Graphics.FromImage(boxedView))
            {
                for (int i = 0; i < cv.num_objects; i++)
                {
                    graphics.DrawRectangle(redPen, rectangles[i]);
                }
            }
            selected = 0;
        }
    }
}