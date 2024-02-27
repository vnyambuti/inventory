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
    public partial class Categories : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\inventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public Categories()
        {
            InitializeComponent();
            LoadCategories();
        }

         public void LoadCategories()
        {
          int i =0;
            dataGridViewCategories.Rows.Clear();

            cmd= new SqlCommand("SELECT * FROM Categories ",sqlConnection);

            sqlConnection.Open();
            dr = cmd.ExecuteReader();
            Console.WriteLine(dr);
            while (dr.Read()) { 
              i++;
                dataGridViewCategories.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            
            }
            dr.Close();
            sqlConnection.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CategoriesManagement categoriesManagement = new CategoriesManagement();
           
            categoriesManagement.ShowDialog();
        }

        private void dataGridViewCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
              //get edit colum
            string colName = dataGridViewCategories.Columns[e.ColumnIndex].Name;
            Console.WriteLine(colName);
            //check the type of column
            if (colName=="Edit")
            {
                //load userform
              CategoriesManagement catmanagement=new CategoriesManagement();
                //set form input values from rows
                catmanagement.textBoxCatName.Text = dataGridViewCategories.Rows[e.RowIndex].Cells[2].Value.ToString();

                catmanagement.label2.Text = dataGridViewCategories.Rows[e.RowIndex].Cells[1].Value.ToString();

                catmanagement.button1.Enabled = false;
                catmanagement.button2.Enabled = true;
                catmanagement.button3.Enabled = true;
               
                
                catmanagement.ShowDialog();
              
            }else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete category", "Delete category", MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand("DELETE FROM Categories WHERE cid LIKE '" + dataGridViewCategories.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", sqlConnection);
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
               LoadCategories();
                    MessageBox.Show("category has been deleted successfully", "DELETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
    
}

