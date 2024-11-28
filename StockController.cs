using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_Tracking_Automation
{
    public class StockController
    {
        private string _connectionString;


        public StockController(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddStock(Stock stck)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Stock (StockCode, Count, Unit,  ProductId) " +
                               "VALUES (@StockCode, @Count, @Unit,  @ProductId)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StockCode", stck.StockCode);
                command.Parameters.AddWithValue("@Count", stck.Count);
                command.Parameters.AddWithValue("@Unit", stck.Unit);
                command.Parameters.AddWithValue("@ProductId", stck.ProductId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<Stock> GetStock(string searchText)
        {
            var stockItems = new List<Stock>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = 
                "SELECT s.Id, s.ProductId, s.StockCode, p.Name AS ProductName, s.Count, s.Unit " +
                "FROM Stock AS s "+
                "LEFT JOIN Product AS p ON s.ProductId = p.Id "+
                "WHERE (p.Name LIKE @searchText OR s.StockCode LIKE @searchText)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@searchText", "%" + searchText.Trim() + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var stock = new Stock
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    ProductId = reader.GetGuid(reader.GetOrdinal("ProductId")),
                                    StockCode = reader["StockCode"].ToString(),
                                    ProductName = reader["ProductName"].ToString(),  
                                    Count = reader.IsDBNull(reader.GetOrdinal("Count")) ? 0 : Convert.ToSingle(reader["Count"]),
                                    //float dönüşümünü single ile yaptık .
                                    Unit = reader["Unit"].ToString()
                                };
                                stockItems.Add(stock);
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

            return stockItems;

        }
        public List<Stock> GetStock(Stock stock)
        {
            var stockItems = new List<Stock>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql =
                    "SELECT s.Id, s.ProductId, s.StockCode, p.Name AS ProductName, s.Count, s.Unit " +
                    "FROM Stock AS s " +
                    "LEFT JOIN Product AS p ON s.ProductId = p.Id " +
                    "WHERE (s.StockCode LIKE @stockCode OR @stockCode IS NULL) " +
                    "AND (p.Name LIKE @productName OR @productName IS NULL) " +
                    "AND (s.Count >= @minCount OR @minCount IS NULL) " +
                    "AND (s.Unit LIKE @unit OR @unit IS NULL)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        
                        cmd.Parameters.AddWithValue("@stockCode", string.IsNullOrEmpty(stock.StockCode) ? (object)DBNull.Value : "%" + stock.StockCode.Trim() + "%");
                        cmd.Parameters.AddWithValue("@productName", string.IsNullOrEmpty(stock.ProductName) ? (object)DBNull.Value : "%" + stock.ProductName.Trim() + "%");
                        cmd.Parameters.AddWithValue("@minCount", stock.Count <= 0 ? (object)DBNull.Value : stock.Count);
                        cmd.Parameters.AddWithValue("@unit", string.IsNullOrEmpty(stock.Unit) ? (object)DBNull.Value : "%" + stock.Unit.Trim() + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var retrievedStock = new Stock
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    ProductId = reader.GetGuid(reader.GetOrdinal("ProductId")),
                                    StockCode = reader["StockCode"].ToString(),
                                    ProductName = reader["ProductName"].ToString(),
                                    Count = reader.IsDBNull(reader.GetOrdinal("Count")) ? 0 : Convert.ToSingle(reader["Count"]),
                                    Unit = reader["Unit"].ToString()
                                };
                                stockItems.Add(retrievedStock);
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

            return stockItems;
        }
    }
}