using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;

namespace ServiceData.DatabaseLayer
{
    public class IngredientProductDatabaseAccess : IIngredientProduct
    {
        readonly string? _connectionString;

        public IngredientProductDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public IngredientProductDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public async Task CreateIngredientProduct(IngredientProduct ingredientProduct)
        {
            string insertString = "INSERT INTO IngredientProduct(ProductId, IngredientId, Min, Max, Count) " +
                                  "VALUES (@ProductId, @IngredientId, @Min, @Max, @Count)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                createCommand.Parameters.AddWithValue("@ProductId", ingredientProduct.ProductId);
                createCommand.Parameters.AddWithValue("@IngredientId", ingredientProduct.IngredientId);
                createCommand.Parameters.AddWithValue("@Min", ingredientProduct.Min);
                createCommand.Parameters.AddWithValue("@Max", ingredientProduct.Max);
                createCommand.Parameters.AddWithValue("@Count", ingredientProduct.Count);

                await con.OpenAsync();
                await createCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task<bool> DeleteIngredientProductByIds(int productId, int ingredientId)
        {
            bool isDeleted = false;

            string deleteString = "DELETE FROM IngredientProduct WHERE ProductId = @ProductId AND IngredientId = @IngredientId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
            {
                deleteCommand.Parameters.AddWithValue("@ProductId", productId);
                deleteCommand.Parameters.AddWithValue("@IngredientId", ingredientId);

                await con.OpenAsync();

                int rowsAffected = await deleteCommand.ExecuteNonQueryAsync();

                isDeleted = (rowsAffected > 0);
            }

            return isDeleted;
        }

        public async Task<List<IngredientProduct>> GetAllIngredientProducts()
        {
            List<IngredientProduct> foundIngredientProducts = new List<IngredientProduct>();
            IngredientProduct readIngredientProduct;

            string queryString = "SELECT * FROM IngredientProduct";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();

                SqlDataReader ingredientProductReader = await readCommand.ExecuteReaderAsync();

                while (await ingredientProductReader.ReadAsync())
                {
                    readIngredientProduct = GetIngredientProductFromReader(ingredientProductReader);
                    foundIngredientProducts.Add(readIngredientProduct);
                }
            }

            return foundIngredientProducts;
        }

        public async Task<IngredientProduct> GetIngredientProductByIds(int productId, int ingredientId)
        {
            IngredientProduct foundIngredientProduct;

            string queryString = "SELECT * FROM IngredientProduct WHERE ProductId = @ProductId AND IngredientId = @IngredientId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@ProductId", productId);
                readCommand.Parameters.AddWithValue("@IngredientId", ingredientId);

                await con.OpenAsync();

                SqlDataReader ingredientProductReader = await readCommand.ExecuteReaderAsync();

                foundIngredientProduct = new IngredientProduct();

                while (await ingredientProductReader.ReadAsync())
                {
                    foundIngredientProduct = GetIngredientProductFromReader(ingredientProductReader);
                }
            }

            return foundIngredientProduct;
        }

        public async Task<bool> UpdateIngredientProductByIds(IngredientProduct ingredientProductToUpdate)
        {
            bool isUpdated = false;

            string updateString = "UPDATE IngredientProduct SET Min = @Min, Max = @Max, Count = @Count " +
                                  "WHERE ProductId = @ProductId AND IngredientId = @IngredientId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Min", ingredientProductToUpdate.Min);
                updateCommand.Parameters.AddWithValue("@Max", ingredientProductToUpdate.Max);
                updateCommand.Parameters.AddWithValue("@Count", ingredientProductToUpdate.Count);
                updateCommand.Parameters.AddWithValue("@ProductId", ingredientProductToUpdate.ProductId);
                updateCommand.Parameters.AddWithValue("@IngredientId", ingredientProductToUpdate.IngredientId);

                await con.OpenAsync();

                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        private IngredientProduct GetIngredientProductFromReader(SqlDataReader ingredientProductReader)
        {
            int readerProductId = ingredientProductReader.GetInt32(ingredientProductReader.GetOrdinal("ProductId"));
            int readerIngredientId = ingredientProductReader.GetInt32(ingredientProductReader.GetOrdinal("IngredientId"));
            int readerMin = ingredientProductReader.GetInt32(ingredientProductReader.GetOrdinal("Min"));
            int readerMax = ingredientProductReader.GetInt32(ingredientProductReader.GetOrdinal("Max"));
            int readerCount = ingredientProductReader.GetInt32(ingredientProductReader.GetOrdinal("Count"));

            return new IngredientProduct(readerProductId, readerIngredientId, readerMin, readerMax, readerCount);
        }
    }
}
