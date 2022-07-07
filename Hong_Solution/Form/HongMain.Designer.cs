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
            this.btnVisionProForm = new System.Windows.Forms.Button();
            this.btnUIForm = new System.Windows.Forms.Button();
            this.btnImageLoad = new System.Windows.Forms.Button();
            this.btnOpenCV = new System.Windows.Forms.Button();
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
            this.btnTestForm.Click += new System.EventHandler(this.ShowForm);
            // 
            // btnVisionProForm
            // 
            this.btnVisionProForm.Location = new System.Drawing.Point(507, 99);
            this.btnVisionProForm.Name = "btnVisionProForm";
            this.btnVisionProForm.Size = new System.Drawing.Size(281, 81);
            this.btnVisionProForm.TabIndex = 1;
            this.btnVisionProForm.Text = "VisionPro";
            this.btnVisionProForm.UseVisualStyleBackColor = true;
            this.btnVisionProForm.Click += new System.EventHandler(this.ShowForm);
            // 
            // btnUIForm
            // 
            this.btnUIForm.Location = new System.Drawing.Point(507, 273);
            this.btnUIForm.Name = "btnUIForm";
            this.btnUIForm.Size = new System.Drawing.Size(281, 81);
            this.btnUIForm.TabIndex = 1;
            this.btnUIForm.Text = "UI";
            this.btnUIForm.UseVisualStyleBackColor = true;
            this.btnUIForm.Click += new System.EventHandler(this.ShowForm);
            // 
            // btnImageLoad
            // 
            this.btnImageLoad.Location = new System.Drawing.Point(507, 357);
            this.btnImageLoad.Name = "btnImageLoad";
            this.btnImageLoad.Size = new System.Drawing.Size(281, 81);
            this.btnImageLoad.TabIndex = 1;
            this.btnImageLoad.Text = "Image Load";
            this.btnImageLoad.UseVisualStyleBackColor = true;
            this.btnImageLoad.Click += new System.EventHandler(this.btnImageLoad_Click);
            // 
            // btnOpenCV
            // 
            this.btnOpenCV.Location = new System.Drawing.Point(507, 186);
            this.btnOpenCV.Name = "btnOpenCV";
            this.btnOpenCV.Size = new System.Drawing.Size(281, 81);
            this.btnOpenCV.TabIndex = 1;
            this.btnOpenCV.Text = "OpenCV";
            this.btnOpenCV.UseVisualStyleBackColor = true;
            this.btnOpenCV.Click += new System.EventHandler(this.ShowForm);
            // 
            // HongMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.btnImageLoad);
            this.Controls.Add(this.btnOpenCV);
            this.Controls.Add(this.btnUIForm);
            this.Controls.Add(this.btnVisionProForm);
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
        private System.Windows.Forms.Button btnVisionProForm;
        private System.Windows.Forms.Button btnUIForm;
        private System.Windows.Forms.Button btnImageLoad;
        private System.Windows.Forms.Button btnOpenCV;
    }
}

