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

namespace DBConTest1
{
    public partial class Form9 : Form
    {
        private panelForm mainForm;
        public Form9(panelForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Load += Form9_Load;
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PaymentsLog;", "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;");
            DataSet ds = new DataSet();
            da.Fill(ds, "PaymentsLog");
            dataGridView1.DataSource = ds.Tables["PaymentsLog"].DefaultView;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            mainForm.NavigateToDashboardForm();
        }

        private void SearchBt1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox4.Text;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ClearSelection();

            try
            {
                bool valueResult = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value != null && row.Cells[i].Value.ToString().Equals(searchValue))
                        {
                            int rowIndex = row.Index;
                            dataGridView1.Rows[rowIndex].Selected = true;
                            valueResult = true;
                            break;
                        }
                    }
                }
                if (!valueResult)
                {
                    MessageBox.Show("Unable to find " + textBox4.Text, "Not Found");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            textBox4.Text = "";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
