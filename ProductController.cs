using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_Tracking_Automation
{
    public class ProductController
    {
        private string _connectionString;


        public ProductController(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddProduct(Product product)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Product (Serial, Name, Description, Lenght, Width, Depth, Weight, Volume, BrandId) " +
                               "VALUES (@Serial, @Name, @Description, @Lenght, @Width, @Depth, @Weight, @Volume, @BrandId)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Serial", product.Serial);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Lenght", product.Lenght);
                command.Parameters.AddWithValue("@Width", product.Width);
                command.Parameters.AddWithValue("@Depth", product.Depth);
                command.Parameters.AddWithValue("@Weight", product.Weight);
                command.Parameters.AddWithValue("@Volume", product.Volume);
                command.Parameters.AddWithValue("@BrandId", product.BrandId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<Product> GetProduct(ProductFilter filter)
        {
            var products = new List<Product>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // SQL sorgusunu oluştur
                    var queryBuilder = new StringBuilder("SELECT p.Serial, p.Name, p.Description, p.Lenght, p.Width, p.Depth, p.Weight, p.Volume, b.Name AS BrandName, b.Id AS BrandId , p.Id AS ProductId FROM Product p INNER JOIN Brand b ON p.BrandId = b.Id WHERE ");
                    var parameters = new List<SqlParameter>();
                    string conditions = string.Empty;


                    if (!string.IsNullOrEmpty(filter.Serial))
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Serial LIKE @Serial";
                        }
                        else
                        {
                            conditions += " AND p.Serial LIKE @Serial";
                        }
                        parameters.Add(new SqlParameter("@Serial", "%" + filter.Serial.Trim() + "%"));
                    }

                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Name LIKE @Name";
                        }
                        else
                        {
                            conditions += " AND p.Name LIKE @Name";
                        }

                        parameters.Add(new SqlParameter("@Name", "%" + filter.Name.Trim() + "%"));
                    }

                    if (!string.IsNullOrEmpty(filter.Description))
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Description LIKE @Description";
                        }
                        else
                        {
                            conditions += " AND p.Description LIKE @Description";
                        }

                        parameters.Add(new SqlParameter("@Description", "%" + filter.Description.Trim() + "%"));
                    }

                    if (filter.MinLenght > 0)
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Lenght >= @MinLength";
                        }
                        else
                        {
                            conditions += " AND p.Lenght >= @MinLength";
                        }

                        parameters.Add(new SqlParameter("@MinLength", filter.MinLenght));
                    }

                    if (filter.MaxLenght > 0)
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Lenght <= @MaxLength";
                        }
                        else
                        {
                            conditions += " AND p.Lenght <= @MaxLength";
                        }

                        parameters.Add(new SqlParameter("@MaxLength", filter.MaxLenght));
                    }

                    if (filter.MinWidth > 0)
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Width >= @MinWidth";
                        }
                        else
                        {
                            conditions += " AND p.Width >= @MinWidth";
                        }

                        parameters.Add(new SqlParameter("@MinWidth", filter.MinWidth));
                    }

                    if (filter.MaxWidth > 0)
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Width <= @MaxWidth";
                        }
                        else
                        {
                            conditions += " AND p.Width <= @MaxWidth";
                        }

                        parameters.Add(new SqlParameter("@MaxWidth", filter.MaxWidth));
                    }

                    if (filter.MinDepth > 0)
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Depth >= @MinDepth";
                        }
                        else
                        {
                            conditions += " AND p.Depth >= @MinDepth";
                        }

                        parameters.Add(new SqlParameter("@MinDepth", filter.MinDepth));
                    }

                    if (filter.MaxDepth.HasValue)
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Depth <= @MaxDepth";
                        }
                        else
                        {
                            conditions += " AND p.Depth <= @MaxDepth";
                        }

                        parameters.Add(new SqlParameter("@MaxDepth", filter.MaxDepth.Value));
                    }

                    if (filter.MinWeight > 0)
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Weight >= @MinWeight";
                        }
                        else
                        {
                            conditions += " AND p.Weight >= @MinWeight";
                        }

                        parameters.Add(new SqlParameter("@MinWeight", filter.MinWeight));
                    }

                    if (filter.MaxWeight > 0)
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Weight <= @MaxWeight";
                        }
                        else
                        {
                            conditions += " AND p.Weight <= @MaxWeight";
                        }

                        parameters.Add(new SqlParameter("@MaxWeight", filter.MaxWeight));
                    }

                    if (filter.MinVolume > 0)
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Volume >= @MinVolume";
                        }
                        else
                        {
                            conditions += " AND p.Volume >= @MinVolume";
                        }

                        parameters.Add(new SqlParameter("@MinVolume", filter.MinVolume));
                    }

                    if (filter.MaxVolume > 0)
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "p.Volume <= @MaxVolume";
                        }
                        else
                        {
                            conditions += " AND p.Volume <= @MaxVolume";
                        }

                        parameters.Add(new SqlParameter("@MaxVolume", filter.MaxVolume));
                    }

                    if (!string.IsNullOrEmpty(filter.BrandName))
                    {
                        if (String.IsNullOrEmpty(conditions))
                        {
                            conditions += "b.Name LIKE @BrandName";
                        }
                        else
                        {
                            conditions += " AND b.Name LIKE @BrandName";
                        }

                        parameters.Add(new SqlParameter("@BrandName", "%" + filter.BrandName.Trim() + "%"));
                    }
                    queryBuilder.Append(conditions);
                    using (var cmd = new SqlCommand(queryBuilder.ToString(), conn))
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var product = new Product
                                {
                                    Serial = reader["Serial"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Lenght = reader.GetDecimal(reader.GetOrdinal("Lenght")),
                                    Width = reader.GetDecimal(reader.GetOrdinal("Width")),
                                    Depth = reader.GetDecimal(reader.GetOrdinal("Depth")),
                                    Weight = reader.GetDecimal(reader.GetOrdinal("Weight")),
                                    Volume = reader.GetDecimal(reader.GetOrdinal("Volume")),
                                    BrandId = (Guid)reader["BrandId"],
                                    BrandName = reader["BrandName"].ToString(),
                                    Id = (Guid)reader["ProductId"]
                                };
                                products.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return products;
        }
        public List<Product> GetProduct(string searchText)
        {
            List<Product> getproduct = new List<Product>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT Id, [Name]  FROM Product WHERE Name LIKE @pname";


                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@pname", "%" + searchText.Trim() + "%");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var prdct = new Product
                                {
                                    Name = reader["Name"].ToString(),
                                    Id = reader.GetGuid(0)//yukarda namden sonra çekildiği için 1 oldu önce olsaydı 0 olacaktı
                                    // Id = (Guid)reader["Id"]

                                };
                                getproduct.Add(prdct);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return getproduct;
        }


    }
}






