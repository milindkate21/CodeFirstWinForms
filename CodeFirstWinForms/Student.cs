using CodeFirstWinForms.Datamodel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeFirstWinForms.Datamodel;

namespace CodeFirstWinForms
{
    public partial class Student : Form
    {
        private DataContext db = new DataContext();
        int id = 0;
        List<StudentInfo> aList = new List<StudentInfo>();
        List<Country> aCountryList = new List<Country>();
        int pageSize = 4; 
        int pageNumber = 1;
        int count = 0;
        public Student()
        {
            InitializeComponent();
        }
        private void Student_Load(object sender, EventArgs e)
        {
            loadGrid(1);
            loadCountry();
        }
        public void loadGrid(int pageNumber)
        {
            try
            {
                IGenericData<StudentInfo> repository = new GenericDataRepo<StudentInfo>();
                aList = (List<StudentInfo>)repository.GetAll<StudentInfo>();
                
                var studentInfo = from e in aList
                                  join o in db.Country on e.countryID equals o.Id
                                  select new
                                  {
                                      id = e.id,
                                      firstname = e.firstname,
                                      lastname = e.lastname,
                                      phonenumber = e.phonenumber,
                                      city = e.city,
                                      countryName = o.Name
                                  };

                dtGridView.DataSource = null;
                dtGridView.AutoGenerateColumns = false;
                dtGridView.ColumnCount = 6;

                dtGridView.Columns[0].Name = "id";
                dtGridView.Columns[0].HeaderText = "Sr No.";
                dtGridView.Columns[0].DataPropertyName = "id";

                dtGridView.Columns[1].Name = "first_name";
                dtGridView.Columns[1].HeaderText = "First Name";
                dtGridView.Columns[1].DataPropertyName = "firstname";

                dtGridView.Columns[2].Name = "last_name";
                dtGridView.Columns[2].HeaderText = "Last Name";
                dtGridView.Columns[2].DataPropertyName = "lastname";

                dtGridView.Columns[3].Name = "phone_number";
                dtGridView.Columns[3].HeaderText = "Phone Number";
                dtGridView.Columns[3].DataPropertyName = "phonenumber";

                dtGridView.Columns[4].Name = "city";
                dtGridView.Columns[4].HeaderText = "City";
                dtGridView.Columns[4].DataPropertyName = "city";

                dtGridView.Columns[5].Name = "country";
                dtGridView.Columns[5].HeaderText = "Country";
                dtGridView.Columns[5].DataPropertyName = "countryName";

                var result = studentInfo.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                if (studentInfo.Count() > 0)
                {
                    dtGridView.DataSource = result.ToList();
                    count = Convert.ToInt32(studentInfo.Count());
                    String message = "Showing " + result[0].id + "-" + result[result.Count() - 1].id + " of " + count + " items.";
                    lblPageDataBottom.Text = message;
                    PopulatePager(count, pageNumber);

                    var buttonCol = new DataGridViewButtonColumn();
                    buttonCol.Name = "btnUpdate";
                    buttonCol.HeaderText = "";
                    buttonCol.UseColumnTextForButtonValue = true;
                    buttonCol.Text = "Update";

                    var buttonCol1 = new DataGridViewButtonColumn();
                    buttonCol1.Name = "btnDelete";
                    buttonCol1.HeaderText = "";
                    buttonCol1.UseColumnTextForButtonValue = true;
                    buttonCol1.Text = "Delete";

                    dtGridView.Columns.Add(buttonCol);
                    dtGridView.Columns.Add(buttonCol1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void loadCountry()
        {
            try
            {
                IGenericData<Country> repository = new GenericDataRepo<Country>();
                aCountryList = (List<Country>)repository.GetAll<Country>();

                cbCountry.Items.Clear();

                if (aCountryList.Count() > 0)
                {
                    cbCountry.DataSource = aCountryList;
                    cbCountry.ValueMember = "Id";
                    cbCountry.DisplayMember = "Name";
                    cbCountry.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                StudentInfo studentOp = new StudentInfo();
                IGenericData<StudentInfo> repository = new GenericDataRepo<StudentInfo>();
                if (btnAdd.Text == "Add")
                {
                    if(txtFirstName.Text == "")
                    {
                        MessageBox.Show("Please enter first name!!");
                        return;
                    }
                    studentOp.recordCount = count;
                    studentOp.typeValue = "I";
                    studentOp.firstname = Convert.ToString(txtFirstName.Text);
                    studentOp.lastname = Convert.ToString(txtLastName.Text);
                    studentOp.phonenumber = Convert.ToString(txtPhone.Text);
                    studentOp.city = Convert.ToString(txtCity.Text);
                    studentOp.countryID = Convert.ToInt32(cbCountry.SelectedValue.ToString());
                    repository.Add(studentOp);
                    MessageBox.Show("Record added successfully.");
                    loadGrid(1);
                    clearFields();
                }
                else
                {
                    studentOp.recordCount = count;
                    studentOp.typeValue = "U";
                    studentOp.id = id;
                    studentOp.firstname = txtFirstName.Text;
                    studentOp.lastname = txtLastName.Text;
                    studentOp.city = txtCity.Text;
                    studentOp.phonenumber = txtPhone.Text;
                    studentOp.countryID = Convert.ToInt32(cbCountry.SelectedValue);
                    repository.Update(studentOp);
                    MessageBox.Show("Record upadted successfully.");
                    loadGrid(1);
                    btnAdd.Text = "Add";
                    clearFields();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void dtGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IGenericData<StudentInfo> repository = new GenericDataRepo<StudentInfo>();
            StudentInfo studentOp = new StudentInfo();
            id = Convert.ToInt32(dtGridView.Rows[e.RowIndex].Cells["id"].Value);
            if (e.ColumnIndex == 6) //Update
            {
                txtFirstName.Text = Convert.ToString(dtGridView.Rows[e.RowIndex].Cells["first_name"].Value);
                txtLastName.Text = Convert.ToString(dtGridView.Rows[e.RowIndex].Cells["last_name"].Value);
                txtCity.Text = Convert.ToString(dtGridView.Rows[e.RowIndex].Cells["city"].Value);
                txtPhone.Text = Convert.ToString(dtGridView.Rows[e.RowIndex].Cells["phone_number"].Value);
                cbCountry.Text = Convert.ToString(dtGridView.Rows[e.RowIndex].Cells["country"].Value);
                btnAdd.Text = "Save";
            }
            else if (e.ColumnIndex == 7) //Delete
            {
                studentOp.typeValue = "D";
                studentOp.id = id;
                repository.Delete(studentOp);
                MessageBox.Show("Record deleted successfully.");
                loadGrid(1);
                clearFields();
            }

        }
        public void clearFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtCity.Text = "";
            txtPhone.Text = "";
            cbCountry.SelectedIndex = -1;
        }
        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(pageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 5;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "First", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString() });
            }

            //Clear existing Pager Buttons.
            pnlPager.Controls.Clear();

            //Loop and add Buttons for Pager.
            int count = 0;
            foreach (Page page in pages)
            {
                Button btnPage = new Button();
                btnPage.Location = new System.Drawing.Point(38 * count, 5);
                btnPage.Size = new System.Drawing.Size(35, 20);
                btnPage.Name = page.Value;
                btnPage.Text = page.Text;
                btnPage.Enabled = !page.Selected;
                btnPage.Click += new System.EventHandler(this.Page_Click);
                pnlPager.Controls.Add(btnPage);
                count++;
            }
        }
        private void Page_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);
            this.loadGrid(int.Parse(btnPager.Name));
        }
        public class Page
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }
        }

        
    }
}
