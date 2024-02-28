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
    public partial class AddProductForm : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\inventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public AddProductForm()
        {
            InitializeComponent();
            LoadCtegory();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCtegory()
        {
            category.Items.Clear();
            cmd = new SqlCommand("SELECT * FROM Categories", sqlConnection);
            sqlConnection.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                category.Items.Add(dr[1].ToString());
            }
            dr.Close();
            sqlConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this product", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO Products(pname,pprice,pqty,pdescription,pcategory)VALUES(@name,@price,@qty,@description,@category)", sqlConnection);
                    cmd.Parameters.AddWithValue("@name", textBoxProductname.Text);
                    cmd.Parameters.AddWithValue("@price",Convert.ToInt16(textBoxPrice.Text));
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt16(textBoxQuantity.Text));
                    cmd.Parameters.AddWithValue("@description", textBoxdescription.Text);
                    cmd.Parameters.AddWithValue("@category", category.Text);

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();

                    MessageBox.Show("Product has been saved successfully");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
