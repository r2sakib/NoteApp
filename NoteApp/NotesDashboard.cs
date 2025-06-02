using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace NoteApp
{
    public partial class NotesDashboard : Form
    {

        private string UserType { get; set; }
        private string Id { get; set; }

        private FlowLayoutPanel notesContainer;

        public NotesDashboard(string userType, string id)
        {
            UserType = userType;
            Id = id;

            //InitializeComponent();
            SetupNotesContainer();
            SetupTitleLabel();
            LoadNotes(GetAllNotesFromDB());
        }

        private DataTable GetAllNotesFromDB()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NoteApp.mdf;Integrated Security=True;Connect Timeout=30";

            string query;
            if (UserType == "Admin")
            {
                query = @"SELECT NoteID, Title, Content, CreatedDate, ModifiedDate, Approved, Tags, AuthorID 
                        FROM [dbo].[Notes] 
                        ORDER BY CreatedDate DESC";
            }
            else
            {
                query = @"SELECT NoteID, Title, Content, CreatedDate, ModifiedDate, Approved, Tags, AuthorID
                        FROM [dbo].[Notes] 
                        WHERE Approved = 1
                        ORDER BY CreatedDate DESC";
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        private void LoadNotes(DataTable dt)
        {
            try
            {
                notesContainer.Controls.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    Note note = new Note(dr);
                    Panel noteCard = CreateNoteCard(note);
                    notesContainer.Controls.Add(noteCard);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error loading notes: {exp.Message}");
            }
        }

        private void SetupTitleLabel()
        {
            Label titleLabel = new Label
            {
                Text = "Notes Dashboard",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(20, 15, 0, 0)
            };
            this.Controls.Add(titleLabel);

            
        }

        private void SetupNotesContainer()
        {
            // Create main container
            notesContainer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(245, 245, 245),
                Top = 70
            };
            this.Controls.Add(notesContainer);
        }

        private Panel CreateNoteCard(Note note)
        {
            // Main card panel
            Panel card = new Panel
            {
                Size = new Size(300, 200),
                Margin = new Padding(10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Title Label 
            Label titleLabel = new Label
            {
                Text = note.Title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(15, 15),
                Size = new Size(270, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Content
            string content = note.Content;
            Label contentLabel = new Label
            {
                Text = content.Length > 100 ? content.Substring(0, 100) + "..." : content,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(15, 45),
                Size = new Size(270, 60),
                TextAlign = ContentAlignment.TopLeft
            };

            // Author
            string author = note.AuthorID;
            Label authorLabel = new Label
            {
                Text = author,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(15, 110),
                Size = new Size(100, 30)
            };

            // Created date
            Label dateLabel = new Label
            {
                Text = note.CreatedAt.ToString("MMM dd, yyyy"),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(150, 150, 150),
                Location = new Point(15, 130),
                Size = new Size(100, 20)
            };

            // Status
            bool isApproved = note.Approved;
            Label statusLabel = new Label
            {
                Text = isApproved ? "✓ Approved" : "⏳ Pending",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = isApproved ? Color.Green : Color.Orange,
                Location = new Point(200, 160),
                Size = new Size(80, 20),
                TextAlign = ContentAlignment.MiddleRight
            };

            // Tags
            string tags = note.Tag;
            Label tagLabel = new Label
            {
                Text = $"Tags: {tags}",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(200, 130),
                Size = new Size(80, 20),
                TextAlign = ContentAlignment.MiddleRight
            };


            // View button
            Button viewButton = new Button
            {
                Text = "View",
                Size = new Size(60, 35),
                Location = new Point(15, 150),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            viewButton.FlatAppearance.BorderSize = 0;
            viewButton.Font = new Font("Segoe UI", 8);

            // Edit button
            Button editButton = new Button
            {
                Text = "Edit",
                Size = new Size(60, 35),
                Location = new Point(85, 150),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            editButton.FlatAppearance.BorderSize = 0;
            editButton.Font = new Font("Segoe UI", 8);

            // Approve button
            Button approveButton = new Button
            {
                Text = "Approve",
                Size = new Size(60, 35),
                Location = new Point(85, 150),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            approveButton.FlatAppearance.BorderSize = 0;
            approveButton.Font = new Font("Segoe UI", 8);

            // Disapprove button
            Button disApproveButton = new Button
            {
                Text = "Disapprove",
                Size = new Size(75, 35),
                Location = new Point(85, 150),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            disApproveButton.FlatAppearance.BorderSize = 0;
            disApproveButton.Font = new Font("Segoe UI", 8);


            int noteId = note.Id;
            viewButton.Click += (s, e) => ViewNote(note);
            editButton.Click += (s, e) => EditNote(note);
            approveButton.Click += (s, e) => ApproveNote(note);
            disApproveButton.Click += (s, e) => DisapproveNote(note);

            // hover effects
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(248, 249, 250);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;


            // Add controls
            card.Controls.AddRange(new Control[] {
            titleLabel, contentLabel, dateLabel, statusLabel, viewButton, tagLabel, authorLabel

        });

            if (userType == "User")
                card.Controls.Add(editButton);

            if (!isApproved && userType == "Admin")
                card.Controls.Add(approveButton);

            if (isApproved && userType == "Admin")
                card.Controls.Add(disApproveButton);

            return card;
        }

        private void ViewNote(Note note)
        {
            MessageBox.Show($"View note with ID: {note.Id}");
        }

        private void EditNote(Note note)
        {
            MessageBox.Show($"Edit note with ID: {note.Id}");
        }

        private void ApproveNote(Note note)
        {
            bool response = note.ApproveNote();

            if (response)
            {
                MessageBox.Show($"Note with ID: {note.Id} has been approved.");
                LoadNotes(GetAllNotesFromDB());
            }
            else
            {
                MessageBox.Show("Failed to approve the note. Please try again.");
            }
        }

        private void DisapproveNote(Note note)
        {
            bool response = note.DisapproveNote();

            if (response)
            {
                MessageBox.Show($"Note with ID: {note.Id} has been disapproved.");
                LoadNotes(GetAllNotesFromDB());
            }
            else
            {
                MessageBox.Show("Failed to disapprove the note. Please try again.");
            }
        }
    }
}