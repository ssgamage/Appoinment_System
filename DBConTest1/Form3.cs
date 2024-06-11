using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DBConTest1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.Load += Form3_Load;
            //label1.Click += label1_Click;
            //label3.Click += label3_Click;
            //label4.Click += label4_Click;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM patients1", "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;");
            DataSet ds = new DataSet();
            da.Fill(ds, "patients1");
            dataGridView1.DataSource = ds.Tables["patients1"].DefaultView;
        }

        private void Clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            textBox3.Text = "";
        }

        private void SaveBt1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.SelectedIndex == -1 || textBox3.Text == "")
            {
                MessageBox.Show("Please fill all the informations!");
            } else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        // Open the connection
                        conn.Open();

                        // Setup the query
                        string name = textBox1.Text;
                        string address = textBox2.Text;
                        string gender = comboBox1.Text;
                        string dob = dateTimePicker1.Value.ToShortDateString();
                        string phone = textBox3.Text;

                        string query = "INSERT INTO patients1(Names,Addresses,Gender,DOB,Phone)VALUES(@Name, @Addresses, @Gender, @DOB, @Phone)";

                        // Execute
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Addresses", address);
                            cmd.Parameters.AddWithValue("@Gender", gender);
                            cmd.Parameters.AddWithValue("@DOB", dob);
                            cmd.Parameters.AddWithValue("@Phone", phone);

                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Data insert successful");

                        LoadData();
                        Clear();
                    }
                } catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
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

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void DeleteBt3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Select a patient !");
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
                        string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); ;

                        string query = "DELETE FROM patients1 WHERE PtsID=@Id";

                        // Execute
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters
                            cmd.Parameters.AddWithValue("@Id", id);
                            
                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Patient delete successful");

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

            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Select a patient !");
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
                        string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        string name = textBox1.Text;
                        string address = textBox2.Text;
                        string gender = comboBox1.Text;
                        string dob = dateTimePicker1.Value.ToShortDateString();
                        string phone = textBox3.Text;

                        string query = "UPDATE patients1 SET Names = @Name, Addresses = @Addresses, Gender = @Gender, DOB = @DOB, Phone = @Phone WHERE PtsID = @Id";

                        // Execute
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Addresses", address);
                            cmd.Parameters.AddWithValue("@Gender", gender);
                            cmd.Parameters.AddWithValue("@DOB", dob);
                            cmd.Parameters.AddWithValue("@Phone", phone);

                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Patient information updated successfully");

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            // Create an instance of the target form
            Form2 Form2 = new Form2();

            // Display the target form
            Form2.Show();

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
