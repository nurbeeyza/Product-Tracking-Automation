using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Product_Tracking_Automation
{
    public class LoginController
    {
        private string _connectionString;

        public LoginController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool ValidateUser(string username, string password, out User user)
        {
            user = null; // Başlangıçta kullanıcıyı null olarak ayarlıyoruz

            try
            {
                string hashedPassword = CalculateHash(password); // Şifreyi hashle

                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // SQL komutunu oluştur
                    string sql = "SELECT Name, Surname, UserName, IsAdmin, IsActive, IsWaitingApprove " +
                                 "FROM [User] WHERE UserName = @UserName AND Password = @Password";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        // Parametreleri ata
                        cmd.Parameters.AddWithValue("@UserName", username);
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);

                        // Sonuçları al
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bool isActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                                bool isWaitingApprove = reader.GetBoolean(reader.GetOrdinal("IsWaitingApprove"));

                                if (isActive && !isWaitingApprove)
                                {
                                    // Kullanıcı aktif ve onay beklemiyor
                                    user = new User
                                    {
                                        Name = reader.GetString(reader.GetOrdinal("Name")),
                                        Surname = reader.GetString(reader.GetOrdinal("Surname")),
                                        UserName = username,
                                        IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                                        IsActive = isActive,
                                        IsWaitingApprove = isWaitingApprove
                                    };
                                    return true;
                                }
                                else if (!isActive)
                                {
                                    // Kullanıcı aktif değil
                                    MessageBox.Show("Hesabınız aktif değil. Lütfen yetkili ile iletişime geçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (isWaitingApprove)
                                {
                                    // Kullanıcı onay bekliyor
                                    MessageBox.Show("Hesabınız onay bekliyor. Lütfen yetkili ile iletişime geçin.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                // Kullanıcı adı veya şifre hatalı
                                MessageBox.Show("Kullanıcı adı veya şifre yanlış. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
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
