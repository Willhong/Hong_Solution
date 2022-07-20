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
        bool isEditMode = false;
        public UIForm(HongMain Main)
        {
            InitializeComponent();
            MainHong = Main;
            
                
        }
        int i = 0;
        

        public void nothing(object sender,EventArgs e)
        {
            HongTools.ShowDialog("adsf", "adfsd");

        }

        public void AddPictureBox(string ControlName,Point _Location, Size _Size)
        {
            this.Controls.Add(new PictureBox()
            {
                Name = ControlName,
                SizeMode = PictureBoxSizeMode.Normal,
                Size = _Size,
                Location = _Location,
                Visible = true
            });
        }
        public void AddTextbox(string ControlName, Point _Location, string Text)
        {
            this.Controls.Add(new TextBox()
            {
                Name = ControlName,
                Location= _Location,
                Visible= true
            });
        }
        public void AddButton(string ControlName, Point _Location, string _Text, Size _Size)
        {
            foreach(Control control in this.Controls)
            {
                if(control.Name == ControlName)
                {
                    return;
                }
            }
            this.Controls.Add(new Button()
            {
                Name = ControlName,
                Location = _Location,
                Text=_Text,
                Size= _Size,
                Visible = true,                
            });

            

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            if (comboBox1.SelectedItem.ToString() != "")
            {
                if(comboBox1.SelectedItem.ToString() == "TextBox")
                {
                    AddTextbox("", new Point(100, 100), "TextBoxTest");
                }
                else if (comboBox1.SelectedItem.ToString() == "Button")
                {
                    AddButton("", new Point(100, 100), "ButtonTest", new Size(100, 50));
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                isEditMode = false;
            }
            else
            {
                isEditMode = true;
            }
        }

        public void MoveControl(object sender, EventArgs e)
        {

            if (isEditMode)
            {
                ((Control)sender).Location = Cursor.Position;
            }
        }
        public void StopMoveControl(object sender, EventArgs e)
        {

            if (isEditMode)
            {
                isEditMode = false;
            }
        }
    }

}
    

