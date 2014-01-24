using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZGraphTools;

namespace Efficient_IP_Tasks
{
    public partial class GammaForm : Form
    {
        MyColor[,] OriginalImageMatrix;
        MyColor[,] GammaCorruptedImageMatrix;
        MyColor[,] GammaCorrectedImageMatrix;

        public GammaForm()
        {
            InitializeComponent();
        }

        private void MainMenu_button_Click(object sender, EventArgs e)
        {
            MainForm form = new MainForm();
            form.ShowDialog();
            this.Close();
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
            textBox1.Text = ImageOperations.GetWidth(OriginalImageMatrix).ToString();
            textBox2.Text = ImageOperations.GetHeight(OriginalImageMatrix).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                GammaCorruptedImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(GammaCorruptedImageMatrix, pictureBox2);
            }

        }

        private void btnGammaEffcient_Click(object sender, EventArgs e)
        {
            GammaCorrectedImageMatrix = ImageOperations.CalculateGammaCorrection_Efficient(OriginalImageMatrix, GammaCorruptedImageMatrix);

            textBox3.Text = ImageOperations.gammavalueEfficient.ToString();

            ImageOperations.DisplayImage(GammaCorrectedImageMatrix, pictureBox2);

        }

        private void btnGammaNormal_Click(object sender, EventArgs e)
        {
            GammaCorrectedImageMatrix = ImageOperations.CalculateGammaCorrection_Normal(OriginalImageMatrix, GammaCorruptedImageMatrix);

            textBox3.Text = ImageOperations.gammavalueNormal.ToString();

            ImageOperations.DisplayImage(GammaCorrectedImageMatrix, pictureBox2);
        }

        private void GammaGraph_Click(object sender, EventArgs e)
        {
            if (OriginalImageMatrix != null)
            {
                int OriginalWidth = int.Parse(textBox1.Text);
                int OriginalHeight = int.Parse(textBox2.Text);
                //int ContrastDegree = int.Parse(contrastTextBox.Text);

               ZGraphForm ZGF = new ZGraphForm("Gamma Time Execution Graph", "Window Size", "Time (ms)");

                if (textBox1_MaxWindowSize.Text != "")
                {
                    int maxWindowSize = int.Parse(textBox1_MaxWindowSize.Text);

                    // Make up some data points from contrast image
                    double[] x_values = new double[maxWindowSize + 1];
                    double[] y_values_Normal = new double[maxWindowSize + 1];
                    double[] y_values_Efficient = new double[maxWindowSize + 1];

                    for (int i = 1; i <= maxWindowSize; i++)
                    {
                        x_values[i] = i;

                        MyColor[,] OriginalResizedImage = ImageOperations.NormalResize(OriginalImageMatrix, OriginalWidth * i, OriginalHeight * i);

                        MyColor[,] CorruptedResizedImage = ImageOperations.NormalResize(GammaCorruptedImageMatrix, OriginalWidth * i, OriginalHeight * i);

                        ImageOperations.CalculateGammaCorrection_Normal(OriginalResizedImage, CorruptedResizedImage);
                        double NormalTime = ImageOperations.elapsedMs;
                        y_values_Normal[i] = NormalTime;

                        ImageOperations.CalculateGammaCorrection_Efficient(OriginalResizedImage, CorruptedResizedImage);
                        double EfficientTime = ImageOperations.elapsedMs;
                        y_values_Efficient[i] = EfficientTime;
                    }

                    //Create a graph and add two curves to it
                    ZGF.add_curve("Gamma Normal Algorithm", x_values, y_values_Normal, Color.Red);
                    ZGF.add_curve("Gamma Efficient Algorithm", x_values, y_values_Efficient, Color.Blue);
                    ZGF.Show();
                }
            }

        }
    }
}
