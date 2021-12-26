using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace Discount
{
    public partial class UserControlSettings : UserControl
    {
        string cs = @"Data Source=DESKTOP-BNV4UQS;Initial Catalog=LOG_DB;Integrated Security=True;";
        public UserControlSettings()
        {
            InitializeComponent();
            BindGridView();
            ResetContro();
        }

        private void UserControlSettings_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmb = new SqlCommand("select id,picture, uname from User_Details where id=@id", con);
                cmb.Parameters.AddWithValue("@id", MyConnection.key);

                SqlDataReader rd = cmb.ExecuteReader();
                if (rd.Read())
                {
                    pictureBox1.Image = GetPhoto((byte[])rd.GetValue(1));
                    textBox2.Text = rd.GetValue(2).ToString();
                }
            }
        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "insert into mas_tbl values(@img,@subject,@message,@uname)";
                SqlCommand cmd = new SqlCommand(query, con);
                //cmd.Parameters.AddWithValue("@id", textBox4.Text);
                cmd.Parameters.AddWithValue("@img", SavePhoto());
                cmd.Parameters.AddWithValue("@subject", textBox1.Text);
                cmd.Parameters.AddWithValue("@message", textBox3.Text);
                cmd.Parameters.AddWithValue("@uname", textBox2.Text);

                int b = cmd.ExecuteNonQuery();
                if (b > 0)
                {
                    //MessageBox.Show("Data Inserted Successfully ! ");
                    BindGridView();
                    ResetContro();
                }
                else
                {
                    MessageBox.Show("Data not Inserted ! ");
                }
            }
        }
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }
        void BindGridView()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select id,picture,subject,message from mas_tbl where uname like'%" + textBox2.Text + "%'", con);
                SqlDataAdapter sda1 = new SqlDataAdapter();
                DataTable data1 = new DataTable();
                sda1.SelectCommand = cmd1;
                sda1.Fill(data1);
                dataGridView1.DataSource = data1;

                //Image Column
                DataGridViewImageColumn dgv1 = new DataGridViewImageColumn();
                dgv1 = (DataGridViewImageColumn)dataGridView1.Columns[1];
                dgv1.ImageLayout = DataGridViewImageCellLayout.Zoom;

                //AUTOSIZE
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //row Height
                dataGridView1.RowTemplate.Height = 50;
            }
        }
        void ResetContro() // resent empty text fields
        {
            textBox1.Clear();
            //textBox2.Clear();
            textBox3.Clear();
            //textBox4.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select id,picture,subject,message from mas_tbl where uname like'%" + textBox2.Text + "%'", con);
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable data = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(data);
                dataGridView1.DataSource = data;

                //Image Column
                DataGridViewImageColumn dgv = new DataGridViewImageColumn();
                dgv = (DataGridViewImageColumn)dataGridView1.Columns[1];
                dgv.ImageLayout = DataGridViewImageCellLayout.Zoom;

                //AUTOSIZE
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //row Height
                dataGridView1.RowTemplate.Height = 50;
            }
        }
    }
}
