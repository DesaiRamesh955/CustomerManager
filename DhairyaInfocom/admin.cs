using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DhairyaInfocom
{
    public partial class admin : UserControl
    {
        MySqlConnection con;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        
        public admin()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            changePassword();
        }

        private void changePassword()
        {
            

                string old_pass = txtOldPass.Text;
                string new_pass = txtNewPass.Text;
                string confirm_pass = txtConfirmPass.Text;

                if (old_pass == "")
                {
                    ErrorMsg.msg("Please enter old password");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                if (new_pass == "")
                {
                    ErrorMsg.msg("Please enter new password");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                if (confirm_pass == "")
                {
                    ErrorMsg.msg("Please enter confirm password");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                if (confirm_pass != new_pass)
                {
                    ErrorMsg.msg("Password does not match");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                con = new MySqlConnection(cs);
                string query = "select password from user where id=@id";
                con.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", login.id);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();

                        string password_fetch = dr.GetString(0);

                        if (old_pass == password_fetch)
                        {
                            MySqlConnection con2 = new MySqlConnection(cs);
                            con2.Open();
                            string query1 = "update user set password = @pass_new where id=@id";
                            MySqlCommand cd = new MySqlCommand(query1, con2);
                            cd.Parameters.AddWithValue("@pass_new", new_pass);
                            cd.Parameters.AddWithValue("@id", login.id);
                            int a = cd.ExecuteNonQuery();
                            if (a > 0)
                            {
                                ErrorMsg.msg("Password change successfully!!");
                                Error er = new Error();
                                er.ShowDialog();
                                Application.Exit();


                            }
                            con2.Close();
                        }
                        else
                        {
                            ErrorMsg.msg("Invalid credential");
                            Error er = new Error();
                            er.ShowDialog();

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

        private void txtWorkSubmit_Click(object sender, EventArgs e)
        {
             addWork();
        }

        private void addWork()
        {
            
                string work = txtWork.Text;
                string date = DateTime.Now.ToString("dd-MM-yyyy hh:mm");

                if (work == "")
                {
                    ErrorMsg.msg("Enter work first");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                con = new MySqlConnection(cs);
                string query = "insert into user_work(user,user_work,date)values(@user,@work,@date)";
                con.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user", login.id);
                    cmd.Parameters.AddWithValue("@work", work);
                    cmd.Parameters.AddWithValue("@date", date);

                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        ErrorMsg.msg("Work added!!");
                        Error er = new Error();
                        er.ShowDialog();
                        txtWork.Clear();
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
