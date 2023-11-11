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
    public partial class StudentDash : Form
    {
        public StudentDash()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-LV8TTKU\SQLEXPRESS;Initial Catalog=WednesdayGenQuiz;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            String id = textBox2.Text;
            String name = textBox3.Text;
            String phone = textBox1.Text;
            String description = textBox4.Text;
            String position = comboBox1.Text;                        

            String query = "INSERT INTO Requests (id, name, phone, description, positions, status) VALUES (@id, @name, @phone, @description, @positions, @status)";
            conn.Open();

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@positions", position);
                command.Parameters.AddWithValue("@status", "PENDING");

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    MessageBox.Show(" Request Sent successful!");
                    ClearData();
                    conn.Close();
                    conn.Open();
                    displayData();

                }
            }

            conn.Close();
        }
        public void displayData()
        {

            //SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Requests WHERE id='"+ textBox2.Text + "'", conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Requests ", conn);
            DataSet ds = new DataSet();
            sda.Fill(ds, "Requests");
            dataGridView1.DataSource = ds.Tables["Requests"];
        }
        public void ClearData()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox1.Text = "";
            textBox4.Text= "";
            comboBox1.Text = "";

        }
        private void StudentDash_Load(object sender, EventArgs e)
        {
            displayData();
        }

        private void signUpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SgnUp sig = new SgnUp();
            this.Hide();
            sig.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Requests WHERE id='"+ textBox5.Text + "'", conn);            
            DataSet ds = new DataSet();
            sda.Fill(ds, "Requests");
            dataGridView1.DataSource = ds.Tables["Requests"];
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            displayData();
        }
    }
}
