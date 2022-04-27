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
        TestForm testForm=null;
        
        public HongMain()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(1920, 1080);

            Initialize();
        }
        public void Initialize()
        {
            testForm = new TestForm(this);
            testForm.TopLevel = false;
            AddFormToPanel(testForm);

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
            pnlForm.Controls["testForm"].Visible = true;
        }

        private void btnTmpForm_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pnlForm.Controls.Count; i++)
            {
                pnlForm.Controls[i].Visible = false;
            } 
        }
    }
}
