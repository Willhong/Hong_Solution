using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hong_Solution
{
    public partial class HongMain : Form
    {
        public TestForm FormTest=null;
        public VisionProForm FormVisionPro=null;
        public UIForm FormUI = null;
        public Byte[] ImageArray;
        public HongMain()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(1920, 1080);

            Initialize();
        }
        public void Initialize()
        {
            FormTest = new TestForm(this);
            FormTest.TopLevel = false;
            FormVisionPro = new VisionProForm(this);
            FormVisionPro.TopLevel = false;
            FormUI = new UIForm(this);
            FormUI.TopLevel = false;

            pnlForm.Size=FormVisionPro.Size;
            AddFormToPanel(FormTest);
            AddFormToPanel(FormVisionPro);
            AddFormToPanel(FormUI);

            ResizePanel();
            MoveButtons();
        }
        public void MoveButtons()
        {
            foreach(Control c in this.Controls)
            {
                if (c.Name.Contains("btn"))
                {
                    c.Location=new Point(this.Width-400,c.Location.Y);
                }
            }
        }
        public void ResizePanel()
        {
            double pnlWidth=0;
            double pnlHeight=0;
            foreach (Control a in pnlForm.Controls)
            {
                pnlWidth = a.Size.Width > pnlWidth ? a.Size.Width : pnlWidth;
                pnlHeight = a.Size.Height > pnlHeight ? a.Size.Height : pnlHeight;
            }
        }
        public void AddFormToPanel(Control control)
        {
            pnlForm.Controls.Add(control);
        }
        private void btnTestForm_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < pnlForm.Controls.Count; i++)
            {
                pnlForm.Controls[i].Visible = false;
            }
            pnlForm.Controls["TestForm"].Visible = true;
        }

        private void btnTmpForm_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pnlForm.Controls.Count; i++)
            {
                pnlForm.Controls[i].Visible = false;
            }
            pnlForm.Controls["VisionProForm"].Visible = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormTest.ToolsHong.ShowDialog("", "");
        }

        private void btnUIForm_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pnlForm.Controls.Count; i++)
            {
                pnlForm.Controls[i].Visible = false;
            }
            pnlForm.Controls["UIForm"].Visible = true;
        }

        private void btnImageLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            MemoryStream stream = new MemoryStream();

            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                FormTest.a.Image.ImagePath = ofd.FileName;

                
                using (FileStream fileStream = new FileStream(ofd.FileName, FileMode.Open))
                {
                    FormTest.a.Image.Image = new Bitmap(fileStream);
                }
                FormTest.ToolsHong.LoadPictureboxImage(FormUI.pictureBox1,ofd.FileName);
            }
            FormUI.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            //FormUI.pictureBox1.Image = FormTest.a.Image.Image;
            //FormUI.pictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            //ImageArray=stream.ToArray();
            //for(int i = 0; i < ImageArray; i++)
        }
    }
}
