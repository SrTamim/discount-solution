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
    public partial class UserControlProfile : UserControl
    {
        string cs = ConfigurationManager.ConnectionStrings["tam"].ConnectionString;

        public UserControlProfile()
        {
            InitializeComponent();
        }

        private void UserControlProfile_Load(object sender, EventArgs e)  // User profile view
        {
            SqlConnection con = new SqlConnection(cs);
            
            SqlCommand cmb = new SqlCommand("select id, picture, uname, phone, role, pass from User_Details where id=@id", con);
            cmb.Parameters.AddWithValue("@id", MyConnection.key);
            con.Open();
            SqlDataReader rd = cmb.ExecuteReader();
            if (rd.Read())
            {
                textBox4.Text = rd.GetValue(0).ToString();
                pictureBox1.Image = GetPhoto((byte[])rd.GetValue(1));
                textBox1.Text = rd.GetValue(2).ToString();
                textBox2.Text = rd.GetValue(3).ToString();
                label6.Text = rd.GetValue(4).ToString();
                textBox3.Text = rd.GetValue(5).ToString();
            }
            con.Close();
        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }
    }
}
