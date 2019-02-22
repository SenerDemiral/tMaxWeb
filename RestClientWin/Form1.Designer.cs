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
            this.SuspendLayout();
            // 
            // FrtPutFullButton
            // 
            this.FrtPutFullButton.Location = new System.Drawing.Point(12, 12);
            this.FrtPutFullButton.Name = "FrtPutFullButton";
            this.FrtPutFullButton.Size = new System.Drawing.Size(75, 23);
            this.FrtPutFullButton.TabIndex = 0;
            this.FrtPutFullButton.Text = "FrtPut Full";
            this.FrtPutFullButton.UseVisualStyleBackColor = true;
            this.FrtPutFullButton.Click += new System.EventHandler(this.FrtPutFullButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FrtPutFullButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button FrtPutFullButton;
    }
}

