using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Windows.Forms;

namespace NoteApp
{
    class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Tag { get; set; }
        public string AuthorID { get; set; }
        public bool Approved { get; set; } = false;


        public Note(int id, string title, string content, DateTime createdAt, DateTime updatedAt, string tag, string authorId, bool approved)
        {
            Id = id;
            Title = title;
            Content = content;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Tag = tag;
            AuthorID = authorId;
            Approved = approved;
        }

        public Note(DataRow dr)
        {
            Id = Convert.ToInt32(dr["NoteID"]);
            Title = dr["Title"].ToString();
            Content = dr["Content"].ToString();
            CreatedAt = Convert.ToDateTime(dr["CreatedDate"]);
            UpdatedAt = Convert.ToDateTime(dr["ModifiedDate"]);
            Tag = dr["Tags"]?.ToString() ?? "";
            AuthorID = dr["AuthorID"].ToString();
            Approved = Convert.ToBoolean(dr["Approved"]);
        }

        public Note()
        {
            
        }

        public bool ApproveNote()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NoteApp.mdf;Integrated Security=True;Connect Timeout=30";

            string query = @"UPDATE [dbo].[Notes] 
                            SET Approved = 1
                            WHERE NoteID = @NoteID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NoteID", Id);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public bool DisapproveNote()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NoteApp.mdf;Integrated Security=True;Connect Timeout=30";

            string query = @"UPDATE [dbo].[Notes] 
                            SET Approved = 0
                            WHERE NoteID = @NoteID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NoteID", Id);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }



    }
}
