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
            this.SourceFilePathTextBox = new MetroFramework.Controls.MetroTextBox();
            this.DestinationFolderPathTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.OpenFileDialogBtn = new MetroFramework.Controls.MetroButton();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.FileNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.DeCompressBtn = new MetroFramework.Controls.MetroButton();
            this.CompressBtn = new MetroFramework.Controls.MetroButton();
            this.OpenDialogBtn2 = new MetroFramework.Controls.MetroButton();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new MetroFramework.Controls.MetroProgressBar();
            this.SuspendLayout();
            // 
            // SourceFilePathTextBox
            // 
            this.SourceFilePathTextBox.Enabled = false;
            this.SourceFilePathTextBox.Location = new System.Drawing.Point(150, 100);
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
            this.DestinationFolderPathTextBox.Location = new System.Drawing.Point(150, 159);
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
            this.metroLabel1.Location = new System.Drawing.Point(18, 100);
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
            this.metroLabel2.Location = new System.Drawing.Point(18, 159);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(107, 25);
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "目標資料夾";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // OpenFileDialogBtn
            // 
            this.OpenFileDialogBtn.Location = new System.Drawing.Point(575, 100);
            this.OpenFileDialogBtn.Name = "OpenFileDialogBtn";
            this.OpenFileDialogBtn.Size = new System.Drawing.Size(31, 23);
            this.OpenFileDialogBtn.TabIndex = 4;
            this.OpenFileDialogBtn.Text = "...";
            this.OpenFileDialogBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.OpenFileDialogBtn.Click += new System.EventHandler(this.OpenFileDialogBtn_Click);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(18, 222);
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
            this.FileNameTextBox.Location = new System.Drawing.Point(150, 222);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.Size = new System.Drawing.Size(419, 23);
            this.FileNameTextBox.TabIndex = 5;
            this.FileNameTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // DeCompressBtn
            // 
            this.DeCompressBtn.Location = new System.Drawing.Point(426, 285);
            this.DeCompressBtn.Name = "DeCompressBtn";
            this.DeCompressBtn.Size = new System.Drawing.Size(109, 47);
            this.DeCompressBtn.TabIndex = 7;
            this.DeCompressBtn.Text = "解壓縮";
            this.DeCompressBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.DeCompressBtn.Click += new System.EventHandler(this.DeCompressBtn_Click);
            // 
            // CompressBtn
            // 
            this.CompressBtn.Location = new System.Drawing.Point(541, 285);
            this.CompressBtn.Name = "CompressBtn";
            this.CompressBtn.Size = new System.Drawing.Size(109, 47);
            this.CompressBtn.TabIndex = 8;
            this.CompressBtn.Text = "壓縮";
            this.CompressBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.CompressBtn.Click += new System.EventHandler(this.CompressBtn_Click);
            // 
            // OpenDialogBtn2
            // 
            this.OpenDialogBtn2.Location = new System.Drawing.Point(575, 159);
            this.OpenDialogBtn2.Name = "OpenDialogBtn2";
            this.OpenDialogBtn2.Size = new System.Drawing.Size(31, 23);
            this.OpenDialogBtn2.TabIndex = 9;
            this.OpenDialogBtn2.Text = "...";
            this.OpenDialogBtn2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.OpenDialogBtn2.Click += new System.EventHandler(this.OpenDialogBtn2_Click);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(150, 185);
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
            this.metroLabel5.Location = new System.Drawing.Point(150, 248);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(424, 15);
            this.metroLabel5.TabIndex = 11;
            this.metroLabel5.Text = "(壓縮時留白表示與來源檔案名稱相同, 解壓縮時請完整填入檔名及副檔名)";
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(23, 303);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(397, 29);
            this.progressBar.Style = MetroFramework.MetroColorStyle.Green;
            this.progressBar.TabIndex = 12;
            this.progressBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 355);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.OpenDialogBtn2);
            this.Controls.Add(this.CompressBtn);
            this.Controls.Add(this.DeCompressBtn);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.FileNameTextBox);
            this.Controls.Add(this.OpenFileDialogBtn);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.DestinationFolderPathTextBox);
            this.Controls.Add(this.SourceFilePathTextBox);
            this.Name = "Main";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroForm.MetroFormShadowType.DropShadow;
            this.Text = "My Compression";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox SourceFilePathTextBox;
        private MetroFramework.Controls.MetroTextBox DestinationFolderPathTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroButton OpenFileDialogBtn;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroTextBox FileNameTextBox;
        private MetroFramework.Controls.MetroButton DeCompressBtn;
        private MetroFramework.Controls.MetroButton CompressBtn;
        private MetroFramework.Controls.MetroButton OpenDialogBtn2;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MetroFramework.Controls.MetroProgressBar progressBar;
    }
}

