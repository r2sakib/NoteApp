using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    internal class NoteServices_T
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NoteApp.mdf;Integrated Security=True;Connect Timeout=30";

        public bool CreateNote(Note_T note)
        {
            string query = @"INSERT INTO Notes (Title, Content, Tags, AuthorID)
                         VALUES (@Title, @Content, @Tags, @AuthorID)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Title", note.Title);
                cmd.Parameters.AddWithValue("@Content", note.Content);
                cmd.Parameters.AddWithValue("@Tags", note.Tags);
                cmd.Parameters.AddWithValue("@AuthorID", note.AuthorID);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool IsTitleUnique(string title)
        {
            string query = "SELECT COUNT(*) FROM Notes WHERE Title = @Title";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Title", title);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count == 0;
            }
        }
    }
}
