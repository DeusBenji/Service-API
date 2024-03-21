using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;

namespace ServiceData.DatabaseLayer
{
    public class ProductDatabaseAccess : IProduct
    {
        private readonly string? _connectionString;

        public ProductDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public ProductDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public async Task<int> CreateProduct(Product product)
        {
            int insertedId = -1;
            string insertString = "INSERT INTO Product(ProductNumber, Description, BasePrice, Barcode, Category, ProductGroupID, ImageName) " +
                "OUTPUT INSERTED.ID values(@Productnumber, @Description, @BasePrice, @Barcode, @Category, @ProductGroupID, @ImageName)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
            {
                CreateCommand.Parameters.AddWithValue("@Productnumber", product.ProductNumber);
                CreateCommand.Parameters.AddWithValue("@Description", product.Description);
                CreateCommand.Parameters.AddWithValue("@BasePrice", product.BasePrice);
                CreateCommand.Parameters.AddWithValue("@Barcode", product.Barcode);
                CreateCommand.Parameters.AddWithValue("@Category", product.CategoryType);
                CreateCommand.Parameters.AddWithValue("@ProductGroupID", product.ProductGroup);
                CreateCommand.Parameters.AddWithValue("@ImageName", product.ImageName);

                await con.OpenAsync();
                insertedId = (int)await CreateCommand.ExecuteScalarAsync();
            }

            return insertedId;
        }

        public async Task<bool> DeleteProductById(int id)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM Product WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
            {
                deleteCommand.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();
                int rowsAffected = await deleteCommand.ExecuteNonQueryAsync();

                isDeleted = (rowsAffected > 0);
            }

            return isDeleted;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> foundProducts = new List<Product>();
            string queryString = "SELECT * FROM Product";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();
                using (SqlDataReader productReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await productReader.ReadAsync())
                    {
                        Product readProduct = GetProductFromReader(productReader);
                        foundProducts.Add(readProduct);
                    }
                }
            }

            return foundProducts;
        }

        public async Task<Product> GetProductById(int id)
        {
            Product foundProduct = null;
            string queryString = "SELECT * FROM Product WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();

                using (SqlDataReader productReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await productReader.ReadAsync())
                    {
                        foundProduct = GetProductFromReader(productReader);
                    }
                }
            }

            return foundProduct;
        }

        public async Task<bool> UpdateProductById(Product productToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Product SET ProductNumber = @ProductNumber, Description = @Description, BasePrice = @BasePrice, " +
                "Barcode = @Barcode, Category = @Category, ProductGroupID = @ProductGroupID, ImageName = @ImageName WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@ProductNumber", productToUpdate.ProductNumber);
                updateCommand.Parameters.AddWithValue("@Description", productToUpdate.Description);
                updateCommand.Parameters.AddWithValue("@BasePrice", productToUpdate.BasePrice);
                updateCommand.Parameters.AddWithValue("@Barcode", productToUpdate.Barcode);
                updateCommand.Parameters.AddWithValue("@Category", productToUpdate.CategoryType);
                updateCommand.Parameters.AddWithValue("@ProductGroupID", productToUpdate.ProductGroup);
                updateCommand.Parameters.AddWithValue("@Id", productToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@ImageName", productToUpdate.ImageName);

                await con.OpenAsync();
                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                isUpdated = rowsAffected > 0;
            }

            return isUpdated;
        }

        public async Task<List<Product>> GetProductsByShopId(int shopId)
        {
            List<Product> products = new List<Product>();
            string queryString = "SELECT P.* FROM Product P INNER JOIN ShopProduct SP ON P.Id = SP.ProductId WHERE SP.ShopId = @ShopId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@ShopId", shopId);

                await con.OpenAsync();

                using (SqlDataReader productReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await productReader.ReadAsync())
                    {
                        products.Add(GetProductFromReader(productReader));
                    }
                }
            }

            return products;
        }

        public async Task<List<string>> GetAllCategories()
        {
            List<string> categories = new List<string>();
            string queryString = "SELECT DISTINCT Category FROM Product";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();

                using (SqlDataReader categoryReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await categoryReader.ReadAsync())
                    {
                        string category = categoryReader.GetString(categoryReader.GetOrdinal("Category"));
                        categories.Add(category);
                    }
                }
            }

            return categories;
        }

        public async Task<List<Product>> GetProductsByCategoryAndShop(string category, int shopId)
        {
            List<Product> products = new List<Product>();

            string queryString = "SELECT P.* FROM Product P INNER JOIN ShopProduct SP ON P.Id = SP.ProductId WHERE SP.ShopId = @ShopId AND P.Category = @Category";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@ShopId", shopId);
                readCommand.Parameters.AddWithValue("@Category", category);

                await con.OpenAsync();

                using (SqlDataReader productReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await productReader.ReadAsync())
                    {
                        products.Add(GetProductFromReader(productReader));
                    }
                }
            }

            return products;
        }


        private Product GetProductFromReader(SqlDataReader productReader)
        {
            Product foundProduct;
            int readerID;
            string readerProductNumber;
            string readerDescription;
            decimal readerBasePrice;
            int readerBarcode;
            string tempCategory;
            bool readerCategory;
            int readerProductGroup;
            string readerImage;

            //Fetch values
            readerID = productReader.GetInt32(productReader.GetOrdinal("Id"));
            readerProductNumber = productReader.GetString(productReader.GetOrdinal("ProductNumber"));
            readerBarcode = productReader.GetInt32(productReader.GetOrdinal("Barcode"));
            readerDescription = productReader.GetString(productReader.GetOrdinal("Description"));
            readerBasePrice = productReader.GetDecimal(productReader.GetOrdinal("BasePrice"));
            tempCategory = productReader.GetString(productReader.GetOrdinal("Category"));
            readerCategory = Enum.TryParse(tempCategory, out Product.Category categoryValue);
            readerProductGroup = productReader.GetInt32(productReader.GetOrdinal("ProductGroupID"));
            readerImage = productReader.GetString(productReader.GetOrdinal("ImageName"));


            //Create product object
            foundProduct = new Product(readerID, readerProductNumber, readerDescription, readerBasePrice, readerBarcode, categoryValue, readerProductGroup, readerImage);

            return foundProduct;
        }

    }
}
