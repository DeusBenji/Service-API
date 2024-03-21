using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;

namespace ServiceData.DatabaseLayer
{
    public class ShopProductDatabaseAccess : IShopProduct
    {
        readonly string? _connectionString;

        public ShopProductDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public ShopProductDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public async Task CreateShopProduct(ShopProduct shopProduct)
        {
            string insertString = "INSERT INTO ShopProduct(ShopId, ProductId) " +
                                  "VALUES (@ShopId, @ProductId)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                createCommand.Parameters.AddWithValue("@ShopId", shopProduct.ShopId);
                createCommand.Parameters.AddWithValue("@ProductId", shopProduct.ProductId);

                await con.OpenAsync();
                await createCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task<bool> DeleteShopProductByIds(int shopId, int productId)
        {
            bool isDeleted = false;

            string deleteString = "DELETE FROM ShopProduct WHERE ShopId = @ShopId AND ProductId = @ProductId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
            {
                deleteCommand.Parameters.AddWithValue("@ShopId", shopId);
                deleteCommand.Parameters.AddWithValue("@ProductId", productId);

                await con.OpenAsync();

                int rowsAffected = await deleteCommand.ExecuteNonQueryAsync();

                isDeleted = (rowsAffected > 0);
            }

            return isDeleted;
        }

        public async Task<List<ShopProduct>> GetAllShopProducts()
        {
            List<ShopProduct> foundShopProducts = new List<ShopProduct>();
            ShopProduct readShopProduct;

            string queryString = "SELECT * FROM ShopProduct";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();

                SqlDataReader shopProductReader = await readCommand.ExecuteReaderAsync();

                while (await shopProductReader.ReadAsync())
                {
                    readShopProduct = GetShopProductFromReader(shopProductReader);
                    foundShopProducts.Add(readShopProduct);
                }
            }

            return foundShopProducts;
        }

        public async Task<ShopProduct> GetShopProductByIds(int shopId, int productId)
        {
            ShopProduct foundShopProduct;

            string queryString = "SELECT * FROM ShopProduct WHERE ShopId = @ShopId AND ProductId = @ProductId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@ShopId", shopId);
                readCommand.Parameters.AddWithValue("@ProductId", productId);

                await con.OpenAsync();

                SqlDataReader shopProductReader = await readCommand.ExecuteReaderAsync();

                foundShopProduct = new ShopProduct();

                while (await shopProductReader.ReadAsync())
                {
                    foundShopProduct = GetShopProductFromReader(shopProductReader);
                }
            }

            return foundShopProduct;
        }
        public async Task<List<ShopProduct>> GetShopProductByShopId(int shopId)
        {
            List<ShopProduct> foundShopProducts = new List<ShopProduct>();

            string queryString = "SELECT * FROM ShopProduct WHERE ShopId = @ShopId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@ShopId", shopId);

                await con.OpenAsync();

                SqlDataReader shopProductReader = await readCommand.ExecuteReaderAsync();

                while (await shopProductReader.ReadAsync())
                {
                    foundShopProducts.Add(GetShopProductFromReader(shopProductReader));
                }
            }

            return foundShopProducts;
        }


        public async Task<bool> UpdateShopProductByIds(ShopProduct shopProductToUpdate)
        {
            bool isUpdated = false;

            string updateString = "UPDATE ShopProduct SET ShopId = @ShopId " +
                                  "WHERE ProductId = @ProductId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@ShopId", shopProductToUpdate.ShopId);
                updateCommand.Parameters.AddWithValue("@ProductId", shopProductToUpdate.ProductId);

                await con.OpenAsync();

                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        private ShopProduct GetShopProductFromReader(SqlDataReader shopProductReader)
        {
            int readerShopId = shopProductReader.GetInt32(shopProductReader.GetOrdinal("ShopId"));
            int readerProductId = shopProductReader.GetInt32(shopProductReader.GetOrdinal("ProductId"));

            return new ShopProduct(readerShopId, readerProductId);
        }



    }
}
