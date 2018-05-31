using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GIS.AddIns.Ca.CommonClass;

namespace GIS.AddIns.Ca.CommonDialog
{
    public partial class ImageForm : Form
    {

        private Bitmap bitmap;


        public void UpdateImage(double[] buffer, int width, int height, LandUseClassificationInfo landUseInfo)
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    double type = buffer[pos];
                    if (type < 0)
                    {
                        bitmap.SetPixel(col, row, Color.Transparent); // null value
                        continue;
                    }
                    for (int i = 0; i < landUseInfo.AllTypes.Count; i++)
                    {
                        if (landUseInfo.AllTypes[i].LandUseTypeValue == type)
                        {
                            bitmap.SetPixel(col, row, Color.FromArgb(landUseInfo.AllTypes[i].LandUseTypeColor));
                            break;
                        }
                    }

                }
            }
            this.pictureBox1.Image = bitmap;

        }




        public void UpdateImage(double[] buffer, int width, int height)
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    int type = (int)buffer[pos];                 
                    if (type == 1)
                    {
                        bitmap.SetPixel(col, row, Color.Black);
                    }                  

                }
            }
            this.pictureBox1.Image = bitmap;
            
        }

        public ImageForm(double[] buffer, int width, int height, LandUseClassificationInfo landUseInfo)
        {
            InitializeComponent();

            bitmap = new Bitmap(width, height);
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    double type = buffer[pos];
                    if (type < 0)
                    {
                            bitmap.SetPixel(col, row, Color.Transparent); // null value
                            continue;
                    }
                    for (int i = 0; i < landUseInfo.AllTypes.Count; i++)
                    {
                        if (landUseInfo.AllTypes[i].LandUseTypeValue == type)
                        {
                            bitmap.SetPixel(col, row, Color.FromArgb(landUseInfo.AllTypes[i].LandUseTypeColor));
                            break;
                        }
                    }
                    //if (type < 0)
                    //{
                    //    bitmap.SetPixel(col, row, Color.Transparent);
                    //}
                    //if (type == 1)
                    //{
                    //    bitmap.SetPixel(col, row, Color.Black);
                    //}
                    //if (type == 2)
                    //{
                    //    bitmap.SetPixel(col, row, Color.Blue);
                    //}
                    //if (type == 0)
                    //{
                    //    bitmap.SetPixel(col, row, Color.White);
                    //}

                }
            }

            this.pictureBox1.Image = bitmap;

        }


        public ImageForm(double[] buffer, int width, int height)
        {
            InitializeComponent();

            bitmap = new Bitmap(width, height);

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    int type = (int)buffer[pos];
                    if (type < 0)
                    {
                        bitmap.SetPixel(col, row, Color.Transparent);
                    }
                    if (type == 1)
                    {
                        bitmap.SetPixel(col, row, Color.Black);
                    }
                    if (type == 2)
                    {
                        bitmap.SetPixel(col, row, Color.Blue);
                    }
                    if (type == 0)
                    {
                        bitmap.SetPixel(col, row, Color.White);
                    }
                    
                }
            }

            this.pictureBox1.Image = bitmap;
        }

        public ImageForm()
        {
            InitializeComponent();



        }
    }
}
