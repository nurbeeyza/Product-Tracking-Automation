using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Product_Tracking_Automation
{
    public class UserController
    {
        private string _connectionString;

        public UserController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<User> GetActiveUsers(User usr)
        {
            
            var activeUsers = new List<User>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT Name, Surname, UserName FROM [User] WHERE IsActive = 1 AND (Name LIKE @name AND Surname LIKE @surname AND UserName LIKE @username)";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", "%" + usr.UserName.Trim() + "%");
                        cmd.Parameters.AddWithValue("@name", "%" + usr.Name.Trim() + "%");
                        cmd.Parameters.AddWithValue("@surname", "%" + usr.Surname.Trim() + "%");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    Name = reader["Name"].ToString(),
                                    Surname = reader["Surname"].ToString(),
                                    UserName = reader["UserName"].ToString()
                                };
                                activeUsers.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return activeUsers;
        }
        public List<User> GetActiveUsers(string searchText)
        {
            var activeUsers = new List<User>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT Name, Surname, UserName FROM [User] WHERE IsActive = 1 AND (Name LIKE @usr OR Surname LIKE @usr OR UserName LIKE @usr)";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@usr", "%" + searchText.Trim() + "%");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    Name = reader["Name"].ToString(),
                                    Surname = reader["Surname"].ToString(),
                                    UserName = reader["UserName"].ToString()
                                };
                                activeUsers.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return activeUsers;
        }
        public List<User> GetInaciveUsers(User usr)
        {
            var inactiveUsers = new List<User>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT Name, Surname, UserName FROM [User] WHERE IsActive = 0 AND IsWaitingApprove = 0 AND (Name LIKE @name AND Surname LIKE @surname AND UserName LIKE @username)";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", "%" + usr.UserName.Trim() + "%");
                        cmd.Parameters.AddWithValue("@name", "%" + usr.Name.Trim() + "%");
                        cmd.Parameters.AddWithValue("@surname", "%" + usr.Surname.Trim() + "%");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    Name = reader["Name"].ToString(),
                                    Surname = reader["Surname"].ToString(),
                                    UserName = reader["UserName"].ToString()
                                };
                                inactiveUsers.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return inactiveUsers;
        }

        public List<User> GetInactiveUsers(string searchText)
        {
            var inactiveUsers = new List<User>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT Name, Surname, UserName FROM [User] WHERE IsActive = 0 AND (Name LIKE @usr OR Surname LIKE @usr OR UserName LIKE @usr)";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@usr", "%" + searchText.Trim() + "%");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    Name = reader["Name"].ToString(),
                                    Surname = reader["Surname"].ToString(),
                                    UserName = reader["UserName"].ToString()
                                };
                                inactiveUsers.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return inactiveUsers;
        }
      
        public List<User> GetWaitingApproveUsers(string searchText)
        {
           
            var waitingApproveUsers = new List<User>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT Name, Surname, UserName FROM [User] WHERE IsWaitingApprove = 1 AND (Name LIKE @usr OR Surname LIKE @usr OR UserName LIKE @usr)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@usr", "%" + searchText.Trim() + "%");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    Name = reader["Name"].ToString(),
                                    Surname = reader["Surname"].ToString(),
                                    UserName = reader["UserName"].ToString()
                                };
                                waitingApproveUsers.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return waitingApproveUsers;
        }
        public List<User> GetWaitingApproveUsers(User usr)
        {
           // MessageBox.Show(usr.Name + usr.Surname + usr.UserName); veriler doğru geliyormu diye kontrol mesajı verildi
           
            var waitingApproveUsers = new List<User>();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                     conn.Open();
                     string sql = "SELECT Name, Surname, UserName FROM [User] WHERE IsWaitingApprove = 1 AND  IsActive = 0 AND (Name LIKE @NAME AND Surname LIKE @SURNAME AND UserName LIKE @USERNAME)";
                     using (var cmd = new SqlCommand(sql, conn))
                     {
                        cmd.Parameters.AddWithValue ("@NAME","%"+usr.Name+"%");
                        cmd.Parameters.AddWithValue("@SURNAME", "%"+usr.Surname+ "%");
                        cmd.Parameters.AddWithValue("@USERNAME", "%"+ usr.UserName+ "%");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    Name = reader["Name"].ToString(),
                                    Surname = reader["Surname"].ToString(),
                                    UserName = reader["UserName"].ToString()
                                };
                                waitingApproveUsers.Add(user);
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
            //MessageBox.Show(waitingApproveUsers.Count.ToString());//modelin içinde kaç veri var sayısına bakıyor
    return waitingApproveUsers;
}
        public void DeactivateUser(string username)
        {
            string connectionString = _connectionString;
            string query = "UPDATE Users SET IsActive = 0 WHERE UserName = @UserName";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                   
                    command.Parameters.AddWithValue("@UserName", username);

                    try
                    {
                     
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("Kullanıcı bulunamadı veya işlem gerçekleşmedi.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        
                        MessageBox.Show($"Veritabanı hatası: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Bir hata oluştu: {ex.Message}");
                    }
                }
            }
        }
        public void ActivateUser(string username)
        {
            string connectionString = _connectionString;

            string query = "UPDATE [User] SET IsActive = 1 WHERE UserName = @UserName";

            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                  
                    command.Parameters.AddWithValue("@UserName", username);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            MessageBox.Show("Kullanıcı bulunamadı veya işlem gerçekleşmedi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Kullanıcı {username} başarıyla aktif hale getirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (SqlException ex)
                    {
                        
                        MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                      
                        MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public void ApproveUser(string username)
        {
            string connectionString = _connectionString;
            string query = "UPDATE [User] SET IsActive = 1, IsWaitingApprove = 0 WHERE UserName = @UserName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", username);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            MessageBox.Show("Kullanıcı bulunamadı veya işlem gerçekleşmedi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Kullanıcı {username} başarıyla onaylandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }






        // ActivateUser(User usr) => IsActive : 0 (false)
        // PassiveUser(User usr) => IsActive : 1 (true)
        // ApproveUser(User usr) => IsActive : 1 AND IsWaitingApproval : 0

    }
}
