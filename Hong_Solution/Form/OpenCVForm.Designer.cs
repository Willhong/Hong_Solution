namespace Hong_Solution
{
    partial class OpenCVForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRoi = new System.Windows.Forms.Button();
            this.tbCoorX = new System.Windows.Forms.TextBox();
            this.tbCoorY = new System.Windows.Forms.TextBox();
            this.btnMag1 = new System.Windows.Forms.Button();
            this.btnMag3 = new System.Windows.Forms.Button();
            this.btnMag2 = new System.Windows.Forms.Button();
            this.btnMag4 = new System.Windows.Forms.Button();
            this.tbPixel = new System.Windows.Forms.TextBox();
            this.btnBinarize = new System.Windows.Forms.Button();
            this.tbBinarizeThreshold = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBlob = new System.Windows.Forms.Button();
            this.tbBlobThreshold = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnReverse = new System.Windows.Forms.Button();
            this.btnResult = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 142);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(405, 358);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.VisibleChanged += new System.EventHandler(this.pictureBox1_VisibleChanged);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // btnRoi
            // 
            this.btnRoi.Location = new System.Drawing.Point(12, 66);
            this.btnRoi.Name = "btnRoi";
            this.btnRoi.Size = new System.Drawing.Size(183, 70);
            this.btnRoi.TabIndex = 1;
            this.btnRoi.Text = "Set ROI";
            this.btnRoi.UseVisualStyleBackColor = true;
            this.btnRoi.Click += new System.EventHandler(this.btnRoi_Click);
            // 
            // tbCoorX
            // 
            this.tbCoorX.Location = new System.Drawing.Point(55, 12);
            this.tbCoorX.Name = "tbCoorX";
            this.tbCoorX.Size = new System.Drawing.Size(66, 21);
            this.tbCoorX.TabIndex = 2;
            // 
            // tbCoorY
            // 
            this.tbCoorY.Location = new System.Drawing.Point(55, 36);
            this.tbCoorY.Name = "tbCoorY";
            this.tbCoorY.Size = new System.Drawing.Size(66, 21);
            this.tbCoorY.TabIndex = 2;
            // 
            // btnMag1
            // 
            this.btnMag1.Location = new System.Drawing.Point(201, 66);
            this.btnMag1.Name = "btnMag1";
            this.btnMag1.Size = new System.Drawing.Size(82, 33);
            this.btnMag1.TabIndex = 3;
            this.btnMag1.Text = "X 1";
            this.btnMag1.UseVisualStyleBackColor = true;
            this.btnMag1.Click += new System.EventHandler(this.ResizePicturebox);
            // 
            // btnMag3
            // 
            this.btnMag3.Location = new System.Drawing.Point(201, 103);
            this.btnMag3.Name = "btnMag3";
            this.btnMag3.Size = new System.Drawing.Size(82, 33);
            this.btnMag3.TabIndex = 3;
            this.btnMag3.Text = "X 3";
            this.btnMag3.UseVisualStyleBackColor = true;
            this.btnMag3.Click += new System.EventHandler(this.ResizePicturebox);
            // 
            // btnMag2
            // 
            this.btnMag2.Location = new System.Drawing.Point(289, 66);
            this.btnMag2.Name = "btnMag2";
            this.btnMag2.Size = new System.Drawing.Size(82, 33);
            this.btnMag2.TabIndex = 3;
            this.btnMag2.Text = "X 2";
            this.btnMag2.UseVisualStyleBackColor = true;
            this.btnMag2.Click += new System.EventHandler(this.ResizePicturebox);
            // 
            // btnMag4
            // 
            this.btnMag4.Location = new System.Drawing.Point(289, 103);
            this.btnMag4.Name = "btnMag4";
            this.btnMag4.Size = new System.Drawing.Size(82, 33);
            this.btnMag4.TabIndex = 3;
            this.btnMag4.Text = "X 4";
            this.btnMag4.UseVisualStyleBackColor = true;
            this.btnMag4.Click += new System.EventHandler(this.ResizePicturebox);
            // 
            // tbPixel
            // 
            this.tbPixel.Location = new System.Drawing.Point(188, 21);
            this.tbPixel.Name = "tbPixel";
            this.tbPixel.Size = new System.Drawing.Size(66, 21);
            this.tbPixel.TabIndex = 2;
            // 
            // btnBinarize
            // 
            this.btnBinarize.Location = new System.Drawing.Point(377, 66);
            this.btnBinarize.Name = "btnBinarize";
            this.btnBinarize.Size = new System.Drawing.Size(183, 70);
            this.btnBinarize.TabIndex = 1;
            this.btnBinarize.Text = "이진화";
            this.btnBinarize.UseVisualStyleBackColor = true;
            this.btnBinarize.Click += new System.EventHandler(this.btnBinarize_Click);
            // 
            // tbBinarizeThreshold
            // 
            this.tbBinarizeThreshold.Location = new System.Drawing.Point(422, 36);
            this.tbBinarizeThreshold.Name = "tbBinarizeThreshold";
            this.tbBinarizeThreshold.Size = new System.Drawing.Size(66, 21);
            this.tbBinarizeThreshold.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "X좌표";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y좌표";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(141, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "픽셀값";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(375, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "임계값";
            // 
            // btnBlob
            // 
            this.btnBlob.Location = new System.Drawing.Point(566, 66);
            this.btnBlob.Name = "btnBlob";
            this.btnBlob.Size = new System.Drawing.Size(183, 70);
            this.btnBlob.TabIndex = 1;
            this.btnBlob.Text = "블랍";
            this.btnBlob.UseVisualStyleBackColor = true;
            this.btnBlob.Click += new System.EventHandler(this.btnBlob_Click);
            // 
            // tbBlobThreshold
            // 
            this.tbBlobThreshold.Location = new System.Drawing.Point(611, 36);
            this.tbBlobThreshold.Name = "tbBlobThreshold";
            this.tbBlobThreshold.Size = new System.Drawing.Size(66, 21);
            this.tbBlobThreshold.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(564, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "임계값";
            // 
            // btnReverse
            // 
            this.btnReverse.Location = new System.Drawing.Point(289, 29);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(82, 33);
            this.btnReverse.TabIndex = 3;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.ReverseMag);
            // 
            // btnResult
            // 
            this.btnResult.Location = new System.Drawing.Point(755, 66);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(183, 70);
            this.btnResult.TabIndex = 1;
            this.btnResult.Text = "결과";
            this.btnResult.UseVisualStyleBackColor = true;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // OpenCVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1365, 1000);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMag4);
            this.Controls.Add(this.btnMag3);
            this.Controls.Add(this.btnMag2);
            this.Controls.Add(this.tbBlobThreshold);
            this.Controls.Add(this.btnReverse);
            this.Controls.Add(this.btnMag1);
            this.Controls.Add(this.tbBinarizeThreshold);
            this.Controls.Add(this.tbPixel);
            this.Controls.Add(this.tbCoorY);
            this.Controls.Add(this.tbCoorX);
            this.Controls.Add(this.btnResult);
            this.Controls.Add(this.btnBlob);
            this.Controls.Add(this.btnBinarize);
            this.Controls.Add(this.btnRoi);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OpenCVForm";
            this.Text = "OpenCVForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRoi;
        private System.Windows.Forms.TextBox tbCoorX;
        private System.Windows.Forms.TextBox tbCoorY;
        private System.Windows.Forms.Button btnMag1;
        private System.Windows.Forms.Button btnMag3;
        private System.Windows.Forms.Button btnMag2;
        private System.Windows.Forms.Button btnMag4;
        private System.Windows.Forms.TextBox tbPixel;
        private System.Windows.Forms.Button btnBinarize;
        private System.Windows.Forms.TextBox tbBinarizeThreshold;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBlob;
        private System.Windows.Forms.TextBox tbBlobThreshold;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.Button btnResult;
    }
}