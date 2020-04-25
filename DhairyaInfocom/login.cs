using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;

namespace DhairyaInfocom
{
    public partial class login : Form
    {


        public static string user_name;
        public static int id;
        public static string last_login;
        public static string user_type;

        MySqlConnection con;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public login()
        {
            InitializeComponent();

        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private  void btnLogin_ClickAsync(object sender, EventArgs e)
        {

            string username = txtUsername.Text;
            string password = txtPassword.Text;
            if (username != "")
            {
                if (password != "")
                {
                    loginForm(username, password);
                }
                else
                {
                    ErrorMsg.msg("Enter password");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
            }
            else
            {
                ErrorMsg.msg("Enter username");
                Error er = new Error();
                er.ShowDialog();
                return;
            }
        }
        private void loginForm(string user, string pass)
        {
           
                con = new MySqlConnection(cs);
               string dd = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
               string query = "SELECT * FROM user where binary username=@user and binary password=@pass limit 1";
               con.Open();
               try
               {
                   MySqlCommand cmd = new MySqlCommand(query, con);
                   cmd.Parameters.AddWithValue("@user", user);
                   cmd.Parameters.AddWithValue("@pass", pass);

                   MySqlDataReader dr = cmd.ExecuteReader();
                   dr.Read();
                   if (dr.HasRows)
                   {


                       if (dr.GetInt32(5) == 0)
                       {
                           user_name = dr.GetString(1);
                           id = dr.GetInt32(0);
                           last_login = dr.GetString(4);
                           user_type = dr.GetString(3);


                           MySqlConnection con2 = new MySqlConnection(cs);
                           con2.Open();
                           string query1 = "update user set last_login=@dt";
                           MySqlCommand cd = new MySqlCommand(query1, con2);
                           cd.Parameters.AddWithValue("dt", dd);

                           int a = cd.ExecuteNonQuery();

                           if (a > 0)
                           {
                               Form1 f1 = new Form1();
                               this.Hide();
                               f1.Show();

                           }
                           con2.Close();



                       }
                       else
                       {
                           ErrorMsg.msg("You are blocked");
                           Error er = new Error();
                           er.ShowDialog();
                           return;
                       }
                   }
                   else
                   {
                       ErrorMsg.msg("Enter valid credential");
                       Error er = new Error();
                       er.ShowDialog();
                       return;

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
