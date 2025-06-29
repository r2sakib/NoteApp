using NoteApp;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

public class NoteService
{
    private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NoteApp.mdf;Integrated Security=True;Connect Timeout=30";

    public Note_F GetNoteById(int noteId)
    {
        Note_F note = null;

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Notes WHERE NoteID = @NoteID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NoteID", noteId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            note = new Note_F
                            {
                                NoteID = (int)reader["NoteID"],
                                Title = reader["Title"].ToString(),
                                Content = reader["Content"].ToString(),
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                ModifiedDate = (DateTime)reader["ModifiedDate"],
                                AuthorID = (int)reader["AuthorID"],
                                Tags = reader["Tags"].ToString(),
                                AttachmentPath = reader["AttachmentPath"].ToString(),
                                Approved = (bool)reader["Approved"]
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
        }

        return note;
    }

    public bool ApproveNote(int noteId)
    {
        bool success = false;

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Notes SET Approved = 1 WHERE NoteID = @NoteID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NoteID", noteId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    success = rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }

        return success;
    }

    public bool UpdateNoteContent(int noteId, string newContent)
    {
        bool success = false;

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Notes SET Content = @Content WHERE NoteID = @NoteID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NoteID", noteId);
                    cmd.Parameters.AddWithValue("@Content", newContent);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    success = rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }

        return success;
    }

    public bool DisapproveNote(int noteId)
    {
        bool success = false;

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Notes SET Approved = 0 WHERE NoteID = @NoteID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NoteID", noteId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    success = rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }

        return success;
    }

    public bool DeleteNote(int noteId)
    {
        bool success = false;
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Notes WHERE NoteID = @NoteID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NoteID", noteId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    success = rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
        return success;
    }

    public string getAuthorName(int authorId)
    {
        string authorName = null;
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Name FROM [User] WHERE Id = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", authorId);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        authorName = result.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
        return authorName;

    }
}
