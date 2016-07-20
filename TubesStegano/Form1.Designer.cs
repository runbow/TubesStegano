namespace TubesStegano
{
    partial class SteganoForm
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
            this.initialImage = new System.Windows.Forms.PictureBox();
            this.steganoImg = new System.Windows.Forms.PictureBox();
            this.labelImage = new System.Windows.Forms.Label();
            this.labelSteganoImg = new System.Windows.Forms.Label();
            this.openImgButton = new System.Windows.Forms.Button();
            this.saveSteganoImgButton = new System.Windows.Forms.Button();
            this.labelHide = new System.Windows.Forms.Label();
            this.fileNameBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.keyBox = new System.Windows.Forms.TextBox();
            this.EncryptButton = new System.Windows.Forms.Button();
            this.DecryptButton = new System.Windows.Forms.Button();
            this.labelAlgoritma = new System.Windows.Forms.Label();
            this.ModeBox = new System.Windows.Forms.ComboBox();
            this.maxBitsLabel = new System.Windows.Forms.Label();
            this.countLabel = new System.Windows.Forms.Label();
            this.openSteganoImgButton = new System.Windows.Forms.Button();
            this.PSNRlabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.initialImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.steganoImg)).BeginInit();
            this.SuspendLayout();
            // 
            // initialImage
            // 
            this.initialImage.BackColor = System.Drawing.SystemColors.Window;
            this.initialImage.Location = new System.Drawing.Point(1, 48);
            this.initialImage.Name = "initialImage";
            this.initialImage.Size = new System.Drawing.Size(442, 317);
            this.initialImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.initialImage.TabIndex = 0;
            this.initialImage.TabStop = false;
            // 
            // steganoImg
            // 
            this.steganoImg.BackColor = System.Drawing.SystemColors.Window;
            this.steganoImg.Location = new System.Drawing.Point(454, 48);
            this.steganoImg.Name = "steganoImg";
            this.steganoImg.Size = new System.Drawing.Size(442, 317);
            this.steganoImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.steganoImg.TabIndex = 1;
            this.steganoImg.TabStop = false;
            // 
            // labelImage
            // 
            this.labelImage.AutoSize = true;
            this.labelImage.Location = new System.Drawing.Point(11, 23);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(63, 13);
            this.labelImage.TabIndex = 2;
            this.labelImage.Text = "Initial Image";
            // 
            // labelSteganoImg
            // 
            this.labelSteganoImg.AutoSize = true;
            this.labelSteganoImg.Location = new System.Drawing.Point(465, 23);
            this.labelSteganoImg.Name = "labelSteganoImg";
            this.labelSteganoImg.Size = new System.Drawing.Size(79, 13);
            this.labelSteganoImg.TabIndex = 3;
            this.labelSteganoImg.Text = "Stegano-Image";
            // 
            // openImgButton
            // 
            this.openImgButton.Location = new System.Drawing.Point(89, 18);
            this.openImgButton.Name = "openImgButton";
            this.openImgButton.Size = new System.Drawing.Size(75, 23);
            this.openImgButton.TabIndex = 4;
            this.openImgButton.Text = "Open Image";
            this.openImgButton.UseVisualStyleBackColor = true;
            this.openImgButton.Click += new System.EventHandler(this.openImgButton_Click);
            // 
            // saveSteganoImgButton
            // 
            this.saveSteganoImgButton.Location = new System.Drawing.Point(556, 18);
            this.saveSteganoImgButton.Name = "saveSteganoImgButton";
            this.saveSteganoImgButton.Size = new System.Drawing.Size(123, 23);
            this.saveSteganoImgButton.TabIndex = 5;
            this.saveSteganoImgButton.Text = "Save Stegano-Image";
            this.saveSteganoImgButton.UseVisualStyleBackColor = true;
            this.saveSteganoImgButton.Click += new System.EventHandler(this.saveSteganoImgButton_Click);
            // 
            // labelHide
            // 
            this.labelHide.AutoSize = true;
            this.labelHide.Location = new System.Drawing.Point(6, 383);
            this.labelHide.Name = "labelHide";
            this.labelHide.Size = new System.Drawing.Size(93, 13);
            this.labelHide.TabIndex = 6;
            this.labelHide.Text = "Select File to Hide";
            // 
            // fileNameBox
            // 
            this.fileNameBox.Location = new System.Drawing.Point(105, 381);
            this.fileNameBox.Name = "fileNameBox";
            this.fileNameBox.Size = new System.Drawing.Size(247, 20);
            this.fileNameBox.TabIndex = 7;
            this.fileNameBox.Click += new System.EventHandler(this.fileNameBox_Click);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(361, 379);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 8;
            this.browseButton.Text = "Browse!";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 416);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Provide Key";
            // 
            // keyBox
            // 
            this.keyBox.Location = new System.Drawing.Point(105, 412);
            this.keyBox.Name = "keyBox";
            this.keyBox.Size = new System.Drawing.Size(247, 20);
            this.keyBox.TabIndex = 10;
            // 
            // EncryptButton
            // 
            this.EncryptButton.Location = new System.Drawing.Point(105, 480);
            this.EncryptButton.Name = "EncryptButton";
            this.EncryptButton.Size = new System.Drawing.Size(118, 23);
            this.EncryptButton.TabIndex = 11;
            this.EncryptButton.Text = "Do Hiding!";
            this.EncryptButton.UseVisualStyleBackColor = true;
            this.EncryptButton.Click += new System.EventHandler(this.EncryptButton_Click);
            // 
            // DecryptButton
            // 
            this.DecryptButton.Location = new System.Drawing.Point(770, 374);
            this.DecryptButton.Name = "DecryptButton";
            this.DecryptButton.Size = new System.Drawing.Size(113, 23);
            this.DecryptButton.TabIndex = 12;
            this.DecryptButton.Text = "Get The File Back!";
            this.DecryptButton.UseVisualStyleBackColor = true;
            this.DecryptButton.Click += new System.EventHandler(this.DecryptButton_Click);
            // 
            // labelAlgoritma
            // 
            this.labelAlgoritma.AutoSize = true;
            this.labelAlgoritma.Location = new System.Drawing.Point(6, 451);
            this.labelAlgoritma.Name = "labelAlgoritma";
            this.labelAlgoritma.Size = new System.Drawing.Size(73, 13);
            this.labelAlgoritma.TabIndex = 13;
            this.labelAlgoritma.Text = "Choose Mode";
            // 
            // ModeBox
            // 
            this.ModeBox.FormattingEnabled = true;
            this.ModeBox.Items.AddRange(new object[] {
            "Standar",
            "4-pixel Differencing",
            "9-pixel Differencing"});
            this.ModeBox.Location = new System.Drawing.Point(105, 446);
            this.ModeBox.Name = "ModeBox";
            this.ModeBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ModeBox.Size = new System.Drawing.Size(151, 21);
            this.ModeBox.TabIndex = 14;
            this.ModeBox.SelectedIndexChanged += new System.EventHandler(this.ModeBox_SelectedIndexChanged);
            // 
            // maxBitsLabel
            // 
            this.maxBitsLabel.AutoSize = true;
            this.maxBitsLabel.Location = new System.Drawing.Point(216, 23);
            this.maxBitsLabel.Name = "maxBitsLabel";
            this.maxBitsLabel.Size = new System.Drawing.Size(111, 13);
            this.maxBitsLabel.TabIndex = 15;
            this.maxBitsLabel.Text = "Maximum bits to hide :";
            this.maxBitsLabel.Visible = false;
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(331, 23);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(0, 13);
            this.countLabel.TabIndex = 16;
            this.countLabel.Visible = false;
            // 
            // openSteganoImgButton
            // 
            this.openSteganoImgButton.Location = new System.Drawing.Point(770, 17);
            this.openSteganoImgButton.Name = "openSteganoImgButton";
            this.openSteganoImgButton.Size = new System.Drawing.Size(123, 23);
            this.openSteganoImgButton.TabIndex = 17;
            this.openSteganoImgButton.Text = "Open Stegano-Image";
            this.openSteganoImgButton.UseVisualStyleBackColor = true;
            this.openSteganoImgButton.Click += new System.EventHandler(this.openSteganoImgButton_Click);
            // 
            // PSNRlabel
            // 
            this.PSNRlabel.AutoSize = true;
            this.PSNRlabel.Location = new System.Drawing.Point(531, 383);
            this.PSNRlabel.Name = "PSNRlabel";
            this.PSNRlabel.Size = new System.Drawing.Size(43, 13);
            this.PSNRlabel.TabIndex = 18;
            this.PSNRlabel.Text = "PSNR :";
            // 
            // SteganoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 515);
            this.Controls.Add(this.PSNRlabel);
            this.Controls.Add(this.openSteganoImgButton);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.maxBitsLabel);
            this.Controls.Add(this.ModeBox);
            this.Controls.Add(this.labelAlgoritma);
            this.Controls.Add(this.DecryptButton);
            this.Controls.Add(this.EncryptButton);
            this.Controls.Add(this.keyBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.fileNameBox);
            this.Controls.Add(this.labelHide);
            this.Controls.Add(this.saveSteganoImgButton);
            this.Controls.Add(this.openImgButton);
            this.Controls.Add(this.labelSteganoImg);
            this.Controls.Add(this.labelImage);
            this.Controls.Add(this.steganoImg);
            this.Controls.Add(this.initialImage);
            this.Name = "SteganoForm";
            this.Text = "Tubes Steganografi";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.initialImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.steganoImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox initialImage;
        private System.Windows.Forms.PictureBox steganoImg;
        private System.Windows.Forms.Label labelImage;
        private System.Windows.Forms.Label labelSteganoImg;
        private System.Windows.Forms.Button openImgButton;
        private System.Windows.Forms.Button saveSteganoImgButton;
        private System.Windows.Forms.Label labelHide;
        private System.Windows.Forms.TextBox fileNameBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox keyBox;
        private System.Windows.Forms.Button EncryptButton;
        private System.Windows.Forms.Button DecryptButton;
        private System.Windows.Forms.Label labelAlgoritma;
        private System.Windows.Forms.ComboBox ModeBox;
        private System.Windows.Forms.Label maxBitsLabel;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.Button openSteganoImgButton;
        private System.Windows.Forms.Label PSNRlabel;

    }
}

