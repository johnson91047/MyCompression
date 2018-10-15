using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace MyCompression
{
    public partial class Main : MetroForm
    {
        public string SourceFilePath
        {
            get => SourceFilePathTextBox.Text;
            set => SourceFilePathTextBox.Text = value;
        }

        public string DestinationFolderPath
        {
            get => DestinationFolderPathTextBox.Text;
            set => DestinationFolderPathTextBox.Text = value;
        }

        public string FileName
        {
            get => FileNameTextBox.Text;
            set => FileNameTextBox.Text = value;
        }

        public const string MyExtension = ".mycom";

        public string CompressDestinationFilePath
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

        public string DeCompressDestinationFilePath
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

        public Main()
        {
            InitializeComponent();
        }

        private string OpenFile()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = Directory.GetCurrentDirectory();
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

            if(string.IsNullOrEmpty(Path.GetExtension(FileName)))
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
                        DeCompress();
                        break;
                    case DialogResult.No:
                        return;
                }
            }
            else
            {
                DeCompress();
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
                        Compress();
                        break;
                    case DialogResult.No:
                        return;
                }
            }
            else
            {
                Compress();
            }
        }

        private void DeCompress()
        {
            try
            {
                ProgressionController.Show();

                AdaptiveHuffman adaptiveHuffman = new AdaptiveHuffman();
                List<byte> contents = File.ReadAllBytes(SourceFilePath).ToList();

                List<byte> writeBuffer = adaptiveHuffman.Decode(contents);

                File.WriteAllBytes(DeCompressDestinationFilePath, writeBuffer.ToArray());

                ProgressionController.Abort();
                MessageBox.Show("解壓縮完成!", "成功", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                ProgressionController.Abort();
                MessageBox.Show(e.Message, "錯誤", MessageBoxButtons.OK);
            }
        }

        private void Compress()
        {
            try
            {
                ProgressionController.Show();

                AdaptiveHuffman adaptiveHuffman = new AdaptiveHuffman();
                List<bool> writeBuffer = new List<bool>();
                byte[] contents = File.ReadAllBytes(SourceFilePath);

                foreach (byte content in contents)
                {
                    bool[] code = adaptiveHuffman.Encode(content);
                    writeBuffer.AddRange(code);
                }

                File.WriteAllBytes(CompressDestinationFilePath, Utility.BoolArrayToByteArray(writeBuffer.ToArray()));

                ProgressionController.Abort();
                MessageBox.Show("壓縮完成!", "成功", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                ProgressionController.Abort();
                MessageBox.Show(e.Message, "錯誤", MessageBoxButtons.OK);
            }
           
        }
    }
}
