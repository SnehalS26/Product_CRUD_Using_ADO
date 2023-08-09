using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ADODemo.Model;
namespace ADODemo
{
    public partial class Form1 : Form
    {
        ProductCRUD crud;
        List<Category> list;
        public Form1()
        {
            InitializeComponent();
            crud = new ProductCRUD();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list = crud.GetCategories();
            comboCategory.DataSource = list;
            comboCategory.DisplayMember = "Cname";
            comboCategory.ValueMember = "Cid";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = new Product();
                p.Name = textProdName.Text;
                p.Price = Convert.ToInt32(textProdPrice.Text);
                p.Cid = Convert.ToInt32(comboCategory.SelectedValue);
                int res = crud.AddProduct(p);
                if (res > 0)
                {
                    MessageBox.Show("Record inserted..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Product prod = crud.GetProductById(Convert.ToInt32(textProdId.Text));
            
            if (prod.Id> 0)
            {
                foreach(Category item in list)
                {
                    if(item.Cid == prod.Cid)
                    {
                        comboCategory.Text = item.Cname;
                        break;
                    }
                }
                textProdName.Text = prod.Name;
                textProdPrice.Text = prod.Price.ToString(); 
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = new Product();
                p.Id = Convert.ToInt32(textProdId.Text);
                p.Name = textProdName.Text;
                p.Price = Convert.ToInt32(textProdPrice.Text);
                p.Cid = Convert.ToInt32(comboCategory.SelectedValue);
                int res = crud.UpdateProduct(p);
                if (res > 0)
                {
                    MessageBox.Show("Record updated..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                
                int res = crud.DeleteProduct(Convert.ToInt32(textProdId.Text));
                if (res > 0)
                {
                    MessageBox.Show("Record deleted..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
