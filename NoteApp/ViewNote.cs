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
    public partial class ViewNote : Form
    {
        private int noteId;
        private string userType;
        private int userId;
        private NotesDashboard Dashboard;
        public ViewNote(int noteId, string userType, int userId, NotesDashboard dashboard)
        {
            InitializeComponent();
            this.noteId = noteId;
            this.userType = userType;
            this.userId = userId;
            this.Dashboard = dashboard;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewNote));
            this.btnDisapprove = new System.Windows.Forms.Button();
            this.uploadedBy = new System.Windows.Forms.Label();
            this.tags = new System.Windows.Forms.Label();
            this.content = new System.Windows.Forms.TextBox();
            this.title = new System.Windows.Forms.Label();
            this.modifiedAt = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.status = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDisapprove
            // 
            this.btnDisapprove.BackColor = System.Drawing.Color.Crimson;
            this.btnDisapprove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDisapprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisapprove.Font = new System.Drawing.Font("Dubai", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisapprove.ForeColor = System.Drawing.Color.White;
            this.btnDisapprove.Location = new System.Drawing.Point(762, 570);
            this.btnDisapprove.Margin = new System.Windows.Forms.Padding(0);
            this.btnDisapprove.Name = "btnDisapprove";
            this.btnDisapprove.Size = new System.Drawing.Size(131, 38);
            this.btnDisapprove.TabIndex = 9;
            this.btnDisapprove.Text = "Disapprove";
            this.btnDisapprove.UseVisualStyleBackColor = false;
            this.btnDisapprove.Visible = false;
            this.btnDisapprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // uploadedBy
            // 
            this.uploadedBy.AutoSize = true;
            this.uploadedBy.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold);
            this.uploadedBy.ForeColor = System.Drawing.Color.MidnightBlue;
            this.uploadedBy.Location = new System.Drawing.Point(47, 488);
            this.uploadedBy.Name = "uploadedBy";
            this.uploadedBy.Size = new System.Drawing.Size(107, 27);
            this.uploadedBy.TabIndex = 8;
            this.uploadedBy.Text = "Uploaded by: ";
            // 
            // tags
            // 
            this.tags.AutoSize = true;
            this.tags.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold);
            this.tags.ForeColor = System.Drawing.Color.DarkBlue;
            this.tags.Location = new System.Drawing.Point(47, 460);
            this.tags.Name = "tags";
            this.tags.Size = new System.Drawing.Size(51, 27);
            this.tags.TabIndex = 7;
            this.tags.Text = "Tags: ";
            this.tags.Click += new System.EventHandler(this.tags_Click);
            // 
            // content
            // 
            this.content.AcceptsReturn = true;
            this.content.BackColor = System.Drawing.Color.WhiteSmoke;
            this.content.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.content.Cursor = System.Windows.Forms.Cursors.Default;
            this.content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.content.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.content.ForeColor = System.Drawing.Color.Black;
            this.content.Location = new System.Drawing.Point(10, 10);
            this.content.Multiline = true;
            this.content.Name = "content";
            this.content.ReadOnly = true;
            this.content.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.content.Size = new System.Drawing.Size(818, 335);
            this.content.TabIndex = 6;
            this.content.TabStop = false;
            this.content.Enter += new System.EventHandler(this.content_Enter);
            this.content.Leave += new System.EventHandler(this.content_Leave);
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.Color.MidnightBlue;
            this.title.Location = new System.Drawing.Point(46, 54);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(58, 30);
            this.title.TabIndex = 5;
            this.title.Text = "Title";
            // 
            // modifiedAt
            // 
            this.modifiedAt.AutoSize = true;
            this.modifiedAt.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold);
            this.modifiedAt.ForeColor = System.Drawing.Color.MidnightBlue;
            this.modifiedAt.Location = new System.Drawing.Point(47, 516);
            this.modifiedAt.Name = "modifiedAt";
            this.modifiedAt.Size = new System.Drawing.Size(98, 27);
            this.modifiedAt.TabIndex = 10;
            this.modifiedAt.Text = "Modified at: ";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DarkGray;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Dubai", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(762, 523);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(131, 38);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Font = new System.Drawing.Font("Dubai", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(762, 476);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(131, 38);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.content);
            this.panel1.Location = new System.Drawing.Point(53, 95);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(840, 357);
            this.panel1.TabIndex = 13;
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold);
            this.status.ForeColor = System.Drawing.Color.DarkGreen;
            this.status.Location = new System.Drawing.Point(116, 543);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(81, 27);
            this.status.TabIndex = 14;
            this.status.Text = "Approved";
            this.status.Click += new System.EventHandler(this.status_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(47, 543);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 27);
            this.label1.TabIndex = 15;
            this.label1.Text = "Status:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Crimson;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Font = new System.Drawing.Font("Dubai", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(762, 570);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(131, 38);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ViewNote
            // 
            this.AccessibleName = "";
            this.BackgroundImage = global::NoteApp.Properties.Resources.View_Create_bg;
            this.ClientSize = new System.Drawing.Size(944, 661);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.status);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.modifiedAt);
            this.Controls.Add(this.btnDisapprove);
            this.Controls.Add(this.uploadedBy);
            this.Controls.Add(this.tags);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.title);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewNote";
            this.Text = "View Note";
            this.Load += new System.EventHandler(this.ViewNoteForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ViewNoteForm_Load(object sender, EventArgs e)
        {
            NoteService noteService = new NoteService();
            Note_F note = noteService.GetNoteById(noteId);

            string AuthorName = noteService.getAuthorName(note.AuthorID);
            if (note != null)
            {
                if (userType == "User" && note.AuthorID == userId)
                {
                    btnEdit.Enabled = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnEdit.Enabled = false;
                    btnEdit.BackColor = Color.DarkGray;
                    btnDelete.Visible = false;
                }

                    title.Text = note.Title;
                content.Text = note.Content;
                tags.Text = "Tags: " + note.Tags;
                uploadedBy.Text = "Uploaded By: " + AuthorName;
                modifiedAt.Text = "Modified At: " + note.ModifiedDate.ToString();

                if (userType == "Admin")
                {
                    btnDisapprove.Visible = true;
                    btnDelete.Visible = false;

                    if (note.Approved)
                    {
                        
                        btnDisapprove.Text = "Disapprove";
                        btnDisapprove.BackColor = Color.Red;
                        btnDisapprove.ForeColor = Color.White;
                        status.Text = "Approved";
                        status.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2FA442");
                    }
                    else
                    {
                        btnDisapprove.Text = "Approve";
                        btnDisapprove.BackColor = Color.Green;
                        btnDisapprove.ForeColor = Color.White;
                        status.Text = "Pending";
                        status.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFA400");
                    }
                }
                else
                {
                    btnDisapprove.Visible = false;
                    status.Visible = true;

                    if (note.Approved)
                    {
                        status.Text = "Approved";
                        status.ForeColor = Color.Green;
                    }
                    else
                    {
                        status.Text = "Pending";
                        status.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFA400");
                    }
                }
            }
            else
            {
                MessageBox.Show("Note not found.");
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            NoteService noteService = new NoteService();
            Note_F note = noteService.GetNoteById(noteId);

            if (note != null)
            {
                bool success = false;

                if (note.Approved)
                {
                    success = noteService.DisapproveNote(noteId);
                    if (success)
                    {
                        MessageBox.Show("Note Disapproved Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dashboard.LoadNotes(Dashboard.GetAllNotesFromDB());
                    }
                }
                else
                {
                    success = noteService.ApproveNote(noteId);
                    if (success)
                    {
                        MessageBox.Show("Note Approved Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dashboard.LoadNotes(Dashboard.GetAllNotesFromDB());
                    }
                }

                if (success)
                {
                    ViewNoteForm_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Failed to update note status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            content.ReadOnly = false;
            btnSave.Enabled = true;
            btnSave.BackColor = Color.RoyalBlue;
            btnEdit.Enabled = false;
            btnEdit.BackColor = Color.DarkGray;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            NoteService noteService = new NoteService();
            bool isUpdated = noteService.UpdateNoteContent(noteId, content.Text);

            if (isUpdated)
            {
                MessageBox.Show("Note updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dashboard.LoadNotes(Dashboard.GetAllNotesFromDB());

                content.ReadOnly = true;
                btnSave.Enabled = false;
                btnSave.BackColor = Color.DarkGray;
                btnEdit.Enabled = true;
                btnEdit.BackColor = Color.RoyalBlue;
            }
            else
            {
                MessageBox.Show("Failed to update the note.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void content_Enter(object sender, EventArgs e)
        {
            this.panel1.BackColor = Color.LightBlue;
        }

        private void content_Leave(object sender, EventArgs e)
        {
            this.panel1.BackColor = Color.White;
        }

        private void status_Click(object sender, EventArgs e)
        {

        }

        private void tags_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            NoteService noteService = new NoteService();
            bool success = noteService.DeleteNote(noteId);

            if (success)
            {
                MessageBox.Show("Note deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                Dashboard.LoadNotes(Dashboard.GetAllNotesFromDB());
            }
            else
            {
                MessageBox.Show("Failed to delete the note.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
