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
using MySql.Data.MySqlClient;

namespace DhairyaInfocom
{
    public partial class Edit : Form
    {
        Show show = new Show();
        int _cust_id;
        Error er = new Error();
        MySqlConnection con;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Edit()
        {
            InitializeComponent();
            cmbStatus.Text = "--Select--";
        }

        public void EditUser(int cust_id,string cust_name,string device,string note,string number,string fault,string paid)
        {
            
                _cust_id = cust_id;
                txtNameUP.Text = cust_name;
                txtNote.Text = note;
                txtDevice.Text = device;
                txtNumber.Text = number;
                txtFault.Text = fault;
                txtPaid.Text = paid;
           
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
             updateCustomer();
        }

        private void updateCustomer()
        {
           
                string name = txtNameUP.Text;
                string device = txtDevice.Text;
                string note = txtNote.Text;
                string fault = txtFault.Text;
                string number = txtNumber.Text;
                string date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string status = cmbStatus.Text;
                string query;

                if (name == "")
                {
                    ErrorMsg.msg("Please enter name");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                if (device == "")
                {
                    ErrorMsg.msg("Please enter device");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                if (note == "")
                {
                    note = "No note";
                }
                if (fault == "")
                {
                    ErrorMsg.msg("Please enter fault");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                if (number != "")
                {
                    if (number.Length != 10)
                    {
                        ErrorMsg.msg("Number should be 10 digit");
                        Error er = new Error();
                        er.ShowDialog();
                        return;
                    }
                }
                else
                {
                    ErrorMsg.msg("Please enter number");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                if (status == "" || status == "--Select--")
                {
                    ErrorMsg.msg("Please select status");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }


                try
                {
                    con = new MySqlConnection(cs);
                    query = "update customer set Name=@name,Device=@device,Fault=@fault,Paid=@paid,status=@status,number=@number,Close_date=@close where id = @id";
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@device", device);
                    cmd.Parameters.AddWithValue("@fault", fault);
                    cmd.Parameters.AddWithValue("@paid", Convert.ToInt32(txtPaid.Text));
                    if (status == "Complete")
                    {
                        cmd.Parameters.AddWithValue("@status", 1);
                        cmd.Parameters.AddWithValue("@close", date);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@status", 0);
                        cmd.Parameters.AddWithValue("@close", "-");
                    }
                    cmd.Parameters.AddWithValue("@number", Convert.ToInt64(number));
                    cmd.Parameters.AddWithValue("@id", _cust_id);
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        ErrorMsg.msg("Update successfully");
                        Error er = new Error();
                        er.ShowDialog();
                    }



                }
                catch (MySqlException)
                {
                    MessageBox.Show("No internet connection");
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                        this.Hide();

                    }
                }
            
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (Char.IsDigit(ch) || ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtNameUP_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (Char.IsLetter(ch) || ch == 8 || ch == 32)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             deleteCustomer();
        }

        private void deleteCustomer()
        {
           
                try
                {
                    con = new MySqlConnection(cs);
                    string query = "delete from customer where id=@id";
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", _cust_id);
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        ErrorMsg.msg("Delete successfully");
                        Error er = new Error();
                        er.ShowDialog();
                    }


                }
                catch (MySqlException)
                {
                    MessageBox.Show("No internet connection");
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                        this.Hide();
                    }
                }

        }
    }
}
