using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.CNLSearch;
using Cognex.VisionPro.ColorMatch;
using Cognex.VisionPro.Dimensioning;
using Cognex.VisionPro.ID;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.OCRMax;
using Cognex.VisionPro.OCVMax;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ToolBlock;
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
    public partial class VisionProForm : Form
    {
        public HongMain MainHong = null;
        public VisionProClass ClassVisionPro = new VisionProClass();
        public CogToolBlock Toolblock = null;
        public CogToolBlock newToolblock = new CogToolBlock();
        public string sCurrentToolBlock="";
        public string sCurrentTool = "";
        public bool isUIset = false;
        public VisionProForm(HongMain main)
        {
            InitializeComponent();
            MainHong = main;

        }
        public void SetUI()
        {
            if (!isUIset)
            {
                //pnlVProControl.Controls.Clear();
                ClassVisionPro.AddToolControl(pnlVProControl);
                isUIset = true;
            }
        }

        public void LoadToolblock()
        {
            string FilePath = MainHong.FormTest.ToolsHong.OpenFile("vpp");
            object obj = new object[2]
            {
                FilePath,0
            };
            if (FilePath == null)
            {
                return;
            }
            ClassVisionPro.LoadToolblock(obj);
            //ClassVisionPro.LoadToolblock(Toolblock);
        }

        public void LoadToListbox()
        {
            foreach (string ToolName in ClassVisionPro.LoadToolList())
            {
                lbToolList.Items.Add(ToolName);

            }
        }

        private void btnLoadToolblock_Click(object sender, EventArgs e)
        {
            lbToolList.Items.Clear();
            sCurrentToolBlock = "";
            LoadToolblock();

            LoadToListbox();

        }

        private void VisionProForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                SetUI();
            }
        }

        private void lbToolList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ICogTool Tool;
            string ToolName="";
            try
            {
                if (lbToolList.SelectedItem != null)
                {
                    ToolName = lbToolList.SelectedItem.ToString();
                }
                else
                {
                    return;
                }
            }
            catch(Exception ex)
            {
                ToolName = "";
                return;
            }
            if (ToolName == "...")
            {
                lbToolList.Items.Clear();
                LoadToListbox();
                sCurrentToolBlock = "";
            }
            else if (ToolName.Contains("ToolBlock"))
            {
                Tool = ClassVisionPro.tmpToolBlock.Tools[ToolName];
                lbToolList.Items.Clear();
                lbToolList.Items.Add("...");
                foreach (ICogTool SubTool in ((CogToolBlock)Tool).Tools)
                {
                    lbToolList.Items.Add(SubTool.Name);
                }
                sCurrentToolBlock = ToolName;
            }
            else
            {
                if (sCurrentToolBlock != "")
                {
                    Tool = ((CogToolBlock)ClassVisionPro.tmpToolBlock.Tools[sCurrentToolBlock]).Tools[ToolName];

                }
                else
                {
                    Tool = ClassVisionPro.tmpToolBlock.Tools[ToolName];
                }
                foreach (Control control in pnlVProControl.Controls)
                {
                    SetVproControl(control, Tool);
                }
            }
        }
        public void SetVproControl(Control control,ICogTool Tool)
        {
            dynamic cont = control;
            string ControlName = control.GetType().ToString().Split(new string[] { "." }, StringSplitOptions.None).Last();
            string[] splitword = new string[3]
            {
                "EditV2","Edit","V2"
            };
            foreach(string word in splitword)
            {
                if (ControlName.Contains(word))
                {
                    ControlName = ControlName.Split(new string[] { word }, StringSplitOptions.None)[0];
                    break;
                }
            }
            
            string sToolName = Tool.GetType().ToString().Split(new string[] { "." }, StringSplitOptions.None).Last();
            if (sToolName.Contains("Tool"))
            {
                sToolName = sToolName.Split(new string[] { "Tool" }, StringSplitOptions.None)[0];

            }
            if (ControlName == sToolName)
            {
                dynamic ttool = Tool;
                cont.Subject = ttool;
                cont.Visible = true;
            }
            else
            {
                control.Visible = false;
            }
        }
        
        private void btnScript_Click(object sender, EventArgs e)
        {
            CogToolBlock ToolBlock;
            if (sCurrentToolBlock != "")
            {
                ToolBlock = ((CogToolBlock)ClassVisionPro.tmpToolBlock.Tools[sCurrentToolBlock]);

            }
            else
            {
                ToolBlock = ClassVisionPro.tmpToolBlock;
            }
            ClassVisionPro.OpenToolScript(ToolBlock);
        }

        private void btnSetImage_Click(object sender, EventArgs e)
        {
            foreach (Control control in pnlVProControl.Controls)
            {
                if (control.Visible)
                {
                    dynamic a = control;
                    try
                    {
                        try
                        {
                            a.Subject.InputImage = new CogImage8Grey(MainHong.FormTest.a.Image.Image);
                        }
                        catch (Exception ex)
                        {

                        }                           
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
                    {
                        if (ex.Message.Contains("24"))
                        {
                            MessageBox.Show("24비트이미지를 입력하시오");
                        }
                        else
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void lbToolList_DoubleClick(object sender, EventArgs e)
        {
            foreach (Control control in pnlVProControl.Controls)
            {
                if (control.Visible)
                {
                    dynamic a = control;
                    try
                    {
                        newToolblock.Tools.Add(a.Subject);
                        lbNewList.Items.Add(a.Subject.Name);

                    }
                    catch (Exception ex)
                    {

                    }
                    
                    
                }
            }
        }

        private void btnNewScript_Click(object sender, EventArgs e)
        {
            //CogToolBlock ToolBlock;
            //if (sCurrentToolBlock != "")
            //{
            //    ToolBlock = ((CogToolBlock)ClassVisionPro.tmpToolBlock.Tools[sCurrentToolBlock]);

            //}
            //else
            //{
            //    ToolBlock = ClassVisionPro.tmpToolBlock;
            //}
            ClassVisionPro.OpenToolScript(newToolblock);

        }
    }
}
