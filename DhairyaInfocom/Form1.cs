using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

namespace DhairyaInfocom
{
 
    public partial class Form1 : Form
    {

        UserControl1 user1 = new UserControl1();
        public Form1()
        {
            InitializeComponent();
            SideTip.Height = button1.Height;
            SideTip.Top = button1.Top;

            panel4.Controls.Add(user1);
            user1.BringToFront();
            user1.getTotalUser();
            datet.Text = login.user_name;
            lastlogin.Text = login.last_login;


            if (login.user_type != "superadmin")
            {
                btnUsers.Visible = false;
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SideTip.Height = button1.Height;
            SideTip.Top = button1.Top;
            user1.BringToFront();
        }

        private async void MenuBtn2_Click(object sender, EventArgs e)
        {
            SideTip.Height = MenuBtn2.Height;
            SideTip.Top = MenuBtn2.Top;
            Show show = new Show();


            panel4.Controls.Add(show);
            show.BringToFront();
            show.getData();
        }
       
        private void MenuBtn3_Click(object sender, EventArgs e)
        {
            SideTip.Height = MenuBtn3.Height;
            SideTip.Top = MenuBtn3.Top;


            AddCustomer addCust = new AddCustomer();
           
            panel4.Controls.Add(addCust);
            addCust.BringToFront();
        }

        private void MenuBtn4_Click(object sender, EventArgs e)
        {
            SideTip.Height = Extra.Height;
            SideTip.Top = Extra.Top;

            Extra extra = new Extra();
           
            panel4.Controls.Add(extra);
            extra.BringToFront();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private async void Form1_Activated(object sender, EventArgs e)
        {
            //  show.getData();
            /// addCust.getReceiptNo();
            //replace.getReplaceDataEdit();
           
        }

        private void btnReplaceMenu_Click(object sender, EventArgs e)
        {
            SideTip.Height = btnReplaceMenu.Height;
            SideTip.Top = btnReplaceMenu.Top;

            Replace replace = new Replace();
           
            panel4.Controls.Add(replace);
            replace.BringToFront();
           
        }

        private void btnAdminMenu_Click(object sender, EventArgs e)
        {
            SideTip.Height = btnAdminMenu.Height;
            SideTip.Top = btnAdminMenu.Top;
            admin admin = new admin();
           
            panel4.Controls.Add(admin);
            admin.BringToFront();
           
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            SideTip.Height = btnUsers.Height;
            SideTip.Top = btnUsers.Top;
             superadmin users = new superadmin();
            panel4.Controls.Add(users);
            users.BringToFront();
        }
    }
}
