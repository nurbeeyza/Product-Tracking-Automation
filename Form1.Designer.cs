namespace Product_Tracking_Automation
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.TC_Register = new System.Windows.Forms.TabPage();
            this.btn_Register = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TC_Login = new System.Windows.Forms.TabPage();
            this.btn_Login = new System.Windows.Forms.Button();
            this.text_Password = new System.Windows.Forms.TextBox();
            this.text_UserName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_LoginPage = new System.Windows.Forms.Button();
            this.btn_RegisterPage = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl.SuspendLayout();
            this.TC_Register.SuspendLayout();
            this.TC_Login.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.TC_Register);
            this.tabControl.Controls.Add(this.TC_Login);
            this.tabControl.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl.Location = new System.Drawing.Point(222, 2);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1416, 1095);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 9;
            // 
            // TC_Register
            // 
            this.TC_Register.Controls.Add(this.btn_Register);
            this.TC_Register.Controls.Add(this.txtPassword);
            this.TC_Register.Controls.Add(this.txtUserName);
            this.TC_Register.Controls.Add(this.txtSurname);
            this.TC_Register.Controls.Add(this.txtName);
            this.TC_Register.Controls.Add(this.label4);
            this.TC_Register.Controls.Add(this.label3);
            this.TC_Register.Controls.Add(this.label2);
            this.TC_Register.Controls.Add(this.label1);
            this.TC_Register.Location = new System.Drawing.Point(4, 5);
            this.TC_Register.Name = "TC_Register";
            this.TC_Register.Padding = new System.Windows.Forms.Padding(3);
            this.TC_Register.Size = new System.Drawing.Size(1408, 1086);
            this.TC_Register.TabIndex = 1;
            this.TC_Register.Text = "Kayıt Ol";
            this.TC_Register.UseVisualStyleBackColor = true;
            // 
            // btn_Register
            // 
            this.btn_Register.Location = new System.Drawing.Point(328, 178);
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.Size = new System.Drawing.Size(99, 38);
            this.btn_Register.TabIndex = 16;
            this.btn_Register.Text = "Kaydet";
            this.btn_Register.UseVisualStyleBackColor = true;
            this.btn_Register.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(113, 132);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(201, 22);
            this.txtPassword.TabIndex = 15;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(113, 98);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(201, 22);
            this.txtUserName.TabIndex = 14;
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(113, 64);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(201, 22);
            this.txtSurname.TabIndex = 13;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(113, 27);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(201, 22);
            this.txtName.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Şifre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Kullanıcı Adı ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Soyad";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Ad";
            // 
            // TC_Login
            // 
            this.TC_Login.Controls.Add(this.btn_Login);
            this.TC_Login.Controls.Add(this.text_Password);
            this.TC_Login.Controls.Add(this.text_UserName);
            this.TC_Login.Controls.Add(this.label6);
            this.TC_Login.Controls.Add(this.label5);
            this.TC_Login.Location = new System.Drawing.Point(4, 5);
            this.TC_Login.Name = "TC_Login";
            this.TC_Login.Padding = new System.Windows.Forms.Padding(3);
            this.TC_Login.Size = new System.Drawing.Size(1408, 1086);
            this.TC_Login.TabIndex = 0;
            this.TC_Login.Text = "Giriş";
            this.TC_Login.UseVisualStyleBackColor = true;
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(406, 130);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(82, 38);
            this.btn_Login.TabIndex = 17;
            this.btn_Login.Text = "Giriş";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // text_Password
            // 
            this.text_Password.Location = new System.Drawing.Point(132, 78);
            this.text_Password.Name = "text_Password";
            this.text_Password.Size = new System.Drawing.Size(235, 22);
            this.text_Password.TabIndex = 3;
            // 
            // text_UserName
            // 
            this.text_UserName.Location = new System.Drawing.Point(132, 31);
            this.text_UserName.Name = "text_UserName";
            this.text_UserName.Size = new System.Drawing.Size(235, 22);
            this.text_UserName.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Şifre";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Kullanıcı Adı";
            // 
            // btn_LoginPage
            // 
            this.btn_LoginPage.Location = new System.Drawing.Point(13, 137);
            this.btn_LoginPage.Name = "btn_LoginPage";
            this.btn_LoginPage.Size = new System.Drawing.Size(194, 65);
            this.btn_LoginPage.TabIndex = 17;
            this.btn_LoginPage.Text = "Giriş Yap";
            this.btn_LoginPage.UseVisualStyleBackColor = true;
            this.btn_LoginPage.Click += new System.EventHandler(this.btn_LoginPage_Click);
            // 
            // btn_RegisterPage
            // 
            this.btn_RegisterPage.Location = new System.Drawing.Point(13, 28);
            this.btn_RegisterPage.Name = "btn_RegisterPage";
            this.btn_RegisterPage.Size = new System.Drawing.Size(194, 63);
            this.btn_RegisterPage.TabIndex = 18;
            this.btn_RegisterPage.Text = "Kayıt Ol";
            this.btn_RegisterPage.UseVisualStyleBackColor = true;
            this.btn_RegisterPage.Click += new System.EventHandler(this.btn_RegisterPage_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_LoginPage);
            this.panel1.Controls.Add(this.btn_RegisterPage);
            this.panel1.Location = new System.Drawing.Point(-1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 449);
            this.panel1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.TC_Register.ResumeLayout(false);
            this.TC_Register.PerformLayout();
            this.TC_Login.ResumeLayout(false);
            this.TC_Login.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage TC_Login;
        private System.Windows.Forms.TabPage TC_Register;
        private System.Windows.Forms.Button btn_Register;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox text_Password;
        private System.Windows.Forms.TextBox text_UserName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Button btn_RegisterPage;
        private System.Windows.Forms.Button btn_LoginPage;
    }
}

