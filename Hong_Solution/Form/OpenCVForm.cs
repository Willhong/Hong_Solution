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
        public Bitmap RectImage = null;
        public Mat matOriginImg = null;
        public Mat RoiImage=null;
        public Mat BinaryImg= null;
        public Mat ResultImg= null;
        public Point pStartPoint_Region;

        public Size sizePicturebox;

        public Rectangle rectRoi;

        Timer RoiTimer = new Timer();
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
                if (MainHong != null) {
                    bmpOriginImg = MainHong.FormTest.a.Image.Image;
                    pictureBox1.Image = bmpOriginImg;
                    matOriginImg = ClassOpenCV.ToMat(bmpOriginImg);
                    RoiImage = null;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
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
                int rectX = HongTools.Smaller<int>(pStartPoint_Region.X, pRectPoint_Region.X);
                int rectY = HongTools.Smaller<int>(pStartPoint_Region.Y, pRectPoint_Region.Y);
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
                RoiTimer.Interval = 500;
                RoiTimer.Tick += Tim_Tick;
                RoiTimer.Start();
            }
            else
            {
                RoiTimer.Stop();
                btnRoi.BackColor = SystemColors.ButtonFace;
                bSetRoi = false;
                RoiImage = ClassOpenCV.SubMat(ClassOpenCV.ToMat(bmpOriginImg), rectRoi);

                OpenCvSharp.Cv2.ImShow("asdf", RoiImage);
            }
        }

        private void Tim_Tick(object sender, EventArgs e)
        {
            if (btnRoi.BackColor == SystemColors.ButtonFace)
            {
                btnRoi.BackColor = Color.Red;
            }
            else
            {
                btnRoi.BackColor = SystemColors.ButtonFace;
            }
            throw new NotImplementedException();
        }

        public void SetButtonColor()
        {
            
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
                ResultImg = ClassOpenCV.FindBlob(BinaryImg, int.Parse(tbBinarizeThreshold.Text));
            }
            catch (Exception ex)
            {

            }
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            form.Size = new Size(1920, 1080);
            form.AutoScroll = true;
            List<Mat> images = new List<Mat>();
            images.Add(matOriginImg);
            images.Add(RoiImage);
            images.Add(BinaryImg);
            images.Add(ResultImg);
            AddPicturebox(form, images);
            new Task(new Action(() => form.ShowDialog())).Start();
        }
        private void AddPicturebox(Form form,List<Mat> images)
        {
            int index = 0;
            foreach(Mat img in images)
            {
                if (img != null)
                {
                    form.Controls.Add(new Button()
                    {
                        Text = "Image " + index,
                        Name = "btn" + index,
                        Location = new Point(100 * index, 0)
                    });
                    form.Controls.Add(new PictureBox()
                    {
                        Name = "PictureBox" + index,
                        Image = ClassOpenCV.ToBitmap(img),
                        SizeMode = PictureBoxSizeMode.Normal,
                        Size = new Size(500, 500),
                        Location = new Point(form.Controls["btn" + index].Location.X + 400 * index, form.Controls["btn" + index].Location.Y + 100),
                        Visible = false
                    });
                    ((Button)form.Controls["btn" + index]).Click += new EventHandler((object sender, EventArgs e) => 
                    form.Controls["Picturebox" + ((Button)sender).Name.Replace("btn", "")].Visible = form.Controls["Picturebox" + ((Button)sender).Name.Replace("btn", "")].Visible ? false : true);
                    index++;
                }
            }
        }
        
    }
}
