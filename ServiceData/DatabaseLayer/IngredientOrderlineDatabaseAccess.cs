using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;

namespace ServiceData.DatabaseLayer
{
    public class IngredientOrderlineDatabaseAccess : IIngredientOrderline
    {
        readonly string? _connectionString;

        public IngredientOrderlineDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public IngredientOrderlineDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public async Task CreateIngredientOrderline(IngredientOrderline anIngredientOrderline)
        {
            string insertString = "INSERT INTO IngredientOrderline(IngredientId, OrderlineId, Delta) " +
                                  "VALUES (@IngredientId, @OrderlineId, @Delta)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                createCommand.Parameters.AddWithValue("@IngredientId", anIngredientOrderline.IngredientId);
                createCommand.Parameters.AddWithValue("@OrderlineId", anIngredientOrderline.OrderlineId);
                createCommand.Parameters.AddWithValue("@Delta", anIngredientOrderline.Delta);

                await con.OpenAsync();
                await createCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task<bool> DeleteIngredientOrderlineByIds(int ingredientId, int orderlineId)
        {
            bool isDeleted = false;

            string deleteString = "DELETE FROM IngredientOrderline WHERE IngredientId = @IngredientId AND OrderlineId = @OrderlineId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
            {
                deleteCommand.Parameters.AddWithValue("@IngredientId", ingredientId);
                deleteCommand.Parameters.AddWithValue("@OrderlineId", orderlineId);

                await con.OpenAsync();

                int rowsAffected = await deleteCommand.ExecuteNonQueryAsync();

                isDeleted = (rowsAffected > 0);
            }

            return isDeleted;
        }

        public async Task<List<IngredientOrderline>> GetAllIngredientOrderlines()
        {
            List<IngredientOrderline> foundIngredientOrderlines = new List<IngredientOrderline>();
            IngredientOrderline readIngredientOrderline;

            string queryString = "SELECT * FROM IngredientOrderline";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();

                SqlDataReader ingredientOrderlineReader = await readCommand.ExecuteReaderAsync();

                while (await ingredientOrderlineReader.ReadAsync())
                {
                    readIngredientOrderline = GetIngredientOrderlineFromReader(ingredientOrderlineReader);
                    foundIngredientOrderlines.Add(readIngredientOrderline);
                }
            }

            return foundIngredientOrderlines;
        }

        public async Task<IngredientOrderline> GetIngredientOrderlineByIds(int ingredientId, int orderlineId)
        {
            IngredientOrderline foundIngredientOrderline;

            string queryString = "SELECT * FROM IngredientOrderline WHERE IngredientId = @IngredientId AND OrderlineId = @OrderlineId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@IngredientId", ingredientId);
                readCommand.Parameters.AddWithValue("@OrderlineId", orderlineId);

                await con.OpenAsync();

                SqlDataReader ingredientOrderlineReader = await readCommand.ExecuteReaderAsync();

                foundIngredientOrderline = new IngredientOrderline();

                while (await ingredientOrderlineReader.ReadAsync())
                {
                    foundIngredientOrderline = GetIngredientOrderlineFromReader(ingredientOrderlineReader);
                }
            }

            return foundIngredientOrderline;
        }

        public async Task<bool> UpdateIngredientOrderlineByIds(IngredientOrderline ingredientOrderlineToUpdate)
        {
            bool isUpdated = false;

            string updateString = "UPDATE IngredientOrderline SET Delta = @Delta " +
                                  "WHERE IngredientId = @IngredientId AND OrderlineId = @OrderlineId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Delta", ingredientOrderlineToUpdate.Delta);
                updateCommand.Parameters.AddWithValue("@IngredientId", ingredientOrderlineToUpdate.IngredientId);
                updateCommand.Parameters.AddWithValue("@OrderlineId", ingredientOrderlineToUpdate.OrderlineId);

                await con.OpenAsync();

                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        private IngredientOrderline GetIngredientOrderlineFromReader(SqlDataReader ingredientOrderlineReader)
        {
            int readerIngredientId = ingredientOrderlineReader.GetInt32(ingredientOrderlineReader.GetOrdinal("IngredientId"));
            int readerOrderlineId = ingredientOrderlineReader.GetInt32(ingredientOrderlineReader.GetOrdinal("OrderlineId"));
            int readerDelta = ingredientOrderlineReader.GetInt32(ingredientOrderlineReader.GetOrdinal("Delta"));

            return new IngredientOrderline(readerIngredientId, readerOrderlineId, readerDelta);
        }
    }
}
