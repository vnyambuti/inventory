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

namespace IMS
{
    public partial class Customers : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\inventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public Customers()
        {
            InitializeComponent();
            LoadUser();
        }

        public void LoadUser()
        {
            int i = 0;
            dataGridViewCustomers.Rows.Clear();

            cmd = new SqlCommand("SELECT * FROM Customers", sqlConnection);

            sqlConnection.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridViewCustomers.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            sqlConnection.Close();



        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           this.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();
            addCustomer.ShowDialog();
        }

        private void dataGridViewCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //get edit colum
            string colName = dataGridViewCustomers.Columns[e.ColumnIndex].Name;
           
            //check the type of column
            if (colName == "Edit")
            {
                //load userform
                AddCustomer customerManagement = new AddCustomer();
                //set form input values from rows
                customerManagement.textBoxName.Text = dataGridViewCustomers.Rows[e.RowIndex].Cells[1].Value.ToString();

                customerManagement.textBoxPhone.Text = dataGridViewCustomers.Rows[e.RowIndex].Cells[2].Value.ToString();
                customerManagement.button1.Enabled = false;
                customerManagement.button2.Enabled = true;
                customerManagement.button3.Enabled = true;


                customerManagement.ShowDialog();

            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete customer", "Delete Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand("DELETE FROM Customers WHERE phone LIKE '" + dataGridViewCustomers.Rows[e.RowIndex].Cells[2].Value.ToString() + "'", sqlConnection);
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    LoadUser();
                    MessageBox.Show("Customer has been deleted successfully", "DELETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
