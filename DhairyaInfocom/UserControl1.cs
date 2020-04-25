using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DhairyaInfocom
{
    public partial class UserControl1 : UserControl
    {
        MySqlConnection con;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public UserControl1()
        {
            InitializeComponent();
        }
        public void getUserData()
        {
            getTotalUser();
            getPendingUser();
            getDoneUser();
            getPayPendingUser();

          
        }
        public void getTotalUser()
        {
            
           
                string users;
                try
                {
                    con = new MySqlConnection(cs);
                    string query = "SELECT count(id) as totalUser from customer";
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    users = cmd.ExecuteScalar().ToString();
                    if (int.Parse(users) > 0)
                    {
                            this.lblTotalUser.Text = users;
                            
                    }
                    else
                    {
                        this.lblTotalUser.Text = "0";
                    }

                }
                catch (MySqlException)
                {
                    MessageBox.Show("Check internet connection");
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
           

        }


        public void getPendingUser()
        {
            
                string pending;
            try
            {
                con = new MySqlConnection(cs); 
                string query = "SELECT count(id) from customer where status = 0";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                pending = cmd.ExecuteScalar().ToString();
                if (int.Parse(pending) > 0)
                {
                        this.lblPending.Text = pending;
                }
                else
                {
                        this.lblPending.Text = "0";
                }
                

            }
            catch (MySqlException)
            {
                MessageBox.Show("Check internet connection");
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
           
        }

        public void getDoneUser()
        {
           
                string done;
            try
            {
                con = new MySqlConnection(cs);
                string query = "SELECT count(id) from customer where status = 1";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                done = cmd.ExecuteScalar().ToString();
                if (int.Parse(done) > 0)
                {
                        this.lblDone.Text = done;
                }
                else
                {
                        this.lblDone.Text = "0";
                }

            }
            catch (MySqlException)
            {
                MessageBox.Show("Check internet connection");
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            
        }


        public void getPayPendingUser()
        {
           
                string payPending; 
            try
            {
                con = new MySqlConnection(cs);
                string query = "SELECT count(id) from customer where Paid = 0";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                payPending = cmd.ExecuteScalar().ToString();
                if (int.Parse(payPending) > 0)
                {
                        this.lblPayPending.Text = payPending;
                }
                else
                {
                        this.lblPayPending.Text = "0";
                }

            }
            catch (MySqlException)
            {
                MessageBox.Show("Check internet connection");
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
           
        }

        private void UserControl1_Enter(object sender, EventArgs e)
        {
            getUserData();
        }
    }
}
