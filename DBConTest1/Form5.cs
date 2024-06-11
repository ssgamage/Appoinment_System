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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBConTest1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            this.Load += Form5_Load;
            //label2.Click += label2_Click;
            //label1.Click += label1_Click;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM department1", "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;");
            DataSet ds = new DataSet();
            da.Fill(ds, "department1");
            dataGridView1.DataSource = ds.Tables["department1"].DefaultView;
        }

        private void Clear()
        {
            textBox1.Text = "";
        }

        private void SaveBt1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

            if (textBox1.Text == "")
            {
                MessageBox.Show("Empty recod !");
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
                        string dep = textBox1.Text;

                        string query = "INSERT INTO department1(Departments)VALUES(@departments)";

                        // Execute
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters
                            cmd.Parameters.AddWithValue("@Departments", dep);

                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Department added successful");

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
                MessageBox.Show("Please Select a department !");
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
                        string dep = textBox1.Text;

                        string query = "UPDATE department1 SET Departments = @Departments WHERE PepID = @Id";

                        // Execute
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Departments", dep);

                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Department name updated successfully");

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

                        string query = "DELETE FROM department1 WHERE PepID=@Id";

                        // Execute
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters
                            cmd.Parameters.AddWithValue("@Id", id);

                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Department delete successful");

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

        private void label3_Click_1(object sender, EventArgs e)
        {
            // Create an instance of the target form
            Form4 Form4 = new Form4();

            // Display the target form
            Form4.Show();

            // Optionally, hide the current form if you don't need it anymore
            Visible = false;
        }
    }
}
