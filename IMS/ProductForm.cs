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

            cmd = new SqlCommand("SELECT * FROM Products", sqlConnection);

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

    }
}
