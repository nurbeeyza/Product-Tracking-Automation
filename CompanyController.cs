using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;

namespace Product_Tracking_Automation
{
    public class CompanyController
    {
        private string _connectionString;


        public CompanyController(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddCompany(Company cmpny)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Company (TaxNo, Name, Description,  Type,Adress) " +
                               "VALUES (@TaxNo, @Name, @Description,  @Type, @Adress)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaxNo", cmpny.TaxNo);
                command.Parameters.AddWithValue("@Name", cmpny.Name);
                command.Parameters.AddWithValue("@Description", cmpny.Description);
                command.Parameters.AddWithValue("@Type", cmpny.Type);
                command.Parameters.AddWithValue("@Adress", cmpny.Adress);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<Company> GetCompany(Company cmpny)
        {

            var companies = new List<Company>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string sql = "SELECT Id, TaxNo, Name, Description, Type, Adress FROM [Company] " +
                                 "WHERE TaxNo LIKE @taxno AND Name LIKE @name AND Description LIKE @dscrp AND Type LIKE @typ AND Adress LIKE @adrs";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@taxno", "%" + cmpny.TaxNo.Trim() + "%");
                        cmd.Parameters.AddWithValue("@name", "%" + cmpny.Name.Trim() + "%");
                        cmd.Parameters.AddWithValue("@dscrp", "%" + cmpny.Description.Trim() + "%");
                        cmd.Parameters.AddWithValue("@typ", "%" + cmpny.Type.Trim() + "%");
                        cmd.Parameters.AddWithValue("@adrs", "%" + cmpny.Adress.Trim() + "%");

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var company = new Company
                                {
                                    Id = (Guid)reader["Id"],
                                    TaxNo = reader["TaxNo"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Type = reader["Type"].ToString(),
                                    Adress = reader["Adress"].ToString()
                                };
                                companies.Add(company);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return companies;
        }
        public List<Company> GetCompany(string searchText)
        {
            var companies = new List<Company>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT Id,TaxNo, Name, Description, Type, Adress FROM Company " +
                                 "WHERE (TaxNo LIKE @searchText OR Name LIKE @searchText OR Description LIKE @searchText " +
                                 "OR Type LIKE @searchText OR Adress LIKE @searchText)";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@searchText", "%" + searchText.Trim() + "%");

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var company = new Company
                                {
                                    Id = (Guid)reader["Id"], // reader.GetGuid(reader.GetOrdinal("Id"))
                                    TaxNo = reader["TaxNo"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Type = reader["Type"].ToString(),
                                    Adress = reader["Adress"].ToString()
                                };
                                companies.Add(company);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return companies;
        }
    }
}
