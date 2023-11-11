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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-LV8TTKU\SQLEXPRESS;Initial Catalog=WednesdayGenQuiz;Integrated Security=True");
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String phone = textBox2.Text;
            String enteredPassword = textBox3.Text;

            String query = "SELECT id, fullnames, phone, password, role FROM Stud_AucaPanel WHERE phone = @phone";

            conn.Open();

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@phone", phone);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string hashedPasswordFromDB = reader["password"].ToString();


                        if (reader.GetOrdinal("role") != -1)
                        {
                            String role = reader["role"].ToString();
                            phone = reader["phone"].ToString();


                            if (string.Equals(role.Trim(), "admin", StringComparison.OrdinalIgnoreCase))
                            {
                                MessageBox.Show("Admin logged successful!");
                                AdminDash ad = new AdminDash();
                                this.Hide();
                                ad.Show();                                
                                
                            }
                            else
                            {
                                MessageBox.Show("User logged successful!");
                                StudentDash ad = new StudentDash();
                                this.Hide();
                                ad.Show();
                                
                            }


                        }
                        else
                        {
                            MessageBox.Show("User does not have a role.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("User not found");
                    }
                }
            }

            conn.Close();
        }

        private void signUpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SgnUp sig = new SgnUp();
            this.Hide();
            sig.ShowDialog();

        }
    }
}
