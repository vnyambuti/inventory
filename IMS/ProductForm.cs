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
    public partial class ProductForm : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\inventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public ProductForm()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddProductForm pf=new AddProductForm();
            pf.ShowDialog();
            
        }


        public void LoadProducts()
        {
            int i = 0;
            dataGridViewP.Rows.Clear();

            cmd = new SqlCommand("SELECT * FROM Products WHERE CONCAT(pId,pname,pprice,pqty,pdescription,pcategory) LIKE '%"+textBoxSearch.Text+"%'", sqlConnection);

            sqlConnection.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridViewP.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[3].ToString(), dr[2].ToString(), dr[5].ToString(), dr[4].ToString());
            }
            dr.Close();
            sqlConnection.Close();



        }

        private void dataGridViewP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //get edit colum
            string colName = dataGridViewP.Columns[e.ColumnIndex].Name;
            Console.WriteLine(colName);
            //check the type of column
            if (colName == "Edit")
            {
                //load userform
                AddProductForm productform = new AddProductForm();
                //set form input values from row
                productform.label7.Text = dataGridViewP.Rows[e.RowIndex].Cells[1].Value.ToString();
                productform.textBoxProductname.Text = dataGridViewP.Rows[e.RowIndex].Cells[2].Value.ToString();
                productform.textBoxPrice.Text = dataGridViewP.Rows[e.RowIndex].Cells[4].Value.ToString();
                productform.textBoxQuantity.Text = dataGridViewP.Rows[e.RowIndex].Cells[3].Value.ToString();
                productform.textBoxdescription.Text = dataGridViewP.Rows[e.RowIndex].Cells[6].Value.ToString();
                productform.category.Text = dataGridViewP.Rows[e.RowIndex].Cells[5].Value.ToString();
                productform.button1.Enabled = false;
                productform.button2.Enabled = true;
                productform.button3.Enabled = true;
              

                productform.ShowDialog();

            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete product", "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand("DELETE FROM Products WHERE pid LIKE '" + dataGridViewP.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", sqlConnection);
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    LoadProducts();
                    MessageBox.Show("Product has been deleted successfully", "DELETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
}
