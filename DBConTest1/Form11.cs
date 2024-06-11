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
    public partial class Form11 : Form
    {
        private panelForm mainForm;

        public Form11(panelForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            mainForm.NavigateToDashboardForm();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
                int doctorID;
                if (int.TryParse(txtPatientID.Text, out doctorID))
                {
                    int totalAppointments = GetTotalAppointments(doctorID);
                    lblResult.Text = $"Total Appointments: {totalAppointments}";
                }
                else
                {
                    MessageBox.Show("Please enter a valid Doctor ID.");
                }
        }

        private int GetTotalAppointments(int doctorID)
        {
            int totalAppointments = 0;

            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";
            string query = "SELECT dbo.GetTotalAppointmentsD(@DoctorID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DoctorID", doctorID);

                try
                {
                    connection.Open();
                    totalAppointments = (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }

            return totalAppointments;
        }

        private void btnCalculateMonthlyAppointments_Click(object sender, EventArgs e)
        {
            int year;
            if (int.TryParse(txtYear.Text, out year))
            {
                DataTable monthlyAppointments = GetMonthlyAppointments(year);
                dataGridViewMonthlyAppointments.DataSource = monthlyAppointments;
            }
            else
            {
                MessageBox.Show("Please enter a valid year.");
            }
        }

        private DataTable GetMonthlyAppointments(int year)
        {
            DataTable dataTable = new DataTable();
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;"; // Update with your database connection string
            string query = "SELECT Month, TotalAppointments FROM dbo.GetMonthlyAppointments(@Year)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Year", year);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }

            return dataTable;
        }
    }
}
