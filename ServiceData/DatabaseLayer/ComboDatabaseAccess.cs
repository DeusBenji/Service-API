using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;

namespace ServiceData.DatabaseLayer
{
    public class ComboDatabaseAccess : ICombo
    {
        private readonly string? _connectionString;

        public ComboDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public ComboDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public async Task<int> CreateCombo(Combo aCombo)
        {
            int insertedId = -1;
            string insertString = "INSERT INTO Combo(Name, ImageName, ComboPrice) OUTPUT INSERTED.Id VALUES (@Name, @ImageName, @ComboPrice)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                createCommand.Parameters.AddWithValue("@Name", aCombo.Name);
                createCommand.Parameters.AddWithValue("@ImageName", aCombo.ImageName);
                createCommand.Parameters.AddWithValue("@ComboPrice", aCombo.ComboPrice);

                con.Open();
                insertedId = (int)await createCommand.ExecuteScalarAsync();
            }

            return insertedId;
        }

        public async Task<bool> DeleteComboById(int id)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM Combo WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
            {
                deleteCommand.Parameters.AddWithValue("@Id", id);

                con.Open();
                int rowsAffected = await deleteCommand.ExecuteNonQueryAsync();

                isDeleted = (rowsAffected > 0);
            }

            return isDeleted;
        }

        public async Task<List<Combo>> GetAllCombos()
        {
            List<Combo> foundCombos = new List<Combo>();

            string queryString = "SELECT Id, Name, ImageName, ComboPrice FROM Combo";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                SqlDataReader combosReader = await readCommand.ExecuteReaderAsync();

                while (await combosReader.ReadAsync())
                {
                    Combo readCombo = GetComboFromReader(combosReader);
                    foundCombos.Add(readCombo);
                }
            }

            return foundCombos;
        }

        public async Task<Combo> GetComboById(int id)
        {
            Combo foundCombo;
            string queryString = "SELECT Id, Name, ImageName, ComboPrice FROM Combo WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader comboReader = await readCommand.ExecuteReaderAsync();
                foundCombo = new Combo();

                while (await comboReader.ReadAsync())
                {
                    foundCombo = GetComboFromReader(comboReader);
                }
            }

            return foundCombo;
        }

        public async Task<bool> UpdateComboById(Combo comboToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Combo SET Name = @Name, ImageName = @ImageName, ComboPrice = @ComboPrice WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", comboToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@Name", comboToUpdate.Name);
                updateCommand.Parameters.AddWithValue("@ImageName", comboToUpdate.ImageName);
                updateCommand.Parameters.AddWithValue("@ComboPrice", comboToUpdate.ComboPrice);

                con.Open();
                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        public async Task<List<Combo>> GetCombosByShopId(int shopId)
        {
            List<Combo> combos = new List<Combo>();

            string queryString = "SELECT C.* FROM Combo C INNER JOIN ShopCombo SC ON C.Id = SC.ComboId WHERE SC.ShopId = @ShopId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@ShopId", shopId);

                await con.OpenAsync();

                using (SqlDataReader comboReader = await readCommand.ExecuteReaderAsync())
                {
                    while (await comboReader.ReadAsync())
                    {
                        combos.Add(GetComboFromReader(comboReader));
                    }
                }
            }

            return combos;
        }


        private Combo GetComboFromReader(SqlDataReader comboReader)
        {
            Combo foundCombo;
            int tempId;
            string tempName, tempImageName;
            decimal tempComboPrice;

            tempId = comboReader.GetInt32(comboReader.GetOrdinal("Id"));
            tempName = comboReader.GetString(comboReader.GetOrdinal("Name"));
            tempImageName = comboReader.GetString(comboReader.GetOrdinal("ImageName"));
            tempComboPrice = comboReader.GetDecimal(comboReader.GetOrdinal("ComboPrice"));

            foundCombo = new Combo(tempId, tempName, tempImageName, tempComboPrice);

            return foundCombo;
        }
    }
}
