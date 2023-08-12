using ADODemo.Model;
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

namespace ADODemo
{
    public partial class Form3 : Form
    {
        ProductDisconnectted crud;
        //List<Category> list;
        DataTable table;
        public Form3()
        {
            InitializeComponent();
            crud = new ProductDisconnectted();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            table = crud.GetAllCategories();
            comboCategory.DataSource = table;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Product prod = crud.GetProductById(Convert.ToInt32(textProdId.Text));
                if (prod.Id > 0)
                {
                    List<Category> list = new List<Category>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        Category c = new Category();
                        c.Cid = Convert.ToInt32(table.Rows[i]["cid"]);
                        c.Cname = table.Rows[i]["cname"].ToString();
                        list.Add(c);
                    }
                    foreach (Category item in list)
                    {
                        if (item.Cid == prod.Cid)
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

        private void btnShowAll_Click(object sender, EventArgs e)
        {
           DataSet ds = crud.GetAllProducts();
           dataGridView1.DataSource = ds.Tables["Product"];
        }
    }
}
