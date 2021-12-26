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
    public partial class UserControlAddNotice : UserControl
    {
        string cs = @"Data Source=DESKTOP-BNV4UQS;Initial Catalog=LOG_DB;Integrated Security=True;";
        public UserControlAddNotice()
        {
            InitializeComponent();
            BindGridView();
            ResetContro();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "insert into notice_tbl values(@notice)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@notice", textBox1.Text);

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
        void BindGridView()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "select * from notice_tbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView1.DataSource = data;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        void ResetContro() // resent empty text fields
        {
            textBox1.Clear();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "update notice_tbl set notice=@notice";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@notice", textBox1.Text);

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

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "delete from notice_tbl";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@notice", textBox1.Text);

                int a = cmd.ExecuteNonQuery();//0 1
                if (a >= 0)
                {
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
