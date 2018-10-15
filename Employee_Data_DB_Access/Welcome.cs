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
    public partial class Welcome : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Austin\\EmployeeInfo\\EmployeeInfoDB.mdf;Integrated Security=True;Connect Timeout=30");

        public Welcome()
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new EDA().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new SearchUpdateDelete().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                MessageBox.Show("Connection to DataBase Succsessful");
                con.Close();
            }
            catch
            {
                MessageBox.Show("Connection to DataBase Failed \n Contact IT Team for support");
            }
        }
    }
}
