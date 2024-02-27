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
    public partial class UserForm : Form
    {

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\inventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public UserForm()
        {
            InitializeComponent();
            LoadUser();
        }

        public void LoadUser()
        {
           int  i= 0;
            dataGridView1.Rows.Clear();

            cmd=new SqlCommand("SELECT * FROM Users",sqlConnection);

            sqlConnection.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
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
            UserManagement usermanagement= new UserManagement();
            usermanagement.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //get edit colum
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            Console.WriteLine(colName);
            //check the type of column
            if (colName=="Edit")
            {
                //load userform
                UserManagement userManagement = new UserManagement();
                //set form input values from rows
                userManagement.textBoxFullname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                userManagement.textBoxEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                userManagement.textBoxAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                userManagement.textBoxPhone.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                userManagement.textBoxPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                userManagement.button1.Enabled = false;
                userManagement.button2.Enabled = true;
                userManagement.button3.Enabled = true;
                userManagement.textBoxPhone.Enabled = false;

                userManagement.ShowDialog();
              
            }else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete user","Delete User",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand("DELETE FROM Users WHERE phone LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", sqlConnection);
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    LoadUser();
                    MessageBox.Show("User has been deleted successfully", "DELETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
