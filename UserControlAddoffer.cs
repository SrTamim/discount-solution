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
    public partial class UserControlAddoffer : UserControl
    {
        string cs = @"Data Source=DESKTOP-BNV4UQS;Initial Catalog=LOG_DB;Integrated Security=True;";
        public UserControlAddoffer()
        {
            InitializeComponent();
            ResetContro();
        }

        private void UserControlAddoffer_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmb = new SqlCommand("select id, picture, uname from User_Details where id=@id", con);
                cmb.Parameters.AddWithValue("@id", MyConnection.key);

                SqlDataReader rd = cmb.ExecuteReader();
                if (rd.Read())
                {
                    pictureBox1.Image = GetPhoto((byte[])rd.GetValue(1));
                    textBox1.Text = rd.GetValue(2).ToString();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "insert into coupon_tbl values(@img,@shop,@scate,@coupon,@ccoupon,@disc,@code)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", textBox4.Text);
                cmd.Parameters.AddWithValue("@img", SavePhoto());
                cmd.Parameters.AddWithValue("@shop", textBox1.Text);
                cmd.Parameters.AddWithValue("@scate", textBox2.Text);
                cmd.Parameters.AddWithValue("@coupon", textBox3.Text);
                cmd.Parameters.AddWithValue("@ccoupon", textBox5.Text);
                cmd.Parameters.AddWithValue("@disc", textBox6.Text);
                cmd.Parameters.AddWithValue("@code", textBox7.Text);

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
                SqlCommand cmd1 = new SqlCommand("select * from coupon_tbl where shop like'%" + textBox1.Text + "%'", con);
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
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //Image Height
                dataGridView1.RowTemplate.Height = 50;
            }
        }

        void ResetContro() // resent empty text fields
        {
            //textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            //pictureBox1.Image = Properties.Resources.profile;
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "update coupon_tbl set picture=@img, scate=@scate, coupon=@coupon, ccoupon=@ccoupon, disc=@disc, code=@code  where id=@id";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", textBox4.Text);
                cmd.Parameters.AddWithValue("@img", SavePhoto());
                //cmd.Parameters.AddWithValue("@shop", textBox1.Text);
                cmd.Parameters.AddWithValue("@scate", textBox2.Text);
                cmd.Parameters.AddWithValue("@coupon", textBox3.Text);
                cmd.Parameters.AddWithValue("@ccoupon", textBox5.Text);
                cmd.Parameters.AddWithValue("@disc", textBox6.Text);
                cmd.Parameters.AddWithValue("@code", textBox7.Text);

                int c = cmd.ExecuteNonQuery();
                if (c > 0)
                {
                    BindGridView();
                    ResetContro();
                }
                else
                {
                    MessageBox.Show("Data not Updated ! ");
                }
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            //pictureBox1.Image = GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[1].Value);
            //textBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "delete from coupon_tbl where id=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", textBox4.Text);

                int a = cmd.ExecuteNonQuery();//0 1
                if (a >= 0)
                {
                    MessageBox.Show("Data Deleted Successfully ! ");
                    BindGridView();
                    ResetContro();
                }
                else
                {
                    MessageBox.Show("Data not Deleted ! ");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from coupon_tbl where shop like'%" + textBox1.Text + "%'", con);
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
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //Image Height
                dataGridView1.RowTemplate.Height = 50;
            }
        }
    }
}
