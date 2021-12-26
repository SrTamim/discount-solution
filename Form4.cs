using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Configuration;
using ComponentFactory.Krypton.Toolkit;
using System.Threading;

namespace Discount
{
    public partial class Form4 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
           (
               int nLeftRect,     // x-coordinate of upper-left corner
               int nTopRect,      // y-coordinate of upper-left corner
               int nRightRect,    // x-coordinate of lower-right corner
               int nBottomRect,   // y-coordinate of lower-right corner
               int nWidthEllipse, // height of ellipse
               int nHeightEllipse // width of ellipse
             );
        Thread th;
        public Form4()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.progressBar1.Increment(2);
            if (progressBarPanel.Width!= panel1.Width)
            {
                progressBarPanel.Width = progressBarPanel.Width + 1;
            }
            else
            {
                this.Close();
                th = new Thread(opennewform3);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                
            }

            
            
        }
        private void opennewform3(object obj) // which form will show next
        {
            Application.Run(new Form3());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
