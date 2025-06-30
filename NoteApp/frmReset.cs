using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Noteapp
{
    public partial class frmReset : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NoteApp.mdf;Integrated Security=True;Connect Timeout=30";
        string UserType;

        public frmReset(string userType)
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
            txtRePass.UseSystemPasswordChar = true;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            lblPassword.Text = "Must be at least 7 characters with uppercase, lowercase, number, and special character.";
            lblPassword.Visible = false;
            lblRePass.Visible = false;
            lblUsername.Visible = false;
            btnConfirm.MouseEnter += btn_MouseEnter;
            btnConfirm.MouseLeave += btn_MouseLeave;
            this.Load += EditProfile_Load;
            this.UserType = userType;

            this.AcceptButton = btnConfirm;
        }

        private void EditProfile_Load(object sender, EventArgs e)
        {
            if (UserType == "User")
            {
                label1.Text = "Reset Password";
            }
            else if (UserType == "Admin")
            {
                label1.Text = "Admin Reset Password";
            }
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
                string query;
                if (UserType == "User")
                {
                    query = "SELECT COUNT(*) FROM [User] WHERE Username = @Username";
                }
                else
                {
                    query = "SELECT COUNT(*) FROM [Admin] WHERE Username = @Username";
                }

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

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            //string password = txtPassword.Text;
            //string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{7,}$";
            //lblPassword.Visible = true;
            //if (Regex.IsMatch(password, pattern))
            //{
            //    lblPassword.Text = "Strong password.";
            //    lblPassword.ForeColor = Color.Green;
            //}
            //else
            //{
            //    lblPassword.Text = "Must be at least 7 characters with uppercase,\nlowercase, number, and special character.";
            //    lblPassword.ForeColor = Color.Red;

            //}

        }


        private void txtRePass_TextChanged(object sender, EventArgs e)
        {
            string password = txtRePass.Text;
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{7,}$";
            lblRePass.Visible = true;
            if (Regex.IsMatch(password, pattern))
            {
                lblRePass.Text = "Strong password.";
                lblRePass.ForeColor = Color.Green;
            }
            else
            {
                lblRePass.Text = "Must be at least 7 characters with uppercase,\nlowercase, number, and special character.";
                lblRePass.ForeColor = Color.Red;

            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            txtRePass.UseSystemPasswordChar = !checkBox2.Checked;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtRePass.Text))
            {
                MessageBox.Show("Must include Username and Password.", "ERROR", MessageBoxButtons.OK);
                return;
            }

            string username = txtUsername.Text.Trim();
            string oldPassword = txtPassword.Text;
            string newPassword = txtRePass.Text;

            if (lblUsername.Visible || lblRePass.ForeColor == Color.Red)
            {
                MessageBox.Show("Please correct the errors before proceeding.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string selectQuery;
                if (UserType == "User")
                {
                    selectQuery = "SELECT Password FROM [User] WHERE Username = @Username";

                }
                else
                {
                    selectQuery = "SELECT Password FROM [Admin] WHERE Username = @Username";
                }
                SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                selectCmd.Parameters.AddWithValue("@Username", username);
                conn.Open();

                object dbPasswordObj = selectCmd.ExecuteScalar();
                if (dbPasswordObj == null)
                {
                    MessageBox.Show("Username does not exist.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string dbPassword = dbPasswordObj.ToString();
                if (dbPassword != oldPassword)
                {
                    MessageBox.Show("Old password is incorrect.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string updateQuery;
                if (UserType == "User")
                {
                    updateQuery = "UPDATE [User] SET Password = @NewPassword WHERE Username = @Username";
                }
                else
                {
                    updateQuery = "UPDATE [Admin] SET Password = @NewPassword WHERE Username = @Username";
                }


                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@NewPassword", newPassword);
                updateCmd.Parameters.AddWithValue("@Username", username);

                int rowsAffected = updateCmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password reset successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmLogin l = new frmLogin();
                    l.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Failed to reset password. Please try again.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmLogin l = new frmLogin();
            l.Show();
            this.Hide();
        }

        private void frmReset_Load(object sender, EventArgs e)
        {

        }
    }
}
