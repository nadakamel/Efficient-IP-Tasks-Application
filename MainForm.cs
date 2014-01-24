using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Efficient_IP_Tasks
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void contrast_button_Click(object sender, EventArgs e)
        {
            ContrastForm form1 = new ContrastForm();
            form1.Location = new Point(0, 0);
            form1.ShowDialog();
            this.Close();
        }

        private void blur_button_Click(object sender, EventArgs e)
        {
            BlurForm form2 = new BlurForm();
            form2.Location = new Point(0, 0);
            form2.ShowDialog();
            this.Close();
        }

        private void gamma_button_Click(object sender, EventArgs e)
        {
            GammaForm form3 = new GammaForm();
            form3.Location = new Point(0, 0);
            form3.ShowDialog();
            this.Close();
        }
    }
}
