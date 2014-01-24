namespace Efficient_IP_Tasks
{
    partial class MainForm
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
            this.blur_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.contrast_button = new System.Windows.Forms.Button();
            this.gamma_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // blur_button
            // 
            this.blur_button.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blur_button.Location = new System.Drawing.Point(175, 197);
            this.blur_button.Margin = new System.Windows.Forms.Padding(4);
            this.blur_button.Name = "blur_button";
            this.blur_button.Size = new System.Drawing.Size(116, 57);
            this.blur_button.TabIndex = 6;
            this.blur_button.Text = "Blur";
            this.blur_button.UseVisualStyleBackColor = true;
            this.blur_button.Click += new System.EventHandler(this.blur_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(151, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 30);
            this.label1.TabIndex = 7;
            this.label1.Text = "WELCOME";
            // 
            // contrast_button
            // 
            this.contrast_button.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contrast_button.Location = new System.Drawing.Point(40, 197);
            this.contrast_button.Margin = new System.Windows.Forms.Padding(4);
            this.contrast_button.Name = "contrast_button";
            this.contrast_button.Size = new System.Drawing.Size(116, 59);
            this.contrast_button.TabIndex = 8;
            this.contrast_button.Text = "Contrast";
            this.contrast_button.UseVisualStyleBackColor = true;
            this.contrast_button.Click += new System.EventHandler(this.contrast_button_Click);
            // 
            // gamma_button
            // 
            this.gamma_button.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gamma_button.Location = new System.Drawing.Point(311, 199);
            this.gamma_button.Margin = new System.Windows.Forms.Padding(4);
            this.gamma_button.Name = "gamma_button";
            this.gamma_button.Size = new System.Drawing.Size(116, 57);
            this.gamma_button.TabIndex = 9;
            this.gamma_button.Text = "Gamma Correction";
            this.gamma_button.UseVisualStyleBackColor = true;
            this.gamma_button.Click += new System.EventHandler(this.gamma_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(436, 30);
            this.label2.TabIndex = 10;
            this.label2.Text = "Choose what task you want to operate";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 360);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gamma_button);
            this.Controls.Add(this.contrast_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.blur_button);
            this.Name = "MainForm";
            this.Text = "Image Processing Efficient Tasks ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button blur_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button contrast_button;
        private System.Windows.Forms.Button gamma_button;
        private System.Windows.Forms.Label label2;
    }
}
