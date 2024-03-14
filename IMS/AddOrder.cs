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
    public partial class AddOrder : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\inventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public AddOrder()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProducts();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadCustomer()
        {
            int i = 0;
            dataGridViewCustomers1.Rows.Clear();

            cmd = new SqlCommand("SELECT * FROM Castomers ", sqlConnection);

            sqlConnection.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridViewCustomers1.Rows.Add(i, dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            sqlConnection.Close();



        }

        public void LoadProducts()
        {
            int i = 0;
            dataGridViewP.Rows.Clear();

            cmd = new SqlCommand("SELECT * FROM Products WHERE CONCAT(pId,pname,pprice,pqty,pdescription,pcategory) LIKE '%" + textBoxSearchProduct.Text + "%'", sqlConnection);

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
        int qnty = 0;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(numericUpDown1.Value) > qnty)
            {
                MessageBox.Show("Inadequate stock", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDown1.Value = numericUpDown1.Value -1;
                return;
            }
            int total=Convert.ToInt32(textBoxprice.Text) * Convert.ToInt32(numericUpDown1.Value);
            textBoxTotal.Text = total.ToString();
            textBoxTotal.Enabled = false;
        }

        private void dataGridViewCustomers1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxCustomerid.Text = dataGridViewCustomers1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBoxCustomerid.Enabled=false;
            textBoxCustomerName.Text = dataGridViewCustomers1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxCustomerName.Enabled=false;  
        }

        private void dataGridViewP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxProductid.Text = dataGridViewP.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxProductid.Enabled = false;
            textBoxProductName.Text = dataGridViewP.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBoxProductName.Enabled = false;
            textBoxprice.Text = dataGridViewP.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBoxprice.Enabled = false;
            qnty = Convert.ToInt16(dataGridViewP.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxCustomerid.Text == "")
                {
                    MessageBox.Show("Customer has not been selected", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (textBoxProductid.Text == "")
                {
                    MessageBox.Show("Product has not been selected", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (MessageBox.Show("Are you sure you want to save this order", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO Orders(odate,pid,cid,qty,price,total)VALUES(@odate,@pid,@cid,@qty,@price,@total)", sqlConnection);
                    cmd.Parameters.AddWithValue("@odate",Convert.ToDateTime(dateTimePicker1.Text));
                    cmd.Parameters.AddWithValue("@pid", textBoxProductid.Text);
                    cmd.Parameters.AddWithValue("@cid", textBoxCustomerid.Text);
                    cmd.Parameters.AddWithValue("@qty",Convert.ToInt16(numericUpDown1.Text) );
                    cmd.Parameters.AddWithValue("@price",Convert.ToInt32(textBoxprice.Text) );
                    cmd.Parameters.AddWithValue("@total",Convert.ToInt32(textBoxTotal.Text) );

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    

                   
                    cmd = new SqlCommand("UPDATE Products SET pqty=(pqty-@qty) WHERE pId LIKE '" + textBoxProductid.Text + "'", sqlConnection);
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt16(numericUpDown1.Text));


                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    LoadProducts();

                    MessageBox.Show("Order has been saved successfully");
                    


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sqlConnection.Close();  
            }
        }
    }
}
