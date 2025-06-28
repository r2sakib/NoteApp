using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NoteApp
{
    public partial class NotesDashboard : Form
    {

        private string UserType { get; set; }
        private string AuthorID { get; set; }

        private FlowLayoutPanel notesContainer;

        public NotesDashboard(string userType, string authorId)
        {
            UserType = userType;
            AuthorID = authorId;

            this.Size = new Size(1200, 800);

            string bgPath = Path.Combine(Application.StartupPath, "Resources", "background.png");
            //if (File.Exists(bgPath))
            //{
            //    this.BackgroundImage = Image.FromFile(bgPath);
            //    this.BackgroundImageLayout = ImageLayout.Stretch;
            //}
                this.BackColor = Color.FromArgb(242, 242, 242);

            //InitializeComponent();
            SetupNotesContainer();
            SetupHeader();
            LoadNotes(GetAllNotesFromDB());
            SearchNotes("");
            FilterNotes();
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
            Color headerButtonColor = Color.White;
            int headerButtonBorder = 1;

            // Title label
            Label titleLabel = new Label
            {
                Text = "Notes Dashboard",
                Font = new Font("Franklin Gothic Demi", 22, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                BackColor = Color.Transparent,
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
                Height = 60,
                Width = 600,
                Location = new Point(titleLabel.Width + 30, 15),
                Anchor = AnchorStyles.Top,
                BackColor = Color.Transparent,
            };

            // Search TextBox
            TextBox searchBox = new TextBox
            {
                Width = 350,
                Height = 30,
                Location = new Point(120, 10),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Text = "Search notes",
            };


            //searchBox.Multiline = true;
            //searchBox.Padding = new Padding(20, 15, 8, 8);

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
                Height = 30,
                Location = new Point(490, 8),
                BackColor = headerButtonColor,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.System,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            searchButton.FlatAppearance.BorderSize = headerButtonBorder;
            searchButton.FlatAppearance.BorderColor = Color.Black;


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

            // Author Notes filter Button
            Button authorNotesBtn = new Button
            {
                Text = "My Notes",
                Width = 100,
                Height = 30,
                Location = new Point(0, 5),
                BackColor = headerButtonColor,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            authorNotesBtn.FlatAppearance.BorderSize = headerButtonBorder;
            authorNotesBtn.FlatAppearance.BorderColor = Color.Black;

            authorNotesBtn.Click += (s, e) =>
            {
                if (authorNotesBtn.ForeColor == Color.Black)
                {
                    authorNotesBtn.BackColor = Color.Black;
                    authorNotesBtn.ForeColor = Color.White;
                    FilterNotes("AuthorNotes");
                }
                else if (authorNotesBtn.ForeColor == Color.White)
                {
                    authorNotesBtn.BackColor = Color.FromArgb(242, 242, 242);
                    authorNotesBtn.ForeColor = Color.Black;
                    FilterNotes("All");
                }
            };

            searchPanel.Controls.Add(authorNotesBtn);


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
                Location = new Point(10, 5),
                BackColor = headerButtonColor,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            editProfileButton.FlatAppearance.BorderSize = headerButtonBorder;
            editProfileButton.FlatAppearance.BorderColor = Color.Black;
            editProfileButton.Click += (s, e) => EditProfile();

            // Create Note Button
            Button createNoteButton = new Button
            {
                Text = "Create Note",
                Width = 110,
                Height = 30,
                Location = new Point(130, 5),
                BackColor = headerButtonColor,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            createNoteButton.FlatAppearance.BorderSize = headerButtonBorder;
            createNoteButton.FlatAppearance.BorderColor = Color.Black;
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
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            logoutButton.FlatAppearance.BorderSize = headerButtonBorder;
            logoutButton.FlatAppearance.BorderColor = Color.Black;
            logoutButton.Click += (s, e) => Logout();

            rightPanel.Controls.Add(editProfileButton);
            rightPanel.Controls.Add(createNoteButton);
            rightPanel.Controls.Add(logoutButton);
            this.Controls.Add(rightPanel);

            if (UserType == "User")
            {
                createNoteButton.Visible = true;
                editProfileButton.Visible = true;
            }
            else if (UserType == "Admin")
            {
                createNoteButton.Visible = false;
                editProfileButton.Visible = false;
            }


            // Approve filter Button
            Button approveFilterBtn = new Button
            {
                Text = "Approved",
                Width = 100,
                Height = 30,
                Location = new Point(0, 5),
                BackColor = headerButtonColor,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            approveFilterBtn.FlatAppearance.BorderSize = headerButtonBorder;
            approveFilterBtn.FlatAppearance.BorderColor = Color.Black;
            rightPanel.Controls.Add(approveFilterBtn);

            // Disapprove filter Button
            Button disapproveFilterBtn = new Button
            {
                Text = "Disapproved",
                Width = 120,
                Height = 30,
                Location = new Point(120, 5),
                BackColor = headerButtonColor,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            disapproveFilterBtn.FlatAppearance.BorderSize = headerButtonBorder;
            disapproveFilterBtn.FlatAppearance.BorderColor = Color.Black;
            rightPanel.Controls.Add(disapproveFilterBtn);


            approveFilterBtn.Click += (s, e) =>
            {
                if (approveFilterBtn.ForeColor == Color.Black)
                {
                    disapproveFilterBtn.BackColor = Color.FromArgb(242, 242, 242);
                    disapproveFilterBtn.ForeColor = Color.Black;
                    approveFilterBtn.BackColor = Color.Black;
                    approveFilterBtn.ForeColor = Color.White;
                    FilterNotes("Approved");
                }
                else if (approveFilterBtn.ForeColor == Color.White)
                {
                    approveFilterBtn.BackColor = Color.FromArgb(242, 242, 242);
                    approveFilterBtn.ForeColor = Color.Black;
                    FilterNotes("All");
                }
            };

            disapproveFilterBtn.Click += (s, e) =>
            {
                if (disapproveFilterBtn.ForeColor == Color.Black)
                {
                    approveFilterBtn.BackColor = Color.FromArgb(242, 242, 242);
                    approveFilterBtn.ForeColor = Color.Black;
                    disapproveFilterBtn.BackColor = Color.Black;
                    disapproveFilterBtn.ForeColor = Color.White;
                    FilterNotes("Pending");
                }
                else if (disapproveFilterBtn.ForeColor == Color.White)
                {
                    disapproveFilterBtn.BackColor = Color.FromArgb(242, 242, 242);
                    disapproveFilterBtn.ForeColor = Color.Black;
                    FilterNotes("All");
                }
            };

            if (UserType == "User")
            {
                approveFilterBtn.Visible = false;
                disapproveFilterBtn.Visible = false;
            }
            if (UserType == "Admin")
            {
                approveFilterBtn.Visible = true;
                disapproveFilterBtn.Visible = true;
            }

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
                Padding = new Padding(100, 30, 20, 100),
                BackColor = Color.Transparent,
                Top = 70
            };
            this.Controls.Add(notesContainer);
        }

        private Panel CreateNoteCard(Note note)
        {
            Color buttonColor = Color.FromArgb(235, 235, 235);
            int buttonBorder = 0;

            // Scaling factor for the new size
            float scaleFactor = 1.3f;

            // Use the custom BorderedPanel instead of Panel
            BorderedPanel card = new BorderedPanel
            {
                Size = new Size((int)(300 * scaleFactor), (int)(200 * scaleFactor)),
                Margin = new Padding(20),
                BackColor = Color.White,
                BorderColor = Color.FromArgb(242, 242, 242),
                BorderThickness = 0,
            };

            // Calculate card dimensions for easier positioning
            int cardWidth = (int)(300 * scaleFactor);
            int cardHeight = (int)(200 * scaleFactor);

            // Title Label
            Label titleLabel = new Label
            {
                Text = note.Title,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(15, 15),
                Size = new Size(cardWidth - 44, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Content
            string content = note.Content;
            Label contentLabel = new Label
            {
                Text = content.Length > 150 ? content.Substring(0, 150) + "..." : content,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Black,
                Location = new Point(15, 50),
                Size = new Size(cardWidth - 44, 90),
                TextAlign = ContentAlignment.TopLeft,
            };


            // Author
            string author = note.AuthorID;
            Label authorLabel = new Label
            {
                Text = author,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Black,
                Location = new Point(15, 155),
                Size = new Size(cardWidth / 2 - 30, 25)
            };

            // Created date
            Label dateLabel = new Label
            {
                Text = note.CreatedAt.ToString("MMM dd, yyyy"),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Black,
                Location = new Point(15, 180),
                Size = new Size(150, 20)
            };

            // Status - moved to visible position
            bool isApproved = note.Approved;
            Label statusLabel = new Label
            {
                Text = isApproved ? "✓ Approved" : "⏳ Pending",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = isApproved ? Color.Green : Color.Brown,
                Location = new Point(cardWidth - 130, 215),
                Size = new Size(120, 25),
                TextAlign = ContentAlignment.MiddleRight
            };

            string tags = note.Tag;
            Label tagLabel = new Label
            {
                Text = $"Tags: {tags}",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.Black,
                Location = new Point(cardWidth - 130, 180),
                Size = new Size(120, 20),
                TextAlign = ContentAlignment.MiddleRight
            };
            if (!tags.Equals(""))
            {
                card.Controls.Add(tagLabel);
            }

            // View button
            Button viewButton = new Button
            {
                Text = "View",
                Size = new Size(80, 35),
                Location = new Point(15, cardHeight - 50),
                ForeColor = Color.Black,
                BackColor = buttonColor,
                FlatStyle = FlatStyle.System,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
            };
            viewButton.FlatAppearance.BorderSize = buttonBorder;
            viewButton.FlatAppearance.BorderColor = buttonColor;

            //viewButton.MouseHover += (s, e) =>
            //{
            //    viewButton.BackColor = Color.Red;
            //};

            // Edit button
            Button editButton = new Button
            {
                Text = "Edit",
                Size = new Size(80, 35),
                Location = new Point(112, cardHeight - 50),
                ForeColor = Color.Black,
                BackColor = buttonColor,
                FlatStyle = FlatStyle.System,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            editButton.FlatAppearance.BorderSize = buttonBorder;
            editButton.FlatAppearance.BorderColor = Color.Black;

            // Approve button
            Button approveButton = new Button
            {
                Text = "Approve",
                Size = new Size(90, 35),
                Location = new Point(112, cardHeight - 50),
                ForeColor = Color.Black,
                BackColor = buttonColor,
                FlatStyle = FlatStyle.System,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            approveButton.FlatAppearance.BorderSize = buttonBorder;
            approveButton.FlatAppearance.BorderColor = Color.Black;

            // Disapprove button
            Button disApproveButton = new Button
            {
                Text = "Disapprove",
                Size = new Size(110, 35),
                Location = new Point(112, cardHeight - 50),
                ForeColor = Color.Black,
                BackColor = buttonColor ,
                FlatStyle = FlatStyle.System,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            disApproveButton.FlatAppearance.BorderSize = buttonBorder;
            disApproveButton.FlatAppearance.BorderColor = Color.Black;

            int noteId = note.Id;
            viewButton.Click += (s, e) => ViewNote(note);
            editButton.Click += (s, e) => EditNote(note);
            approveButton.Click += (s, e) => ApproveNote(note);
            disApproveButton.Click += (s, e) => DisapproveNote(note);

            // Add controls
            card.Controls.AddRange(new Control[] {
        titleLabel, contentLabel, dateLabel, statusLabel, viewButton, authorLabel
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

        private void FilterNotes(string type = "All")
        {
            LoadNotes(GetAllNotesFromDB());

            if (type == "Approved")
            {
                DataTable allNotes = GetAllNotesFromDB();
                DataTable approvedNotes = allNotes.Select("Approved = 1").CopyToDataTable();
                LoadNotes(approvedNotes);
            }
            else if (type == "Pending")
            {
                DataTable allNotes = GetAllNotesFromDB();
                DataTable pendingNotes = allNotes.Select("Approved = 0").CopyToDataTable();
                LoadNotes(pendingNotes);
            }
            else if (type == "AuthorNotes")
            {
                DataTable allNotes = GetAllNotesFromDB();
                DataTable authorNotes = allNotes.Select($"AuthorID = {Convert.ToInt32(AuthorID)}").CopyToDataTable();
                LoadNotes(authorNotes);
            }
            else
            {
                LoadNotes(GetAllNotesFromDB());
            }
        }
    }
        // Custom Panel class for border
        public class BorderedPanel : Panel
        {
            public Color BorderColor { get; set; } = Color.Black;
            public int BorderThickness { get; set; } = 2;

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                using (Pen pen = new Pen(BorderColor, BorderThickness))
                {
                    pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                }
            }
        }
}