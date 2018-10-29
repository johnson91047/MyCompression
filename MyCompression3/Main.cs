using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace MyCompression
{
    public partial class Main : MetroForm
    {
        private string SourceFilePath
        {
            get => Environment.ExpandEnvironmentVariables(SourceFilePathTextBox.Text);
            set => SourceFilePathTextBox.Text = value;
        }

        private string DestinationFolderPath
        {
            get => DestinationFolderPathTextBox.Text;
            set => DestinationFolderPathTextBox.Text = value;
        }

        private string FileName
        {
            get => FileNameTextBox.Text;
            set => FileNameTextBox.Text = value;
        }

        private string EntropyText
        {
            get => EntropyTextbox.Text;
            set => EntropyTextbox.Text = value;
        }

        private const string MyExtension = ".mycom";

        private string CompressDestinationFilePath
        {
            get {
                string folder = DestinationFolderPath;
                string fileName = FileName;

                if (string.IsNullOrEmpty(folder))
                {
                    folder = Path.GetDirectoryName(SourceFilePath);
                }

                if(string.IsNullOrEmpty(fileName))
                {
                    fileName = Path.GetFileNameWithoutExtension(SourceFilePath);
                }

                return Path.Combine(folder, fileName + MyExtension);
            }
        }

        private string DeCompressDestinationFilePath
        {
            get
            {
                string folder = DestinationFolderPath;
                string fileName = FileName;

                if (string.IsNullOrEmpty(folder))
                {
                    folder = Path.GetDirectoryName(SourceFilePath);
                }

                return Path.Combine(folder, fileName);
            }
        }

        private readonly BackgroundWorker _compressProcess;
        private readonly BackgroundWorker _deCompressProcess;

        //delegate for other process 
        private delegate void SetProgressBarValue(int max);
        private delegate void SetButtonState(bool state);
        private delegate void SetEntropyText(string text);
        private readonly SetProgressBarValue _setValueDelegate;
        private readonly SetButtonState _setButtonStateDelegate;
        private readonly SetEntropyText _setEntropyTextDelegate;

        public Main()
        {
            // setup background worker
            _compressProcess = new BackgroundWorker();
            _compressProcess.DoWork += (handler, sender) => { Compress(); };
            _compressProcess.RunWorkerCompleted += ( sender, handler ) => { ProgressComplete(); };
            _compressProcess.ProgressChanged += ProgressChanged;
            _compressProcess.WorkerReportsProgress = true;

            _deCompressProcess = new BackgroundWorker();
            _deCompressProcess.DoWork += (handler, sender) => { DeCompress(); };
            _deCompressProcess.RunWorkerCompleted += ( sender, handler ) => { ProgressComplete(); };
            _deCompressProcess.ProgressChanged += ProgressChanged;
            _deCompressProcess.WorkerReportsProgress = true;

            InitializeComponent();

            _setValueDelegate += SetProgressbar;
            _setButtonStateDelegate += ButtonState;
            _setEntropyTextDelegate += SetEntropyTextValue;
        }

        #region UI Events
        private void OpenFileDialogBtn_Click(object sender, EventArgs e)
        {
            SourceFilePath = OpenFile();
        }

        private void OpenDialogBtn2_Click(object sender, EventArgs e)
        {
            DestinationFolderPath = OpenDirectory();
        }



        private void DeCompressBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists(SourceFilePath))
            {
                MessageBox.Show("來源檔案不存在", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(Path.GetExtension(SourceFilePath) != MyExtension)
            {
                MessageBox.Show("來源檔案不是壓縮檔", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(FileName))
            {
                MessageBox.Show("請輸入檔案名稱", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(Path.GetExtension(FileName)))
            {
                MessageBox.Show("請指定檔案名稱的副檔名", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(DeCompressDestinationFilePath))
            {
                DialogResult result = MessageBox.Show("目的地檔案已存在\n要覆蓋檔案嗎?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                switch (result)
                {
                    case DialogResult.Yes:
                        _deCompressProcess.RunWorkerAsync();
                        break;
                    case DialogResult.No:
                        return;
                }
            }
            else
            {
                _deCompressProcess.RunWorkerAsync();
            }
        }

        private void CompressBtn_Click(object sender, EventArgs e)
        {
            if(!File.Exists(SourceFilePath))
            {
                MessageBox.Show("來源檔案不存在", "錯誤", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if(File.Exists(CompressDestinationFilePath))
            {
                DialogResult result = MessageBox.Show("目的地檔案已存在\n要覆蓋檔案嗎?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                switch(result)
                {
                    case DialogResult.Yes:
                        _compressProcess.RunWorkerAsync();
                        break;
                    case DialogResult.No:
                        return;
                }
            }
            else
            {
                _compressProcess.RunWorkerAsync();
            }
        }

        # endregion

        private string OpenFile()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
            }

            return string.Empty;
        }

        private string OpenDirectory()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.SelectedPath;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// do decoding
        /// </summary>
        private void DeCompress()
        {
            try
            {
                ButtonState(false);

                AdaptiveHuffman adaptiveHuffman = new AdaptiveHuffman();
                byte[] contents = File.ReadAllBytes(SourceFilePath);
                byte[] result;


                SetProgressbar(contents.Length);

                List<byte> writeBuffer = adaptiveHuffman.Decode(contents,_deCompressProcess);

                if (UseDPCM.Checked)
                {
                    result = DPCM.ConvertFromDPCM(writeBuffer.ToArray());
                }
                else
                {
                    result = writeBuffer.ToArray();
                }

                File.WriteAllBytes(DeCompressDestinationFilePath, result);

            }
            catch (Exception e)
            {
                ButtonState(true);
                MessageBox.Show(e.Message, @"錯誤", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// do encoding
        /// </summary>
        private void Compress()
        {
            try
            {
                ButtonState(false);
                AdaptiveHuffman adaptiveHuffman = new AdaptiveHuffman();
                List<bool> writeBuffer = new List<bool>();
                byte[] contents = File.ReadAllBytes(SourceFilePath);
                SetProgressbar(contents.Length);

                if (UseDPCM.Checked)
                {
                    contents = DPCM.ConvertToDPCM(contents);
                }

                SetEntropyTextValue(EntropyCalculator.Calculate(contents).ToString());

                for (int i = 0; i < contents.Length; i++)
                {
                    bool[] code = adaptiveHuffman.Encode(contents[i]);
                    writeBuffer.AddRange(code);
                    _compressProcess.ReportProgress(i);
                }

                File.WriteAllBytes(CompressDestinationFilePath, Utility.BoolArrayToByteArray(writeBuffer.ToArray()));

            }
            catch (Exception e)
            {
                ButtonState(true);
                MessageBox.Show(e.Message, @"錯誤", MessageBoxButtons.OK);
            }
           
        }

        #region UI
        private void ProgressComplete()
        {
            MessageBox.Show(@"工作完成!", @"成功", MessageBoxButtons.OK);
            progressBar.Value = 0;
            SourceFilePath = string.Empty;
            ButtonState(true);
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            progressBar.Value = args.ProgressPercentage;
        }

        private void SetProgressbar(int max)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(_setValueDelegate,max);
            }
            else
            {
                progressBar.Maximum = max;
                progressBar.Minimum = 0;
            }
        }

        private void ButtonState(bool state)
        {

            if (DeCompressBtn.InvokeRequired)
            {
                DeCompressBtn.Invoke(_setButtonStateDelegate, state);
            }
            else
            {
                DeCompressBtn.Enabled = state;
            }

            if (CompressBtn.InvokeRequired)
            {
                CompressBtn.Invoke(_setButtonStateDelegate, state);
            }
            else
            {
                CompressBtn.Enabled = state;
            }
        }

        private void SetEntropyTextValue(string text)
        {
            if (EntropyTextbox.InvokeRequired)
            {
                EntropyTextbox.Invoke(_setEntropyTextDelegate, text);
            }
            else
            {
                EntropyTextbox.Text = text;
            }
        }
        #endregion


    }
}
