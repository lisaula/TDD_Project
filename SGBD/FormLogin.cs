using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleApp;

namespace SGBD
{
    public partial class FormLogin : Form
    {
        private SqlRepository server;
        private Action load;

        public FormLogin()
        {
            InitializeComponent();
        }

        public FormLogin(ref SqlRepository server)
        {
            InitializeComponent();
            this.server = server;
            //server.openConnection("prueba", "12345");
        }

        public FormLogin(ref SqlRepository server, Action load) 
        {
            this.load = load;
            InitializeComponent();
            this.server = server;

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (UserTextBox1.Text == "" || passwordTextBox.Text == "")
            {
                MessageBox.Show("Please provide UserName and Password");
                return;
            }
            try
            {
                server.openConnection(UserTextBox1.Text, passwordTextBox.Text);
                load();
                this.Close();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
