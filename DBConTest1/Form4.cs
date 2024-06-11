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
    public partial class Form4 : Form
    {
        System.Windows.Forms.ComboBox ComboBox1;
        public Form4()
        {
            InitializeComponent();
            this.Load += Form4_Load;
            //label4.Click += label4_Click;
            //label2.Click += label2_Click;
            //label1.Click += label1_Click;



            ComboBox1 = new System.Windows.Forms.ComboBox();
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            //comboBoxPastRecords.Width = 150;
            //comboBoxPastRecords.Location = new Point(150, 250); // Adjust the location as needed
            Controls.Add(ComboBox1);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadSpecializations(); // Populate ComboBox with past records
        }

        private void LoadData()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM doctors1", "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;");
            DataSet ds = new DataSet();
            da.Fill(ds, "doctors1");
            dataGridView1.DataSource = ds.Tables["doctors1"].DefaultView;
        }

        private void LoadSpecializations()
        {
            // Fetch report IDs from the records table and populate the ComboBox
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Departments FROM department1";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["Departments"].ToString());
                }
                conn.Close();
            }
        }

        private void Clear()
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void SaveBt1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

            if (textBox1.Text == "" || comboBox1.SelectedIndex == -1 || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please fill all the informations!");
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
                        string name = textBox1.Text;
                        string dep = comboBox1.Text;
                        string mail = textBox2.Text;
                        string phone = textBox3.Text;

                        string query = "INSERT INTO doctors1(Doctors,Spacialization,Email,Phone)VALUES(@Doctors, @Spacialization, @Email, @Phone)";

                        // Execute
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters
                            cmd.Parameters.AddWithValue("@Doctors", name);
                            cmd.Parameters.AddWithValue("@Spacialization", dep);
                            cmd.Parameters.AddWithValue("@Email", mail);
                            cmd.Parameters.AddWithValue("@Phone", phone);

                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Doctor added successful");

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
                        string dep = comboBox1.Text;
                        string mail = textBox2.Text;
                        string phone = textBox3.Text;

                        string query = "UPDATE doctors1 SET Doctors = @Doctors, Spacialization = @Spacialization, Email = @Email, Phone = @Phone WHERE DocID = @Id";

                        // Execute
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Doctors", name);
                            cmd.Parameters.AddWithValue("@Spacialization", dep);
                            cmd.Parameters.AddWithValue("@Email", mail);
                            cmd.Parameters.AddWithValue("@Phone", phone);

                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Doctors updated successfully");

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
                MessageBox.Show("Please Select a Doctor !");
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

                        string query = "DELETE FROM doctors1 WHERE DocID=@Id";

                        // Execute
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters
                            cmd.Parameters.AddWithValue("@Id", id);

                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Delete successful");

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
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
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
