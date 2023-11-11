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
    public partial class AdminDash : Form
    {
        public AdminDash()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-LV8TTKU\SQLEXPRESS;Initial Catalog=WednesdayGenQuiz;Integrated Security=True");
        private void updateBtn_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Requests SET status=@status WHERE id=@Id ";            
            {
                conn.Open();
                string id = textBox1.Text;
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    // Add parameters to the query
                    command.Parameters.AddWithValue("@status", "APPROVED");
                    command.Parameters.AddWithValue("@Id", id);
                    try
                    {
                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("APPROVED successFULLY.");
                            displayData();
                        }
                        else
                        {
                            MessageBox.Show("Failed to APPROVE .");
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
        }
        public void displayData()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Requests", conn);
            DataSet ds = new DataSet();
            sda.Fill(ds, "Requests");
            gridView.DataSource = ds.Tables["Requests"];
        }

        private void AdminDash_Load(object sender, EventArgs e)
        {
            displayData();
        }

        private void gridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in gridView.SelectedRows)
            {
                string id = row.Cells[0].Value.ToString();
                string request = row.Cells[1].Value.ToString();
                string status = row.Cells[2].Value.ToString();          
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void denyBtn_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Requests SET status=@status WHERE id=@Id ";
            {
                conn.Open();
            string id = textBox1.Text;
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                // Add parameters to the query
                command.Parameters.AddWithValue("@status", "DENIED");
                command.Parameters.AddWithValue("@Id", id);
                try
                {
                    // Execute the query
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {                            
                            MessageBox.Show("DENIED successFULLY.");
                            displayData();
                        }
                    else
                    {
                        MessageBox.Show("Failed to Denied Attendant.");
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
    }

        private void signUpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SgnUp sig = new SgnUp();
            this.Hide();
            sig.ShowDialog();
        }
    }
}
