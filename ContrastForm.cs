using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZGraphTools;

namespace Efficient_IP_Tasks
{
    public partial class ContrastForm : Form
    {

        MyColor[,] OriginalImageMatrix;
        MyColor[,] ContrastedImageMatrix;
        ToolTip contrastToolTip = new ToolTip();

        public ContrastForm()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                OriginalImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(OriginalImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(OriginalImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(OriginalImageMatrix).ToString();

        }

        private void btnNormalContrast_Click(object sender, EventArgs e)
        {
            double newValue = contrastTrackBar.Value;
            ContrastedImageMatrix = ImageOperations.ContrastImage_Normal(OriginalImageMatrix , newValue);
            ImageOperations.DisplayImage(ContrastedImageMatrix, pictureBox2);
        }

        private void btnEfficientContrast_Click(object sender, EventArgs e)
        {
            double newValue = contrastTrackBar.Value;
            ContrastedImageMatrix = ImageOperations.ContrastImage_Efficient(OriginalImageMatrix, newValue);
            ImageOperations.DisplayImage(ContrastedImageMatrix, pictureBox2);
        }

        protected void trackBar1_Scroll(object sender, EventArgs e)
        {
            double k = contrastTrackBar.Value;
            contrastToolTip.ShowAlways = true;
            contrastToolTip.SetToolTip(contrastTrackBar, k.ToString());
            contrastTextBox.Text = "" + k.ToString();
            if (btnNormalContrast.Enabled)
                ImageOperations.ContrastImage_Normal(OriginalImageMatrix, k);
            else if (btnEfficientContrast.Enabled)
                ImageOperations.ContrastImage_Efficient(OriginalImageMatrix, k);
        }

        private void MainMenu_button_Click(object sender, EventArgs e)
        {
            MainForm form = new MainForm();
            form.ShowDialog();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ImageOperations.SaveImage(ContrastedImageMatrix);
        }

        private void btnContrastGraph_Click(object sender, EventArgs e)
        {
            if (OriginalImageMatrix != null)
            {
                int OriginalWidth = int.Parse(txtWidth.Text);
                int OriginalHeight = int.Parse(txtHeight.Text);
                int ContrastDegree = int.Parse(contrastTextBox.Text);

            ZGraphForm ZGF = new ZGraphForm("Contrast Time Execution Graph", "Window Size", "Time (ms)");

                if (textBox_MaxWindowSize.Text != "")
                {
                    int maxWindowSize = int.Parse(textBox_MaxWindowSize.Text);

                    // Make up some data points from contrast image
                    double[] x_values = new double[maxWindowSize+1];
                    double[] y_values_Normal = new double[maxWindowSize+1];
                    double[] y_values_Efficient = new double[maxWindowSize+1];

                    for (int i = 1; i <= maxWindowSize; i++)
                    {
                        x_values[i] = i;

                        MyColor[,] ResizedImage = ImageOperations.NormalResize(OriginalImageMatrix, OriginalWidth*i, OriginalHeight*i);

                        ImageOperations.ContrastImage_Normal(ResizedImage, ContrastDegree);
                        double NormalTime = ImageOperations.elapsedMs;
                        y_values_Normal[i] = NormalTime;

                        ImageOperations.ContrastImage_Efficient(ResizedImage, ContrastDegree);
                        double EfficientTime = ImageOperations.elapsedMs;
                        y_values_Efficient[i] = EfficientTime;
                    }
                                       
                    //Create a graph and add two curves to it
                    ZGF.add_curve("MinMax Normal Algorithm", x_values, y_values_Normal, Color.Red);
                    ZGF.add_curve("MinMax Efficient Algorithm", x_values, y_values_Efficient, Color.Blue);
                    ZGF.Show();
                }
            }
        }

        private void buttonSave_Click_Click(object sender, EventArgs e)
        {
            ImageOperations.SaveImage(ContrastedImageMatrix);
        }

        
    }
}
