using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBConTest1
{
    public partial class Form2 : Form
    {
        System.Windows.Forms.ComboBox ComboBox1;
        System.Windows.Forms.ComboBox ComboBox2;
        public Form2()
        {
            InitializeComponent();
            this.Load += Form2_Load;



            ComboBox1 = new System.Windows.Forms.ComboBox();
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(ComboBox1);

            ComboBox2 = new System.Windows.Forms.ComboBox();
            ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(ComboBox2);

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            // Specify the custom format to include both date and time components
            dateTimePicker2.CustomFormat = "HH:mm:ss";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadData2();
            LoadPtsID();
            LoadDoctors();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void LoadPtsID()
        {
            // Fetch report IDs from the records table and populate the ComboBox
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT PtsID FROM patients1";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader["PtsID"].ToString());
                }
                conn.Close();
            }
        }

        private void LoadDoctors()
        {
            // Fetch doctors and their IDs from the doctors1 table and populate the ComboBox
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT DocID, Doctors FROM doctors1";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(new KeyValuePair<string, int>(reader["Doctors"].ToString(), (int)reader["DocID"]));
                }
                conn.Close();
            }
        }



        private void LoadData()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Appointment", "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;");
            DataSet ds = new DataSet();
            da.Fill(ds, "Appointment");
            dataGridView1.DataSource = ds.Tables["Appointment"].DefaultView;
        }

        private void LoadData2()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM patients1", "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;");
            DataSet ds = new DataSet();
            da.Fill(ds, "patients1");
            dataGridView2.DataSource = ds.Tables["patients1"].DefaultView;
        }

        private void Clear()
        {
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void SaveBt1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || textBox1.Text == "")
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
                        var selectedDoctor = (KeyValuePair<string, int>)comboBox1.SelectedItem;
                        int DID = selectedDoctor.Value;
                        string PID = comboBox2.Text;
                        string APD = dateTimePicker1.Value.ToShortDateString();
                        string APT = dateTimePicker2.Value.ToShortTimeString();
                        string NO = textBox1.Text;

                        // Create a SqlCommand object for the stored procedure
                        using (SqlCommand cmd = new SqlCommand("InsertAppointment", conn))
                        {
                            // Set the command type to stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameters to the command
                            cmd.Parameters.AddWithValue("@AppointmentDate", APD);
                            cmd.Parameters.AddWithValue("@AppointmentTime", APT);
                            cmd.Parameters.AddWithValue("@DoctorID", DID);
                            cmd.Parameters.AddWithValue("@PatientID", PID);
                            cmd.Parameters.AddWithValue("@Queue_No", NO);

                            // Execute the command (stored procedure)
                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Appointment added successfully");

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

        private void SearchBt1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox2.Text;
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
                    MessageBox.Show("Unable to find " + textBox2.Text, "Not Found");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox3.Text;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.ClearSelection(); // Clear previous selection

            try
            {
                bool valueResult = false;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value != null && row.Cells[i].Value.ToString().Equals(searchValue))
                        {
                            int rowIndex = row.Index;
                            dataGridView2.Rows[rowIndex].Selected = true;
                            valueResult = true;
                            break;
                        }
                    }
                }
                if (!valueResult)
                {
                    MessageBox.Show("Unable to find " + textBox3.Text, "Not Found");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            textBox3.Text = "";
        }

        private void DeleteBt3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

            if (comboBox2.Text == "")
            {
                MessageBox.Show("Please Select a Appointment !");
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
                        int appointmentID = (int)dataGridView1.SelectedRows[0].Cells["AppointmentID"].Value;

                        // Create a SqlCommand object for the stored procedure
                        using (SqlCommand cmd = new SqlCommand("DeleteAppointment", conn))
                        {
                            // Set the command type to stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameter for DoctorID
                            cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);

                            // Execute the command (stored procedure)
                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Appointment Delete successful");

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                comboBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

                // Parse the appointment time with a custom format
                // Parse the appointment time with the correct format string
                //dateTimePicker2.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[1].ToString());


                textBox1.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void UpdateBt2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || textBox1.Text == "")
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
                        var selectedDoctor = (KeyValuePair<string, int>)comboBox1.SelectedItem;
                        int DID = selectedDoctor.Value;
                        string PID = comboBox2.Text;
                        string APD = dateTimePicker1.Value.ToShortDateString();
                        string APT = dateTimePicker2.Value.ToShortTimeString();
                        string NO = textBox1.Text;
                        int appointmentID = (int)dataGridView1.SelectedRows[0].Cells["AppointmentID"].Value; // Assuming you have a column named "AppointmentID"

                        // Create a SqlCommand object for the stored procedure
                        using (SqlCommand cmd = new SqlCommand("UpdateAppointment", conn))
                        {
                            // Set the command type to stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameters to the command
                            cmd.Parameters.AddWithValue("@AppointmentID", appointmentID); // Add the appointment ID parameter
                            cmd.Parameters.AddWithValue("@AppointmentDate", APD);
                            cmd.Parameters.AddWithValue("@AppointmentTime", APT);
                            cmd.Parameters.AddWithValue("@DoctorID", DID);
                            cmd.Parameters.AddWithValue("@PatientID", PID);
                            cmd.Parameters.AddWithValue("@Queue_No", NO);

                            // Execute the command (stored procedure)
                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Appointment updated successfully");

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

        private void label11_Click(object sender, EventArgs e)
        {
            // Create an instance of the target form
            Form7 Form7 = new Form7();

            // Display the target form
            Form7.Show();

            // Optionally, hide the current form if you don't need it anymore
            Visible = false;
        }
    }
}
