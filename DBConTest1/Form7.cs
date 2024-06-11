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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBConTest1
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            this.Load += Form7_Load;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM MedicalService", "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;");
            DataSet ds = new DataSet();
            da.Fill(ds, "MedicalService");
            dataGridView1.DataSource = ds.Tables["MedicalService"].DefaultView;
        }

        private void Clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void SearchBt1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox4.Text;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ClearSelection(); // Clear previous selection

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

        private void SaveBt1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

            if (textBox1.Text == "" || textBox3.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please fill all the information!");
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        // Open the connection
                        conn.Open();

                        // Setup the query
                        string service = textBox1.Text;
                        string price = textBox2.Text;
                        string description = textBox3.Text;

                        // Create a SqlCommand object for the stored procedure
                        using (SqlCommand cmd = new SqlCommand("AddMedicalService", conn))
                        {
                            // Set the command type to stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameters to the command
                            cmd.Parameters.AddWithValue("@ServiceName", service);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.Parameters.AddWithValue("@Description", description);

                            // Execute the command (stored procedure)
                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Medical Service added successfully");

                        LoadData();
                        Clear();
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void UpdateBt2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

            if (textBox1.Text == "" || textBox3.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please select a medical service!");
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        // Open the connection
                        conn.Open();

                        // Setup the query
                        string service = textBox1.Text;
                        string price = textBox2.Text;
                        string description = textBox3.Text;
                        int ServiceID = (int)dataGridView1.SelectedRows[0].Cells["ServiceID"].Value;

                        // Create a SqlCommand object for the stored procedure
                        using (SqlCommand cmd = new SqlCommand("UpdateMedicalService", conn))
                        {
                            // Set the command type to stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameters to the command
                            cmd.Parameters.AddWithValue("@ServiceID", ServiceID); // Add the appointment ID parameter
                            cmd.Parameters.AddWithValue("@ServiceName", service);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.Parameters.AddWithValue("@Description", description);

                            // Execute the command (stored procedure)
                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("service updated successfully");

                        LoadData();
                        Clear();
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBt3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Select a Service !");
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        // Open the connection
                        conn.Open();

                        // Get the DoctorID of the selected doctor from the DataGridView
                        int ServiceID = (int)dataGridView1.SelectedRows[0].Cells["ServiceID"].Value;

                        // Create a SqlCommand object for the stored procedure
                        using (SqlCommand cmd = new SqlCommand("DeleteMedicalService", conn))
                        {
                            // Set the command type to stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameter for DoctorID
                            cmd.Parameters.AddWithValue("@ServiceID", ServiceID);

                            // Execute the command (stored procedure)
                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Medical Service Delete successful");

                        LoadData();
                        Clear();
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Create an instance of the target form
            Form2 Form2 = new Form2();

            // Display the target form
            Form2.Show();

            // Optionally, hide the current form if you don't need it anymore
            Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Create an instance of the target form
            Form3 Form3 = new Form3();

            // Display the target form
            Form3.Show();

            // Optionally, hide the current form if you don't need it anymore
            Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Create an instance of the target form
            Form4 Form4 = new Form4();

            // Display the target form
            Form4.Show();

            // Optionally, hide the current form if you don't need it anymore
            Visible = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Create an instance of the target form
            Form5 Form5 = new Form5();

            // Display the target form
            Form5.Show();

            // Optionally, hide the current form if you don't need it anymore
            Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Create an instance of the target form
            //Form6 Form6 = new Form6();

            // Display the target form
            //Form6.Show();

            // Optionally, hide the current form if you don't need it anymore
            //Visible = false;
        }
    }
}
