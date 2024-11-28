using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Product_Tracking_Automation
{
    public class RegisterController
    {
        private string _connectionString;

        public RegisterController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void RegisterUser(User user)
        {
            try
            {
                string hashedPassword = CalculateHash(user.Password); // Şifreyi hashle

                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // SQL komutunu güncelle
                    string sql = "INSERT INTO [User] (Name, Surname, UserName, Password, IsAdmin, IsActive, IsWaitingApprove) " +
                                 "VALUES (@Name, @Surname, @UserName, @Password, @IsAdmin, @IsActive, @IsWaitingApprove)";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        // Parametreleri ata
                        cmd.Parameters.AddWithValue("@Name", user.Name);
                        cmd.Parameters.AddWithValue("@Surname", user.Surname);
                        cmd.Parameters.AddWithValue("@UserName", user.UserName);
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);
                        cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                        cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                        cmd.Parameters.AddWithValue("@IsWaitingApprove", user.IsWaitingApprove);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Kullanıcı başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcı kaydedilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CalculateHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Byte'ı hexadecimal dizeye dönüştür
                }
                return builder.ToString();
            }
        }
    }
}
