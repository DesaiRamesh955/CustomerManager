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
    public  partial  class Replace : UserControl
    {
        MySqlConnection con;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
       public Replace()
        {
            
            InitializeComponent();
            txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtUser.Text = login.user_name;
        }

        //get to destination
        private void getToDestination()
        {
            
                con = new MySqlConnection(cs);

                string query = "SELECT * FROM `to_tbl` ORDER BY to_name ASC";
                con.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        cmbTo.Items.Clear();
                        cmbEditTo.Items.Clear();
                        while (dr.Read())
                        {
                            cmbTo.Items.Add(dr.GetString(1));
                            cmbEditTo.Items.Add(dr.GetString(1));
                            string[] salerData = { dr.GetInt32(0).ToString(), dr.GetString(1) };
                            addSALERdatagrid.Rows.Add(salerData);
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

        //get from destination

        private void getFromDestination()
        {
           
                con = new MySqlConnection(cs);

                string query = "SELECT * FROM `from_tbl` ORDER BY from_name ASC";
                con.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cmbFrom.Items.Add(dr.GetString(1));
                            cmbEditFrom.Items.Add(dr.GetString(1));
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
        private void reset()
        {
            
                txtCustName.Clear();
                txtDevice.Clear();
                txtSrNo.Text = "0000";
                cmbFrom.SelectedIndex = -1;
                cmbTo.SelectedIndex = -1;
                cmbFrom.Text = "--Select--";
                cmbTo.Text = "--Select--";
                txtNote.Clear();
                txtFault.Clear();
                txtNumber.Clear();
                txtCourierName.Clear();
                txtDocket.Text = "No number";

           
        }



        private void resetEdit()
        {
                txtEditName.Clear();
                txtEditDevice.Clear();
                txtEditSR.Text = "0000";
                cmbEditFrom.SelectedIndex = -1;
                cmbEditTo.SelectedIndex = -1;
                cmbEditFrom.Text = "--Select--";
                cmbEditTo.Text = "--Select--";
                txtEditNote.Clear();
                txtEDitFault.Clear();
                txtEditNumber.Clear();
                txtEditCourier.Clear();
                txtEditDocket.Text = "No number";
                cmbStatus.SelectedIndex = -1;
                cmbCustStatus.SelectedIndex = -1;
                cmbStatus.Text = "--Select--";
                cmbCustStatus.Text = "--Select--";
                laneknfd.Text = "Replacement No : ";
        }

        public void getReplaceDataEdit()
        {
            
                con = new MySqlConnection(cs);
                EDITreplacementData.Rows.Clear();
                string query = "SELECT * FROM replacement";
                con.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            string[] data = { dr.GetString(1), dr.GetString(2), dr.GetString(8), dr.GetString(3), dr.GetString(9), dr.GetString(7), dr.GetString(10), dr.GetString(13), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(14), dr.GetInt32(0).ToString(), dr.GetInt32(11).ToString(), dr.GetInt32(12).ToString() };
                            EDITreplacementData.Rows.Add(data);
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

        /// <summary>
        /// get compelet replace data
        /// </summary>
        public void getComplete()
        {
            
                con = new MySqlConnection(cs);
                COMPLETEdatagrid.Rows.Clear();
                string query = "SELECT * FROM replacement where status_replace = 1";
                con.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            string[] data = { dr.GetString(1), dr.GetString(2), dr.GetString(8), dr.GetString(3), dr.GetString(9), dr.GetString(7), dr.GetString(10), dr.GetString(13), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(14), dr.GetInt32(0).ToString(), dr.GetInt32(11).ToString(), dr.GetInt32(12).ToString() };
                            COMPLETEdatagrid.Rows.Add(data);
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

        public void getPending()
        {
           

                con = new MySqlConnection(cs);
                PENDINGdatagrid.Rows.Clear();
                string query = "SELECT * FROM replacement where status_replace = 0";
                con.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            string[] data = { dr.GetString(1), dr.GetString(2), dr.GetString(8), dr.GetString(3), dr.GetString(9), dr.GetString(7), dr.GetString(10), dr.GetString(13), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(14), dr.GetInt32(0).ToString(), dr.GetInt32(11).ToString(), dr.GetInt32(12).ToString() };
                            PENDINGdatagrid.Rows.Add(data);
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
        private  void btnAdd_Click(object sender, EventArgs e)
        {
             addToGrid();
        }

        private void addToGrid()
        {
           
                string custName = txtCustName.Text;
                string device = txtDevice.Text;
                string serial_no = txtSrNo.Text;
                string from = cmbFrom.Text;
                string to = cmbTo.Text;
                string note = txtNote.Text;
                string fault = txtFault.Text;
                string date = txtDate.Text;
                string number = txtNumber.Text;
                string courier = txtCourierName.Text;
                string docket = txtDocket.Text;
                string user = txtUser.Text;

                //valid customer name
                if (custName == "")
                {
                    ErrorMsg.msg("Please enter customer name");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                //valid device

                if (device == "")
                {
                    ErrorMsg.msg("Please enter device");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                //valid courier

                if (courier == "")
                {
                    ErrorMsg.msg("Please enter courier name");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                //valid srno

                if (serial_no == "")
                {
                    ErrorMsg.msg("Please enter serial number");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                //valid from

                if (from == "" || from == "--Select--")
                {
                    ErrorMsg.msg("Please select from destination");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                //valid to

                if (to == "" || to == "--Select--")
                {
                    ErrorMsg.msg("Please select to destination");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }


                //valid fault

                if (fault == "")
                {
                    ErrorMsg.msg("Please enter fault");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }


                //valid number

                if (number != "")
                {
                    if (number.Length != 10)
                    {
                        ErrorMsg.msg("Please enter valid number");
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



                string[] data = { custName, device, note, serial_no, fault, date, number, user, courier, from, to, docket };

                ADDreplacementData.Rows.Add(data);
                reset();
                return;
           
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(Char.IsDigit(ch) || ch==8)
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
             reset();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
             reset();
            ADDreplacementData.Rows.Clear();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            insertReplacement();
        }

        private void insertReplacement()
        {
           
                int a = 0;

                try
                {
                    con = new MySqlConnection(cs);
                    con.Open();
                    for (int i = 0; i < ADDreplacementData.Rows.Count; i++)
                    {
                        string query = "INSERT INTO replacement(cust_name, device, serial_no, courier_name,where_from, where_to, date, note, fault, number,user,docket) VALUES(@cust_name,@device, @serial_no, @courier_name, @where_from, @where_to, @date, @note, @fault, @number,@user,@docket)";
                        MySqlCommand cmd = new MySqlCommand(query, con);


                        cmd.Parameters.AddWithValue("@cust_name", ADDreplacementData.Rows[i].Cells[0].Value);
                        cmd.Parameters.AddWithValue("@device", ADDreplacementData.Rows[i].Cells[1].Value);
                        cmd.Parameters.AddWithValue("@note", ADDreplacementData.Rows[i].Cells[2].Value);
                        cmd.Parameters.AddWithValue("@serial_no", ADDreplacementData.Rows[i].Cells[3].Value);
                        cmd.Parameters.AddWithValue("@fault", ADDreplacementData.Rows[i].Cells[4].Value);
                        cmd.Parameters.AddWithValue("@date", ADDreplacementData.Rows[i].Cells[5].Value);
                        cmd.Parameters.AddWithValue("@number", ADDreplacementData.Rows[i].Cells[6].Value);
                        cmd.Parameters.AddWithValue("@user", ADDreplacementData.Rows[i].Cells[7].Value);
                        cmd.Parameters.AddWithValue("@courier_name", ADDreplacementData.Rows[i].Cells[8].Value);
                        cmd.Parameters.AddWithValue("@where_from", ADDreplacementData.Rows[i].Cells[9].Value);
                        cmd.Parameters.AddWithValue("@where_to", ADDreplacementData.Rows[i].Cells[10].Value);
                        cmd.Parameters.AddWithValue("@docket", ADDreplacementData.Rows[i].Cells[11].Value);

                        a += cmd.ExecuteNonQuery();

                    }

                    if (a > 0)
                    {
                        ErrorMsg.msg("Insert successfuly");
                        Error er = new Error();
                        er.ShowDialog();
                         reset();
                         getReplaceDataEdit();
                        ADDreplacementData.Rows.Clear();
                    }
                    else
                    {
                        ErrorMsg.msg("Insert failed");
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

        private void EDITreplacementData_DoubleClick(object sender, EventArgs e)
        {
             fetchFromGrid();
        }

        private void fetchFromGrid()
        {
                if (EDITreplacementData.SelectedRows.Count > 0)
                {
                    txtEditName.Text = EDITreplacementData.SelectedRows[0].Cells[0].Value.ToString();
                    txtEditDevice.Text = EDITreplacementData.SelectedRows[0].Cells[1].Value.ToString();
                    txtEditNote.Text = EDITreplacementData.SelectedRows[0].Cells[2].Value.ToString();
                    txtEditSR.Text = EDITreplacementData.SelectedRows[0].Cells[3].Value.ToString();
                    txtEDitFault.Text = EDITreplacementData.SelectedRows[0].Cells[4].Value.ToString();
                    txtEditNumber.Text = EDITreplacementData.SelectedRows[0].Cells[6].Value.ToString();
                    txtEditCourier.Text = EDITreplacementData.SelectedRows[0].Cells[8].Value.ToString();
                    cmbEditFrom.Text = "";
                    cmbEditTo.Text = "";
                    cmbEditFrom.SelectedText = EDITreplacementData.SelectedRows[0].Cells[9].Value.ToString();
                    cmbEditTo.SelectedText = EDITreplacementData.SelectedRows[0].Cells[10].Value.ToString();
                    txtEditDocket.Text = EDITreplacementData.SelectedRows[0].Cells[11].Value.ToString();



                    cmbStatus.Text = "";
                    cmbCustStatus.Text = "";


                    if (EDITreplacementData.SelectedRows[0].Cells[13].Value.ToString() == "0")
                    {

                        cmbStatus.SelectedText = "Pending";
                    }
                    else
                    {
                        cmbStatus.SelectedText = "Complete";
                    }

                    if (EDITreplacementData.SelectedRows[0].Cells[14].Value.ToString() == "0")
                    {

                        cmbCustStatus.SelectedText = "Pending";
                    }
                    else
                    {
                        cmbCustStatus.SelectedText = "Complete";
                    }


                    ReplaceNumber.Text = EDITreplacementData.SelectedRows[0].Cells[12].Value.ToString();



                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           resetEdit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             deleteReplacement();
        }

        private void deleteReplacement()
        {
                int id;
                if (ReplaceNumber.Text == "")
                {
                    ErrorMsg.msg("Please select any row");
                    Error er = new Error();
                    er.ShowDialog();
                    return;

                }
                else
                {
                    id = Convert.ToInt32(ReplaceNumber.Text);
                }
                con = new MySqlConnection(cs);

                string query = "delete from replacement where id=@id";
                con.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        ErrorMsg.msg("Deleted successfully");
                        Error er = new Error();
                        er.ShowDialog();
                        resetEdit();
                        ReplaceNumber.Text = "";
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateReplacement();
            getComplete();
            getPending();

        }

        private void updateReplacement()
        {
                string name = txtEditName.Text;
                string device = txtEditDevice.Text;
                string note = txtEditNote.Text;
                string sr = txtEditSR.Text;
                string fault = txtEDitFault.Text;
                string number = txtEditNumber.Text;
                string courier = txtEditCourier.Text;
                string from = cmbEditFrom.Text;
                string to = cmbEditTo.Text;
                string docket = txtEditDocket.Text;
                string status = cmbStatus.Text;
                string cust_status = cmbCustStatus.Text;
                int _status;
                int _cust_status;
                int id = 0;
                if (ReplaceNumber.Text != "")
                {

                    id = Convert.ToInt32(ReplaceNumber.Text);
                }
                else
                {
                    ErrorMsg.msg("Please select any row");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }


                //valid customer name
                if (name == "")
                {
                    ErrorMsg.msg("Please enter customer name");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                //valid device

                if (device == "")
                {
                    ErrorMsg.msg("Please enter device");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                //valid courier

                if (courier == "")
                {
                    ErrorMsg.msg("Please enter courier name");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                //valid srno

                if (sr == "")
                {
                    ErrorMsg.msg("Please enter serial number");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                //valid from

                if (from == "" || from == "--Select--")
                {
                    ErrorMsg.msg("Please select from destination");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                //valid to

                if (to == "" || to == "--Select--")
                {
                    ErrorMsg.msg("Please select to destination");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }


                //valid fault

                if (fault == "")
                {
                    ErrorMsg.msg("Please enter fault");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }


                //valid number

                if (number != "")
                {
                    if (number.Length != 10)
                    {
                        ErrorMsg.msg("Please enter valid number");
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


                //valid docket

                if (docket == "")
                {
                    ErrorMsg.msg("Please enter docket");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }

                //valid status

                if (status == "" || status == "--Select--")
                {
                    ErrorMsg.msg("Please select status");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                else
                {
                    if (status == "Pending")
                    {
                        _status = 0;
                    }
                    else
                    {
                        _status = 1;
                    }
                }

                //valid status

                if (cust_status == "" || cust_status == "--Select--")
                {
                    ErrorMsg.msg("Please select customer status");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                else
                {
                    if (cust_status == "Pending")
                    {
                        _cust_status = 0;
                    }
                    else
                    {
                        _cust_status = 1;
                    }
                }

                try
                {
                    con = new MySqlConnection(cs);
                    con.Open();
                    string query = "UPDATE `replacement` SET `cust_name`= @name,`device`=@device,`serial_no`=@sr,`courier_name`= @courier,`where_from`=@from,`where_to`=@to,`note`=@note,`fault`=@fault,`number`=@number,`status_replace`=@status_replace,`status_cust`=@status_cust,`user`=@user,`docket`=@docket where id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@device", device);
                    cmd.Parameters.AddWithValue("@sr", sr);
                    cmd.Parameters.AddWithValue("@courier", courier);
                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to", to);
                    cmd.Parameters.AddWithValue("@note", note);
                    cmd.Parameters.AddWithValue("@fault", fault);
                    cmd.Parameters.AddWithValue("@number", Convert.ToInt64(number));
                    cmd.Parameters.AddWithValue("@status_replace", _status);
                    cmd.Parameters.AddWithValue("@status_cust", _cust_status);
                    cmd.Parameters.AddWithValue("@user", login.user_name);
                    cmd.Parameters.AddWithValue("@docket", docket);
                    cmd.Parameters.AddWithValue("@id", id);

                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        ErrorMsg.msg("Update successfully");
                        Error er = new Error();
                        er.ShowDialog();
                        resetEdit();

                    }
                     getPending();
                     getComplete();
                }
                catch (MySqlException ex)
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

        private void txtEditNumber_KeyPress(object sender, KeyPressEventArgs e)
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
        //search complete replacement
        private void btnCompleteSearch_Click(object sender, EventArgs e)
        {
           searchReplacement();
        }

        private void searchReplacement()
        {
                string searchComplete = txtCompleteSearch.Text;
                if (searchComplete == "")
                {
                    ErrorMsg.msg("Please enter search name");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }


                con = new MySqlConnection(cs);

                string query = "select * from replacement where cust_name like @name";
                con.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", searchComplete + "%");
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        COMPLETEdatagrid.Rows.Clear();
                        while (dr.Read())
                        {
                            string[] data = { dr.GetString(1), dr.GetString(2), dr.GetString(8), dr.GetString(3), dr.GetString(9), dr.GetString(7), dr.GetString(10), dr.GetString(13), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(14), dr.GetInt32(0).ToString(), dr.GetInt32(11).ToString(), dr.GetInt32(12).ToString() };
                            COMPLETEdatagrid.Rows.Add(data);
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

        private void btnPending_Click(object sender, EventArgs e)
        {
            searchPending();
        }

        private void searchPending()
        {
                string searchPending = txtPendingSearch.Text;
                if (searchPending == "")
                {
                    ErrorMsg.msg("Please enter search name");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }


                con = new MySqlConnection(cs);

                string query = "select * from replacement where cust_name like @name";
                con.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", searchPending + "%");
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        PENDINGdatagrid.Rows.Clear();
                        while (dr.Read())
                        {
                            string[] data = { dr.GetString(1), dr.GetString(2), dr.GetString(8), dr.GetString(3), dr.GetString(9), dr.GetString(7), dr.GetString(10), dr.GetString(13), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(14), dr.GetInt32(0).ToString(), dr.GetInt32(11).ToString(), dr.GetInt32(12).ToString() };
                            PENDINGdatagrid.Rows.Add(data);
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

        private void tabControl1_Click(object sender, EventArgs e)
        {
           // getComplete();
           // getPending();
        }

       

      

        private void txtCompleteSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCompleteSearch.Text == "")
            {
               getComplete();
            }
        }

        private void txtPendingSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPendingSearch.Text == "")
            {
                 getPending();
            }
        }

       

        private void addSaler()
        {
                string saler = txtSaler.Text;
                if (saler == "")
                {
                    ErrorMsg.msg("Enter name");
                    Error er = new Error();
                    er.ShowDialog();
                    return;
                }
                con = new MySqlConnection(cs);

                string query = "insert into to_tbl (to_name) values (@saler)";
                con.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@saler", saler);
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        ErrorMsg.msg("Add successfully");
                        Error er = new Error();
                        er.ShowDialog();

                        txtSaler.Text = "";
                        addSALERdatagrid.Rows.Clear();
                        getToDestination();
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

        private void btnAddSaler_Click(object sender, EventArgs e)
        {
           addSaler();
        }

        private void Replace_Load(object sender, EventArgs e)
        {
          getToDestination();
          getFromDestination();
          getReplaceDataEdit();
          getComplete();
          getPending();
        }
    }
}
