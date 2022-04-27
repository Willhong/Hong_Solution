namespace Hong_Solution
{
    partial class HongMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlForm = new System.Windows.Forms.Panel();
            this.btnTestForm = new System.Windows.Forms.Button();
            this.btnTmpForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlForm
            // 
            this.pnlForm.Location = new System.Drawing.Point(0, 0);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(501, 438);
            this.pnlForm.TabIndex = 0;
            // 
            // btnTestForm
            // 
            this.btnTestForm.Location = new System.Drawing.Point(507, 12);
            this.btnTestForm.Name = "btnTestForm";
            this.btnTestForm.Size = new System.Drawing.Size(281, 81);
            this.btnTestForm.TabIndex = 1;
            this.btnTestForm.Text = "TestForm";
            this.btnTestForm.UseVisualStyleBackColor = true;
            this.btnTestForm.Click += new System.EventHandler(this.btnTestForm_Click);
            // 
            // btnTmpForm
            // 
            this.btnTmpForm.Location = new System.Drawing.Point(507, 99);
            this.btnTmpForm.Name = "btnTmpForm";
            this.btnTmpForm.Size = new System.Drawing.Size(281, 81);
            this.btnTmpForm.TabIndex = 1;
            this.btnTmpForm.Text = "TMP";
            this.btnTmpForm.UseVisualStyleBackColor = true;
            this.btnTmpForm.Click += new System.EventHandler(this.btnTmpForm_Click);
            // 
            // HongMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTmpForm);
            this.Controls.Add(this.btnTestForm);
            this.Controls.Add(this.pnlForm);
            this.Name = "HongMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Button btnTestForm;
        private System.Windows.Forms.Button btnTmpForm;
    }
}

