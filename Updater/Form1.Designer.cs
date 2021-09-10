
namespace Updater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ProgBar = new System.Windows.Forms.ProgressBar();
            this.LblTitle = new System.Windows.Forms.Label();
            this.LblResponse = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProgBar
            // 
            this.ProgBar.Location = new System.Drawing.Point(37, 62);
            this.ProgBar.Margin = new System.Windows.Forms.Padding(2);
            this.ProgBar.Name = "ProgBar";
            this.ProgBar.Size = new System.Drawing.Size(447, 45);
            this.ProgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgBar.TabIndex = 0;
            // 
            // LblTitle
            // 
            this.LblTitle.AutoSize = true;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(45, 21);
            this.LblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(72, 21);
            this.LblTitle.TabIndex = 1;
            this.LblTitle.Text = "Updater";
            // 
            // LblResponse
            // 
            this.LblResponse.AutoSize = true;
            this.LblResponse.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblResponse.ForeColor = System.Drawing.Color.White;
            this.LblResponse.Location = new System.Drawing.Point(46, 120);
            this.LblResponse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblResponse.Name = "LblResponse";
            this.LblResponse.Size = new System.Drawing.Size(0, 15);
            this.LblResponse.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(520, 169);
            this.ControlBox = false;
            this.Controls.Add(this.LblResponse);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.ProgBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgBar;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Label LblResponse;
    }
}

