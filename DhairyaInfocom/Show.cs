using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Threading;

namespace DhairyaInfocom
{

   
    public partial class Show : UserControl
    {
        public static int cust_id;
        Error er = new Error();
        MySqlConnection con;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Show()
        {   
            InitializeComponent();
        }

      
        
        public void getData()
        {
           
                SHOWdataGridView.Rows.Clear();
                try
                {
                    con = new MySqlConnection(cs);
                    string query = "SELECT * FROM customer order by id desc limit 15";
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {


                        while (dr.Read())
                        {
                            string[] data = { dr.GetInt32(10).ToString(), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetInt32(6).ToString(), dr.GetInt64(7).ToString(), dr.GetString(8), dr.GetString(9), dr.GetInt32(0).ToString() };
                            SHOWdataGridView.Rows.Add(data);

                        }
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
                    }
                }
           

        }

       

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchCustomer();
        }

        private void SearchCustomer()
        {
                string searchBox = txtSearch.Text;
                string query;
                if (searchBox == "" || searchBox == null)
                {
                    ErrorMsg.msg("Please enter receipt no");
                    Error er = new Error();
                    er.ShowDialog();
                }
                else
                {
                    try
                    {
                        con = new MySqlConnection(cs);

                        if (Regex.IsMatch(searchBox, @"^\d+$"))
                        {
                            query = "SELECT * FROM customer where receipt_no = @name";

                        }
                        else
                        {
                            query = "SELECT * FROM customer where Name like @name";
                        }
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand(query, con);
                        if (Regex.IsMatch(searchBox, @"^\d+$"))
                        {
                            cmd.Parameters.AddWithValue("@name", Convert.ToInt32(searchBox));
                        }
                        else
                        {

                            cmd.Parameters.AddWithValue("@name", searchBox + "%");
                        }
                        MySqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            SHOWdataGridView.Rows.Clear();
                            while (dr.Read())
                            {
                                string[] data = { dr.GetInt32(10).ToString(), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetInt32(6).ToString(), dr.GetInt64(7).ToString(), dr.GetString(8), dr.GetString(9), dr.GetInt32(0).ToString() };
                                SHOWdataGridView.Rows.Add(data);

                            }
                        }
                        else
                        {
                            ErrorMsg.msg("No data found");
                            Error er = new Error();
                            er.ShowDialog();
                        }



                    }
                    catch (MySqlException)
                    {
                        MessageBox.Show("Connection failed");
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

        private  void searchByDate_Click(object sender, EventArgs e)
        {
            DateTime datefrom = dateFrom.Value;
            DateTime dateto = dateTo.Value;

            if (dateto.ToString()!="" || dateto!=null)
            {
                if (datefrom.ToString()!="" || datefrom != null) 
                {
                    if (dateTo.Value >= dateFrom.Value)
                    {
                      SearchByDateFun();
                    }
                    else
                    {
                        ErrorMsg.msg("Invalid date selection");
                        Error er = new Error();
                        er.ShowDialog();
                    }
                }
                else
                {
                    ErrorMsg.msg("Enter first date");
                    Error er = new Error();
                    er.ShowDialog();
                }
            }
            else
            {
                ErrorMsg.msg("Enter first date");
                Error er = new Error();
                er.ShowDialog();
            }
        }

       private void SearchByDateFun()
        {
           
                try
                {
                    con = new MySqlConnection(cs);
                    string query = "SELECT * FROM customer WHERE RDate BETWEEN @from AND @to";
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@from", dateFrom.Value.ToString("yyyy-MM-dd hh:mm:ss"));
                    cmd.Parameters.AddWithValue("@to", dateTo.Value.ToString("yyyy-MM-dd hh:mm:ss"));
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        SHOWdataGridView.Rows.Clear();
                        while (dr.Read())
                        {
                            string[] data = { dr.GetInt32(10).ToString(), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5).ToString(), dr.GetInt32(6).ToString(), dr.GetInt64(7).ToString(), dr.GetString(8), dr.GetString(9), dr.GetInt32(0).ToString() };
                            SHOWdataGridView.Rows.Add(data);

                        }
                    }
                    else
                    {
                        ErrorMsg.msg("No data found");
                        Error er = new Error();
                        er.ShowDialog();
                    }

                    //SHOWdataGridView.DataSource = dt;

                }
                catch (MySqlException)
                {
                    MessageBox.Show("Connection failed");
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text == "")
            {
               getData();
            }
        }

        private void SHOWdataGridView_DoubleClick(object sender, EventArgs e)
        {

             selectDataFromGrid();
        }

        private void selectDataFromGrid()
        {
                if (SHOWdataGridView.Rows.Count > 0)
                {
                    cust_id = Convert.ToInt32(SHOWdataGridView.SelectedRows[0].Cells[10].Value);
                    string cust_name = SHOWdataGridView.SelectedRows[0].Cells[1].Value.ToString();
                    string device = SHOWdataGridView.SelectedRows[0].Cells[2].Value.ToString();
                    string note = SHOWdataGridView.SelectedRows[0].Cells[3].Value.ToString();
                    string fault = SHOWdataGridView.SelectedRows[0].Cells[4].Value.ToString();
                    string number = SHOWdataGridView.SelectedRows[0].Cells[7].Value.ToString();
                    string paid = SHOWdataGridView.SelectedRows[0].Cells[6].Value.ToString();
                    Edit ed = new Edit();
                     ed.EditUser(cust_id, cust_name, device, note, number, fault, paid);
                     ed.ShowDialog();
                     getData();
                }
           
        }
    }

    public static class ErrorMsg
    {
        public static string errName;
        public static string msg(string msg)
        {
            errName = msg;
            return errName;
        }
     
    }

}
