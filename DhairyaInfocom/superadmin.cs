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
    public partial class superadmin : UserControl
    {
        public int user_id;
        MySqlConnection con;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public superadmin()
        {
            InitializeComponent();
            getUers();
        }



        public void getUers()
        {
                con = new MySqlConnection(cs);
                string query = "select * from user where id != @id";
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", login.id);

                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        USERSdatagrid.Rows.Clear();
                        while (dr.Read())
                        {
                            string[] data = { dr.GetInt32(0).ToString(), dr.GetString(1), dr.GetInt32(5).ToString(), dr.GetString(4), dr.GetString(3) };
                            USERSdatagrid.Rows.Add(data);
                        }
                    }

                }
                catch (MySqlException)
                {
                    ErrorMsg.msg("Check internet connection");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
           
        }

        private void USERSdatagrid_DoubleClick(object sender, EventArgs e)
        {
            if (USERSdatagrid.SelectedRows.Count > 0)
            {
                string username = USERSdatagrid.SelectedRows[0].Cells[1].Value.ToString();
            
                string role = USERSdatagrid.SelectedRows[0].Cells[4].Value.ToString();
               
                string block = USERSdatagrid.SelectedRows[0].Cells[2].Value.ToString();
                user_id = Convert.ToInt32(USERSdatagrid.SelectedRows[0].Cells[0].Value);
                txtUserName.Text = username;


                if (role=="superadmin")
                {
                    cmbRole.SelectedIndex = 0;
                }
                else if(role=="admin")
                {
                    cmbRole.SelectedIndex = 1;
                }

                if (block == "0")
                {
                    cmbBlock.SelectedIndex = 1;
                }else if (block == "1")
                {
                    cmbBlock.SelectedIndex = 0;
                }
                getWork(USERSdatagrid.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void resetUser()
        {
                txtUserName.Clear();

                cmbBlock.SelectedIndex = -1;
                cmbRole.SelectedIndex = -1;
                cmbRole.Text = "--Select--";
                cmbBlock.Text = "--Select--";
                WORKdatagrid.Rows.Clear();
            
        }
        private void getWork(string user_id)
        {
                con = new MySqlConnection(cs);
                string query = "SELECT * FROM `user_work` where user=@user";
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user", Convert.ToInt32(user_id));

                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        WORKdatagrid.Rows.Clear();
                        while (dr.Read())
                        {
                            string[] data = { dr.GetString(2), dr.GetString(3) };
                            WORKdatagrid.Rows.Add(data);
                        }
                    }

                }
                catch (MySqlException)
                {
                    ErrorMsg.msg("Check internet connection");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
        }

        private void btnUserUpdate_Click(object sender, EventArgs e)
        {
             updateUser();
        }

        private void updateUser()
        {
                string username = txtUserName.Text;
                string role = cmbRole.Text;
                string block = cmbBlock.Text;
                int block_status = 0;
                if (username == "")
                {
                    ErrorMsg.msg("Username can't be empty");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                if (role == "")
                {
                    ErrorMsg.msg("Please select role");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }


                if (block == "")
                {
                    ErrorMsg.msg("Please select block status");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                else
                {
                    if (block == "Block")
                    {
                        block_status = 1;
                    }
                    else if (block == "Unblock")
                    {
                        block_status = 0;
                    }
                }


                con = new MySqlConnection(cs);
                string query = "update user set username=@user,block=@block,role=@role where id=@id";
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@block", block_status);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@id", user_id);

                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                         getUers();
                         resetUser();
                        ErrorMsg.msg("Update succesfully!!");
                        Error er = new Error();
                        er.ShowDialog();
                    }

                }
                catch (MySqlException)
                {
                    ErrorMsg.msg("Check internet connection");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
        }

       

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            addUser();
        }

        private void addUser()
        {
                string username = txtUserName.Text;
                string role = cmbRole.Text;
                string block = cmbBlock.Text;
                string password = "dhairya";
                int block_status = 0;
                if (username == "")
                {
                    ErrorMsg.msg("Username can't be empty");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                if (role == "")
                {
                    ErrorMsg.msg("Please select role");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }


                if (block == "")
                {
                    ErrorMsg.msg("Please select block status");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                else
                {
                    if (block == "Block")
                    {
                        block_status = 1;
                    }
                    else if (block == "Unblock")
                    {
                        block_status = 0;
                    }
                }


                con = new MySqlConnection(cs);
                string query = "insert into user(username,password,role,block)values(@user,@pass,@role,@block)";
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@block", block_status);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@pass", password);

                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                         getUers();
                         resetUser();
                        ErrorMsg.msg("User add succesfully!!");
                        Error er = new Error();
                        er.ShowDialog();
                    }

                }
                catch (MySqlException)
                {
                    ErrorMsg.msg("Check internet connection");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
           

        }

    }
}
