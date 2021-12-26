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
using System.Data.SqlClient;
using System.IO;


namespace Discount
{
    public partial class Form3 : Form
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
        //string cs = ConfigurationManager.ConnectionStrings["tam"].ConnectionString;
        string cs = @"Data Source=DESKTOP-BNV4UQS;Initial Catalog=LOG_DB;Integrated Security=True;";

        MyConnection db = new MyConnection();

        public Form3()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        // close , mini , full screen
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_role_login",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uname", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@pass", kryptonTextBox3.Text);
                    
                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.HasRows)
                    {
                        rd.Read();
                        MyConnection.key = Convert.ToInt32(rd[0]);
                        if (rd[4].ToString()=="Admin")
                        {
                            MyConnection.type = "A";
                        }
                        else if (rd[4].ToString() == "User")
                        {
                            
                            MyConnection.type = "U";
                        }
                        else if (rd[4].ToString() == "Merchant")
                        {
                            MyConnection.type = "M";
                        }

                        
                        //after login send to home page
                        this.Close();
                        th = new Thread(opennewform1);
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start();

                    }
                    else
                    {
                       MessageBox.Show("Incorrect User Name or Password", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        private void opennewform1(object obj) // which form will show next
        {
                Application.Run(new Form1());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // goto registration
        {
            this.Close();
            th = new Thread(opennewform2);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        private void opennewform2(object obj)
        {
            Application.Run(new Form2());
        }

        // ERROR PROVIDER 
        private void TextBox1_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TextBox1.Text) == true)
            {
                TextBox1.Focus();
                //errorProvider1.Icon = Properties.Resources.error;
                errorProvider1.SetError(this.TextBox1, "Enter Your User Name");
            }
            else
            {
                errorProvider1.Clear();
                //errorProvider1.Icon = Properties.Resources.check;
            }
        }

        private void kryptonTextBox3_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(kryptonTextBox3.Text) == true)
            {
                kryptonTextBox3.Focus();
                //errorProvider1.Icon = Properties.Resources.error;
                errorProvider1.SetError(this.kryptonTextBox3, "Enter Your Password");
            }
            else
            {
                errorProvider1.Clear();
                //errorProvider1.Icon = Properties.Resources.check;
            }
        }
    }
}
