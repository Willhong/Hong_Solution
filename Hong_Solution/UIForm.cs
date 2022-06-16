using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hong_Solution
{
    public partial class UIForm : Form
    {
        HongMain MainHong = null;
        public UIForm(HongMain Main)
        {
            InitializeComponent();
            MainHong = Main;
        }
        int i = 0;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            //Button button = new Button
            //{
            //    Text = "ButtonTest",
            //    Name = "Button",
            //    Location = new Point(100, 100)
            //};
            //this.Controls.Add(button);
            ////this.Controls[i].BringToFront();
            //this.Controls[i].Click += nothing;
            

        }

        public void nothing(object sender,EventArgs e)
        {
            MainHong.FormTest.ToolsHong.ShowDialog("adsf", "adfsd");

        }

    }

}
    

