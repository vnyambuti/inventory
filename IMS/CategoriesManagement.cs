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
    public partial class CategoriesManagement : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\inventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
       
        public CategoriesManagement()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { 
            
            cmd=new SqlCommand("INSERT INTO Categories (cname) VALUES (@name)",sqlConnection);
                cmd.Parameters.AddWithValue("@name",textBoxCatName.Text);   
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                MessageBox.Show("Category Added","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
            
            catch (Exception ex){
                MessageBox.Show(ex.Message, "Error");
            
            } 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to Update this Category", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE Categories set cname=@cname WHERE cid LIKE '" + label2.Text + "'", sqlConnection);
                    cmd.Parameters.AddWithValue("@cname", textBoxCatName.Text);
                  

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();

                    MessageBox.Show("Category has been Updated successfully");
                    
                }
                
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
