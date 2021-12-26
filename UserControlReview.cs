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
    public partial class UserControlReview : UserControl
    {
        string cs = @"Data Source=DESKTOP-BNV4UQS;Initial Catalog=LOG_DB;Integrated Security=True;";
        public UserControlReview()
        {
            InitializeComponent();
            BindGridView();
            ResetContro();
        }

        private void UserControlReview_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmb = new SqlCommand("select id, picture from User_Details where id=@id", con);
                cmb.Parameters.AddWithValue("@id", MyConnection.key);
                SqlDataReader rd = cmb.ExecuteReader();
                if (rd.Read())
                {
                    pictureBox1.Image = GetPhoto((byte[])rd.GetValue(1));
                }
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "insert into review_tbl values(@img,@shop,@star,@review)";
                SqlCommand cmd = new SqlCommand(query, con);
               
                cmd.Parameters.AddWithValue("@img", SavePhoto());
                cmd.Parameters.AddWithValue("@shop", textBox1.Text);
                cmd.Parameters.AddWithValue("@star", ComboBox1.Text);
                cmd.Parameters.AddWithValue("@review", textBox2.Text);

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
                string query = "select * from review_tbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView1.DataSource = data;

                //Image Column
                DataGridViewImageColumn dgv = new DataGridViewImageColumn();
                dgv = (DataGridViewImageColumn)dataGridView1.Columns[0];
                dgv.ImageLayout = DataGridViewImageCellLayout.Zoom;

                //AUTOSIZE
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //Image Height
                dataGridView1.RowTemplate.Height = 50;
            }
        }

        void ResetContro() // resent empty text fields
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            pictureBox1.Image = Properties.Resources.profile;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from review_tbl where shop like'%" + textBox3.Text + "%'", con);
                SqlDataAdapter dat2 = new SqlDataAdapter();
                DataTable data2 = new DataTable();
                dat2.SelectCommand = cmd;
                dat2.Fill(data2);
                dataGridView1.DataSource = data2;

                //Image Height
                dataGridView1.RowTemplate.Height = 50;
                //Image Column
                DataGridViewImageColumn dgv = new DataGridViewImageColumn();
                dgv = (DataGridViewImageColumn)dataGridView1.Columns[0];
                dgv.ImageLayout = DataGridViewImageCellLayout.Zoom;

                //AUTOSIZE
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmb = new SqlCommand("select id, picture from User_Details where id=@id", con);
                cmb.Parameters.AddWithValue("@id", MyConnection.key);
                SqlDataReader rd = cmb.ExecuteReader();
                if (rd.Read())
                {
                    pictureBox1.Image = GetPhoto((byte[])rd.GetValue(1));
                }
            }
        }
    }
}
