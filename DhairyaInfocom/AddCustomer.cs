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
using System.Drawing.Printing;

namespace DhairyaInfocom
{
    public partial class AddCustomer : UserControl
    {
        string rno_r;
        int no = 0;
        MySqlConnection con;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public AddCustomer()
        {
            InitializeComponent();
            txtUser.Text = login.user_name;
        }

        private  void AddCustomer_Load(object sender, EventArgs e)
        {
            dateCurrent.Text = DateTime.Now.ToShortDateString();
             getReceiptNo();
        }
        private void resetControl()
        {
          
                txtName.Clear();
                txtDevice.Clear();
                txtNote.Clear();
                txtFault.Clear();
                txtNumber.Clear();
                return;
          
        }
        public string getReceiptNo()
        {

            
                int rno = 0;
                try
                {
                    con = new MySqlConnection(cs);
                    string query = "SELECT max(receipt_no) FROM customer limit 1";
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    string a = cmd.ExecuteScalar().ToString();

                    if (a != "")
                    {
                        rno = int.Parse(a);
                    }
                    ++rno;


                    rno_r = rno.ToString();

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
                receiptNo.Text = rno_r;
                return rno_r;
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
             AddData();
        }
        private void AddData()
        {
            
                string name = txtName.Text;
                string device = txtDevice.Text;
                string note = txtNote.Text;
                string fault = txtFault.Text;
                string number = txtNumber.Text;
                string user = txtUser.Text;
                string date = DateTime.Now.ToString("yyyy-MM-dd");


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
                string[] data = { (++no).ToString(), name, device, note, fault, date, number, user };
                ADDdataGridView.Rows.Add(data);
                resetControl();
           
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

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (Char.IsLetter(ch) || ch==8 || ch==32)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            resetControl();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
             insertData();
        }

        private void insertData()
        {
            

                int a = 0;
                try
                {

                    con = new MySqlConnection(cs);
                    con.Open();
                    for (int i = 0; i < ADDdataGridView.Rows.Count; i++)
                    {
                        string query = "insert into customer( Name,Device, Note, Fault,  Number, User,receipt_no) values ( @name, @device, @note, @fault, @number, @user,@receipt_no)";

                        MySqlCommand cmd = new MySqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@name", ADDdataGridView.Rows[i].Cells[1].Value);
                        cmd.Parameters.AddWithValue("@device", ADDdataGridView.Rows[i].Cells[2].Value);
                        cmd.Parameters.AddWithValue("@note", ADDdataGridView.Rows[i].Cells[3].Value);
                        cmd.Parameters.AddWithValue("@fault", ADDdataGridView.Rows[i].Cells[4].Value);
                        cmd.Parameters.AddWithValue("@number", ADDdataGridView.Rows[i].Cells[6].Value);
                        cmd.Parameters.AddWithValue("@user", ADDdataGridView.Rows[i].Cells[7].Value);
                        cmd.Parameters.AddWithValue("@receipt_no", receiptNo.Text);
                        a += cmd.ExecuteNonQuery();
                    }

                    if (a > 0)
                    {
                        ErrorMsg.msg("Data insert successfully");
                        Error er = new Error();
                        er.ShowDialog();
                         getPrint();
                         resetControl();
                        ADDdataGridView.Rows.Clear();
                        no = 0;
                         getReceiptNo();
                        return;
                    }
                    else
                    {
                        ErrorMsg.msg("Data insertion failed");
                        Error er = new Error();
                        er.ShowDialog();
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

        
        private void getPrint()
        {
         
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            

            e.Graphics.DrawString("Dhairya Infocom", new Font("Arial",32,FontStyle.Bold),new SolidBrush(Color.Black),250,20);

            e.Graphics.DrawString("Invoice no : "+ receiptNo.Text, new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black), 30, 100);
            e.Graphics.DrawString("Date : "+ dateCurrent.Text, new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black), 650, 100);


            e.Graphics.DrawString("Name : "+ ADDdataGridView.Rows[0].Cells[1].Value.ToString(), new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black), 30, 200);
            e.Graphics.DrawString("Number : "+ ADDdataGridView.Rows[0].Cells[6].Value.ToString(), new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black), 30, 230);

            //draw line
              //first horizontal limne
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 0, 284, (this.Height * this.Width), 0);
            //second horizontal line
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 0, 335, (this.Height * this.Width), 0);
            //verticle lines
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 100, 284, 100, 500);
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 400, 284, 400, 500);
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 600, 284, 600, 500);
            //drawline end
            e.Graphics.DrawString("NO", new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black), 30, 300);
            e.Graphics.DrawString("ITEM", new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black), 220, 300);
            e.Graphics.DrawString("FAULT", new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black), 470, 300);
            e.Graphics.DrawString("NOTE", new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black), 700, 300);

            int marginTop = 340;
            for (int i=0;i<ADDdataGridView.Rows.Count;i++)
            {
                e.Graphics.DrawString(ADDdataGridView.Rows[i].Cells[0].Value.ToString(), new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black), 30, marginTop);
                e.Graphics.DrawString(ADDdataGridView.Rows[i].Cells[2].Value.ToString(), new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black), 200, marginTop);
                e.Graphics.DrawString(ADDdataGridView.Rows[i].Cells[4].Value.ToString(), new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black), 450, marginTop);
                e.Graphics.DrawString(ADDdataGridView.Rows[i].Cells[3].Value.ToString(), new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black), 680, marginTop);
                marginTop += 30;
            }
           
        }
    }
}
