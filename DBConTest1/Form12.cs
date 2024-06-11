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
    public partial class Form12 : Form
    {
        private panelForm mainForm;

        public Form12(panelForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            mainForm.NavigateToDashboardForm();
        }

        private void btnCalculateFutureDate_Click(object sender, EventArgs e)
        {
            int days;
            if (int.TryParse(txtDays.Text, out days))
            {
                DateTime futureDate = GetFutureDate(days);
                lblFutureDate.Text = $"Next Date: {futureDate.ToShortDateString()}";
            }
            else
            {
                MessageBox.Show("Please enter a valid number of days.");
            }
        }

        private DateTime GetFutureDate(int days)
        {
            DateTime futureDate = DateTime.Now;

            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;"; // Update with your database connection string
            string query = "SELECT dbo.GetFutureDate(@Days)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Days", days);

                try
                {
                    connection.Open();
                    futureDate = (DateTime)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }

            return futureDate;
        }
    }
}
