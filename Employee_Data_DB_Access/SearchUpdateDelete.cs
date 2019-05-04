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

namespace Employee_Data_DB_Access
{
    public partial class SearchUpdateDelete : Form
    {
        //Allow for draggable window
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        string SEARCHSELECT = "NA";
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Austin\\EmployeeInfo\\EmployeeInfoDB.mdf;Integrated Security=True;Connect Timeout=30");

        public SearchUpdateDelete()
        {
            InitializeComponent();
        }

        private void SearchUpdateDelete_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'employeeInfoDBDataSet.Employee' table. You can move, or remove it, as needed.
            //this.employeeTableAdapter.Fill(this.employeeInfoDBDataSet.Employee);
            refreshTable();

        }

        //Search for the user
        private void button4_Click(object sender, EventArgs e)
        {
            string searchCmd = "";

            if (SEARCHSELECT.Equals("ID"))
            {
                searchCmd = "SELECT * FROM Employee WHERE Employee_ID = " + textBox1.Text;
            }
            else if (SEARCHSELECT.Equals("first_name"))
            {
                searchCmd = "SELECT * FROM Employee WHERE First_Name = '" + textBox2.Text + "'";
            }
            else if (SEARCHSELECT.Equals("last_name"))
            {
                searchCmd = "SELECT * FROM Employee WHERE Last_Name = '" + textBox3.Text + "'";
            }
            else if (SEARCHSELECT.Equals("position"))
            {
                searchCmd = "SELECT * FROM Employee WHERE Position = '" + textBox4.Text + "'";
            }
            else if (SEARCHSELECT.Equals("phone"))
            {
                searchCmd = "SELECT * FROM Employee WHERE Phone_Num = '" + textBox5.Text + "'";
            }
            else if (SEARCHSELECT.Equals("address"))
            {
                searchCmd = "SELECT * FROM Employee WHERE Address = '" + textBox6.Text + "'";
            }
            else if (SEARCHSELECT.Equals("state"))
            {
                searchCmd = "SELECT * FROM Employee WHERE State = '" + textBox7.Text + "'";
            }
            else if (SEARCHSELECT.Equals("zip"))
            {
                searchCmd = "SELECT * FROM Employee WHERE Zip_Code = '" + textBox8.Text + "'";
            }
            else if (SEARCHSELECT.Equals("start"))
            {
                searchCmd = "SELECT * FROM Employee WHERE Start_Date = '" + textBox9.Text + "'";
            }
            else
            {
                MessageBox.Show("Must Select Field to Search By");
            }

            try
            {
                SqlDataAdapter SDA = new SqlDataAdapter(searchCmd, con);
                DataSet ds = new DataSet();

                con.Open();
                SDA.Fill(ds, "SearchResult");
                con.Close();
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "SearchResult";
            }
            catch
            {
                MessageBox.Show("Employee Not Found");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                SEARCHSELECT = "ID";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                SEARCHSELECT = "first_name";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                SEARCHSELECT = "last_name";
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                SEARCHSELECT = "position";
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                SEARCHSELECT = "phone";
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                SEARCHSELECT = "address";
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                SEARCHSELECT = "state";
            }
            else if (comboBox1.SelectedIndex == 7)
            {
                SEARCHSELECT = "zip";
            }
            else if (comboBox1.SelectedIndex == 8)
            {
                SEARCHSELECT = "start_date";
            }
        }

        //Delete the Employee
        private void button3_Click(object sender, EventArgs e)
        {
            string deleteSearchCmd = "SELECT COUNT(1) FROM Employee WHERE Employee_ID = " + textBox1.Text;
            string deleteCmd = "DELETE FROM Employee WHERE Employee_ID = " + textBox1.Text;
            SqlCommand deleteSearchObj = new SqlCommand(deleteSearchCmd, con);
            SqlCommand comm = new SqlCommand(deleteCmd, con);

            try
            {
                con.Open();
                int result =  (int)deleteSearchObj.ExecuteScalar();
                con.Close();
                if(result == 0)
                {
                    MessageBox.Show("Could Not Find Employee to Delete");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Error Looking for Employee to delete");
                return;
            }

            try
            {
                con.Open();
                comm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Employee Deleted");
                refreshTable();
            }
            catch
            {
                MessageBox.Show("Error Deleting Employee");
            }
        }

        //Refresh the table
        public void refreshTable()
        {
            DataSet refreshDs = new DataSet();
            string refreshCmd = "SELECT * FROM Employee";
            SqlDataAdapter SDA = new SqlDataAdapter(refreshCmd, con);
            try
            {
                con.Open();
                SDA.SelectCommand.ExecuteReader();
                con.Close();
                SDA.Fill(refreshDs, "SearchResult");
                dataGridView1.DataSource = refreshDs;
                dataGridView1.DataMember = "SearchResult";
            }
            catch
            {
                MessageBox.Show("Error refreshing Database table");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshTable();
        }

        //Update employee
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string updateText = "UPDATE Employee SET ";

                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    updateText += "First_Name = '" + textBox2.Text + "'";
                }
                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    updateText += ", Last_Name = '" + textBox3.Text + "'";
                }
                if (!string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    updateText += ", Position = '" + textBox4.Text + "'";
                }
                if (!string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    updateText += ", Phone_Num = '" + textBox5.Text + "'";
                }
                if (!string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    updateText += ", Address = '" + textBox6.Text + "'";
                }
                if (!string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    updateText += ", State = '" + textBox7.Text + "'";
                }
                if (!string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    updateText += ", Zip_Code = '" + textBox8.Text + "'";
                }
                if (!string.IsNullOrWhiteSpace(textBox9.Text))
                {
                    updateText += ", Start_Date = '" + textBox9.Text + "'";
                }

                if(updateText.Equals("UPDATE Employee SET "))
                {
                    MessageBox.Show("No Info added to be Updated");
                    return;
                }
                updateText += " WHERE Employee_ID = '" + textBox1.Text + "'";

                SqlCommand updateCmd = new SqlCommand(updateText, con);

                con.Open();
                updateCmd.ExecuteNonQuery();
                con.Close();

                refreshTable();
                MessageBox.Show("Employee Updated");
            }
            catch
            {
                MessageBox.Show("Error Updating Employee");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = "", firstName = "", lastName = "", position = "", phoneNum = "", address = "", state = "", zip = "", startDate = "";
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            //Pull the data out of the row
            id = Convert.ToString(selectedRow.Cells[0].Value);
            firstName = Convert.ToString(selectedRow.Cells[1].Value);
            lastName = Convert.ToString(selectedRow.Cells[2].Value);
            position = Convert.ToString(selectedRow.Cells[3].Value);
            phoneNum = Convert.ToString(selectedRow.Cells[4].Value);
            address = Convert.ToString(selectedRow.Cells[5].Value);
            state = Convert.ToString(selectedRow.Cells[6].Value);
            zip = Convert.ToString(selectedRow.Cells[7].Value);
            startDate = Convert.ToString(selectedRow.Cells[8].Value);

            //Set the text fields to the correct data
            textBox1.Text = id;
            textBox2.Text = firstName;
            textBox3.Text = lastName;
            textBox4.Text = position;
            textBox5.Text = phoneNum;
            textBox6.Text = address;
            textBox7.Text = state;
            textBox8.Text = zip;
            textBox9.Text = startDate;
        }

        //Add new employee
        private void button5_Click(object sender, EventArgs e)
        {
            //The name shouldn't really be EDA but I can go back to that later
            new EDA().Show();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void SearchUpdateDelete_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
