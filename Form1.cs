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
using ComponentFactory.Krypton.Toolkit;
using System.Threading;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace Discount
{
    public partial class Form1 : KryptonForm
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

        static Form1 _obj;
        public static Form1 Instace
        {
            get { if (_obj == null) { _obj = new Form1(); } return _obj; }
        }
        public Panel PnlContainer
        {
            get { return panel3; } set { panel3 = value; }
        }

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            SidePanel.Height = Home.Height;
            SidePanel.Top = Home.Top;
            userControlHome2.Visible = true;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            if (MyConnection.type == "A")
            {
                Home.Visible = true;
                Profile.Visible = true;
                Category.Visible = true;
                Store.Visible = true;
                addoffer.Visible = true;
                Settings.Visible = false;
                UpdateOffer.Visible = true;
                UpdateUser.Visible = true;
                response.Visible = true;
                addoffer.Visible = false;
                review.Visible = true;
                notice.Visible = true;
            }
            else if (MyConnection.type == "U")
            {
                Home.Visible = true;
                Profile.Visible = true;
                Category.Visible = true;
                Store.Visible = true;
                addoffer.Visible = false;
                Settings.Visible = true;
                UpdateOffer.Visible = false;
                UpdateUser.Visible = false;
                response.Visible = false;
                review.Visible = true;
                notice.Visible = false;
            }
            else if (MyConnection.type == "M")
            {
                Home.Visible = true;
                Profile.Visible = true;
                Category.Visible = true;
                Store.Visible = true;
                addoffer.Visible = true;
                Settings.Visible = true;
                UpdateOffer.Visible = false;
                UpdateUser.Visible = false;
                response.Visible = false;
                review.Visible = true;
                notice.Visible = false;
            }

            string cs = @"Data Source=DESKTOP-BNV4UQS;Initial Catalog=LOG_DB;Integrated Security=True;";
            using (SqlConnection con = new SqlConnection(cs)) 
            {
                con.Open();
                SqlCommand cmb = new SqlCommand("select id, picture from User_Details where id=@id", con);
                cmb.Parameters.AddWithValue("@id", MyConnection.key);
                SqlDataReader rd = cmb.ExecuteReader();
                if (rd.Read())
                {
                    roundPictureBox1.Image = GetPhoto((byte[])rd.GetValue(1));
                }
            }

            // hiding all user controll
            userControlHome2.Visible = true;
            userControlUpdateUser2.Visible = false;
            userControlHome1.Visible = false;
            userControlUser1.Visible = false;
            userControlUpdateCoupon1.Visible = false;
            userControlCategory1.Visible = false;
            userControlStore1.Visible = false;
            userControlAddoffer1.Visible = false;
            userControlSettings1.Visible = false;
            userControlAdminHelp1.Visible = false;
            userControlReview1.Visible = false;
            userControlNotice1.Visible = false;
            userControlAddNotice1.Visible = false;
            userControl11.Visible = false;

        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 40, 40));

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            panel3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel3.Width, panel3.Height, 40, 40));
        }

        // Button on side panael moving blue side panel
        private void button1_Click(object sender, EventArgs e)
        {   
            SidePanel.Height = Home.Height;
            SidePanel.Top = Home.Top;
            // change color on click when selected
            Home.BackColor = Color.FromArgb(211, 207, 255);
            Profile.BackColor = Color.White;
            Category.BackColor = Color.White;
            Store.BackColor = Color.White;
            Settings.BackColor = Color.White;
            addoffer.BackColor = Color.White;

            _obj = this;
            UserControlHome uc = new UserControlHome();
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc);

            userControl11.Visible = false;
            userControlHome2.Visible = true;
            userControlUpdateUser2.Visible = false;
            userControlHome1.Visible = false;
            userControlUser1.Visible = false;
            userControlUpdateCoupon1.Visible = false;
            userControlCategory1.Visible = false;
            userControlStore1.Visible = false;
            userControlAddoffer1.Visible = false;
            userControlSettings1.Visible = false;
            userControlAdminHelp1.Visible = false;
            userControlReview1.Visible = false;
            userControlAddNotice1.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SidePanel.Height = Profile.Height;
            SidePanel.Top = Profile.Top;
            // change color on click when selected
            Profile.BackColor = Color.FromArgb(211, 207, 255);
            Home.BackColor = Color.White;
            Category.BackColor = Color.White;
            Store.BackColor = Color.White;
            Settings.BackColor = Color.White;
            addoffer.BackColor = Color.White;

            _obj = this;
            UserControlProfile uc = new UserControlProfile();
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc);

            userControl11.Visible = false;
            userControlHome2.Visible = false;
            userControlUpdateUser2.Visible = false;
            userControlHome1.Visible = false;
            userControlUser1.Visible = true;
            userControlUpdateCoupon1.Visible = false;
            userControlCategory1.Visible = false;
            userControlStore1.Visible = false;
            userControlAddoffer1.Visible = false;
            userControlSettings1.Visible = false;
            userControlAdminHelp1.Visible = false;
            userControlReview1.Visible = false;
            userControlAddNotice1.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SidePanel.Height = Category.Height;
            SidePanel.Top = Category.Top;
            // change color on click when selected
            Category.BackColor = Color.FromArgb(211, 207, 255);
            Home.BackColor = Color.White;
            Profile.BackColor = Color.White;
            Store.BackColor = Color.White;
            Settings.BackColor = Color.White;
            addoffer.BackColor = Color.White;

            _obj = this;
            UserControlCategory uc = new UserControlCategory();
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc);

            userControl11.Visible = false;
            userControlHome2.Visible = false;
            userControlUpdateUser2.Visible = false;
            userControlHome1.Visible = false;
            userControlUser1.Visible = false;
            userControlUpdateCoupon1.Visible = false;
            userControlCategory1.Visible = true;
            userControlStore1.Visible = false;
            userControlAddoffer1.Visible = false;
            userControlSettings1.Visible = false;
            userControlAdminHelp1.Visible = false;
            userControlReview1.Visible = false;
            userControlAddNotice1.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SidePanel.Height = Store.Height;
            SidePanel.Top = Store.Top;
            // change color on click when selected
            Store.BackColor = Color.FromArgb(211, 207, 255);
            Home.BackColor = Color.White;
            Profile.BackColor = Color.White;
            Category.BackColor = Color.White;
            Settings.BackColor = Color.White;
            addoffer.BackColor = Color.White;

            _obj = this;
            UserControlStore uc = new UserControlStore();
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc);

            userControl11.Visible = false;
            userControlHome2.Visible = false;
            userControlUpdateUser2.Visible = false;
            userControlHome1.Visible = false;
            userControlUser1.Visible = false;
            userControlUpdateCoupon1.Visible = false;
            userControlCategory1.Visible = false;
            userControlStore1.Visible = true;
            userControlAddoffer1.Visible = false;
            userControlSettings1.Visible = false;
            userControlAdminHelp1.Visible = false;
            userControlReview1.Visible = false;
            userControlAddNotice1.Visible = false;

        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            SidePanel.Height = Settings.Height;
            SidePanel.Top = Settings.Top;
            // change color on click when selected
            Settings.BackColor = Color.FromArgb(211, 207, 255);
            Home.BackColor = Color.White;
            Profile.BackColor = Color.White;
            Category.BackColor = Color.White;
            Store.BackColor = Color.White;
            addoffer.BackColor = Color.White;

            _obj = this;
            UserControlSettings uc = new UserControlSettings();
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc);

            userControl11.Visible = false;
            userControlHome2.Visible = false;
            userControlUpdateUser2.Visible = false;
            userControlHome1.Visible = false;
            userControlUser1.Visible = false;
            userControlUpdateCoupon1.Visible = false;
            userControlCategory1.Visible = false;
            userControlStore1.Visible = false;
            userControlAddoffer1.Visible = false;
            userControlSettings1.Visible = true;
            userControlAdminHelp1.Visible = false;
            userControlReview1.Visible = false;
            userControlAddNotice1.Visible = false;

        }

        // Window mini max cloce button
        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            
        } 

        private void button6_Click(object sender, EventArgs e)    /// LOG OUT BUTTON ACTION
        {
            this.Close();
            th = new Thread(opennewformLogin);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        private void opennewformLogin(object obj)
        {
            Application.Run(new Form3());
        }

        private void addoffer_Click(object sender, EventArgs e)
        {
            SidePanel.Height = addoffer.Height;
            SidePanel.Top = addoffer.Top;
            // change color on click when selected
            addoffer.BackColor = Color.FromArgb(211, 207, 255);
            Home.BackColor = Color.White;
            Profile.BackColor = Color.White;
            Category.BackColor = Color.White;
            Settings.BackColor = Color.White;
            Store.BackColor = Color.White;

            _obj = this;
            UserControlAddoffer uc = new UserControlAddoffer();
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc);

            userControl11.Visible = false;
            userControlHome2.Visible = false;
            userControlUpdateUser2.Visible = false;
            userControlHome1.Visible = false;
            userControlUser1.Visible = false;
            userControlUpdateCoupon1.Visible = false;
            userControlCategory1.Visible = false;
            userControlStore1.Visible = false;
            userControlAddoffer1.Visible = true;
            userControlSettings1.Visible = false;
            userControlAdminHelp1.Visible = false;
            userControlReview1.Visible = false;
            userControlAddNotice1.Visible = false;

        }

        private void UpdateUser_Click(object sender, EventArgs e)
        {
            Profile.BackColor = Color.White;
            Category.BackColor = Color.White;
            Store.BackColor = Color.White;
            Settings.BackColor = Color.White;
            addoffer.BackColor = Color.White;
            Home.BackColor = Color.White;

            _obj = this;
            UserControlUpdateUser uc = new UserControlUpdateUser();
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc);

            userControl11.Visible = false;
            userControlHome2.Visible = false;
            userControlUpdateUser2.Visible = true;
            userControlHome1.Visible = false;
            userControlUser1.Visible = false;
            userControlUpdateCoupon1.Visible = false;
            userControlCategory1.Visible = false;
            userControlStore1.Visible = false;
            userControlAddoffer1.Visible = false;
            userControlSettings1.Visible = false;
            userControlAdminHelp1.Visible = false;
            userControlReview1.Visible = false;
            userControlAddNotice1.Visible = false;


        }

        private void UpdateOffer_Click(object sender, EventArgs e)
        {

        }

        private void UpdateOffer_Click_1(object sender, EventArgs e)
        {
            Profile.BackColor = Color.White;
            Category.BackColor = Color.White;
            Store.BackColor = Color.White;
            Settings.BackColor = Color.White;
            addoffer.BackColor = Color.White;
            Home.BackColor = Color.White;

            _obj = this;
            UserControlUpdateCoupon uc = new UserControlUpdateCoupon();
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc);

            userControl11.Visible = false;
            userControlHome2.Visible = false;
            userControlUpdateUser2.Visible = false;
            userControlHome1.Visible = false;
            userControlUser1.Visible = false;
            userControlUpdateCoupon1.Visible = true;
            userControlCategory1.Visible = false;
            userControlStore1.Visible = false;
            userControlAddoffer1.Visible = false;
            userControlSettings1.Visible = false;
            userControlAdminHelp1.Visible = false;
            userControlReview1.Visible = false;
            userControlAddNotice1.Visible = false;


        }

        private void Comingsoon_Click(object sender, EventArgs e)
        {
            Profile.BackColor = Color.White;
            Category.BackColor = Color.White;
            Store.BackColor = Color.White;
            Settings.BackColor = Color.White;
            addoffer.BackColor = Color.White;
            Home.BackColor = Color.White;

            _obj = this;
            UserControlAdminHelp uc = new UserControlAdminHelp();
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc);

            userControl11.Visible = false;
            userControlHome2.Visible = false;
            userControlUpdateUser2.Visible = false;
            userControlHome1.Visible = false;
            userControlUser1.Visible = false;
            userControlUpdateCoupon1.Visible = false;
            userControlCategory1.Visible = false;
            userControlStore1.Visible = false;
            userControlAddoffer1.Visible = false;
            userControlSettings1.Visible = false;
            userControlAdminHelp1.Visible = true;
            userControlReview1.Visible = false;
            userControlAddNotice1.Visible = false;

        }

        private void review_Click(object sender, EventArgs e)
        {
            Profile.BackColor = Color.White;
            Category.BackColor = Color.White;
            Store.BackColor = Color.White;
            Settings.BackColor = Color.White;
            addoffer.BackColor = Color.White;
            Home.BackColor = Color.White;

            _obj = this;
            UserControlReview uc = new UserControlReview();
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc);

            userControl11.Visible = false;
            userControlHome2.Visible = false;
            userControlUpdateUser2.Visible = false;
            userControlHome1.Visible = false;
            userControlUser1.Visible = false;
            userControlUpdateCoupon1.Visible = false;
            userControlCategory1.Visible = false;
            userControlStore1.Visible = false;
            userControlAddoffer1.Visible = false;
            userControlSettings1.Visible = false;
            userControlAdminHelp1.Visible = false;
            userControlReview1.Visible = true;
            userControlAddNotice1.Visible = false;

        }

        private void roundPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            userControlNotice1.Visible = true;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            userControlNotice1.Visible = false;
        }

        private void notice_Click(object sender, EventArgs e)
        {
            Profile.BackColor = Color.White;
            Category.BackColor = Color.White;
            Store.BackColor = Color.White;
            Settings.BackColor = Color.White;
            addoffer.BackColor = Color.White;
            Home.BackColor = Color.White;

            _obj = this;
            UserControlAddNotice uc = new UserControlAddNotice();
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc);

            userControl11.Visible = false;
            userControlHome2.Visible = false;
            userControlUpdateUser2.Visible = false;
            userControlHome1.Visible = false;
            userControlUser1.Visible = false;
            userControlUpdateCoupon1.Visible = false;
            userControlCategory1.Visible = false;
            userControlStore1.Visible = false;
            userControlAddoffer1.Visible = false;
            userControlSettings1.Visible = false;
            userControlAdminHelp1.Visible = false;
            userControlReview1.Visible = false;
            userControlAddNotice1.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            userControl11.Visible = true;
            userControlHome2.Visible = false;
            userControlUpdateUser2.Visible = false;
            userControlHome1.Visible = false;
            userControlUser1.Visible = false;
            userControlUpdateCoupon1.Visible = false;
            userControlCategory1.Visible = false;
            userControlStore1.Visible = false;
            userControlAddoffer1.Visible = false;
            userControlSettings1.Visible = false;
            userControlAdminHelp1.Visible = false;
            userControlReview1.Visible = false;
            userControlAddNotice1.Visible = false;
        }
    }
}


