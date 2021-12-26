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
    public partial class Form2 : Form
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

        string cs = ConfigurationManager.ConnectionStrings["tam"].ConnectionString;

        public Form2()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        // Window mini max cloce button
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //max
        private void button9_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        //mini
        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)  
        {// insert data from registration
            if (checkBox1.Checked)
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "Insert into User_details values (@img,@uname,@phone,@role,@pass)";
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@img", SavePhoto());
                cmd.Parameters.AddWithValue("@uname", TextBox1.Text);
                cmd.Parameters.AddWithValue("@phone", TextBox2.Text);
                cmd.Parameters.AddWithValue("@role", ComboBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox4.Text);

                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a>0)
                {
                
                    MessageBox.Show("Registration Succesful", "Succes", MessageBoxButtons.OK, MessageBoxIcon.None);
                    th = new Thread(opennewformLogin);
                    th.SetApartmentState(ApartmentState.STA); // go to login page after registration
                    th.Start();
                    this.Close();
                
                }
                else
                {
                    MessageBox.Show("Registration Faild Try Again", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();

            }
            else
                MessageBox.Show("Check The Terms and Conditions", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private byte[] SavePhoto()  // image convesion to byte
        {
            MemoryStream ms = new MemoryStream();
            PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void opennewformLogin(object obj)
        {
            Application.Run(new Form3());
        }

        //cheking text box to write 1st one then move 2nd one
        private void TextBox1_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TextBox1.Text) == true)
            {
                TextBox1.Focus();
                errorProvider1.SetError(this.TextBox1, "Enter Your User Name");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void kryptonTextBox1_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TextBox2.Text) == true)
            {
                TextBox2.Focus();
                errorProvider2.SetError(this.TextBox2, "Enter Your Phone Number");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void ComboBox1_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ComboBox1.Text) == true)
            {
                ComboBox1.Focus();
                errorProvider4.SetError(this.ComboBox1, "Choose One");
            }
            else
            {
                errorProvider4.Clear();
                
            }
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox2.Checked;
            switch (status)
            {
                case true:
                    textBox4.UseSystemPasswordChar = false;
                    break;

                default:
                    textBox4.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "ALL Image File (*.*) | *.*";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                PictureBox1.Image = new Bitmap(ofd.FileName);
            }

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox4.Text) == true)
            {
                textBox4.Focus();
                errorProvider3.SetError(this.textBox4, "Enter Your Password");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            th = new Thread(opennewform3);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        private void opennewform3(object obj)
        {
            Application.Run(new Form3());
        }
    }
}
