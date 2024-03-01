﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS
{
    public partial class MainForm : Form
    {

 
        public MainForm()
        {
            InitializeComponent();
        }

       private Form activeForm = null;

        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            
               activeForm.Close();
                activeForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;   
                childForm.Dock = DockStyle.Fill;
                panelMain.Controls.Add(childForm);  
                panelMain.Tag= childForm;
                childForm.BringToFront();
                childForm.Show();

            
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            openChildForm(new Customers());
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            openChildForm(new Categories());
        }

        private void label4_Click(object sender, EventArgs e)
        {
           
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            openChildForm(new ProductForm());
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            openChildForm(new Orders());
        }
    }
}
