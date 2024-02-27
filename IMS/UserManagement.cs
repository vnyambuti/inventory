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
using System.ComponentModel.Design;

namespace IMS
{
   
    public partial class UserManagement : Form
    {

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\inventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd= new SqlCommand();
        public UserManagement()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this user","Saving Record",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO Users(fullname,email,phone,address,password)VALUES(@fullname,@email,@phone,@address,@password)", sqlConnection);
                    cmd.Parameters.AddWithValue("@fullname",textBoxFullname.Text);
                    cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                    cmd.Parameters.AddWithValue("@phone", textBoxPhone.Text);
                    cmd.Parameters.AddWithValue("@address", textBoxAddress.Text);
                    cmd.Parameters.AddWithValue("@password", textBoxPassword.Text);

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
              
                    MessageBox.Show("User has been saved successfully");

                }

            }
            catch(Exception ex) { 
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxAddress.Clear();
            textBoxEmail.Clear();   
            textBoxFullname.Clear();
            textBoxPassword.Clear();
            textBoxPhone.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to Update this user", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE Users set fullname=@fullname,email=@email,phone=@phone,address=@address,password=@password WHERE phone LIKE '"+textBoxPhone.Text+"'", sqlConnection);
                    cmd.Parameters.AddWithValue("@fullname", textBoxFullname.Text);
                    cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                    cmd.Parameters.AddWithValue("@phone", textBoxPhone.Text);
                    cmd.Parameters.AddWithValue("@address", textBoxAddress.Text);
                    cmd.Parameters.AddWithValue("@password", textBoxPassword.Text);

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                  
                    MessageBox.Show("User has been saved successfully");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
