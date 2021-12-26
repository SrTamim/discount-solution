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
    public partial class UserControlAdminHelp : UserControl
    {
        string cs = @"Data Source=DESKTOP-BNV4UQS;Initial Catalog=LOG_DB;Integrated Security=True;";

        public UserControlAdminHelp()
        {
            InitializeComponent();
            BindGridView();
            ResetContro();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void UserControlAdminHelp_Load(object sender, EventArgs e)
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
                    //textBox1.Text = rd.GetValue(2).ToString();
                }
            }
        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
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
                    string query = "select * from mas_tbl";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);

                    DataTable data = new DataTable();
                    sda.Fill(data);
                    dataGridView1.DataSource = data;

                    //Image Column
                    DataGridViewImageColumn dgv = new DataGridViewImageColumn();
                    dgv = (DataGridViewImageColumn)dataGridView1.Columns[1];
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
            textBox4.Clear();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            //pictureBox1.Image = GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[1].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "delete from mas_tbl where id=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", textBox4.Text);

                int a = cmd.ExecuteNonQuery();//0 1
                if (a >= 0)
                {
                    //MessageBox.Show("Data Deleted Successfully ! ");
                    BindGridView();
                    ResetContro();
                }
                else
                {
                    MessageBox.Show("Data not Deleted ! ");
                }
            }
        }


    }
}
