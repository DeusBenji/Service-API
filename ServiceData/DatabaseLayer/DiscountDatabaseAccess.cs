using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public class DiscountDatabaseAccess : IDiscount
    {
        readonly string? _connectionString;

        public DiscountDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public DiscountDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public async Task<int> CreateDiscount(Discount anDiscount)
        {
            int insertedId = -1;

            string insertString = "insert into Discount(rate, productGroupId, customerGroupId) OUTPUT INSERTED.ID values(@Rate, @ProductGroupId, @CustomerGroupId)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
            {
                CreateCommand.Parameters.AddWithValue("@Rate", anDiscount.Rate);
                CreateCommand.Parameters.AddWithValue("@ProductGroupId", anDiscount.ProductGroupId);
                CreateCommand.Parameters.AddWithValue("@CustomerGroupId", anDiscount.CustomerGroupId);

                await con.OpenAsync();

                // Execute save and read generated key (ID)
                insertedId = (int)await CreateCommand.ExecuteScalarAsync();
            }

            return insertedId;
        }

        public async Task<bool> DeleteDiscountById(int id)
        {
            bool isDeleted = false;

            string deleteString = "DELETE FROM Discount WHERE Id = @Id";

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

        public async Task<List<Discount>> GetAllDiscount()
        {
            List<Discount> foundDiscounts = new List<Discount>();

            string queryString = "SELECT Id, Rate, ProductGroupId, CustomerGroupId FROM Discount";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();

                // Execute read
                using (SqlDataReader discountReader = await readCommand.ExecuteReaderAsync())
                {
                    // Collect data
                    while (await discountReader.ReadAsync())
                    {
                        var readDiscount = GetDiscountFromReader(discountReader);
                        foundDiscounts.Add(readDiscount);
                    }
                }
            }

            return foundDiscounts;
        }

        public async Task<Discount> GetDiscountById(int id)
        {
            Discount foundDiscount = null;

            string queryString = "select Id, rate, productGroupId, customerGroupId from Discount where id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();

                using (SqlDataReader discountReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await discountReader.ReadAsync())
                    {
                        foundDiscount = GetDiscountFromReader(discountReader);
                    }
                }
            }

            return foundDiscount;
        }

        public async Task<bool> UpdateDiscountById(Discount DiscountToUpdate)
        {
            bool isUpdated = false;

            string updateString = "UPDATE Discount SET rate = @Rate, productGroupId = @ProductGroupId, customerGroupId = @CustomerGroupId WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", DiscountToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@Rate", DiscountToUpdate.Rate);
                updateCommand.Parameters.AddWithValue("@ProductGroupId", DiscountToUpdate.ProductGroupId);
                updateCommand.Parameters.AddWithValue("@CustomerGroupId", DiscountToUpdate.CustomerGroupId);

                await con.OpenAsync();

                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        private Discount GetDiscountFromReader(SqlDataReader discountReader)
        {
            decimal tempRate = discountReader.GetDecimal(discountReader.GetOrdinal("Rate"));
            int tempID = discountReader.GetInt32(discountReader.GetOrdinal("Id"));
            int tempProductGroupId = discountReader.GetInt32(discountReader.GetOrdinal("ProductGroupId"));
            int tempCustomerGroupId = discountReader.GetInt32(discountReader.GetOrdinal("CustomerGroupId"));

            return new Discount(tempID, tempRate, tempProductGroupId, tempCustomerGroupId);
        }
    }
}
