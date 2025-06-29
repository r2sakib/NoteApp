using Noteapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new NotesPage());
            //Application.Run(new NotesDashboard("User", "1001"));
            //Application.Run(new ViewNote(1001, "User", 1001));
            //Application.Run(new CreateNote(1001));
            Application.Run(new frmHomePage());
            //Application.Run(new EditProfile(1001));


        }
    }
}
