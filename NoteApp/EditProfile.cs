using Noteapp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace NoteApp
{
    public partial class EditProfile : Form

    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NoteApp.mdf;Integrated Security=True;Connect Timeout=30";

        private bool isUsernameValid = false;
        private Button btnConfirm;
        private readonly Color originalBtnBackColor = Color.RoyalBlue;
        private readonly Color originalBtnForeColor = Color.White;
        private readonly Color originalBtnBorderColor = Color.RoyalBlue;

       
        private bool isPasswordValid = false;
        private bool isEmailValid = false;
        private bool isNameValid = false;

        private int UserID;
        private NotesDashboard Dashboard;

        public EditProfile(int userId, NotesDashboard dashboard)
        {
            InitializeComponent();

            this.UserID = userId;
            this.Dashboard = dashboard;

            this.Load += EditProfile_Load;

            btnConfirm = new Button();
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Text = "Confirm";
            btnConfirm.Size = new Size(252, 41);
            btnConfirm.Location = new Point(113, 531);
            btnConfirm.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            btnConfirm.BackColor = originalBtnBackColor;
            btnConfirm.ForeColor = originalBtnForeColor;

            btnConfirm.FlatStyle = FlatStyle.Popup;
            btnConfirm.FlatAppearance.BorderColor = originalBtnBorderColor;
            btnConfirm.FlatAppearance.BorderSize = 1;
            btnConfirm.Enabled = false;
            btnConfirm.Click += btnConfirm_Click;
            btnConfirm.MouseEnter += btn_MouseEnter;
            btnConfirm.MouseLeave += btn_MouseLeave;
            this.Controls.Add(btnConfirm);
            UpdateButtonAppearance();


            //txtPassword.UseSystemPasswordChar = true;
            //checkBox1.Checked = false;
            //lblPassword.Text = "Must be at least 7 characters with uppercase, lowercase, number, and special character.";
            //lblPassword.Visible = false;
            lblEmail.Visible = false;
            lblUsername.Visible = false;
            lblName.Visible = false;
            btnConfirm.Enabled = false;

            this.AcceptButton = btnConfirm;
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Enabled)
            {
                button.BackColor = Color.MidnightBlue;
                button.ForeColor = Color.White;
                Cursor = Cursors.Hand;
            }
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Enabled)
            {
                button.BackColor = Color.RoyalBlue;
                button.ForeColor = Color.White;
                Cursor = Cursors.Default;
            }
        }


        private void EditProfile_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Name, Username, Email FROM [User] WHERE Id = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", UserID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader["Name"].ToString();
                            txtUsername.Text = reader["Username"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                        }
                    }
                }
            }
        }

        private void ValidateForm()
        {
            btnConfirm.Enabled = isUsernameValid && isEmailValid && isNameValid;
            UpdateButtonAppearance();
        }

        private void txtUsername_TextChanged_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                lblUsername.Visible = false;
                isUsernameValid = false;
                return;
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM [User] WHERE Username = @Username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);


                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();

                    if (count > 0)
                    {
                        lblUsername.Text = "Username exists";
                        lblUsername.ForeColor = Color.Red;
                        lblUsername.Visible = true;
                        isUsernameValid = false;
                    }
                    string query1 = "SELECT Id FROM [User] WHERE Username = @Username";
                    SqlCommand cmd1 = new SqlCommand(query1, conn);
                    cmd1.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    object result = cmd1.ExecuteScalar();
                    conn.Close();

                    if (result != null)
                    {
                        int foundUserId = Convert.ToInt32(result);
                        if (foundUserId == UserID)
                        {
                            lblUsername.Text = "Username available";
                            lblUsername.ForeColor = Color.Green;
                            lblUsername.Visible = true;
                            isUsernameValid = true;
                        }
                        else
                        {
                            lblUsername.Text = "Username exists";
                            lblUsername.ForeColor = Color.Red;
                            lblUsername.Visible = true;
                            isUsernameValid = false;
                        }
                    }
                    else
                    {
                        // Username does not exist in the database
                        lblUsername.Text = "Username available";
                        lblUsername.ForeColor = Color.Green;
                        lblUsername.Visible = true;
                        isUsernameValid = true;
                    }

                }
            }
            ValidateForm();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                lblName.Visible = false;
                isNameValid = false;
            }

            else
            {
                lblName.Visible = false;
                isNameValid = true;
            }
            ValidateForm();

        }
        //private void txtPassword_TextChanged_1(object sender, EventArgs e)
        //{
        //    string password = txtPassword.Text;
        //    string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{7,}$";
        //    lblPassword.Visible = true;
        //    if (string.IsNullOrWhiteSpace(password))
        //    {
        //        lblPassword.Visible = false;
        //        isPasswordValid = false;
        //    }
        //    else if (Regex.IsMatch(password, pattern))
        //    {
        //        lblPassword.Text = "Strong password.";
        //        lblPassword.ForeColor = Color.Green;
        //        isPasswordValid = true;
        //        lblPassword.Visible = true;

        //    }
        //    else
        //    {
        //        lblPassword.Text = "Must be at least 7 characters with uppercase,\nlowercase, number, and special character.";
        //        lblPassword.ForeColor = Color.Red;
        //        lblPassword.Visible = true;
        //        isPasswordValid = false;

        //    }
        //    ValidateForm();

        //}

        //private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        //{
        //    txtPassword.UseSystemPasswordChar = !checkBox1.Checked;
        //}



        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string pattern = @"^[^@\s]+@(gmail\.com|outlook\.com|yahoo.com|outlook.com|icloud.com|student.aiub.edu)$";

            if (string.IsNullOrWhiteSpace(email))
            {
                lblEmail.Visible = false;
                isEmailValid = false;
            }
            else if (!Regex.IsMatch(email, pattern))
            {
                lblEmail.Text = "Please input a valid address.";
                lblEmail.ForeColor = Color.Red;
                lblEmail.Visible = true;
                isEmailValid = false;
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Check if the email belongs to the current user
                    string queryUser = "SELECT COUNT(*) FROM [User] WHERE Email = @Email AND Id = @UserId";
                    using (SqlCommand cmdUser = new SqlCommand(queryUser, conn))
                    {
                        cmdUser.Parameters.AddWithValue("@Email", email);
                        cmdUser.Parameters.AddWithValue("@UserId", UserID);
                        conn.Open();
                        int userCount = (int)cmdUser.ExecuteScalar();
                        conn.Close();

                        if (userCount > 0)
                        {
                            lblEmail.Visible = false;
                            isEmailValid = true;
                        }
                        else
                        {
                            // Check if email exists for any other user
                            string query = "SELECT COUNT(*) FROM [User] WHERE Email = @Email";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@Email", email);
                                conn.Open();
                                int count = (int)cmd.ExecuteScalar();
                                conn.Close();

                                if (count > 0)
                                {
                                    lblEmail.Text = "Email already exists.";
                                    lblEmail.ForeColor = Color.Red;
                                    lblEmail.Visible = true;
                                    isEmailValid = false;
                                }
                                else
                                {
                                    lblEmail.Visible = false;
                                    isEmailValid = true;
                                }
                            }
                        }
                    }
                }
            }
            ValidateForm();
        }


        private void UpdateButtonAppearance()
        {
            if (btnConfirm.Enabled)
            {

                btnConfirm.BackColor = originalBtnBackColor;
                btnConfirm.ForeColor = originalBtnForeColor;
                btnConfirm.FlatAppearance.BorderColor = originalBtnBorderColor;
                btnConfirm.Cursor = Cursors.Hand;
            }
            else
            {

                btnConfirm.BackColor = Color.Gray;
                btnConfirm.ForeColor = Color.White;
                btnConfirm.FlatAppearance.BorderColor = Color.Gray;
                btnConfirm.Cursor = Cursors.Default;
            }
        }

        

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            frmHomePage hp = new frmHomePage();
            hp.Show();
            this.Hide();
        }



        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!isUsernameValid || !isEmailValid || !isNameValid)
            {
                MessageBox.Show("Please fill all fields correctly before submitting.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE [User] SET Name = @Name, Username = @Username, Email = @Email WHERE Id = @UserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@UserId", UserID);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Profile changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            Dashboard.LoadNotes(Dashboard.GetAllNotesFromDB());
                        }
                        else
                        {
                            MessageBox.Show("Profile change failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}