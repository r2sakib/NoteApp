using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteApp
{
    public partial class CreateNote : Form
    {
        public int UserID;
        public NotesDashboard Dashboard;
        public CreateNote(int userId, NotesDashboard dashboard)
        {
            InitializeComponent();
            this.UserID = userId;
            this.Dashboard = dashboard;

            submit.MouseEnter += btn_MouseEnter;
            submit.MouseLeave += btn_MouseLeave;
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

        private void title_TextChanged(object sender, EventArgs e)
        {
            string enteredTitle = title.Text.Trim();
            if (string.IsNullOrWhiteSpace(enteredTitle))
            {
                return;
            }

            NoteServices_T noteService = new NoteServices_T();
            bool isUnique = noteService.IsTitleUnique(enteredTitle);

            if (!isUnique)
            {
                lblTitleExists.Visible = true;
            }
            else
            {
               lblTitleExists.Visible = false;
            }
        }

        private void content_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void uploadedBy_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(title.Text) || string.IsNullOrWhiteSpace(content.Text))
            {
                MessageBox.Show("Title and Content are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Note_T newNote = new Note_T
            {
                Title = title.Text.Trim(),
                Content = content.Text.Trim(),
                Tags = tags.Text.Trim(),
                AuthorID = this.UserID
            };

            NoteServices_T noteService = new NoteServices_T();
            bool success = noteService.CreateNote(newNote);

            if (success)
            {
                MessageBox.Show("Note created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                Dashboard.LoadNotes(Dashboard.GetAllNotesFromDB());
            }
            else
            {
                MessageBox.Show("Failed to create note.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.uploadedBy.Text = "Uploaded by: " + this.AuthorID;
            //this.modifiedAt.Text = "Modified at: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lblTitleExists.Visible = false;
        }

        
    }
}
