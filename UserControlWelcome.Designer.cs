
namespace Discount
{
    partial class UserControlWelcome
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlWelcome));
            this.about = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // about
            // 
            this.about.Font = new System.Drawing.Font("Roboto", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.about.ForeColor = System.Drawing.SystemColors.WindowText;
            this.about.Location = new System.Drawing.Point(548, 26);
            this.about.Name = "about";
            this.about.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.about.Size = new System.Drawing.Size(100, 33);
            this.about.TabIndex = 0;
            this.about.Text = "ABOUT";
            this.about.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.about.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // UserControlWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.Controls.Add(this.about);
            this.Name = "UserControlWelcome";
            this.Size = new System.Drawing.Size(709, 383);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox about;
    }
}
