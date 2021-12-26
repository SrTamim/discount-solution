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
    public partial class UserControlNotice : UserControl
    {
        string cs = @"Data Source=DESKTOP-BNV4UQS;Initial Catalog=LOG_DB;Integrated Security=True;";
        public UserControlNotice()
        {
            InitializeComponent();
            BindGridView();
        }

        private void UserControlNotice_Load(object sender, EventArgs e)
        {

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

                //AUTOSIZE
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
    }
}
