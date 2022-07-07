using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Hong_Solution
{
    public partial class OpenCVForm : Form
    {
        public HongMain MainHong = null;
        public OpenCVClass ClassOpenCV = null;
        public bool bMouseDown;
        public bool bSetRoi =false;
        public bool bMag = true;
        public Bitmap bmpOriginImg = null;
        public Mat matOriginImg = null;
        public Bitmap RectImage = null;
        public Mat RoiImage=null;
        public Mat BinaryImg= null;
        public Point pStartPoint_Region;

        public Size sizePicturebox;

        public Rectangle rectRoi;
        public OpenCVForm(HongMain main)
        {
            InitializeComponent();
            MainHong = main;
            sizePicturebox = pictureBox1.Size;
            ClassOpenCV= new OpenCVClass();
        }

        private void pictureBox1_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                bmpOriginImg = MainHong.FormTest.a.Image.Image;
                pictureBox1.Image = bmpOriginImg;
                matOriginImg=ClassOpenCV.ToMat(bmpOriginImg);
                RoiImage = null;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {

            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pStartPoint_Region.X = bmpOriginImg.Width * e.X / pictureBox1.Width > bmpOriginImg.Width ? bmpOriginImg.Width - 1 : bmpOriginImg.Width * e.X / pictureBox1.Width;
            pStartPoint_Region.Y = bmpOriginImg.Height * e.Y / pictureBox1.Height >= bmpOriginImg.Height ? bmpOriginImg.Height - 1 : bmpOriginImg.Height * e.Y / pictureBox1.Height;
            tbCoorX.Text = pStartPoint_Region.X.ToString();
            tbCoorY.Text = pStartPoint_Region.Y.ToString();
            tbPixel.Text = ((Bitmap)bmpOriginImg).GetPixel((int)pStartPoint_Region.X, (int)pStartPoint_Region.Y).R.ToString();

            bMouseDown = true;

        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bMouseDown&& bSetRoi)
            {

                Point pRectPoint_Region = new Point();
                pRectPoint_Region.X = bmpOriginImg.Width * e.X / pictureBox1.Width > bmpOriginImg.Width ? bmpOriginImg.Width - 1 : bmpOriginImg.Width * e.X / pictureBox1.Width;
                pRectPoint_Region.Y = bmpOriginImg.Height * e.Y / pictureBox1.Height >= bmpOriginImg.Height ? bmpOriginImg.Height - 1 : bmpOriginImg.Height * e.Y / pictureBox1.Height;
                int rectX = MainHong.FormTest.ToolsHong.Smaller<int>(pStartPoint_Region.X, pRectPoint_Region.X);
                int rectY = MainHong.FormTest.ToolsHong.Smaller<int>(pStartPoint_Region.Y, pRectPoint_Region.Y);
                int Width = Math.Abs(pStartPoint_Region.X - pRectPoint_Region.X);
                int Height = Math.Abs(pStartPoint_Region.Y - pRectPoint_Region.Y);

                rectRoi = new Rectangle(rectX, rectY, Width, Height);
                RectImage = new Bitmap(bmpOriginImg);
                using (Graphics g = Graphics.FromImage(RectImage))
                {
                    
                    Pen pen = new Pen(Color.Red);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    pen.Width = 10;
                    g.DrawRectangle(pen, rectRoi);
                }
                pictureBox1.Image = RectImage;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();



            }
        }

        private void btnRoi_Click(object sender, EventArgs e)
        {
            if (!bSetRoi)
            {
                bSetRoi = true;
            }
            else
            {
                bSetRoi = false;
                RoiImage = ClassOpenCV.SubMat(ClassOpenCV.ToMat(bmpOriginImg), rectRoi);

                OpenCvSharp.Cv2.ImShow("asdf", RoiImage);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            bMouseDown = false;
        }
        public void ResizePicturebox(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (bMag)
            {
                string mag = btn.Text.Replace("X ", "");
                pictureBox1.Size = new Size(sizePicturebox.Width * int.Parse(mag), sizePicturebox.Height * int.Parse(mag));
            }
            else
            {
                string mag = btn.Text.Replace("/ ", "");

                pictureBox1.Size = new Size(sizePicturebox.Width / int.Parse(mag), sizePicturebox.Height / int.Parse(mag));

            }

        }

        public void ReverseMag(object sender, EventArgs e)
        {
            
            foreach(Control cont in this.Controls)
            {
                if (cont.GetType() == typeof(Button))
                {
                    if (cont.Text.Contains("X"))
                    {
                        cont.Text=cont.Text.Replace("X", "/");
                        bMag = false;

                    }
                    else if (cont.Text.Contains("/"))
                    {
                        cont.Text=cont.Text.Replace("/", "X");
                        bMag = true;

                    }
                }
            }
        }

        private void btnBinarize_Click(object sender, EventArgs e)
        {
            if (tbBinarizeThreshold.Text != "")
            {
                BinaryImg=ClassOpenCV.Binarize(RoiImage == null ? matOriginImg : RoiImage,int.Parse(tbBinarizeThreshold.Text));

                
            }

        }

        private void btnBlob_Click(object sender, EventArgs e)
        {
            try
            {
                RoiImage = ClassOpenCV.FindBlob(BinaryImg, int.Parse(tbBinarizeThreshold.Text));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
