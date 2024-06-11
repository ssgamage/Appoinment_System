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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            this.Load += Form6_Load;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM payStatus", "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;");
            DataSet ds = new DataSet();
            da.Fill(ds, "payStatus");
            dataGridView1.DataSource = ds.Tables["payStatus"].DefaultView;
        }

        private void Clear()
        {
            label1.Text = "";
            label3.Text = "";
            label5.Text = "";
            label8.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                label1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                label3.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                label5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                label8.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            }
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

        private void SaveBt1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

            if (label8.Text == "" || label8.Text == "Paid" || label8.Text == "Canceled" || label8.Text == "Refunded" || label8.Text == "Free")
            {
                MessageBox.Show("Can't do this payment!");
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
                        string id = label1.Text;
                        string status = "Paid";

                        // Create a SqlCommand object for the stored procedure
                        using (SqlCommand cmd = new SqlCommand("UpdateStatusPaid", conn))
                        {
                            // Set the command type to stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameters to the command
                            cmd.Parameters.AddWithValue("@PID", id);
                            cmd.Parameters.AddWithValue("@Status", status);

                            // Execute the command (stored procedure)
                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Payment Successful!");

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

            if (label8.Text == "" || label8.Text == "Paid" || label8.Text == "Canceled" || label8.Text == "Refunded" || label8.Text == "Free")
            {
                MessageBox.Show("Can't do this payment!");
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
                        string id = label1.Text;
                        string status = "Free";

                        // Create a SqlCommand object for the stored procedure
                        using (SqlCommand cmd = new SqlCommand("UpdateStatusFree", conn))
                        {
                            // Set the command type to stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameters to the command
                            cmd.Parameters.AddWithValue("@PID", id);
                            cmd.Parameters.AddWithValue("@Status", status);

                            // Execute the command (stored procedure)
                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Payment Successful!");

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

            if (label8.Text == "" || label8.Text == "Pending" || label8.Text == "Canceled" || label8.Text == "Refunded" || label8.Text == "Free")
            {
                MessageBox.Show("Can't do this payment!");
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
                        string id = label1.Text;
                        string status = "Refunded";

                        // Create a SqlCommand object for the stored procedure
                        using (SqlCommand cmd = new SqlCommand("UpdateStatusRefund", conn))
                        {
                            // Set the command type to stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameters to the command
                            cmd.Parameters.AddWithValue("@PID", id);
                            cmd.Parameters.AddWithValue("@Status", status);

                            // Execute the command (stored procedure)
                            cmd.ExecuteNonQuery();
                        }

                        // Close the connection
                        conn.Close();

                        MessageBox.Show("Refund Successful!");

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
    }
}
