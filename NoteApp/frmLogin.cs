using NoteApp;
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

namespace Noteapp
{
    public partial class frmLogin : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NoteApp.mdf;Integrated Security=True;Connect Timeout=30";
        public frmLogin()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
            lblPassword.Text = "Must be at least 7 characters with uppercase, lowercase, number, and special character.";
            lblPassword.Visible = false;
            lblUsername.Visible = false;
            checkBox1.Checked = false;

            btnLogin.MouseEnter += btn_MouseEnter;
            btnLogin.MouseLeave += btn_MouseLeave;
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.BackColor = Color.MidnightBlue;
            button.ForeColor = Color.White;
            Cursor = Cursors.Hand;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.BackColor = Color.RoyalBlue;
            button.ForeColor = Color.White;
            Cursor = Cursors.Default;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                lblUsername.Visible = false;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM [User] WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    lblUsername.Visible = false;
                }
                else
                {
                    lblUsername.Text = "Username doesn't exist.";
                    lblUsername.ForeColor = Color.Red;
                    lblUsername.Visible = true;
                }
            }

        }
        private void txtPassword_TextChanged_1(object sender, EventArgs e)
        {
            /* 
            string password = txtPassword.Text;
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{7,}$";
            lblPassword.Visible = true;
            if (Regex.IsMatch(password, pattern))
            {
                lblPassword.Text = "Strong password.";
                lblPassword.ForeColor = Color.Green;
            }
            else
            {
                lblPassword.Text = "Must be at least 7 characters with uppercase,\nlowercase, number, and special character.";
                lblPassword.ForeColor = Color.Red;
            */
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !checkBox1.Checked;
        }


        private void frmLogin_Load_1(object sender, EventArgs e)
        {

        }

        private void BtnForgetPassword_Click_1(object sender, EventArgs e)
        {
            frmReset r = new frmReset("User");
            r.Show();
            this.Hide();
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            frmHomePage hp = new frmHomePage();
            hp.Show();
            this.Hide();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Must include Username and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else 
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "SELECT Password, Id FROM [User] WHERE Username = @Username";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Username", username);
                            conn.Open();

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string dbPassword = reader["Password"].ToString();
                                    string userId = Convert.ToString(reader["Id"]);

                                    if (password == dbPassword)
                                    {
                                        MessageBox.Show("Login successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close();
                                        NotesDashboard dashboard = new NotesDashboard("User", userId);
                                        this.Hide();
                                        dashboard.Show();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Incorrect password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Username not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Login failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    } 
} 
    
