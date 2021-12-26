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
    public partial class UserControlCategory : UserControl
    {
        string cs = @"Data Source=DESKTOP-BNV4UQS;Initial Catalog=LOG_DB;Integrated Security=True;";
        public UserControlCategory()
        {
            InitializeComponent();
            BindGridView();
        }

        private void UserControlCategory_Load(object sender, EventArgs e)
        {
           
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select picture, shop,coupon,disc,code from coupon_tbl where ccoupon like'%"+ ComboBox1.Text + "%'",con);
                SqlDataAdapter dat = new SqlDataAdapter();
                DataTable data = new DataTable();
                dat.SelectCommand = cmd;
                dat.Fill(data);
                dataGridView1.DataSource = data;

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

        void BindGridView()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "select picture, shop,coupon,disc,code from coupon_tbl";
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
    }
}
