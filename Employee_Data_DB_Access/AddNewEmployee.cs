using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employee_Data_DB_Access
{
    public partial class EDA : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Austin\\EmployeeInfo\\EmployeeInfoDB.mdf;Integrated Security=True;Connect Timeout=30");

        public EDA()
        {
            InitializeComponent();

        }

        private void EDA_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'employeeInfoDBDataSet.Employee' table. You can move, or remove it, as needed.
           // this.employeeTableAdapter.Fill(this.employeeInfoDBDataSet.Employee);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string addCmd = "INSERT INTO Employee (First_Name, Last_Name, Position, Phone_Num, Address, State, Zip_Code, Start_Date) VALUES ( @Val2, @Val3, @Val4, @Val5, @Val6, @Val7, @Val8, @Val9)";
            try
            {
                SqlCommand Comm = new SqlCommand(addCmd, con);
                Comm.Connection = con;
                Comm.CommandText = addCmd;
                Comm.Parameters.AddWithValue("@Val2", textBox2.Text);
                Comm.Parameters.AddWithValue("@Val3", textBox3.Text);
                Comm.Parameters.AddWithValue("@Val4", textBox4.Text);
                Comm.Parameters.AddWithValue("@Val5", textBox5.Text);
                Comm.Parameters.AddWithValue("@Val6", textBox6.Text);
                Comm.Parameters.AddWithValue("@Val7", textBox7.Text);
                Comm.Parameters.AddWithValue("@Val8", textBox8.Text);
                Comm.Parameters.AddWithValue("@Val9", textBox9.Text);

                con.Open();
                Comm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Employee Added Sucessfuly");
            }
            catch(SqlException f)
            {
                MessageBox.Show("Error Adding new employee \n " + f);
            }
        }

    }
}
