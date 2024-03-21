using ServiceData.ModelLayer;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Numerics;
using System.Data;
using ServiceData.DatabaseLayer.Interfaces;

namespace ServiceData.DatabaseLayer
{
    public class IngredientDatabaseAccess : IIngredient
    {

        readonly string? _connectionString;

        public IngredientDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public IngredientDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }


        public async Task<int> CreateIngredient(Ingredient anIngredient)
        {
            int insertedId = -1;
            string insertString = "INSERT INTO Ingredient (name, ingredientPrice, imageName) OUTPUT INSERTED.ID VALUES (@Name, @IngredientPrice, @ImageName)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                createCommand.Parameters.AddWithValue("@Name", anIngredient.Name);
                createCommand.Parameters.AddWithValue("@IngredientPrice", anIngredient.IngredientPrice);
                createCommand.Parameters.AddWithValue("@ImageName", anIngredient.ImageName);

                await con.OpenAsync();
                insertedId = (int)await createCommand.ExecuteScalarAsync();
            }

            return insertedId;
        }

        public async Task<bool> DeleteIngredientById(int id)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM Ingredient WHERE Id = @Id";

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

        public async Task<List<Ingredient>> GetAllIngredients()
        {
            List<Ingredient> foundIngredients = new List<Ingredient>();
            string queryString = "SELECT Id, name, ingredientPrice, imageName FROM Ingredient";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();
                using (SqlDataReader ingredientsReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await ingredientsReader.ReadAsync())
                    {
                        Ingredient readIng = GetIngFromReader(ingredientsReader);
                        foundIngredients.Add(readIng);
                    }
                }
            }

            return foundIngredients;
        }

        public async Task<Ingredient> GetIngredientById(int id)
        {
            Ingredient foundIngredient = null;

            string queryString = "SELECT Id, name, ingredientPrice, imageName FROM Ingredient WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();
                using (SqlDataReader ingredientsReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await ingredientsReader.ReadAsync())
                    {
                        foundIngredient = GetIngFromReader(ingredientsReader);
                    }
                }
            }

            return foundIngredient;
        }

        public async Task<bool> UpdateIngredientById(Ingredient ingredientToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Ingredient SET name = @Name, ingredientPrice = @IngredientPrice, imageName = @ImageName WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", ingredientToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@Name", ingredientToUpdate.Name);
                updateCommand.Parameters.AddWithValue("@IngredientPrice", ingredientToUpdate.IngredientPrice);
                updateCommand.Parameters.AddWithValue("@ImageName", ingredientToUpdate.ImageName);

                await con.OpenAsync();
                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        public async Task<List<Ingredient>> GetIngredientsByProductId(int productId)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            string queryString = "SELECT I.* FROM Ingredient I INNER JOIN IngredientProduct IP ON I.Id = IP.IngredientId WHERE IP.ProductId = @ProductId";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@ProductId", productId);

                await con.OpenAsync();

                using (SqlDataReader ingredientReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await ingredientReader.ReadAsync())
                    {
                        ingredients.Add(GetIngFromReader(ingredientReader));
                    }
                }
            }

            return ingredients;
        }


        private Ingredient GetIngFromReader(SqlDataReader ingredientsReader)
        {
            Ingredient foundIng = null;

            int tempID;
            decimal tempIngPrice;
            string tempIngName, tempImageName;


            // Fetch values
            tempID = ingredientsReader.GetInt32(ingredientsReader.GetOrdinal("Id"));
            tempIngPrice = ingredientsReader.GetDecimal(ingredientsReader.GetOrdinal("IngredientPrice"));
            tempIngName = ingredientsReader.GetString(ingredientsReader.GetOrdinal("Name"));
            tempImageName = ingredientsReader.GetString(ingredientsReader.GetOrdinal("ImageName"));

            // Create the Ingredient object
            foundIng = new Ingredient(tempID, tempIngName, tempIngPrice, tempImageName);

            return foundIng;
        }

    }
}