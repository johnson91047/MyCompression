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

namespace MyCompression3
{
    public partial class Form1 : MetroForm
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

        public string DestinationFilePath
        {
            get {
                string folder = DestinationFolderPath;
                string filename = FileName;

                if (string.IsNullOrEmpty(folder))
                {
                    folder = Path.GetDirectoryName(SourceFilePath);
                }

                if(string.IsNullOrEmpty(filename))
                {
                    filename = Path.GetFileNameWithoutExtension(SourceFilePath);
                }

                return Path.Combine(folder, filename + MyExtension);
            }
        }

        public Form1()
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

        }

        private void CompressBtn_Click(object sender, EventArgs e)
        {
            if(!File.Exists(SourceFilePath))
            {
                MessageBox.Show("selected file is not exist!","File not exist",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if(File.Exists(DestinationFilePath))
            {
                DialogResult result = MessageBox.Show("Destination file is already exist!\nDo you want to override it?", "File exist", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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

        private void Compress()
        {
            AdaptiveHuffman adaptiveHuffman = new AdaptiveHuffman();
            List<byte> writeBuffer = new List<byte>();
            byte[] contents = File.ReadAllBytes(SourceFilePath);

            foreach(byte content in contents)
            {
                BitArray code = adaptiveHuffman.Encode(content);
                byte[] result = Utility.BitArrayToByteArray(code);
                writeBuffer.AddRange(result);
            }

            File.WriteAllBytes(DestinationFilePath, writeBuffer.ToArray());

            MessageBox.Show("Compression complete!", "Success",MessageBoxButtons.OK);
        }
    }
}
