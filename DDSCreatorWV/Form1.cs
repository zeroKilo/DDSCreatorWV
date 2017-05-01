using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDSCreatorWV
{
    public partial class Form1 : Form
    {
        #region Defines
        public static string[] DDSFormat = { 
                                        "R32G32B32A32_FLOAT", 
                                        "R32G32B32A32_UINT", 
                                        "R32G32B32A32_SINT", 
                                        "R32G32B32_FLOAT", 
                                        "R32G32B32_UINT", 
                                        "R32G32B32_SINT", 
                                        "R16G16B16A16_FLOAT", 
                                        "R16G16B16A16_UNORM", 
                                        "R16G16B16A16_UINT", 
                                        "R16G16B16A16_SNORM", 
                                        "R16G16B16A16_SINT", 
                                        "R32G32_FLOAT", 
                                        "R32G32_UINT", 
                                        "R32G32_SINT", 
                                        "R10G10B10A2_UNORM", 
                                        "R10G10B10A2_UINT", 
                                        "R11G11B10_FLOAT", 
                                        "R8G8B8A8_UNORM", 
                                        "R8G8B8A8_UNORM_SRGB", 
                                        "R8G8B8A8_UINT", 
                                        "R8G8B8A8_SNORM", 
                                        "R8G8B8A8_SINT", 
                                        "R16G16_FLOAT", 
                                        "R16G16_UNORM", 
                                        "R16G16_UINT", 
                                        "R16G16_SNORM", 
                                        "R16G16_SINT", 
                                        "R32_FLOAT", 
                                        "R32_UINT", 
                                        "R32_SINT", 
                                        "R8G8_UNORM", 
                                        "R8G8_UINT", 
                                        "R8G8_SNORM", 
                                        "R8G8_SINT", 
                                        "R16_FLOAT", 
                                        "R16_UNORM", 
                                        "R16_UINT", 
                                        "R16_SNORM", 
                                        "R16_SINT", 
                                        "R8_UNORM", 
                                        "R8_UINT", 
                                        "R8_SNORM", 
                                        "R8_SINT", 
                                        "A8_UNORM",
                                        "R9G9B9E5_SHAREDEXP", 
                                        "R8G8_B8G8_UNORM", 
                                        "G8R8_G8B8_UNORM", 
                                        "BC1_UNORM", 
                                        "BC1_UNORM_SRGB", 
                                        "BC2_UNORM", 
                                        "BC2_UNORM_SRGB", 
                                        "BC3_UNORM", 
                                        "BC3_UNORM_SRGB", 
                                        "BC4_UNORM", 
                                        "BC4_SNORM", 
                                        "BC5_UNORM", 
                                        "BC5_SNORM", 
                                        "B5G6R5_UNORM", 
                                        "B5G5R5A1_UNORM", 
                                        "B8G8R8A8_UNORM", 
                                        "B8G8R8X8_UNORM", 
                                        "R10G10B10_XR_BIAS_A2_UNORM", 
                                        "B8G8R8A8_UNORM_SRGB", 
                                        "B8G8R8X8_UNORM_SRGB", 
                                        "BC6H_UF16", 
                                        "BC6H_SF16", 
                                        "BC7_UNORM", 
                                        "BC7_UNORM_SRGB", 
                                        "AYUV", 
                                        "Y410", 
                                        "Y416", 
                                        "YUY2", 
                                        "Y210", 
                                        "Y216", 
                                        "B4G4R4A4_UNORM" };

        public static string[] DDSFilters = { 
                                        "POINT", 
                                        "LINEAR", 
                                        "CUBIC", 
                                        "FANT",
                                        "BOX", 
                                        "TRIANGLE",
                                        "POINT_DITHER",
                                        "LINEAR_DITHER", 
                                        "CUBIC_DITHER", 
                                        "FANT_DITHER",
                                        "BOX_DITHER", 
                                        "TRIANGLE_DITHER", 
                                        "POINT_DITHER_DIFFUSION", 
                                        "LINEAR_DITHER_DIFFUSION", 
                                        "CUBIC_DITHER_DIFFUSION", 
                                        "FANT_DITHER_DIFFUSION", 
                                        "BOX_DITHER_DIFFUSION", 
                                        "TRIANGLE_DITHER_DIFFUSION" };
        #endregion

        public string inputPath = "";
        public string lastArgument = "";

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(DDSFormat);
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(DDSFilters);
            comboBox2.SelectedIndex = 0;
            comboBox3.Items.Clear();
            comboBox3.Items.Add("dds");
            comboBox3.Items.Add("bmp");
            comboBox3.SelectedIndex = 0;
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("New Size X;Y", "Create New Image", "256;256");
            if (input != "")
            {
                string[] parts = input.Split(';');
                if (parts.Length == 2)
                {
                    int x = Convert.ToInt32(parts[0]);
                    int y = Convert.ToInt32(parts[1]);
                    Bitmap bmp = new Bitmap(x, y);
                    Graphics g = Graphics.FromImage(bmp);
                    g.Clear(Color.White);
                    inputPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\temp.bmp";
                    bmp.Save(inputPath, ImageFormat.Bmp);
                    pic1.Image = LoadImageCopy(inputPath);
                    CreateCall();
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.bmp|*.bmp|*.dds|*.dds";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = Path.GetExtension(d.FileName).ToLower();
                switch (ext)
                {
                    case ".bmp":
                        pic1.Image = LoadImageCopy(d.FileName);
                        inputPath = d.FileName;
                        break;
                    case ".dds":
                        RunShell("texconv", "-o " + Path.GetDirectoryName(Application.ExecutablePath) + "\\ -ft bmp " + d.FileName);
                        if (File.Exists(Path.GetFileNameWithoutExtension(d.FileName) + ".bmp"))
                        {
                            if (File.Exists("temp.bmp"))
                                File.Delete("temp.bmp");
                            File.Move(Path.GetFileNameWithoutExtension(d.FileName) + ".bmp", "temp.bmp");
                            inputPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\temp.bmp";
                            pic1.Image = LoadImageCopy(inputPath);
                        }
                        break;
                }
                CreateCall();
            }
        }

        public void CreateCall()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("-f " + comboBox1.SelectedItem + " ");
            if (checkBox2.Checked)
                sb.Append("-if " + comboBox2.SelectedItem + " ");
            if (checkBox3.Checked)
                sb.Append("-pow2 ");
            if (checkBox1.Checked)
                sb.Append("-dx10 ");
            sb.Append("-ft " + comboBox3.SelectedItem + " ");
            sb.Append("-y -o " + textBox1.Text + " ");
            sb.Append("" + inputPath + " ");
            rtb1.Text = "texconv.exe " + sb.ToString();
            lastArgument = sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = textBox1.Text;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBox1.Text = fbd.SelectedPath + "\\";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateCall();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CreateCall();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CreateCall();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateCall();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CreateCall();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateCall();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateCall();
            rtb1.Text = RunShell("texconv.exe", lastArgument);
        }

        public static string RunShell(string file, string command)
        {
            Process process = new System.Diagnostics.Process();
            ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = file;
            startInfo.Arguments = command;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = Path.GetDirectoryName(file) + "\\";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            return process.StandardOutput.ReadToEnd();
        }

        public static Bitmap LoadImageCopy(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            Bitmap loaded = new Bitmap(fs);
            Bitmap result = (Bitmap)loaded.Clone();
            fs.Close();
            return result;
        }
    }
}
