using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public class OrderlineGroupDatabaseAccess : IOrderlineGroup
    {
        readonly string? _connectionString;

        public OrderlineGroupDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Companyconnection");
        }

        public OrderlineGroupDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public async Task CreateOrderlineGroup(OrderlineGroup orderlineGroup)
        {
            string insertString = "INSERT INTO OrderlineGroup (ProductId, OrderlineId, ComboId) VALUES(@ProductId, @OrderlineId, @ComboId);";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                SqlParameter aProIdparam = new("@ProductId", orderlineGroup.ProductId);
                createCommand.Parameters.Add(aProIdparam);

                SqlParameter aOrderlineParam = new("@OrderlineId", orderlineGroup.OrderlineId);
                createCommand.Parameters.Add(aOrderlineParam);

                SqlParameter aCombiParam = new("@ComboId", orderlineGroup.ComboId);
                createCommand.Parameters.Add(aCombiParam);

                await con.OpenAsync();

                await createCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<OrderlineGroup>> GetAllOrderlineGroups()
        {
            List<OrderlineGroup> foundOrderlineGroups = new List<OrderlineGroup>();

            string queryString = "SELECT ProductId, OrderlineId, ComboId FROM OrderlineGroup";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();
                SqlDataReader orderlineGroupsReader = await readCommand.ExecuteReaderAsync();

                while (await orderlineGroupsReader.ReadAsync())
                {
                    OrderlineGroup readOrderlineGroup = GetOrderlineGroupFromReader(orderlineGroupsReader);
                    foundOrderlineGroups.Add(readOrderlineGroup);
                }
            }

            return foundOrderlineGroups;
        }

        public async Task<bool> DeleteOrderlineGroup(int productId, int orderlineId, int comboId)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM OrderlineGroup WHERE ProductId = @ProductId AND OrderlineId = @OrderlineId AND ComboId = @ComboId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
            {
                deleteCommand.Parameters.AddWithValue("@ProductId", productId);
                deleteCommand.Parameters.AddWithValue("@OrderlineId", orderlineId);
                deleteCommand.Parameters.AddWithValue("@ComboId", comboId);

                await con.OpenAsync();
                int rowsAffected = await deleteCommand.ExecuteNonQueryAsync();

                isDeleted = (rowsAffected > 0);
            }

            return isDeleted;
        }



        public async Task<OrderlineGroup> GetOrderlineGroupById(int productId, int orderlineId, int comboId)
        {
            OrderlineGroup foundOrderlineGroup = null;
            string queryString = "SELECT ProductId, OrderlineId, ComboId FROM OrderlineGroup WHERE ProductId = @ProductId AND OrderlineId = @OrderlineId AND ComboId = @ComboId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@ProductId", productId);
                readCommand.Parameters.AddWithValue("@OrderlineId", orderlineId);
                readCommand.Parameters.AddWithValue("@ComboId", comboId);

                await con.OpenAsync();
                SqlDataReader orderlineGroupsReader = await readCommand.ExecuteReaderAsync();

                while (await orderlineGroupsReader.ReadAsync())
                {
                    foundOrderlineGroup = GetOrderlineGroupFromReader(orderlineGroupsReader);
                }
            }

            return foundOrderlineGroup;
        }


        public async Task<bool> UpdateOrderlineGroupById(OrderlineGroup orderlineGroupToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE OrderlineGroup SET ProductId = @ProductId, OrderlineId = @OrderlineId, ComboId = @ComboId WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@ProductId", orderlineGroupToUpdate.ProductId);
                updateCommand.Parameters.AddWithValue("@OrderlineId", orderlineGroupToUpdate.OrderlineId);
                updateCommand.Parameters.AddWithValue("@ComboId", orderlineGroupToUpdate.ComboId);

                await con.OpenAsync();
                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        private OrderlineGroup GetOrderlineGroupFromReader(SqlDataReader orderlineGroupsReader)
        {
            int tempId, tempProductId, tempOrderlineId, tempComboId;

            tempProductId = orderlineGroupsReader.GetInt32(orderlineGroupsReader.GetOrdinal("ProductId"));
            tempOrderlineId = orderlineGroupsReader.GetInt32(orderlineGroupsReader.GetOrdinal("OrderlineId"));
            tempComboId = orderlineGroupsReader.GetInt32(orderlineGroupsReader.GetOrdinal("ComboId"));

            return new OrderlineGroup(tempProductId, tempOrderlineId, tempComboId);
        }
    }
}
