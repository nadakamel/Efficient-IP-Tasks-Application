using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using ZGraphTools;

namespace Efficient_IP_Tasks
{
    public partial class BlurForm : Form
    {
        MyColor[,] OriginalImageMatrix;
        MyColor[,] BlurredImageMatrix;
        ToolTip blurToolTip = new ToolTip();

        public BlurForm()
        {
            InitializeComponent();
        }

        private void BlurForm_Load(object sender, EventArgs e)
        {

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

        private void button_NormalBlurr_Click(object sender, EventArgs e)
        {
            int newValue = blurTrackBar.Value;
            /* Normal Algorithm */
            BlurredImageMatrix = ImageOperations.BlurImage_Normal(OriginalImageMatrix, newValue);
            ImageOperations.DisplayImage(BlurredImageMatrix, pictureBox2);
        }

        private void button_EfficientBlur_Click(object sender, EventArgs e)
        {
            int newValue = blurTrackBar.Value;
            /* Efficient Algorithm */
            BlurredImageMatrix = ImageOperations.BlurImage_Efficient(OriginalImageMatrix, newValue);
            ImageOperations.DisplayImage(BlurredImageMatrix, pictureBox2);
        }

        private void blurrTrackBar_Scroll(object sender, EventArgs e)
        {
            int b = blurTrackBar.Value;
            blurToolTip.ShowAlways = true;
            blurToolTip.SetToolTip(blurTrackBar, b.ToString());
            blurTextBox.Text = "" + b.ToString();
            /* Normal Algorithm */
            if (button_NormalBlur.Enabled)
                ImageOperations.BlurImage_Normal(OriginalImageMatrix, b);
            /* Efficient Algorithm */
            else if (button_EfficientBlur.Enabled)
                ImageOperations.BlurImage_Efficient(OriginalImageMatrix, b);
        }

        private void MainMenu_button_Click(object sender, EventArgs e)
        {
            MainForm form = new MainForm();
            form.ShowDialog();
            this.Close();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            ImageOperations.SaveImage(BlurredImageMatrix);  
        }

        private void btnBlurGraph_Click(object sender, EventArgs e)
        {
            double[] xAxis = { 3, 5, 7, 9, 11, 13, 15, 17 };
            double[] yAxisBlurEfficient = new double[8];
            double[] yAxisBlurNotEfficient = new double[8];

            ZGraphForm graph = new ZGraphForm("Efficient and Non-Efficient Blur Algorithms", "Blur Mask Size", "Execution Time");

            for (int i = 0; i < xAxis.Length; i++)
            {
                MyColor[,] blurredImage = ImageOperations.BlurImage_Efficient(OriginalImageMatrix, (int)xAxis[i]);
                yAxisBlurEfficient[i] = ImageOperations.elapsedMs;
            }
            for (int i = 0; i < xAxis.Length; i++)
            {
                MyColor[,] blurredImage = ImageOperations.BlurImage_Normal(OriginalImageMatrix, (int)xAxis[i]);
                yAxisBlurNotEfficient[i] = ImageOperations.elapsedMs;
            }
            graph.add_curve("Non-Efficient Blur Algorithm", xAxis, yAxisBlurNotEfficient, Color.Red);
            graph.add_curve("Efficient Blur Algorithm", xAxis, yAxisBlurEfficient, Color.Green);
            graph.Show();
            
        }

       
    }
}
