namespace Hong_Solution
{
    partial class VisionProForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlVProControl = new System.Windows.Forms.Panel();
            this.lbToolList = new System.Windows.Forms.ListBox();
            this.btnLoadToolblock = new System.Windows.Forms.Button();
            this.btnScript = new System.Windows.Forms.Button();
            this.btnSetImage = new System.Windows.Forms.Button();
            this.lbNewList = new System.Windows.Forms.ListBox();
            this.btnNewScript = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlVProControl
            // 
            this.pnlVProControl.Location = new System.Drawing.Point(0, 0);
            this.pnlVProControl.Name = "pnlVProControl";
            this.pnlVProControl.Size = new System.Drawing.Size(697, 581);
            this.pnlVProControl.TabIndex = 0;
            // 
            // lbToolList
            // 
            this.lbToolList.FormattingEnabled = true;
            this.lbToolList.ItemHeight = 12;
            this.lbToolList.Location = new System.Drawing.Point(703, 0);
            this.lbToolList.Name = "lbToolList";
            this.lbToolList.Size = new System.Drawing.Size(246, 400);
            this.lbToolList.TabIndex = 1;
            this.lbToolList.SelectedIndexChanged += new System.EventHandler(this.lbToolList_SelectedIndexChanged);
            this.lbToolList.DoubleClick += new System.EventHandler(this.lbToolList_DoubleClick);
            // 
            // btnLoadToolblock
            // 
            this.btnLoadToolblock.Location = new System.Drawing.Point(703, 406);
            this.btnLoadToolblock.Name = "btnLoadToolblock";
            this.btnLoadToolblock.Size = new System.Drawing.Size(120, 46);
            this.btnLoadToolblock.TabIndex = 2;
            this.btnLoadToolblock.Text = "Load";
            this.btnLoadToolblock.UseVisualStyleBackColor = true;
            this.btnLoadToolblock.Click += new System.EventHandler(this.btnLoadToolblock_Click);
            // 
            // btnScript
            // 
            this.btnScript.Location = new System.Drawing.Point(829, 406);
            this.btnScript.Name = "btnScript";
            this.btnScript.Size = new System.Drawing.Size(120, 46);
            this.btnScript.TabIndex = 2;
            this.btnScript.Text = "Script";
            this.btnScript.UseVisualStyleBackColor = true;
            this.btnScript.Click += new System.EventHandler(this.btnScript_Click);
            // 
            // btnSetImage
            // 
            this.btnSetImage.Location = new System.Drawing.Point(703, 458);
            this.btnSetImage.Name = "btnSetImage";
            this.btnSetImage.Size = new System.Drawing.Size(120, 46);
            this.btnSetImage.TabIndex = 2;
            this.btnSetImage.Text = "Set Image";
            this.btnSetImage.UseVisualStyleBackColor = true;
            this.btnSetImage.Click += new System.EventHandler(this.btnSetImage_Click);
            // 
            // lbNewList
            // 
            this.lbNewList.FormattingEnabled = true;
            this.lbNewList.ItemHeight = 12;
            this.lbNewList.Location = new System.Drawing.Point(955, 0);
            this.lbNewList.Name = "lbNewList";
            this.lbNewList.Size = new System.Drawing.Size(246, 400);
            this.lbNewList.TabIndex = 1;
            this.lbNewList.SelectedIndexChanged += new System.EventHandler(this.lbToolList_SelectedIndexChanged);
            this.lbNewList.DoubleClick += new System.EventHandler(this.lbToolList_DoubleClick);
            // 
            // btnNewScript
            // 
            this.btnNewScript.Location = new System.Drawing.Point(1081, 406);
            this.btnNewScript.Name = "btnNewScript";
            this.btnNewScript.Size = new System.Drawing.Size(120, 46);
            this.btnNewScript.TabIndex = 2;
            this.btnNewScript.Text = "Script";
            this.btnNewScript.UseVisualStyleBackColor = true;
            this.btnNewScript.Click += new System.EventHandler(this.btnNewScript_Click);
            // 
            // VisionProForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 593);
            this.Controls.Add(this.btnNewScript);
            this.Controls.Add(this.btnScript);
            this.Controls.Add(this.btnSetImage);
            this.Controls.Add(this.btnLoadToolblock);
            this.Controls.Add(this.lbNewList);
            this.Controls.Add(this.lbToolList);
            this.Controls.Add(this.pnlVProControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VisionProForm";
            this.Text = "VisionProForm";
            this.VisibleChanged += new System.EventHandler(this.VisionProForm_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlVProControl;
        private System.Windows.Forms.ListBox lbToolList;
        private System.Windows.Forms.Button btnLoadToolblock;
        private System.Windows.Forms.Button btnScript;
        private System.Windows.Forms.Button btnSetImage;
        private System.Windows.Forms.ListBox lbNewList;
        private System.Windows.Forms.Button btnNewScript;
    }
}