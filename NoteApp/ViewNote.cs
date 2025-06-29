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
        public ViewNote(int noteId, string userType, int userId)
        {
            InitializeComponent();
            this.noteId = noteId;
            this.userType = userType;
            this.userId = userId;
        }

        private void InitializeComponent()
        {
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
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDisapprove
            // 
            this.btnDisapprove.BackColor = System.Drawing.Color.Crimson;
            this.btnDisapprove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDisapprove.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisapprove.ForeColor = System.Drawing.Color.White;
            this.btnDisapprove.Location = new System.Drawing.Point(745, 542);
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
            this.uploadedBy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.uploadedBy.ForeColor = System.Drawing.Color.MidnightBlue;
            this.uploadedBy.Location = new System.Drawing.Point(32, 475);
            this.uploadedBy.Name = "uploadedBy";
            this.uploadedBy.Size = new System.Drawing.Size(116, 23);
            this.uploadedBy.TabIndex = 8;
            this.uploadedBy.Text = "Uploaded by: ";
            // 
            // tags
            // 
            this.tags.AutoSize = true;
            this.tags.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tags.ForeColor = System.Drawing.Color.DarkBlue;
            this.tags.Location = new System.Drawing.Point(32, 443);
            this.tags.Name = "tags";
            this.tags.Size = new System.Drawing.Size(52, 23);
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
            this.content.Font = new System.Drawing.Font("Segoe UI", 10F);
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
            this.title.Font = new System.Drawing.Font("Dubai", 16F, System.Drawing.FontStyle.Bold);
            this.title.ForeColor = System.Drawing.Color.MidnightBlue;
            this.title.Location = new System.Drawing.Point(28, 23);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(73, 45);
            this.title.TabIndex = 5;
            this.title.Text = "Title";
            // 
            // modifiedAt
            // 
            this.modifiedAt.AutoSize = true;
            this.modifiedAt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.modifiedAt.ForeColor = System.Drawing.Color.MidnightBlue;
            this.modifiedAt.Location = new System.Drawing.Point(34, 506);
            this.modifiedAt.Name = "modifiedAt";
            this.modifiedAt.Size = new System.Drawing.Size(106, 23);
            this.modifiedAt.TabIndex = 10;
            this.modifiedAt.Text = "Modified at: ";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(123)))), ((int)(((byte)(254)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(745, 490);
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
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(123)))), ((int)(((byte)(254)))));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(745, 446);
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
            this.panel1.Location = new System.Drawing.Point(36, 71);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(840, 357);
            this.panel1.TabIndex = 13;
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.status.ForeColor = System.Drawing.Color.DarkGreen;
            this.status.Location = new System.Drawing.Point(32, 548);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(104, 20);
            this.status.TabIndex = 14;
            this.status.Text = "✓ Approved";
            this.status.Click += new System.EventHandler(this.status_Click);
            // 
            // ViewNote
            // 
            this.AccessibleName = "";
            this.ClientSize = new System.Drawing.Size(953, 608);
            this.Controls.Add(this.status);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.modifiedAt);
            this.Controls.Add(this.btnDisapprove);
            this.Controls.Add(this.uploadedBy);
            this.Controls.Add(this.tags);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.title);
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

            if (note != null)
            {
                if (userType == "user" && note.AuthorID == userId)
                {
                    btnEdit.Enabled = true;
                }
                else
                {
                    btnEdit.Enabled = false;
                }

                    title.Text = note.Title;
                content.Text = note.Content;
                tags.Text = "Tags: " + note.Tags;
                uploadedBy.Text = "Uploaded By: " + note.AuthorID;
                modifiedAt.Text = "Modified At: " + note.ModifiedDate.ToString();

                if (userType.ToLower() == "admin")
                {
                    btnDisapprove.Visible = true;

                    if (note.Approved)
                    {
                        
                        btnDisapprove.Text = "Disapprove";
                        btnDisapprove.BackColor = Color.Red;
                        btnDisapprove.ForeColor = Color.White;
                        status.Text = "✓ Approved";
                        status.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2FA442");
                    }
                    else
                    {
                        btnDisapprove.Text = "Approve";
                        btnDisapprove.BackColor = System.Drawing.ColorTranslator.FromHtml("#2FA442");
                        btnDisapprove.ForeColor = Color.White;
                        status.Text = "⏳ Pending";
                        status.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFA400");
                    }
                }
                else
                {
                    btnDisapprove.Visible = false;
                    status.Visible = true;

                    if (note.Approved)
                    {
                        status.Text = "✓ Approved";
                        status.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2FA442");
                    }
                    else
                    {
                        status.Text = "⏳ Pending";
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
                        MessageBox.Show("Note Disapproved Successfully.");
                    }
                }
                else
                {
                    success = noteService.ApproveNote(noteId);
                    if (success)
                    {
                        MessageBox.Show("Note Approved Successfully.");
                    }
                }

                if (success)
                {
                    ViewNoteForm_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Failed to update note status.");
                }
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            content.ReadOnly = false;
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            NoteService noteService = new NoteService();
            bool isUpdated = noteService.UpdateNoteContent(noteId, content.Text);

            if (isUpdated)
            {
                MessageBox.Show("Note updated successfully.");

                content.ReadOnly = true;
                btnSave.Enabled = false;
                btnEdit.Enabled = true;
            }
            else
            {
                MessageBox.Show("Failed to update the note.");
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
    }
}
