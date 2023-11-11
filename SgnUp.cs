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

namespace GeneralQuizApp
{
    public partial class SgnUp : Form
    {
        public SgnUp()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-LV8TTKU\SQLEXPRESS;Initial Catalog=WednesdayGenQuiz;Integrated Security=True");
        
        private void button1_Click(object sender, EventArgs e)
        {
            //Surround with Try and Catch
            try
            {
                //Check if any of the fields are empty
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("All fields are required. Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                String Id = textBox1.Text;
                if (textBox1.Text.Length != 6)
                {
                    MessageBox.Show("Invalid  ID. Please enter a valid 6-digit integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (textBox4.Text.Length != 6)
                {
                    MessageBox.Show("Password must be 6 characters long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Create the saving pipe-Querry in SQLDATA                
            String fullNames = textBox2.Text;
            String phone = textBox3.Text;
            String password = textBox4.Text;
            String hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            
            
            // Define the SQL query with parameter placeholders
            string query = "INSERT INTO Stud_AucaPanel (id, fullnames, phone, password, role) VALUES (@Id, @fullNames, @phone, @password, @role)";


                
                    conn.Open();

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@fullNames", fullNames);
                        command.Parameters.AddWithValue("@phone", phone);
                        command.Parameters.AddWithValue("@password", hashedPassword);
                        command.Parameters.AddWithValue("@role", "user");

                        try
                        {
                            // Execute the query
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("REGISTERED successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Failed to REGISTER.");
                            }
                        }
                        catch (SqlException ex)
                        {
                            // Handle SQL exceptions
                            MessageBox.Show("SQL Error: " + ex.Message);
                        }
                        conn.Close();
                    }
                }
                
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }

        private void loginLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login log = new Login();
            this.Hide();
            log.ShowDialog();
        }

        private void SgnUp_Load(object sender, EventArgs e)
        {

        }
    }
}
