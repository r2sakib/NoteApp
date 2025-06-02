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
            SetupHeader();
            LoadNotes(GetAllNotesFromDB());
            SearchNotes("");
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

        private void SetupHeader()
        {
            // Title label
            Label titleLabel = new Label
            {
                Text = "Notes Dashboard",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(20, 15, 0, 0)
            };
            titleLabel.AutoSize = true;
            this.Controls.Add(titleLabel);

            // Search Panel
            Panel searchPanel = new Panel
            {
                Height = 40,
                Width = 450,
                Location = new Point((this.ClientSize.Width - 430) / 2, 15),
                Anchor = AnchorStyles.Top
            };

            // Search TextBox
            TextBox searchBox = new TextBox
            {
                Width = 350,
                Height = 30,
                Location = new Point(0, 5),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(5),
                Text = "Search notes"
            };

            searchBox.GotFocus += (s, e) =>
            {
                if (searchBox.Text == "Search notes")
                {
                    searchBox.Text = "";
                    searchBox.ForeColor = Color.Black;
                }
            };

            searchBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(searchBox.Text))
                {
                    searchBox.Text = "Search notes";
                    searchBox.ForeColor = Color.Gray;
                }
            };

            // Search Button
            Button searchButton = new Button
            {
                Text = "Search",
                Width = 70,
                Height = 25,
                Location = new Point(370, 5),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            searchButton.FlatAppearance.BorderSize = 0;

            searchButton.Click += (s, e) => SearchNotes(searchBox.Text);
            searchButton.Click += (s, e) => SearchNotes(searchBox.Text);

            searchBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchNotes(searchBox.Text);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            searchPanel.Controls.Add(searchBox);
            searchPanel.Controls.Add(searchButton);

            this.Controls.Add(searchPanel);


            Panel rightPanel = new Panel
            {
                Height = 40,
                Width = 380,
                Location = new Point(this.ClientSize.Width - 400, 15),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent
            };

            // Edit Profile Button
            Button editProfileButton = new Button
            {
                Text = "Edit Profile",
                Width = 100,
                Height = 30,
                Location = new Point(0, 5),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            editProfileButton.FlatAppearance.BorderSize = 0;
            editProfileButton.Click += (s, e) => EditProfile();

            // Create Note Button
            Button createNoteButton = new Button
            {
                Text = "Create Note",
                Width = 120,
                Height = 30,
                Location = new Point(120, 5),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };

            createNoteButton.FlatAppearance.BorderSize = 0;
            createNoteButton.Click += (s, e) => CreateNote();

            // Logout Button
            Button logoutButton = new Button
            {
                Text = "Logout",
                Width = 100,
                Height = 30,
                Location = new Point(260, 5),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.Click += (s, e) => Logout();

            rightPanel.Controls.Add(editProfileButton);
            rightPanel.Controls.Add(createNoteButton);
            rightPanel.Controls.Add(logoutButton);
            this.Controls.Add(rightPanel);
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
                Padding = new Padding(20, 50, 20, 20),
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
                Size = new Size(60, 30),
                Location = new Point(15, 150),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
            };
            viewButton.FlatAppearance.BorderSize = 0;
            viewButton.Font = new Font("Segoe UI", 8);

            // Edit button
            Button editButton = new Button
            {
                Text = "Edit",
                Size = new Size(60, 30),
                Location = new Point(85, 150),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
            };
            editButton.FlatAppearance.BorderSize = 0;
            editButton.Font = new Font("Segoe UI", 8);

            // Approve button
            Button approveButton = new Button
            {
                Text = "Approve",
                Size = new Size(60, 30),
                Location = new Point(85, 150),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
            };
            approveButton.FlatAppearance.BorderSize = 0;
            approveButton.Font = new Font("Segoe UI", 8);

            // Disapprove button
            Button disApproveButton = new Button
            {
                Text = "Disapprove",
                Size = new Size(75, 30),
                Location = new Point(85, 150),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
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

            if (UserType == "User")
                card.Controls.Add(editButton);

            if (!isApproved && UserType == "Admin")
                card.Controls.Add(approveButton);

            if (isApproved && UserType == "Admin")
                card.Controls.Add(disApproveButton);

            return card;
        }

        private void Logout()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                // new Login().Show();
            }
        }

        private void EditProfile()
        {
            MessageBox.Show("Edit Profile clicked.");
        }

        private void CreateNote()
        {
            MessageBox.Show("Create Note clicked.");
        }

        private DataTable SearchNotesFromDB(string searchQuery)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NoteApp.mdf;Integrated Security=True;Connect Timeout=30";

            string query;
            if (UserType == "Admin")
            {
                query = @"SELECT NoteID, Title, Content, CreatedDate, ModifiedDate, Approved, Tags, AuthorID 
                FROM [dbo].[Notes] 
                WHERE Title LIKE @Search OR Content LIKE @Search OR Tags LIKE @Search
                ORDER BY CreatedDate DESC";
            }
            else
            {
                query = @"SELECT NoteID, Title, Content, CreatedDate, ModifiedDate, Approved, Tags, AuthorID
                FROM [dbo].[Notes] 
                WHERE (Title LIKE @Search OR Content LIKE @Search OR Tags LIKE @Search)
                AND Approved = 1
                ORDER BY CreatedDate DESC";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Search", "%" + searchQuery + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        private void SearchNotes(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery) || searchQuery == "Search notes")
            {
                // If search is empty, show all notes
                LoadNotes(GetAllNotesFromDB());
                return;
            }

            try
            {
                DataTable results = SearchNotesFromDB(searchQuery);
                LoadNotes(results);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while searching notes: {ex.Message}", "Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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