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
using System.Xml.Linq;

namespace DBConTest1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // SQL Server address
            string connectionString = "Data Source=SZX-VivoBook-15;Initial Catalog=Stu;Integrated Security=True;";

            try
            {
                // Connection establish
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Setup the query
                    string name = textBox1.Text;
                    string password = textBox2.Text;
                    string role = "Admin"; // Default role
                    bool loginSuccess = false;

                    // Create a SqlCommand object for the login procedure
                    // Inside button1_Click event handler
                    using (SqlCommand cmd = new SqlCommand("LoginUser", conn))
                    {
                        // Set the command type to stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add input parameters
                        cmd.Parameters.AddWithValue("@UserName", name);
                        cmd.Parameters.AddWithValue("@Password", password);

                        // Add output parameters for login success and user role
                        SqlParameter loginSuccessParam = new SqlParameter("@LoginSuccess", SqlDbType.Bit);
                        loginSuccessParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(loginSuccessParam);

                        SqlParameter roleParam = new SqlParameter("@Role", SqlDbType.NVarChar, 50);
                        roleParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(roleParam);

                        // Execute the command (stored procedure)
                        cmd.ExecuteNonQuery();

                        // Check the login success status
                        loginSuccess = (bool)loginSuccessParam.Value;
                        if (loginSuccess)
                        {
                            // Get the user's role
                            role = roleParam.Value.ToString();
                        }
                    }


                    if (loginSuccess)
                    {
                        MessageBox.Show("Login successful");

                        // Hide the current form (Form1)
                        this.Hide();

                        // Redirect to the appropriate form based on the role
                        if (role == "Admin")
                        {
                            panelForm panelForm = new panelForm();
                            panelForm.ShowDialog(); // Show Form2 as a modal dialog
                        }
                        else if (role == "User")
                        {
                            Form3 form3 = new Form3();
                            form3.ShowDialog(); // Show Form3 as a modal dialog
                        }
                        else if (role == "Doctor")
                        {
                            Form4 form4 = new Form4();
                            form4.ShowDialog(); // Show Form4 as a modal dialog
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}