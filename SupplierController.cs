using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_Tracking_Automation
{
    public class SupplierController
    {

        private string _connectionString;


        public SupplierController(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddSupplier(Supplier supplier)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Supplier (ProductId,CompanyId,Price,Currency) " +
                               "VALUES (@ProductId, @CompanyId, @Price,  @Currency)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", supplier.ProductId);
                command.Parameters.AddWithValue("@CompanyId", supplier.CompanyId);
                command.Parameters.AddWithValue("@Price", supplier.Price);
                command.Parameters.AddWithValue("@Currency", supplier.Currency);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<Supplier> GetSupplier(string searchText)
        {
            var supplierItems = new List<Supplier>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT s.Id, s.ProductId,s.CompanyId, p.Name AS ProductName, c.Name AS CompanyName, s.Price, s.Currency " +
                                "FROM Supplier AS s " +
                                "LEFT JOIN Product AS p ON s.ProductId = p.Id " +
                                "LEFT JOIN Company AS c ON s.CompanyId = c.Id " +
                                "WHERE (p.Name LIKE @searchText OR c.Name LIKE @searchText )";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@searchText", "%" + searchText.Trim() + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var supplier = new Supplier
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    ProductId = reader.GetGuid(reader.GetOrdinal("ProductId")),
                                    CompanyId = reader.GetGuid(reader.GetOrdinal("CompanyId")),
                                    ProductName = reader["ProductName"].ToString(),
                                    CompanyName = reader["CompanyName"].ToString(),
                                    Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : Convert.ToSingle(reader["Price"]),
                                    Currency = reader["Currency"].ToString(),
                                    //float dönüşümünü single ile yaptık 
                                };
                                supplierItems.Add(supplier);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("SQL hata oluştu: " + sqlEx.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);//tostring yaptığımızda hatanın detayını kod bazında gösyteriyor 
            }

            return supplierItems;

        }
        public List<Supplier> GetSupplier(SupplierFilter filter)
        {
            var supplierItems = new List<Supplier>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql =
                        "SELECT s.Id, s.ProductId, s.CompanyId, p.Name AS ProductName, c.Name AS CompanyName, s.Price, s.Currency " +
                        "FROM Supplier AS s " +
                        "LEFT JOIN Product AS p ON s.ProductId = p.Id " +
                        "LEFT JOIN Company AS c ON s.CompanyId = c.Id " +
                        "WHERE (@productId IS NULL OR s.ProductId = @productId) " +
                        "AND (@companyId IS NULL OR s.CompanyId = @companyId) " +
                        "AND (@minPrice IS NULL OR s.Price >= @minPrice) " +
                        "AND (@maxPrice IS NULL OR s.Price <= @maxPrice) " +
                        "AND (@currency IS NULL OR s.Currency LIKE @currency)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // Kullanıcının filtreleri
                        cmd.Parameters.AddWithValue("@productId", filter.ProductId.HasValue ? (object)filter.ProductId.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@companyId", filter.CompanyId.HasValue ? (object)filter.CompanyId.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@minPrice", filter.MinPrice.HasValue ? (object)filter.MinPrice.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@maxPrice", filter.MaxPrice.HasValue ? (object)filter.MaxPrice.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@currency", string.IsNullOrEmpty(filter.Currency) ? (object)DBNull.Value : "%" + filter.Currency.Trim() + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var supplier = new Supplier
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    ProductId = reader.GetGuid(reader.GetOrdinal("ProductId")),
                                    CompanyId = reader.GetGuid(reader.GetOrdinal("CompanyId")),
                                    ProductName = reader["ProductName"].ToString(),
                                    CompanyName = reader["CompanyName"].ToString(),
                                    Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : Convert.ToSingle(reader["Price"]),
                                    Currency = reader["Currency"].ToString()
                                };
                                supplierItems.Add(supplier);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("SQL hata oluştu: " + sqlEx.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return supplierItems;
        }
    }
}

