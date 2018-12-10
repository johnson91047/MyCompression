namespace MyCompression
{
    partial class Main
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SourceFilePathTextBox = new MetroFramework.Controls.MetroTextBox();
            this.DestinationFolderPathTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.FileNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.DeCompressBtn = new MetroFramework.Controls.MetroButton();
            this.CompressBtn = new MetroFramework.Controls.MetroButton();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new MetroFramework.Controls.MetroProgressBar();
            this.UseDPCM = new MetroFramework.Controls.MetroCheckBox();
            this.EntropyLabel = new MetroFramework.Controls.MetroLabel();
            this.EntropyTextbox = new MetroFramework.Controls.MetroLabel();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.IsColorCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.JPEGCompressBtn = new MetroFramework.Controls.MetroButton();
            this.JPEGDecompressBtn = new MetroFramework.Controls.MetroButton();
            this.OpenDialogBtn2 = new MetroFramework.Controls.MetroButton();
            this.OpenFileDialogBtn = new MetroFramework.Controls.MetroButton();
            this.metroTextBox1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.QFTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroTextBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // SourceFilePathTextBox
            // 
            this.SourceFilePathTextBox.Location = new System.Drawing.Point(178, 72);
            this.SourceFilePathTextBox.Name = "SourceFilePathTextBox";
            this.SourceFilePathTextBox.Size = new System.Drawing.Size(419, 23);
            this.SourceFilePathTextBox.TabIndex = 0;
            this.SourceFilePathTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // DestinationFolderPathTextBox
            // 
            this.DestinationFolderPathTextBox.CustomForeColor = true;
            this.DestinationFolderPathTextBox.Enabled = false;
            this.DestinationFolderPathTextBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.DestinationFolderPathTextBox.Location = new System.Drawing.Point(178, 131);
            this.DestinationFolderPathTextBox.Name = "DestinationFolderPathTextBox";
            this.DestinationFolderPathTextBox.Size = new System.Drawing.Size(419, 23);
            this.DestinationFolderPathTextBox.TabIndex = 1;
            this.DestinationFolderPathTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(46, 72);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(88, 25);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "來源檔案";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(46, 131);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(107, 25);
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "目標資料夾";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(46, 194);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(126, 25);
            this.metroLabel3.TabIndex = 6;
            this.metroLabel3.Text = "目標檔案名稱";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.CustomForeColor = true;
            this.FileNameTextBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FileNameTextBox.Location = new System.Drawing.Point(178, 194);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.Size = new System.Drawing.Size(419, 23);
            this.FileNameTextBox.TabIndex = 5;
            this.FileNameTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // DeCompressBtn
            // 
            this.DeCompressBtn.Location = new System.Drawing.Point(414, 104);
            this.DeCompressBtn.Name = "DeCompressBtn";
            this.DeCompressBtn.Size = new System.Drawing.Size(109, 47);
            this.DeCompressBtn.TabIndex = 7;
            this.DeCompressBtn.Text = "解壓縮";
            this.DeCompressBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.DeCompressBtn.Click += new System.EventHandler(this.DeCompressBtn_Click);
            // 
            // CompressBtn
            // 
            this.CompressBtn.Location = new System.Drawing.Point(529, 104);
            this.CompressBtn.Name = "CompressBtn";
            this.CompressBtn.Size = new System.Drawing.Size(109, 47);
            this.CompressBtn.TabIndex = 8;
            this.CompressBtn.Text = "壓縮";
            this.CompressBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.CompressBtn.Click += new System.EventHandler(this.CompressBtn_Click);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(178, 157);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(197, 15);
            this.metroLabel4.TabIndex = 10;
            this.metroLabel4.Text = "(留白表示輸出至來源檔案資料夾)";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.Location = new System.Drawing.Point(178, 220);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(424, 15);
            this.metroLabel5.TabIndex = 11;
            this.metroLabel5.Text = "(壓縮時留白表示與來源檔案名稱相同, 解壓縮時請完整填入檔名及副檔名)";
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(11, 122);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(397, 29);
            this.progressBar.Style = MetroFramework.MetroColorStyle.Green;
            this.progressBar.TabIndex = 12;
            this.progressBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // UseDPCM
            // 
            this.UseDPCM.AutoSize = true;
            this.UseDPCM.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.UseDPCM.Location = new System.Drawing.Point(10, 27);
            this.UseDPCM.Name = "UseDPCM";
            this.UseDPCM.Size = new System.Drawing.Size(92, 19);
            this.UseDPCM.TabIndex = 13;
            this.UseDPCM.Text = "Use DPCM";
            this.UseDPCM.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.UseDPCM.UseVisualStyleBackColor = true;
            // 
            // EntropyLabel
            // 
            this.EntropyLabel.AutoSize = true;
            this.EntropyLabel.Location = new System.Drawing.Point(10, 73);
            this.EntropyLabel.Name = "EntropyLabel";
            this.EntropyLabel.Size = new System.Drawing.Size(85, 19);
            this.EntropyLabel.TabIndex = 14;
            this.EntropyLabel.Text = "File Entropy :";
            this.EntropyLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // EntropyTextbox
            // 
            this.EntropyTextbox.AutoSize = true;
            this.EntropyTextbox.Location = new System.Drawing.Point(90, 61);
            this.EntropyTextbox.Name = "EntropyTextbox";
            this.EntropyTextbox.Size = new System.Drawing.Size(0, 0);
            this.EntropyTextbox.TabIndex = 15;
            this.EntropyTextbox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EntropyTextbox.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Location = new System.Drawing.Point(23, 238);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 1;
            this.metroTabControl1.Size = new System.Drawing.Size(649, 194);
            this.metroTabControl1.TabIndex = 16;
            this.metroTabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.EntropyTextbox);
            this.metroTabPage1.Controls.Add(this.EntropyLabel);
            this.metroTabPage1.Controls.Add(this.UseDPCM);
            this.metroTabPage1.Controls.Add(this.progressBar);
            this.metroTabPage1.Controls.Add(this.CompressBtn);
            this.metroTabPage1.Controls.Add(this.DeCompressBtn);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 36);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(641, 154);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Huffman";
            this.metroTabPage1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.metroLabel6);
            this.metroTabPage2.Controls.Add(this.QFTextBox);
            this.metroTabPage2.Controls.Add(this.IsColorCheckBox);
            this.metroTabPage2.Controls.Add(this.JPEGCompressBtn);
            this.metroTabPage2.Controls.Add(this.JPEGDecompressBtn);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 36);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(641, 154);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "JPEG";
            this.metroTabPage2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            // 
            // IsColorCheckBox
            // 
            this.IsColorCheckBox.AutoSize = true;
            this.IsColorCheckBox.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.IsColorCheckBox.Location = new System.Drawing.Point(20, 72);
            this.IsColorCheckBox.Name = "IsColorCheckBox";
            this.IsColorCheckBox.Size = new System.Drawing.Size(87, 19);
            this.IsColorCheckBox.TabIndex = 22;
            this.IsColorCheckBox.Text = "Is Colored";
            this.IsColorCheckBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.IsColorCheckBox.UseVisualStyleBackColor = true;
            // 
            // JPEGCompressBtn
            // 
            this.JPEGCompressBtn.Location = new System.Drawing.Point(529, 104);
            this.JPEGCompressBtn.Name = "JPEGCompressBtn";
            this.JPEGCompressBtn.Size = new System.Drawing.Size(109, 47);
            this.JPEGCompressBtn.TabIndex = 21;
            this.JPEGCompressBtn.Text = "壓縮";
            this.JPEGCompressBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.JPEGCompressBtn.Click += new System.EventHandler(this.JPEGCompressBtn_Click);
            // 
            // JPEGDecompressBtn
            // 
            this.JPEGDecompressBtn.Location = new System.Drawing.Point(414, 104);
            this.JPEGDecompressBtn.Name = "JPEGDecompressBtn";
            this.JPEGDecompressBtn.Size = new System.Drawing.Size(109, 47);
            this.JPEGDecompressBtn.TabIndex = 20;
            this.JPEGDecompressBtn.Text = "解壓縮";
            this.JPEGDecompressBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // OpenDialogBtn2
            // 
            this.OpenDialogBtn2.Location = new System.Drawing.Point(603, 131);
            this.OpenDialogBtn2.Name = "OpenDialogBtn2";
            this.OpenDialogBtn2.Size = new System.Drawing.Size(31, 23);
            this.OpenDialogBtn2.TabIndex = 16;
            this.OpenDialogBtn2.Text = "...";
            this.OpenDialogBtn2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.OpenDialogBtn2.Click += new System.EventHandler(this.OpenDialogBtn2_Click);
            // 
            // OpenFileDialogBtn
            // 
            this.OpenFileDialogBtn.Location = new System.Drawing.Point(603, 72);
            this.OpenFileDialogBtn.Name = "OpenFileDialogBtn";
            this.OpenFileDialogBtn.Size = new System.Drawing.Size(31, 23);
            this.OpenFileDialogBtn.TabIndex = 15;
            this.OpenFileDialogBtn.Text = "...";
            this.OpenFileDialogBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.OpenFileDialogBtn.Click += new System.EventHandler(this.OpenFileDialogBtn_Click);
            // 
            // metroTextBox1
            // 
            this.metroTextBox1.Owner = null;
            this.metroTextBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(19, 30);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(50, 19);
            this.metroLabel6.TabIndex = 18;
            this.metroLabel6.Text = "Quality";
            this.metroLabel6.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // QFTextBox
            // 
            this.QFTextBox.CustomForeColor = true;
            this.QFTextBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.QFTextBox.Location = new System.Drawing.Point(75, 29);
            this.QFTextBox.Name = "QFTextBox";
            this.QFTextBox.Size = new System.Drawing.Size(44, 23);
            this.QFTextBox.TabIndex = 17;
            this.QFTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 437);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.OpenDialogBtn2);
            this.Controls.Add(this.SourceFilePathTextBox);
            this.Controls.Add(this.OpenFileDialogBtn);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.FileNameTextBox);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.DestinationFolderPathTextBox);
            this.Name = "Main";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroForm.MetroFormShadowType.DropShadow;
            this.Text = "My Compression";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroTextBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox SourceFilePathTextBox;
        private MetroFramework.Controls.MetroTextBox DestinationFolderPathTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroTextBox FileNameTextBox;
        private MetroFramework.Controls.MetroButton DeCompressBtn;
        private MetroFramework.Controls.MetroButton CompressBtn;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MetroFramework.Controls.MetroProgressBar progressBar;
        private MetroFramework.Controls.MetroCheckBox UseDPCM;
        private MetroFramework.Controls.MetroLabel EntropyLabel;
        private MetroFramework.Controls.MetroLabel EntropyTextbox;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroButton OpenDialogBtn2;
        private MetroFramework.Controls.MetroButton OpenFileDialogBtn;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroButton JPEGCompressBtn;
        private MetroFramework.Controls.MetroButton JPEGDecompressBtn;
        private MetroFramework.Components.MetroStyleManager metroTextBox1;
        private MetroFramework.Controls.MetroCheckBox IsColorCheckBox;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroTextBox QFTextBox;
    }
}

