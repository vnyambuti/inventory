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
    public partial class AddCustomer : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\inventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
     
        public Customers customers { get; set; }
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this customer", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO Customers(name,phone)VALUES(@fullname,@phone)", sqlConnection);
                    cmd.Parameters.AddWithValue("@fullname", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@phone", textBoxPhone.Text);
                  

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                  
                    MessageBox.Show("Customer has been saved successfully");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxName.Clear();
            textBoxPhone.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this customer","Customer Update",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE Customers set name=@name,phone=@phone WHERE phone LIKE '"+textBoxPhone.Text+"'", sqlConnection);
                    cmd.Parameters.AddWithValue("@name",textBoxName.Text);
                    cmd.Parameters.AddWithValue("@phone", textBoxPhone.Text);

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    
                    MessageBox.Show("Customer Updated","Customer Update",MessageBoxButtons.OKCancel);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
