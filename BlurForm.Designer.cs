namespace Efficient_IP_Tasks
{
    partial class BlurForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.blurTrackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_blurr = new System.Windows.Forms.Label();
            this.button_NormalBlur = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.blurTextBox = new System.Windows.Forms.TextBox();
            this.MainMenu_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button_EfficientBlur = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.blurTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // blurTrackBar
            // 
            this.blurTrackBar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.blurTrackBar.LargeChange = 2;
            this.blurTrackBar.Location = new System.Drawing.Point(1209, 78);
            this.blurTrackBar.Margin = new System.Windows.Forms.Padding(4);
            this.blurTrackBar.Maximum = 17;
            this.blurTrackBar.Minimum = 3;
            this.blurTrackBar.Name = "blurTrackBar";
            this.blurTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.blurTrackBar.Size = new System.Drawing.Size(56, 388);
            this.blurTrackBar.SmallChange = 2;
            this.blurTrackBar.TabIndex = 18;
            this.blurTrackBar.Value = 3;
            this.blurTrackBar.Scroll += new System.EventHandler(this.blurrTrackBar_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(803, 459);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 24);
            this.label2.TabIndex = 17;
            this.label2.Text = "Blurred Image";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(123, 459);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 24);
            this.label1.TabIndex = 16;
            this.label1.Text = "Original Image";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(608, 12);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(568, 430);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(14, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(568, 430);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // label_blurr
            // 
            this.label_blurr.AutoSize = true;
            this.label_blurr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_blurr.Location = new System.Drawing.Point(1188, 21);
            this.label_blurr.Name = "label_blurr";
            this.label_blurr.Size = new System.Drawing.Size(100, 20);
            this.label_blurr.TabIndex = 19;
            this.label_blurr.Text = "Blur Degree";
            // 
            // button_NormalBlur
            // 
            this.button_NormalBlur.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_NormalBlur.Location = new System.Drawing.Point(708, 501);
            this.button_NormalBlur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_NormalBlur.Name = "button_NormalBlur";
            this.button_NormalBlur.Size = new System.Drawing.Size(145, 76);
            this.button_NormalBlur.TabIndex = 21;
            this.button_NormalBlur.Text = "Normal Blurr Image";
            this.button_NormalBlur.UseVisualStyleBackColor = true;
            this.button_NormalBlur.Click += new System.EventHandler(this.button_NormalBlurr_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(146, 501);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(109, 76);
            this.btnOpen.TabIndex = 20;
            this.btnOpen.Text = "Open Image";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // blurTextBox
            // 
            this.blurTextBox.Enabled = false;
            this.blurTextBox.Location = new System.Drawing.Point(1208, 50);
            this.blurTextBox.Name = "blurTextBox";
            this.blurTextBox.Size = new System.Drawing.Size(58, 22);
            this.blurTextBox.TabIndex = 22;
            this.blurTextBox.Text = "3";
            this.blurTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainMenu_button
            // 
            this.MainMenu_button.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenu_button.Location = new System.Drawing.Point(1141, 611);
            this.MainMenu_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MainMenu_button.Name = "MainMenu_button";
            this.MainMenu_button.Size = new System.Drawing.Size(124, 62);
            this.MainMenu_button.TabIndex = 23;
            this.MainMenu_button.Text = "Back to Main Menu";
            this.MainMenu_button.UseVisualStyleBackColor = true;
            this.MainMenu_button.Click += new System.EventHandler(this.MainMenu_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(350, 525);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 21);
            this.label3.TabIndex = 24;
            this.label3.Text = "Image Width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(350, 561);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 21);
            this.label4.TabIndex = 25;
            this.label4.Text = "Image Height";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.textBox1.Location = new System.Drawing.Point(483, 524);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 27);
            this.textBox1.TabIndex = 26;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.textBox2.Location = new System.Drawing.Point(483, 561);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 27);
            this.textBox2.TabIndex = 27;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(806, 609);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 62);
            this.button1.TabIndex = 28;
            this.button1.Text = "Execution Time Graph";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnBlurGraph_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(1143, 498);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 75);
            this.button2.TabIndex = 29;
            this.button2.Text = "Save Image";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button_EfficientBlur
            // 
            this.button_EfficientBlur.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_EfficientBlur.Location = new System.Drawing.Point(878, 501);
            this.button_EfficientBlur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_EfficientBlur.Name = "button_EfficientBlur";
            this.button_EfficientBlur.Size = new System.Drawing.Size(148, 76);
            this.button_EfficientBlur.TabIndex = 30;
            this.button_EfficientBlur.Text = "Efficient Blurr Image";
            this.button_EfficientBlur.UseVisualStyleBackColor = true;
            this.button_EfficientBlur.Click += new System.EventHandler(this.button_EfficientBlur_Click);
            // 
            // BlurForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 699);
            this.Controls.Add(this.button_EfficientBlur);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MainMenu_button);
            this.Controls.Add(this.blurTextBox);
            this.Controls.Add(this.button_NormalBlur);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.label_blurr);
            this.Controls.Add(this.blurTrackBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "BlurForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Image Blurr";
            this.Load += new System.EventHandler(this.BlurForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.blurTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar blurTrackBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_blurr;
        private System.Windows.Forms.Button button_NormalBlur;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox blurTextBox;
        private System.Windows.Forms.Button MainMenu_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button_EfficientBlur;
    }
}
