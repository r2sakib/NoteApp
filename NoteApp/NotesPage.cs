using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteApp
{
    public partial class NotesPage: Form
    {
        SqlConnection con;

        public void sqlcon()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NoteApp.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
        }

        public NotesPage()
        {
            InitializeComponent();
        }

        public void NotesPage_Load(object sender, EventArgs e)
        {
            sqlcon();
            SqlCommand sq1 = new SqlCommand("SELECT * FROM [dbo].[Notes]", con);
            DataTable dt = new DataTable();

            SqlDataAdapter sd1 = new SqlDataAdapter(sq1);
            sd1.Fill(dt);

            MessageBox.Show($"Rows returned: {dt.Rows.Count}");

            // Check if DataGridView exists
            if (dataGridView != null)
            {
                dataGridView.DataSource = dt;
                MessageBox.Show("DataSource set successfully");
            }
            else
            {
                MessageBox.Show("DataGridView is null!");
            }

            con.Close();

        }

        private void PageHeading_Click(object sender, EventArgs e)
        {

        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
