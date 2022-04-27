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
        Tools.HongTools ToolsHong = new Tools.HongTools();
        ParamConfig a = new ParamConfig();
        
        public HongMain()
        {
            InitializeComponent();
            a.NameValue= new NameValue();
            a.Age=new Age();
            a.Arraytest=new Arraytest();

            a.Arraytest.arraytest= new string[10];
            for(int i = 0; i < 10; i++)
            {
                a.Arraytest.arraytest[i]=i.ToString();
            }
        }

        public void LoadJson()
        {
            string Path = HongDef.CONFIG_FOLDER + "\\Config.json";
            a.NameValue = ToolsHong.LoadJson<NameValue>(Path, "NameValue");
            a.Age = ToolsHong.LoadJson<Age>(Path, "Age");
            a.Arraytest = ToolsHong.LoadJson<Arraytest>(Path, "Arraytest");
        }
        public void SaveJson()
        {
            a.NameValue.Name = tbName.Text;
            a.NameValue.Value = tbValue.Text;
            a.Age.ageisnaee = tbAge.Text;
            ToolsHong.SaveJson(a);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadJson();
            tbAge.Text = a.Age.ageisnaee.ToString();
            tbName.Text =   a.NameValue.Name.ToString();
            tbValue.Text = a.NameValue.Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveJson();
        }
    }
}
