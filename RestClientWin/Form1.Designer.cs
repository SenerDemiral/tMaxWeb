namespace RestClientWin
{
    partial class Form1
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
            this.FrtPutFullButton = new System.Windows.Forms.Button();
            this.OpmPutFullButton = new System.Windows.Forms.Button();
            this.OphPutFullButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FrtPutFullButton
            // 
            this.FrtPutFullButton.Location = new System.Drawing.Point(12, 12);
            this.FrtPutFullButton.Name = "FrtPutFullButton";
            this.FrtPutFullButton.Size = new System.Drawing.Size(109, 23);
            this.FrtPutFullButton.TabIndex = 0;
            this.FrtPutFullButton.Text = "FrtPut Full";
            this.FrtPutFullButton.UseVisualStyleBackColor = true;
            this.FrtPutFullButton.Click += new System.EventHandler(this.FrtPutFullButton_Click);
            // 
            // OpmPutFullButton
            // 
            this.OpmPutFullButton.Location = new System.Drawing.Point(12, 41);
            this.OpmPutFullButton.Name = "OpmPutFullButton";
            this.OpmPutFullButton.Size = new System.Drawing.Size(109, 23);
            this.OpmPutFullButton.TabIndex = 1;
            this.OpmPutFullButton.Text = "OpmPut Full";
            this.OpmPutFullButton.UseVisualStyleBackColor = true;
            this.OpmPutFullButton.Click += new System.EventHandler(this.OpmPutFullButton_Click);
            // 
            // OphPutFullButton
            // 
            this.OphPutFullButton.Location = new System.Drawing.Point(12, 70);
            this.OphPutFullButton.Name = "OphPutFullButton";
            this.OphPutFullButton.Size = new System.Drawing.Size(109, 23);
            this.OphPutFullButton.TabIndex = 2;
            this.OphPutFullButton.Text = "OphPut Full";
            this.OphPutFullButton.UseVisualStyleBackColor = true;
            this.OphPutFullButton.Click += new System.EventHandler(this.OphPutFullButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OphPutFullButton);
            this.Controls.Add(this.OpmPutFullButton);
            this.Controls.Add(this.FrtPutFullButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FrtPutFullButton;
        private System.Windows.Forms.Button OpmPutFullButton;
        private System.Windows.Forms.Button OphPutFullButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

