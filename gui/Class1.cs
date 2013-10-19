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
        KinectSensor sensor = KinectSensor.KinectSensors.First();
        Bitmap kinectView;
        Rectangle[] objects;
        public ComputerVision()
        {
            sensor.Start();
        }

        public void set_handler(EventHandler<ColorImageFrameReadyEventArgs> handler)
        {
            sensor.ColorStream.Enable();
            sensor.ColorFrameReady += handler;
        }

        public Rectangle[] getBoxes()
        {
            Random rand = new Random();
            int num_objects = (rand.Next() % 4) + 2;
            objects = new Rectangle[num_objects];
            int max_width = sensor.ColorStream.FrameWidth;
            int max_height = sensor.ColorStream.FrameHeight;
            int x, y, z, w;
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
    }
}