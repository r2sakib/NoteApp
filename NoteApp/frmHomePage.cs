using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Noteapp
{
    public partial class frmHomePage : Form
    {
        public frmHomePage()
        {
            InitializeComponent();
            
            btnLogin.MouseEnter += btn_MouseEnter;
            btnLogin.MouseLeave += btn_MouseLeave;

            btnAdmin.MouseEnter += btn_MouseEnter;
            btnAdmin.MouseLeave += btn_MouseLeave;

            btnSignUp.MouseEnter += btn_MouseEnter;
            btnSignUp.MouseLeave += btn_MouseLeave;
        }
        private void btn_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.BackColor = Color.MidnightBlue;
            button.ForeColor = Color.White;
            Cursor = Cursors.Hand;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.BackColor = Color.RoyalBlue;
            button.ForeColor = Color.White;
            Cursor = Cursors.Default;
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {

            frmLogin l = new frmLogin();
            l.Show();
            this.Hide();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {

            frmAdmin a = new frmAdmin();
            a.Show();
            this.Hide();
        }

        

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            frmRegistration r = new frmRegistration();
            r.Show();
            this.Hide();
        }
    }
}
