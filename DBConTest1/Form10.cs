using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OfficeOpenXml;
using ClosedXML.Excel;
using System.IO;

namespace DBConTest1
{
    public partial class Form10 : Form
    {
        private panelForm mainForm;
        private string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

        public Form10(panelForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            mainForm.NavigateToDashboardForm();
        }

        private void getBt1_Click(object sender, EventArgs e)
        {
            LoadDoctorRevenuePerDay();
        }

        private void LoadDoctorRevenuePerDay()
        {
            try
            {
                int selectedDocID = GetSelectedDocID();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM DoctorRevenuePerDay WHERE DocID = @SelectedDocID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SelectedDocID", selectedDocID);

                        connection.Open();

                        DataTable dataTable = new DataTable();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            textBox1.Text = "";
        }

        private int GetSelectedDocID()
        {
            int selectedDocID;

            if (int.TryParse(textBox1.Text, out selectedDocID))
            {
                return selectedDocID;
            }
            else
            {
                MessageBox.Show("Please enter a valid DocID.");
                return -1;
            }
        }

        private void generateExcelbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0 && dataGridView1.Columns.Count > 0)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("DoctorRevenuePerDay");

                        // Copy the DataGridView data to the Excel worksheet
                        for (int i = 1; i <= dataGridView1.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i).Value = dataGridView1.Columns[i - 1].HeaderText;
                        }

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView1.Columns.Count; j++)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    worksheet.Cell(i + 2, j + 1).Value = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                }
                                else
                                {
                                    worksheet.Cell(i + 2, j + 1).Value = string.Empty;
                                }
                            }
                        }

                        // Save the Excel file
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                        saveFileDialog.RestoreDirectory = true;

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            workbook.SaveAs(saveFileDialog.FileName);
                            MessageBox.Show("Excel file generated successfully!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No data available to generate Excel file.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
