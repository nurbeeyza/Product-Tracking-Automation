using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Product_Tracking_Automation
{
    public partial class Form1 : Form
    {
        public RegisterController _registerController;
        public LoginController _loginController;
        string connectionString = DBConnections.DbConnectionString;

        public Form1()
        {
            InitializeComponent();
            _registerController = new RegisterController(connectionString);
            _loginController = new LoginController(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User newUser = new User
            {
                Name = txtName.Text,
                Surname = txtSurname.Text,
                UserName = txtUserName.Text,
                Password = txtPassword.Text ,
                IsAdmin=false,
                IsActive=false,
                IsWaitingApprove=true

            };

            _registerController.RegisterUser(newUser);
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string username = text_UserName.Text.Trim();
            string password = text_Password.Text.Trim();

            // Kullanıcı girişini doğrula
            User user;
            bool isValidUser = _loginController.ValidateUser(username, password, out user);

            if (isValidUser)
            {
               
                MessageBox.Show(" Hoşgeldiniz  " + user.Name + "!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form2 form2 = new Form2();
               
                this.Hide(); // Form1'i gizle
                form2.Show(); // Form2'yi aç
                
               
                
            }
            else
            {
             
                MessageBox.Show("Giriş başarısız. Lütfen kullanıcı adı ve şifreyi kontrol edin veya hesabınızın aktif olduğunu doğrulayın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_RegisterPage_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = TC_Register;
        }

        private void btn_LoginPage_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = TC_Login;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}