using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Segment3
{
    public partial class Form1 : Form
    {
        int ID = -1;
      


        public Form1()
        {
            InitializeComponent();
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
        }

       private void Form1_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        public void PopulateGrid() 
        {
            DataSet ds = new DataSet();
            PersonBusinessLayer.FillDataSet(ref ds, false);
            gridVw.DataSource = ds.Tables[0];
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            PersonBusinessLayer.FillDataSet(ref ds, true);
            gridVw.DataSource = ds.Tables[0];
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ID != -1)
            {
                MessageBox.Show("Record is in Update mode..No adding accepted");
            }
            else
            {
                if (cmbGender.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select the gender");
                }
                else
                {
                    try
                    {

                        bool result = PersonBusinessLayer.Add(txtLst.Text, txtFirst.Text, cmbGender.SelectedItem.ToString(), dtpiker.Value.Date);
                        if (result)
                        {
                          
                            PopulateGrid();
                            ClearAll();
                        }
                        else
                        {
                            MessageBox.Show("Record Not added .. something went wrong");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
            }
        }

        public void ClearAll ()
        {
            txtLst.Text = string.Empty;
            txtFirst.Text = string.Empty;
            cmbGender.SelectedIndex = -1;
            dtpiker.Value = DateTime.Today.Date;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtSerch.Text, "^[0-9]*$") || string.IsNullOrEmpty(txtSerch.Text))
            {
                MessageBox.Show("Please enter a valid number");
            }
            else
            {
                PersonBusinessLayer personTobeUpdate = new PersonBusinessLayer();
                personTobeUpdate = PersonBusinessLayer.GetPerson(int.Parse(txtSerch.Text));
                if (personTobeUpdate == null)
                {
                    MessageBox.Show("No records Found");
                    ClearAll();
                    ID = -1;
                }
                else
                {
                    ID = personTobeUpdate.ID;
                    txtFirst.Text = personTobeUpdate.FirstName;
                    txtLst.Text = personTobeUpdate.LastName;
                    cmbGender.SelectedItem = personTobeUpdate.Gender;
                    dtpiker.Value = personTobeUpdate.DOB;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ID != -1)
            {
                if (cmbGender.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select the gender");
                }
                else
                {
                    try
                    {

                        bool result = PersonBusinessLayer.Update(ID, txtLst.Text, txtFirst.Text, cmbGender.SelectedItem.ToString(), dtpiker.Value.Date);
                        if (result)
                        {
                            MessageBox.Show("Succesfully Updated");
                            ID = -1;
                            ClearAll();
                            txtSerch.Clear();
                            PopulateGrid();
                        }
                        else
                        {
                            MessageBox.Show("Record Not updated .. something went wrong");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
            }
            else
            {
                MessageBox.Show("Update not accepted...Please select a record");
            }
        }

       



    }
}
