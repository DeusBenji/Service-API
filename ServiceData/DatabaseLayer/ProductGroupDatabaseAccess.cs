using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public class ProductGroupDatabaseAccess : IProductGroup
    {
        readonly string? _connectionString;

        public ProductGroupDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Companyconnection");
        }

        public ProductGroupDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public async Task<int> CreateProductGroup(ProductGroup aProductGroup)
        {
            int insertedId = -1;
            string insertString = "INSERT INTO ProductGroup (Name) OUTPUT INSERTED.ID VALUES (@Name)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                createCommand.Parameters.AddWithValue("@Name", aProductGroup.Name);


                await con.OpenAsync();
                insertedId = (int)await createCommand.ExecuteScalarAsync();
            }

            return insertedId;
        }

        public async Task<bool> DeleteProductGroupById(int id)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM ProductGroup WHERE Id = @Id";

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

        public async Task<List<ProductGroup>> GetAllProductGroups()
        {
            List<ProductGroup> foundProductGroups = new List<ProductGroup>();

            string queryString = "SELECT Id, Name FROM ProductGroup";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();
                using (SqlDataReader productGroupsReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await productGroupsReader.ReadAsync())
                    {
                        ProductGroup readProductGroup = GetProductGroupFromReader(productGroupsReader);
                        foundProductGroups.Add(readProductGroup);
                    }
                }
            }

            return foundProductGroups;
        }

        public async Task<ProductGroup> GetProductGroupById(int id)
        {
            ProductGroup foundProductGroup = null;
            string queryString = "SELECT Id, Name FROM ProductGroup WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();
                using (SqlDataReader productGroupsReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await productGroupsReader.ReadAsync())
                    {
                        foundProductGroup = GetProductGroupFromReader(productGroupsReader);
                    }
                }
            }

            return foundProductGroup;
        }

        public async Task<bool> UpdateProductGroupById(ProductGroup productGroupToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE ProductGroup SET Name = @Name WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", productGroupToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@Name", productGroupToUpdate.Name);

                await con.OpenAsync();
                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        private ProductGroup GetProductGroupFromReader(SqlDataReader productGroupsReader)
        {
            ProductGroup foundProductGroup;
            int tempId;
            string tempName;

            tempId = productGroupsReader.GetInt32(productGroupsReader.GetOrdinal("Id"));
            tempName = productGroupsReader.GetString(productGroupsReader.GetOrdinal("Name"));

            foundProductGroup = new ProductGroup(tempId, tempName);

            return foundProductGroup;
        }
    }
}
