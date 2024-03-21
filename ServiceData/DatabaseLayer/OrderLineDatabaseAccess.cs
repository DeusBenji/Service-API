using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public class OrderLineDatabaseAccess : IOrderLine
    {
        readonly string? _connectionString;

        public OrderLineDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public OrderLineDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public async Task<int> CreateOrderLine(OrderLine orderLine)
        {
            int insertedId = -1;
            string insertString = "INSERT INTO OrderLine (OrderlinePrice, Quantity, OrderId) OUTPUT INSERTED.ID " +
                "VALUES (@OrderlinePrice, @Quantity, @OrderId)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                // Begin a database transaction
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand createCommand = new SqlCommand(insertString, con, transaction))
                        {
                            createCommand.Parameters.AddWithValue("@OrderlinePrice", orderLine.OrderlinePrice);
                            createCommand.Parameters.AddWithValue("@Quantity", orderLine.Quantity);
                            createCommand.Parameters.AddWithValue("@OrderId", orderLine.OrderId);

                            // Execute the command and retrieve the generated ID
                            insertedId = (int)await createCommand.ExecuteScalarAsync();
                        }

                        // Commit the transaction if everything is successful
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Handle exceptions, log errors, or roll back the transaction in case of failure
                        transaction.Rollback();
                        throw; // Rethrow the exception for higher-level error handling
                    }
                }
            }

            return insertedId;
        }


        public async Task<bool> DeleteOrderLineById(int id)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM OrderLine WHERE Id = @Id";

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

        public async Task<List<OrderLine>> GetAllOrderLines()
        {
            List<OrderLine> foundOrderLines = new List<OrderLine>();

            string queryString = "SELECT * FROM OrderLine";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();
                SqlDataReader orderLineReader = await readCommand.ExecuteReaderAsync();

                while (await orderLineReader.ReadAsync())
                {
                    OrderLine readOrderLine = GetOrderLinesFromReader(orderLineReader);
                    foundOrderLines.Add(readOrderLine);
                }
            }

            return foundOrderLines;
        }

        public async Task<OrderLine> GetOrderLineById(int id)
        {
            OrderLine foundOrderLine = new OrderLine();

            string queryString = "SELECT * FROM OrderLine WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();
                SqlDataReader orderLineReader = await readCommand.ExecuteReaderAsync();

                while (await orderLineReader.ReadAsync())
                {
                    foundOrderLine = GetOrderLinesFromReader(orderLineReader);
                }
            }

            return foundOrderLine;
        }

        public async Task<bool> UpdateOrderLineById(OrderLine orderLineToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE OrderLine SET OrderlinePrice = @OrderlinePrice, Quantity = @Quantity, OrderId = @OrderId WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@OrderlinePrice", orderLineToUpdate.OrderlinePrice);
                updateCommand.Parameters.AddWithValue("@Quantity", orderLineToUpdate.Quantity);
                updateCommand.Parameters.AddWithValue("@OrderId", orderLineToUpdate.OrderId);
                updateCommand.Parameters.AddWithValue("@Id", orderLineToUpdate.Id);

                await con.OpenAsync();

                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();
                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        public async Task<OrderlineGroup> GetOrderlineGroupByOrderlineId(int orderlineId)
        {
            OrderlineGroup foundOrderlineGroup = null;

            string queryString = "SELECT * FROM OrderlineGroup WHERE OrderlineId = @OrderlineId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@OrderlineId", orderlineId);

                await con.OpenAsync();
                SqlDataReader orderlineGroupReader = await readCommand.ExecuteReaderAsync();

                if (await orderlineGroupReader.ReadAsync())
                {
                    foundOrderlineGroup = GetOrderlineGroupFromReader(orderlineGroupReader);
                }
            }

            return foundOrderlineGroup;
        }

        private OrderLine GetOrderLinesFromReader(SqlDataReader orderLineReader)
        {
            int readerId = orderLineReader.GetInt32(orderLineReader.GetOrdinal("Id"));
            decimal readerPrice = orderLineReader.GetDecimal(orderLineReader.GetOrdinal("OrderlinePrice"));
            int readerQuantity = orderLineReader.GetInt32(orderLineReader.GetOrdinal("Quantity"));
            int readerOrderId = orderLineReader.GetInt32(orderLineReader.GetOrdinal("OrderId"));

            OrderLine foundOrderLine = new OrderLine(readerId, readerPrice, readerQuantity, readerOrderId);
            return foundOrderLine;
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
