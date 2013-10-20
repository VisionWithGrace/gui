using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Kinect;

namespace gui
{
    class ComputerVision
    {
        KinectSensor sensor;
        Bitmap kinectView;
        Rectangle[] objects;
        public bool kinectFlag;
        public int num_objects;
        public ComputerVision()
        {
            // Start kinect sensor
            try
            {
                sensor = KinectSensor.KinectSensors.First();
                sensor.Start();
                kinectFlag = true;
            }
            // Open file dialog if no kinect is found
            catch
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "openFileDialog1";
                openFileDialog1.Title = "Select a picture file";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    kinectView = new Bitmap(openFileDialog1.FileName);
                }
                kinectFlag = false;
            }
        }

        public void set_handler(EventHandler<ColorImageFrameReadyEventArgs> handler)
        {
            if (!kinectFlag)
                return;
            sensor.ColorStream.Enable();
            sensor.ColorFrameReady += handler;
        }

        // Draw Rectangles at random locations
        // Need to receive x,y coordinates from Kinect
        public Rectangle[] getBoxes()
        {
            Random rand = new Random();
            num_objects = (rand.Next() % 4) + 2;
            objects = new Rectangle[num_objects];
            int x, y, z, w;
            int max_width;
            int max_height;

            // Get sensor frame size if kinect is present
            if (kinectFlag)
            {
                max_width = sensor.ColorStream.FrameWidth;
                max_height = sensor.ColorStream.FrameHeight;
            }
            // Set frame size to be size of mockup image
            else
            {
                max_width = kinectView.Width;
                max_height = kinectView.Height;
            }
            for (int i = 0; i < num_objects; i++)
            {
                x = rand.Next() % (max_width - 160);
                y = rand.Next() % (max_height - 160);
                z = (rand.Next() % 120) + 40;
                w = (rand.Next() % 120) + 40;
                objects[i] = new Rectangle(x, y, z, w);
            }
            return objects;
        }

        public Bitmap getImage()
        {
            return kinectView;
        }
    }
}